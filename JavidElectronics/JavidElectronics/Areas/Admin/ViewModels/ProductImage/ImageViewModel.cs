namespace JavidElectronics.Areas.Admin.ViewModels.Image
{
    public class ImageViewModel
    {
        public int ProductId { get; set; }
        public List<ListItem>? ProductImages { get; set; }

        public class ListItem
        {
            public int Id { get; set; }
            public string? ProductImageUrl { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}
