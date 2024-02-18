using Cosmetics.Models;
using Cosmetics.Models.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using E = Cosmetics.Models;
namespace Cosmetics.Areas.Admin.ViewModels.Product
{
    public class CreateProductVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int  CategoryId { get; set; }

        public List<SelectListItem> Category { get; set; }
        public int CreatedAt { get; set; }
        public double Price { get; set; }

        public Delivery Delivery { get; set; }

        public string FilePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
        public ICollection<BasketProduct> BasketProducts { get; set; }
    }
}
