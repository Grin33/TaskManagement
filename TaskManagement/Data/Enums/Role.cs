using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TaskManagement.Data.Enums
{
	/// <summary>
	/// Роль пользователя
	/// </summary>
  public enum Role
	{
		[Display(Name = "Админ")]
		Admin = 1,
		[Display(Name = "Модератор")]
		Moderator = 2,
		[Display(Name = "Без привилегий")]
		Default = 0
  }
}
