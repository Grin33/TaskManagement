using System.ComponentModel.DataAnnotations;
using TaskManagement.Data.Interfaces;

namespace TaskManagement.Models
{
  /// <summary>
  /// Модель комментария в БД
  /// </summary>
  public class Comment : IEntity
  {
    /// <summary>
    /// Id коммента
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Задача, в которой находится этот коммент
    /// </summary>
    public Task Task { get; set; }

    /// <summary>
    /// Текст комментария
    /// </summary>
    [Required]
    [Display(Name = "Введите текст комментария")]
    [StringLength(100)]
    public string Message { get; set; }

    /// <summary>
    /// Дата написания комментария
    /// </summary>
    [Display(Name = "Написан ")]
    public DateTime CreatedAt { get; set; }

    public Guid? UserId { get; set; }

    public Comment()
    {

    }
    public Comment(Guid userId, string message, Task task)
    {
      this.UserId = userId;
      this.Message = message;
      this.Task = task;
      this.CreatedAt = DateTime.Now;
    }
  }
}
