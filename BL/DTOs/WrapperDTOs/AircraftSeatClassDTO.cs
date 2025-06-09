using BL.DTOs.AircraftDTOs;
using BL.DTOs.SeatClassDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.WrapperDTOs
{
    public class AircraftSeatClassDTO
    {
        public AddAircraftDTO Aircraft { get; set; }
        public ICollection<AddSeatClassDTO> SeatClasses { get; set; }
    }
}
