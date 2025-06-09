using BL.DTOs.AuthDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using CORE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;
        readonly UserManager<AppUser> _userManager;
        public AuthController(UserManager<AppUser> userManager, IAuthService authService)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                string token = await _authService.LoginAsync(loginDTO);
                Console.WriteLine(token);
                return Ok(new { token });
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                    return BadRequest(errors);
                }
                await _authService.RegisterAsync(registerDTO);
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

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, string token)
        {
            try
            {
                await _authService.ConfirmEmailAsync(userId, token);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("forgot-password")]
        public async Task<IActionResult> ResetPasswordToken(string email)
        {
            try
            {
                await _authService.ForgotPasswordAsync(email);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model, [FromQuery] string userId, string token )
        {
            try
            {
                await _authService.ResetPasswordAsync(model, userId, token);
                return Ok();
            }
            catch (BaseException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
