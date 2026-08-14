using OS.Domain.Models.Base;

namespace OS.Domain.Models
{
    public class Product : DefaultTable
    {
        public ProductCategory? Category { get; set; }
        public int? Price { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime? OpenedAt { get; set; }
        public int? Count { get; set; }
    }
}
