using static NuGet.Packaging.PackagingConstants;

namespace TaskManagement.Structs
{
	/// <summary>
	/// Структура фильтров в поиске пользователей
	/// </summary>
	public class UserFilterStruct
	{
		public string FilterName { get; set; }
		public bool isAdmin { get; set; }
		public bool isModer { get; set; }
		public bool isDefault { get; set; }

		public UserFilterStruct(string FilterName, bool isAdmin,
			bool isModer, bool isDefault)
		{
			this.isAdmin = isAdmin;
			this.isModer = isModer;
			this.isDefault = isDefault;
			this.FilterName = FilterName == null ? string.Empty : FilterName;
		}

		public UserFilterStruct()
		{
			this.isModer = true;
			this.isDefault = true;
			this.isAdmin = true;
			this.FilterName = string.Empty;
		}

		public IEnumerable<Models.User?> Filter(IEnumerable<Models.User?> allUsers)
		{
			if ((FilterName != null)
				&& FilterName != string.Empty)
				allUsers = allUsers.Where((k) => k.Username.Contains(FilterName.Trim(' ')));
			if (!isAdmin)
				allUsers = allUsers.Where((k) => k.Role != Data.Enums.Role.Admin);
			if (!isModer)
				allUsers = allUsers.Where((k) => k.Role != Data.Enums.Role.Moderator);
			if (!isDefault)
				allUsers = allUsers.Where((k) => k.Role != Data.Enums.Role.Default);

			return allUsers;
		}
	}
}
