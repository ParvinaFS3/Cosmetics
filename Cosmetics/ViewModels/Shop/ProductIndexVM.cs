using Cosmetics.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cosmetics.ViewModels.Shop
{
    public class ProductIndexVM
    {
        public List<Product> Products { get; set; }

       public List<Category> Categories { get; set; }
        #region Filter
        public string? Title { get; set; }
        [Display(Name = "Min. Quantity")]
        public int? MinQuantity { get; set; }
        [Display(Name = "Max. Quantity")]
        public int? MaxQuantity { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public List<SelectListItem>? Category { get; set; }

        public bool FilterByPriceLowToHigh { get; set; }

        public bool FilterByPriceHighToLow { get; set; }

        public bool FilterByLatestProduct { get; set; }
        #endregion

    }
}
