using Cosmetics.Models;

namespace Cosmetics.ViewModels.Home
{
    public class HomeIndexVM
    {

        public Header Headers { get; set; }  
        
        public List<Product> Products { get; set; } 

        public List<Service> Services { get; set; } 

        public ProductWholeLook ProductWholeLook { get; set; }
    }
}
