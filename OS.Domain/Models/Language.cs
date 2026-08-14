using OS.Domain.Models.Base;

namespace OS.Domain.Models
{
    public class Language : EmptyTable
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Flag { get; set; }
        public bool IsDefault { get; set; }

    }
}
