using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models;
using TaskManagement.Structs;
using Task = TaskManagement.Models.Task;

namespace TaskManagementTests
{
	public class UsersFilterTests
	{
		public List<User?> Users { get; set; }
		public List<Task?> Tasks { get; set; }

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
      Users = new List<User?>
      {
        new User()
        {
          Id = Guid.NewGuid(),
          Username = "Test",
          Email = "test@gmail.ru",
          Information = "",
          Password = "000000",
          Role = TaskManagement.Data.Enums.Role.Admin,
          CreatedAt = DateTime.Now,
        },
        new User() 
        {
          Id = Guid.NewGuid(),
          Username = "DimonZaminirovaliTapok",
          Email = "falshivka@gmail.ru",
          Information = "",
          Password = "000000",
          Role = TaskManagement.Data.Enums.Role.Moderator,
          CreatedAt = DateTime.Now,
        },
        new User() 
        {
          Id = Guid.NewGuid(),
          Username = "Xi Veliky Sterzhen",
          Email = "miskaris@gmail.ru",
          Information = "",
          Password = "000000",
          Role = TaskManagement.Data.Enums.Role.Default,
          CreatedAt = DateTime.Now,
        }
      };

    }

    [Test]
    public void UserFilterT_FilterByName_SuccesfulyFiltered()
    {
      var filter = new UserFilterStruct();
      filter.FilterName = "Test";
      var filteredUsers = filter.Filter(Users);

      Assert.True(filteredUsers.Count() == 1);
    }

    [Test]
    public void UserFilterT_FilterByAdminRole_SuccesfulyFiltered()
    {
      var filter = new UserFilterStruct();
      filter.isAdmin = false;
      var filteredUsers = filter.Filter(Users);

      Assert.True(filteredUsers.Count() == 2);
    }

    [Test]
    public void UserFilterT_FilterByModerRole_SuccesfulyFiltered()
    {
      var filter = new UserFilterStruct();
      filter.isModer = false;
      var filteredUsers = filter.Filter(Users);

      Assert.True(filteredUsers.Count() == 2);
    }

    [Test]
    public void UserFilterT_FilterByDefaultRole_SuccesfulyFiltered()
    {
      var filter = new UserFilterStruct();
      filter.isDefault = false;
      var filteredUsers = filter.Filter(Users);

      Assert.True(filteredUsers.Count() == 2);
    }
	}
}
