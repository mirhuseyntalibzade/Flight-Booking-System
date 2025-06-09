using CORE.Enums;
using CORE.Models.Base;

namespace CORE.Models
{
    public class Booking : BaseAuditableEntity
    {
        public string PNR { get; set; }
        public int NumberOfPassengers { get; set; }
        public ICollection<Passenger>? Passengers { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public string? StripeSessionId { get; set; }
        public ICollection<BookingFlight> BookingFlights { get; set; } = new List<BookingFlight>();
    }
}
