namespace JavidElectronics.Areas.Client.ViewModels.Home.Index
{
    public class ProductListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MainImgUrl { get; set; }
        public string HoverImgUrl { get; set; }

        public ProductListItemViewModel(int id, string name, string description, decimal price, DateTime createdAt, string mainImgUrl, string hoverImgUrl)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CreatedAt = createdAt;
            MainImgUrl = mainImgUrl;
            HoverImgUrl = hoverImgUrl;
        }

    }
}
