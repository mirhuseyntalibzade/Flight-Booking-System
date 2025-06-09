using CORE.Models.Base;

namespace CORE.Models
{
    public class Airline : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public string LogoUrl { get; set; }
        public ICollection<Flight>? Flights { get; set; }
        public ICollection<Aircraft>? Aircrafts { get; set; }
    }
}
