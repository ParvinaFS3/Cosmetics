using Cosmetics.Models.Customer;

namespace Cosmetics.Models
{
    public class About: BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }

        public string? FilePath { get; set; }     
        

        public ICollection<AboutPhoto>? Photos { get; set; }
        public ICollection<FeaturedProduct> FeaturedProducts { get; set;}
    }
}
