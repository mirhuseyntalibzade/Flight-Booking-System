using BL.Services.Abstracts;
using Stripe.Checkout;

namespace BL.Services.Concretes
{

    public class PaymentService : IPaymentService
    {
        public async Task<string> CreateCheckoutSessionAsync(decimal amount, string currency)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Flight Ticket"
                        },
                        UnitAmount = (long)(amount * 100)
                    },
                    Quantity = 1
                }
            },
                Mode = "payment",
                SuccessUrl = "http://localhost:5173/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "http://localhost:5173/cancel"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return session.Url;
        }
    }

}
