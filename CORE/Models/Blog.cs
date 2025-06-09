using CORE.Models.Base;

namespace CORE.Models
{
    public class Blog : BaseAuditableEntity
    {
        public string BackgroundImageURL { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDesc { get; set; }

        public string Author { get; set; }
        public string Category { get; set; }
    }
}
