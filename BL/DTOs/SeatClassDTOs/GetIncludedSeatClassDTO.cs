using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.SeatClassDTOs
{
    public class GetIncludedSeatClassDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int StartingRow { get; set; }
        public int EndingRow { get; set; }
        public bool AutoAssign { get; set; }

        public List<string> Columns { get; set; }
    }
}
