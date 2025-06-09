using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.SeatClassDTOs
{
    public class UpdateSeatClassDTO
    {
        public string ClassName { get; set; }
        public int StartingRow { get; set; }
        public int EndingRow { get; set; }
        public bool AutoAssign { get; set; }

        public List<string> Columns { get; set; }
        public int AircraftId { get; set; }
    }
}
