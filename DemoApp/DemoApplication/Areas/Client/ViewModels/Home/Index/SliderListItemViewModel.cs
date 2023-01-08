namespace DemoApplication.Areas.Client.ViewModels.Home.Index
{
    public class SliderListItemViewModel
    {
        public SliderListItemViewModel(string? title, string? content, string buttonName, string buttonRedirectUrl, int order, string? imageUrl)
        {
            Title = title;
            Content = content;
            ButtonName = buttonName;
            ButtonRedirectUrl = buttonRedirectUrl;
            Order = order;
            ImageUrl = imageUrl;
        }

        public string? Title { get; set; }
        public string? Content { get; set; }
        public string ButtonName { get; set; }
        public string ButtonRedirectUrl { get; set; }
        public int Order { get; set; }
        public string? ImageUrl { get; set; }
        
    }
}
