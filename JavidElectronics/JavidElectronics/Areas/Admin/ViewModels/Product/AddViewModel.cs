using System.ComponentModel.DataAnnotations;

namespace JavidElectronics.Areas.Admin.ViewModels.Product
{
    public class AddViewModel
    {
        public string? Name { get; set; }
        public int? Rate { get; set; }
        public List<int>? CategoryIds { get; set; }
        public List<int>? ColorIds { get; set; }

        public List<int>? TagIds { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public List<CategoryListItemViewModel>? Categories { get; set; }
        public List<ColorListItemViewModel>? Colors { get; set; }
        public List<TagListItemViewModel>? Tags { get; set; }
    }
}
