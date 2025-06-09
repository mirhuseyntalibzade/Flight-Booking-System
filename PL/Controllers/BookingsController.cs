using BL.DTOs.PassengerDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using CORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        readonly IBookingService _service;
        public BookingsController(IBookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {

                return Ok(await _service.GetAllBookingsAsync());
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            try
            {
                return Ok(await _service.GetBookingByIdAsync(id));
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong.");
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateBooking(int outboundFlightId, int? returnFlightId, ICollection<AddPassengerDTO> passengers)
        {
            try
            {
                int id = await _service.CreateBooking(outboundFlightId, returnFlightId, passengers);
                return Ok(new { BookingId = id });
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpPost("payment")]
        public async Task<IActionResult> Payment(int bookingId)
        {
            try
            {
                string url = await _service.ProcessPayment(bookingId);
                await _service.SaveChangesAsync();
                return Ok(url);
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpPost("e-ticket")]
        public async Task<IActionResult> GenerateETicket([FromBody] string email, [FromQuery] int bookingId)
        {
            try
            {
                await _service.GenerateETicket(email, bookingId);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveTemporaryBooking(int bookingId)
        {
            try
            {
                await _service.RemoveBookingAsync(bookingId);
                await _service.SaveChangesAsync();
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong.");
            }
        }
    }
}
