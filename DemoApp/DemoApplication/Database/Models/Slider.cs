using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Slider : BaseEntity<int>, IAuditable
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageName { get; set; } //<original_name>.<extension>
        public string? ImageNameInFileSystem { get; set; } //Guid.<extension>
        public string ButtonName { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get ; set; }
        public DateTime UpdatedAt { get ; set; }
    }
}
