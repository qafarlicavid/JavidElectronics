namespace JavidElectronics.Areas.Admin.ViewModels.BlogImage
{
    public class BlogImagesViewModel
    {
        public int BlogId { get; set; }
        public List<ListItem>? Images { get; set; }

        public class ListItem
        {
            public int Id { get; set; }
            public string? ImageUrL { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
