using CORE.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Firstname can not be empty")
                .MinimumLength(3).WithMessage("Firstname can not be less than 2 characters.")
                .MaximumLength(50).WithMessage("Firstname can not be more than 50 characters.");
            
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Lastname can not be empty")
                .MinimumLength(3).WithMessage("Lastname can not be less than 2 characters.")
                .MaximumLength(50).WithMessage("Lastname can not be more than 50 characters.");
            
            RuleFor(r => r.DOB)
                .NotEmpty().WithMessage("Can not be empty.");

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("Please write valid email address.")
                .NotEmpty().WithMessage("Firstname can not be empty");
            
            RuleFor(r => r.UserName)
                .NotEmpty().WithMessage("Username can not be empty")
                .MinimumLength(3).WithMessage("Username can not be less than 2 characters.")
                .MaximumLength(50).WithMessage("Username can not be more than 50 characters.");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password can not be empty.")
                .Equal(r => r.ConfirmPassword).WithMessage("Passwords does not match.");

            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("Please confirm your password.");
        }
    }
}
