using CORE.Models.Base;

namespace CORE.Models
{
    public class Country : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string ISOCode { get; set; }
        public ICollection<Airline>? Airlines { get; set; }
    }
}
