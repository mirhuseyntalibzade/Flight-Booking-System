using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.FlightDTOs
{
    public class AddFlightDTO
    {
        public int AirlineId { get; set; }
        public int AircraftId { get; set; }
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
    }

    public class AddFlightValidator : AbstractValidator<AddFlightDTO>
    {
        public AddFlightValidator()
        {
            RuleFor(f => f.AircraftId)
                .NotEmpty().WithMessage("You must choose aircraft.");

            RuleFor(f => f.AirlineId)
                .NotEmpty().WithMessage("You must choose airline.");

            RuleFor(f => f.FlightNumber)
                .NotEmpty().WithMessage("Flight number can not be empty.");

            RuleFor(f => f.Origin)
                .NotEmpty().WithMessage("Origin can not be empty.");

            RuleFor(f => f.Destination)
                .NotEmpty().WithMessage("Destination can not be empty.");

            RuleFor(f => f.DepartureTime)
                .NotEmpty().WithMessage("Departure time can not be empty.");

            RuleFor(f => f.ArrivalTime)
                .NotEmpty().WithMessage("Arrival time can not be empty.");

            RuleFor(f => f.Price)
                .NotEmpty().WithMessage("Price can not be empty.");
        }
    }
}
