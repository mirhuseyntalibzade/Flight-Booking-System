using CORE.Enums;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.BookingDTOs
{
    public class GetBookingDTO
    {
        public int Id { get; set; }
        public string PNR { get; set; }
        public int NumberOfPassengers { get; set; }
        public ICollection<Passenger>? Passengers { get; set; }
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; }
        public string? StripeSessionId { get; set; }
    }
}
