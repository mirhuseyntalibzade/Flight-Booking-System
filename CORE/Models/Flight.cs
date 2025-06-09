using CORE.Models.Base;

namespace CORE.Models
{
    public class Flight : BaseAuditableEntity
    {
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }
        
        public int AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }
        public ICollection<Seat>? Seats { get; set; } = new List<Seat>();
        
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan Duration => ArrivalTime - DepartureTime;
        public decimal Price { get; set; }

        public ICollection<BookingFlight> BookingFlights { get; set; } = new List<BookingFlight>();

    }
}
