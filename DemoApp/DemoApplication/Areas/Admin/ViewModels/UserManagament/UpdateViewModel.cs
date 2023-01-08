namespace DemoApplication.Areas.Admin.ViewModels.UserManagament
{
    public class UpdateViewModel
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int? RoleId { get; set; }
        public List<RoleListItemViewModel>? RoleListItemViewModels { get; set; }
    }
}
