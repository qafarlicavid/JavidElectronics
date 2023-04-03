namespace JavidElectronics.Areas.Client.ViewModels.Blog
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string name, string mainImageUrl, string hoverImageUrl, string videoURL, string adminName, DateTime createdAt, string content)
        {
            Id = id;
            Name = name;
            MainImageUrl = mainImageUrl;
            HoverImageUrl = hoverImageUrl;
            VideoURL = videoURL;
            AdminName = adminName;
            CreatedAt = createdAt;
            Content = content;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string MainImageUrl { get; set; }
        public string HoverImageUrl { get; set; }
        public string VideoURL { get; set; }
        public string AdminName { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
