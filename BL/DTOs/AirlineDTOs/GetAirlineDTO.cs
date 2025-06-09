using BL.DTOs.AircraftDTOs;
using BL.DTOs.CountryDTOs;
using BL.DTOs.FlightDTOs;
using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AirlineDTOs
{
    public class GetAirlineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public GetIncludedCountryDTO Country { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<GetIncludedFlightDTO>? Flights { get; set; }
        public ICollection<GetIncludedAircraftDTO>? Aircrafts { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
