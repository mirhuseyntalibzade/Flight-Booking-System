using AutoMapper;
using BL.DTOs.CountryDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        readonly ICountryService _service;

        public CountriesController(ICountryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(AddCountryDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                await _service.AddCountryAsync(dto);
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
        public async Task<IActionResult> GetAllCountries()
        {
            try
            {

                return Ok(await _service.GetAllCountriesAsync());
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
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                return Ok(await _service.GetCountryByIdAsync(id));
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
        public async Task<IActionResult> UpdateCountry(int id, UpdateCountryDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                await _service.UpdateCountryAsync(id, dto);
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
        public async Task<IActionResult> RemoveCountry(int id)
        {
            try
            {
                await _service.RemoveCountryAsync(id);
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
        public async Task<IActionResult> SoftDeleteCountry(int id)
        {
            try
            {
                await _service.SoftDeleteCountry(id);
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
        public async Task<IActionResult> RevertSoftDeleteCountry(int id)
        {
            try
            {
                await _service.RevertSoftDeleteCountry(id);
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
