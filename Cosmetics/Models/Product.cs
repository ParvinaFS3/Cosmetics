using Cosmetics.Models.Constants;
using Cosmetics.Models.Customer;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Models
{
    public class Product:BaseEntity
    {
        public string? Name { get; set; }        
        public string? Description { get; set; }
        public int  CategoryId { get; set; }

        public Category? Category { get; set; }  
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int CreatedAt { get; set; }

        public Delivery Delivery { get; set; }

        public string? FilePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public ICollection<BasketProduct> BasketProducts { get; set; }

    }
}
