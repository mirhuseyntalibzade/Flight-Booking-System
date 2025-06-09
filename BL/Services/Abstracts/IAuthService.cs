using BL.DTOs.AuthDTOs;
using BL.Exceptions;
using BL.Services.Concretes;
using CORE.Models;
using Microsoft.AspNetCore.Identity;

namespace BL.Services.Abstracts
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDTO loginDTO);
        Task RegisterAsync(RegisterDTO registerDTO);
        public Task ConfirmEmailAsync(string userId, string token);
        string GenerateJwtToken(AppUser user, IList<string> roles,bool rememberMe);
        public Task ForgotPasswordAsync(string email);
        public Task ResetPasswordAsync(ResetPasswordDTO model, string userId, string token);

    }
}
