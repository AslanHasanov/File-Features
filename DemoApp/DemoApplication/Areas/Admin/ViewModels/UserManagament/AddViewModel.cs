using DemoApplication.Database.Models;

namespace DemoApplication.Areas.Admin.ViewModels.UserManagament
{
	public class AddViewModel
	{
	

		public string? Email { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Password { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public int RoleId { get; set; }
		public List<RoleListItemViewModel>? RoleListItemViewModels { get; set; }

		
	}
}
