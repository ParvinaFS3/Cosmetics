using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetics.Areas.Admin.ViewModels.MakeupFooterVM
{
    public class CreateMakeupFooterVM
    {

        public string Title { get; set; }

        public string FilePath { get; set; }

        [NotMapped]
        public IFormFile formFile { get; set; }

    }
}
