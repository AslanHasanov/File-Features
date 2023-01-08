using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Image : BaseEntity<int>, IAuditable
    {
        public string? ImageName { get; set; } //<original_name>.<extension>
        public string? ImageNameInFileSystem { get; set; } //Guid.<extension>

        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
