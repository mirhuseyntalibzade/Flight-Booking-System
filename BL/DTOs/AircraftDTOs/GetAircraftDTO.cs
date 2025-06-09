using BL.DTOs.AirlineDTOs;
using BL.DTOs.FlightDTOs;
using BL.DTOs.SeatClassDTOs;
using BL.DTOs.SeatDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AircraftDTOs
{
    public class GetAircraftDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Capacity { get; set; }
        public GetIncludedAirlineDTO Airline { get; set; }
        public ICollection<GetIncludedFlightDTO> Flights { get; set; }
        public ICollection<GetIncludedSeatClassDTO> SeatClasses { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
