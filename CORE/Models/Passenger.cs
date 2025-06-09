using CORE.Enums;
using CORE.Models.Base;

namespace CORE.Models
{
    public class Passenger : BaseAuditableEntity
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public string PassportNumber { get; set; }
        public int? SeatId { get; set; }
        public Seat? Seat { get; set; }
        public bool IsCheckedIn { get; set; }
    }
}
