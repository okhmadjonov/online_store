using OS.Domain.Models.Base;

namespace OS.Domain.Models
{
    public class Language : EmptyTable
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Flag { get; set; } = string.Empty;
        public bool IsDefault { get; set; }

    }
}
