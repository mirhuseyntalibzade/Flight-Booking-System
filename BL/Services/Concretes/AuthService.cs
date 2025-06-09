using BL.DTOs.AuthDTOs;
using BL.Exceptions;
using BL.Services.Abstracts;
using CORE.Enums;
using CORE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BL.Services.Concretes
{
    public class AuthService : IAuthService
    {
        readonly IConfiguration _configuration;
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;
        readonly IEmailService _emailService;
        public AuthService( IEmailService emailService, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task RegisterAsync(RegisterDTO registerDTO)
        {
            AppUser user = new AppUser
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Gender = registerDTO.Gender,
                DOB = registerDTO.DOB,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                throw new BaseException();
            }
            result = await _userManager.AddToRoleAsync(user, Role.User.ToString());
            if (!result.Succeeded)
            {
                throw new BaseException();
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var frontendUrls = _configuration.GetSection("FrontendUrls");
            string frontendUrl = await _userManager.IsInRoleAsync(user, "Admin")
                ? frontendUrls["Admin"]
                : frontendUrls["User"];

            var confirmationLink = $"{frontendUrl}/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            await _emailService.SendEmailAsync(user.Email, "Confirm Your Email", $"Click here to confirm your email: <a href='{confirmationLink}'>Confirm Email</a>");
        }

        public async Task<string> LoginAsync(LoginDTO loginDTO)
        {
            AppUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
            {
                throw new BaseException("Credentials are not correct.");
            }
            bool result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!result)
            {
                throw new BaseException("Credentials are not correct.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(user, roles, loginDTO.RememberMe);
            return token;
        }

        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new BaseException("User not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var frontendUrls = _configuration.GetSection("FrontendUrls");
            string frontendUrl = await _userManager.IsInRoleAsync(user,"Admin")
                ? frontendUrls["Admin"]
                : frontendUrls["User"];

            var resetLink = $"{frontendUrl}/reset-password?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            await _emailService.SendEmailAsync(user.Email, "Reset Your Password", $"Click here to reset your password: <a href='{resetLink}'>Reset Password</a>");
        }

        public async Task ResetPasswordAsync(ResetPasswordDTO model, string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new BaseException("User not found.");
            }
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            if (!result.Succeeded)
            {
                throw new BaseException("Couldn't change password.");
            }
            await _emailService.SendEmailAsync(user.Email, "Password Change", $"Dear {user.UserName}, we have successfully changed your password.");
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new BaseException("Couldn't find user.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new BaseException("Couldn't confirm password.");
            }
        }

        public string GenerateJwtToken(AppUser user, IList<string> roles, bool rememberMe)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = rememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
