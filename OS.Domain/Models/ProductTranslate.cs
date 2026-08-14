using OS.Domain.Models.Base;

namespace OS.Domain.Models
{
    public class ProductTranslate : DefaultTable
    {
        public Product? Product { get; set; }
        public Language? Language { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
