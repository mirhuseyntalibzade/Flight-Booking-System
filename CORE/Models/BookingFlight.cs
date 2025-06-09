using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Models
{
    public class BookingFlight
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
