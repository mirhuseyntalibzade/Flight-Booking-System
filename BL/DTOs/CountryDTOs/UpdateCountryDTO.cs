using CORE.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.CountryDTOs
{
    public class UpdateCountryDTO
    {
        public string Name { get; set; }
        public string ISOCode { get; set; }
    }

    public class UpdateCountryValidator : AbstractValidator<UpdateCountryDTO>
    {
        public UpdateCountryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Country name can not be empty.")
                .MaximumLength(100).WithMessage("Country name can not exceed 100 characters.")
                .MinimumLength(3).WithMessage("Country name can not be less than 3 characters.");

            RuleFor(c => c.ISOCode)
                .NotEmpty().WithMessage("ISOCode can not be empty.")
                .Length(3).WithMessage("ISOCode only accepts 3 characters");
        }
    }
}
