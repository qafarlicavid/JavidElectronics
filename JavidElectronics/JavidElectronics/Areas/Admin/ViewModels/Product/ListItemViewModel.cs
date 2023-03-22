namespace JavidElectronics.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string name, int? rate, string description,
           decimal price, DateTime createdAt, List<CategoryViewModel> categories, List<ColorViewModel> colors, List<TagViewModel> tags)
        {
            Id = id;
            Name = name;
            Rate = rate;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            Categories = categories;
            Colors = colors;
            Tags = tags;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Rate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ColorViewModel> Colors { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public List<TagViewModel> Tags { get; set; }


        public class CategoryViewModel
        {
            public string Title { get; set; }
            public string ParentTitle { get; set; }
            public CategoryViewModel(string title, string parentTitle)
            {
                Title = title;
                ParentTitle = parentTitle;
            }
        }
        public class ColorViewModel
        {
            public string Name { get; set; }
            public ColorViewModel(string name)
            {
                Name = name;
            }
        }
        public class TagViewModel
        {
            public string Title { get; set; }
            public TagViewModel(string title)
            {
                Title = title;
            }
        }
    }
}
