using BL.DTOs.AircraftDTOs;
using BL.DTOs.SeatClassDTOs;
using BL.DTOs.WrapperDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftsController : ControllerBase
    {
        readonly IAircraftService _service;

        public AircraftsController(IAircraftService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddAircraft(AircraftSeatClassDTO aircraftSeatClassDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                await _service.AddAircraftAsync(aircraftSeatClassDTO);
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

        [HttpGet]
        public async Task<IActionResult> GetAllAircrafts()
        {
            try
            {

                return Ok(await _service.GetAllAircraftsAsync());
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
        public async Task<IActionResult> GetAircraft(int id)
        {
            try
            {
                return Ok(await _service.GetAircraftByIdAsync(id));
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
        public async Task<IActionResult> UpdateAircraft(int id, UpdateAircraftDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                await _service.UpdateAircraftAsync(id, dto);
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
        public async Task<IActionResult> RemoveAircraft(int id)
        {
            try
            {
                await _service.RemoveAircraftAsync(id);
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
        public async Task<IActionResult> SoftDeleteAircraft(int id)
        {
            try
            {
                await _service.SoftDeleteAircraft(id);
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
        public async Task<IActionResult> RevertSoftDeleteAircraft(int id)
        {
            try
            {
                await _service.RevertSoftDeleteAircraft(id);
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
