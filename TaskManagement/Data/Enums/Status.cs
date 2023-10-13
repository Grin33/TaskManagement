using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TaskManagement.Data.Enums
{
	/// <summary>
	/// Статус задачи
	/// </summary>
  public enum Status
	{
		[Display(Name = "В работе")]
		InProgress = 3,
		[Display(Name = "Не начата")]
		ExecutorRequired = 2,
		[Display(Name = "Ревью")]
		Review = 1,
		[Display(Name = "Закончена")]
		Finished = 0
  }
}
