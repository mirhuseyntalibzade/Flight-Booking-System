using CORE.Models.Base;
using System.Text.Json;

namespace CORE.Models
{
    public class SeatClass : BaseAuditableEntity
    {
        public int AircraftId { get; set; }
        public Aircraft Aircraft { get; set; }
        public string ClassName { get; set; }
        public int StartingRow { get; set; }
        public int EndingRow { get; set; }
        public bool AutoAssign { get; set; }

        public List<string> Columns { get; set; }
    }
}
