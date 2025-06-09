using BL.DTOs.AirlineDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class AirlinesController : ControllerBase
    {
        readonly IAirlineService _service;

        public AirlinesController(IAirlineService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddAirline(AddAirlineDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                await _service.AddAirlineAsync(dto);
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
        public async Task<IActionResult> GetAllAirlines()
        {
            try
            {

                return Ok(await _service.GetAllAirlinesAsync());
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
        public async Task<IActionResult> GetAirline(int id)
        {
            try
            {
                return Ok(await _service.GetAirlineByIdAsync(id));
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
        public async Task<IActionResult> UpdateAirline(int id, UpdateAirlineDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                await _service.UpdateAirlineAsync(id, dto);
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
        public async Task<IActionResult> RemoveAirline(int id)
        {
            try
            {
                await _service.RemoveAirlineAsync(id);
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
        public async Task<IActionResult> SoftDeleteAirline(int id)
        {
            try
            {
                await _service.SoftDeleteAirline(id);
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
        public async Task<IActionResult> RevertSoftDeleteAirline(int id)
        {
            try
            {
                await _service.RevertSoftDeleteAirline(id);
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
