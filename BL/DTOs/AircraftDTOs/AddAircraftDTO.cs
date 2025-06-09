using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AircraftDTOs
{
    public class AddAircraftDTO
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Capacity { get; set; }
        public int AirlineId { get; set; }
    }

    public class AddAircraftValidator : AbstractValidator<AddAircraftDTO>
    {
        public AddAircraftValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Name can not be empty")
                .MaximumLength(100).WithMessage("Name requires to be maximum 100 characters")
                .MinimumLength(3).WithMessage("Name requires to be maximum 100 characters");

            RuleFor(a => a.Manufacturer)
                .NotEmpty().WithMessage("Manufacturer can not be empty")
                .MaximumLength(100).WithMessage("Manufacturer requires to be maximum 100 characters")
                .MinimumLength(3).WithMessage("Manufacturer requires to be maximum 100 characters");

            RuleFor(a => a.Capacity)
                .NotEmpty().WithMessage("Capacity can not be empty");

            RuleFor(a => a.AirlineId)
                .NotEmpty().WithMessage("Airline can not be empty");
        }
    }
}
