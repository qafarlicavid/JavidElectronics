namespace JavidElectronics.Areas.Admin.ViewModels.Contact
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? name, string? email, string? phone, string? message, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Message = message;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
