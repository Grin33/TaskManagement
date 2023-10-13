using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.Enums
{
  /// <summary>
  /// Приоритет задачи
  /// </summary>
  public enum Priority
  {
    [Display(Name = "Высокий")]
    High = 2,
    [Display(Name = "Средний")]
    Medium = 1,
    [Display(Name = "Низкий")]
    Low = 0
  }
}
