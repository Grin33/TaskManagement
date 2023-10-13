using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using TaskManagement.Data.Interfaces;

namespace TaskManagement.Models
{
  /// <summary>
  /// Модель уведомления в БД. Не реализовано
  /// </summary>
  public class Notification : IEntity
  {
    /// <summary>
    /// Id уведомления
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id пользователя уведомления
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Пользователь уведомления
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Сообщения уведомления
    /// </summary>
    [Display(Name = "Сообщение")]
    public string Message { get; set; }

    /// <summary>
    /// Дата создания уведомления
    /// </summary>
    public DateTime CreatedAt { get; set; }

  }
}
