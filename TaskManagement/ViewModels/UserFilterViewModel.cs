using TaskManagement.Models;
using TaskManagement.Structs;

namespace TaskManagement.ViewModels
{
	public class UserFilterViewModel
	{
		public IEnumerable<User?> Users { get; set; }

		public UserFilterStruct Filters { get; set; }

		public UserFilterViewModel(IEnumerable<User?> users, UserFilterStruct Filters)
			: this(users)
		{
			this.Filters = Filters;
		}

		public UserFilterViewModel(IEnumerable<User?> users)
		{
			this.Users = users;
			this.Filters = new UserFilterStruct();
		}
	}
}
