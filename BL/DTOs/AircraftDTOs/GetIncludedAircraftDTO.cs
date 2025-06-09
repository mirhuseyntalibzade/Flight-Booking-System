using BL.DTOs.SeatDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AircraftDTOs
{
    public class GetIncludedAircraftDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Capacity { get; set; }
    }
}
