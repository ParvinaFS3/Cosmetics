using Cosmetics.Models.Customer;

namespace Cosmetics.Models
{
    public class FeaturedProduct: BaseEntity
    {
        public string Name { get; set; }  
        
        public int AboutId { get; set; }    

        public About About { get; set; }    
    }
}
