using CORE.Models;
using BL.AdditionalServices;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AirlineDTOs
{
    public class UpdateAirlineDTO
    {
        public string Name { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public int CountryId { get; set; }
        public IFormFile Logo { get; set; }
    }

    public class UpdateAircraftValidator : AbstractValidator<UpdateAirlineDTO>
    {
        public UpdateAircraftValidator()
        {
            RuleFor(a => a.Name)
                 .NotEmpty().WithMessage("Name can not be empty")
                 .MaximumLength(100).WithMessage("Name requires to be maximum 100 characters")
                 .MinimumLength(3).WithMessage("Name requires to be maximum 100 characters");

            RuleFor(a => a.IATA)
                 .NotEmpty().WithMessage("IATA can not be empty")
                 .Length(2).WithMessage("IATA only requires 3 characters only");

            RuleFor(a => a.ICAO)
                 .NotEmpty().WithMessage("ICAO can not be empty")
                 .Length(3).WithMessage("ICAO only requires 2 characters only");

            RuleFor(a => a.CountryId)
                 .NotEmpty().WithMessage("ICAO can not be empty");

            RuleFor(a => a.Logo)
                .Cascade(CascadeMode.Stop)
                .Must(a => a is null || a.Length < 5 * 1024 * 1024).WithMessage("Maximum file size is 5mb.")
                .Must(a => a is null || a.CheckType("image")).WithMessage("Only accepts image type");
        }
    }
}