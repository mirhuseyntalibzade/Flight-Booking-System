using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.SeatClassDTOs
{
    public class GetSeatClassDTO
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int StartingRow { get; set; }
        public int EndingRow { get; set; }
        public bool AutoAssign { get; set; }

        public List<string> Columns { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
