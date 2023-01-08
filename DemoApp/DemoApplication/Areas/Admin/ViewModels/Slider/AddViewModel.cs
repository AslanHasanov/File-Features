namespace DemoApplication.Areas.Admin.ViewModels.Slider
{
    public class AddViewModel
    {
       

        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string ButtonName { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    
    }
}
