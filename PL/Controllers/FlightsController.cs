using BL.DTOs.FlightDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        readonly IFlightService _service;

        public FlightsController(IFlightService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight(AddFlightDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                await _service.AddFlightAsync(dto);
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

        [HttpGet("round-trip")]
        public async Task<IActionResult> GetFlightsByRoundTripAsync(string origin, string destination, DateTime outBound, DateTime returnTime)
        {
            try
            {
                return Ok(await _service.GetFlightsByRoundTripAsync(origin,destination,outBound,returnTime));
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

        [HttpGet("one-way")]
        public async Task<IActionResult> GetFlightOneWayAsync(string origin, string destination, DateTime outBound)
        {
            try
            {
                return Ok(await _service.GetFlightOneWayAsync(origin, destination, outBound));
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

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            try
            {

                return Ok(await _service.GetAllFlightsAsync());
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
        public async Task<IActionResult> GetFlight(int id)
        {
            try
            {
                return Ok(await _service.GetFlightByIdAsync(id));
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlight(int id, UpdateFlightDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                await _service.UpdateFlightAsync(id, dto);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFlight(int id)
        {
            try
            {
                await _service.RemoveFlightAsync(id);
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

        [HttpPatch("{id}/soft-delete")]
        public async Task<IActionResult> SoftDeleteFlight(int id)
        {
            try
            {
                await _service.SoftDeleteFlight(id);
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

        [HttpPatch("{id}/revert")]
        public async Task<IActionResult> RevertSoftDeleteFlight(int id)
        {
            try
            {
                await _service.RevertSoftDeleteFlight(id);
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
