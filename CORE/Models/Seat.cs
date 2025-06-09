using CORE.Models.Base;

namespace CORE.Models
{
    public class Seat : BaseAuditableEntity
    {
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        public int? PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
        public int Row { get; set; }
        public string Column { get; set; }
        public string SeatNumber { get; set; }
        public string SeatClass { get; set; }
        public bool IsAvailable { get; set; }
        public bool AutoAssign { get; set; }
    }
}
