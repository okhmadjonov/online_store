using OS.Domain.Models.Base;

namespace OS.Domain.Models
{
    public class ProductCategoryTranslate : DefaultTable
    {
        public ProductCategory? ProductCategory { get; set; }
        public Language? Language { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

    }
}
