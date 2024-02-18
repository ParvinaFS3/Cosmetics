using Cosmetics.Models.Customer;

namespace Cosmetics.Models
{
    public class Category:BaseEntity
    {

        public string Name { get; set; }

        public int Count { get; set; }  
        public ICollection<Product> Products { get; set; }
    }
}
