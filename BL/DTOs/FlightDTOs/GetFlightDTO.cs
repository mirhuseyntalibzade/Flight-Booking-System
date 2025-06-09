using BL.DTOs.AircraftDTOs;
using BL.DTOs.AirlineDTOs;
using BL.DTOs.SeatDTOs;
using CORE.Models;

namespace BL.DTOs.FlightDTOs
{
    public class GetFlightDTO
    {
        public int Id { get; set; }
        public GetIncludedAirlineDTO Airline { get; set; }
        public GetIncludedAircraftDTO Aircraft { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<GetIncludedSeatDTO>? Seats { get; set; }
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan Duration => ArrivalTime - DepartureTime;
        public decimal Price { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
