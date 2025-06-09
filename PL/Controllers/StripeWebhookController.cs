using BL.Services.Abstracts;
using CORE.Enums;
using CORE.Models;
using DAL.Repositories.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace PL.Controllers
{
    [Route("api/webhooks")]
    [ApiController]
    public class StripeWebhookController : ControllerBase
    {
        readonly IRepository<Booking> _bookingRepository;
        readonly IBookingService _bookingService;
        private readonly string _webhookSecret;
        public StripeWebhookController(IBookingService bookingService, IConfiguration configuration, IRepository<Booking> bookingRepository)
        {
            _bookingService = bookingService;
            _bookingRepository = bookingRepository;
            _webhookSecret = configuration["Stripe:WebhookSigningSecret"];
        }

        [HttpPost]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeSignature = Request.Headers["Stripe-Signature"];

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    stripeSignature,
                    _webhookSecret
                );

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Session;
                    string sessionId = session.Id;
                    int bookingId = int.Parse(session.Metadata["booking_id"]);
                    var customerEmail = session.CustomerDetails.Email;

                    Booking booking = await _bookingRepository.GetByConditionAsync(b => b.StripeSessionId == sessionId);
                    if (booking != null)
                    {
                        booking.Status = Status.Confirmed;
                        _bookingRepository.Update(booking);
                        await _bookingService.GenerateETicket(customerEmail,bookingId);
                        await _bookingRepository.SaveChangesAsync();
                    }
                    
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest($"Webhook error: {e.Message}");
            }
        }

    }
}
