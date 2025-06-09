using CORE.Models.Base;

namespace CORE.Models
{
    public class Aircraft : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Capacity { get; set; }
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }
        public ICollection<Flight>? Flights { get; set; }
        public ICollection<SeatClass>? SeatClasses { get; set; } = new List<SeatClass>();
    }
}

