namespace DemoApplication.Areas.Admin.ViewModels.Slider
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int? id, string title, string content, string? backgroundImageUrl, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Title = title;
            Content = content;
            BackgroundImageUrl = backgroundImageUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? BackgroundImageUrl { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
