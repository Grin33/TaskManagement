using TaskManagement.Models;

namespace TaskManagement.ViewModels
{
	public class UserFilterViewModel
	{
		public IEnumerable<User?> Users { get; set; }

		public string FilterName { get; set; }

		public bool isAdmin { get; set; }
		public bool isModer { get; set; }
		public bool isDefault { get; set; }

		public UserFilterViewModel(IEnumerable<User?> users, string filterName
														, bool isAdmin, bool isModer, bool isDefault)
		{
			this.Users = users;
			this.FilterName = filterName;
			this.isDefault = isDefault;
			this.isModer = isModer;
			this.isAdmin = isAdmin;
		}

		public UserFilterViewModel(IEnumerable<User?> users)
		{
			this.Users = users;
			this.isModer = true;
			this.isDefault = true;
			this.isAdmin = true;
			this.FilterName = string.Empty;
		}
	}
}
