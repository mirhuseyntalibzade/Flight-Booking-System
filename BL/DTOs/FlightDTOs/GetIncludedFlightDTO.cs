using BL.DTOs.AircraftDTOs;
using BL.DTOs.AirlineDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.FlightDTOs
{
    public class GetIncludedFlightDTO
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan Duration => ArrivalTime - DepartureTime;
        public decimal Price { get; set; }
    }
}
