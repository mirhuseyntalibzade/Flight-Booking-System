using CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.SeatDTOs
{
    public class GetIncludedSeatDTO
    {
        public string SeatNumber { get; set; }
        public string SeatClass { get; set; }
        public bool IsAvailable { get; set; }
        public bool AutoAssign { get; set; }

    }
}
