
namespace JavidElectronics.Areas.Client.ViewModels.Home.Index.Modal
{
    public class ModalViewModel
    {
        public ModalViewModel(int id,string title, string description, decimal price, string imgUrl, List<ColorViewModel> colors, List<CategoryViewModel> categories, List<TagViewModel> tags)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            ImgUrl = imgUrl;
            Colors = colors;
            Categories = categories;
            Tags = tags;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImgUrl { get; set; }
        public List<ColorViewModel> Colors { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<TagViewModel> Tags { get; set; }

        public class ColorViewModel
        {
            public ColorViewModel(string name, int id)
            {
                Name = name;
                Id = id;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class CategoryViewModel
        {
            public CategoryViewModel(string title, int id)
            {
                Title = title;
                Id = id;
            }
            public int Id { get; set; }
            public string Title { get; set; }
        }
        public class TagViewModel
        {
            public TagViewModel(string title, int id)
            {
                Title = title;
                Id = id;
            }
            public int Id { get; set; }
            public string Title { get; set; }
        }
    }
}
