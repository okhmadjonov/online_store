using OS.Domain.Models.Base;

namespace OS.Domain.Models
{
    public class RegionTranslate : DefaultTable
    {
        public Region? Region { get; set; }
        public Language? Language { get; set; }
        public string? Name { get; set; }
    }
}
