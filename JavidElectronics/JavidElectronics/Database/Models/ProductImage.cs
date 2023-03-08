namespace JavidElectronics.Database.Models
{
    public class ProductImage
    {
        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; }
        public int ProductId { get; set; }
        public Product? Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
