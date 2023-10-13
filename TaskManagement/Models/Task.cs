using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TaskManagement.Data.Enums;
using TaskManagement.Data.Interfaces;

namespace TaskManagement.Models
{
  /// <summary>
  /// Модель задачи в БД
  /// </summary>
  public class Task : IEntity
  {
    /// <summary>
    /// Id задачи
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование задачи
    /// </summary>
    [Required]
    [Display(Name = "Наименование")]
    public string Name { get; set; }

    /// <summary>
    /// Описание задачи
    /// </summary>
    [Required]
    [Display(Name = "Описание")]
    public string Description { get; set; }

    /// <summary>
    /// Приоритет задачи
    /// </summary>
    [Required]
    [Display(Name = "Приоритет")]
    public Priority Priority { get; set; }

    /// <summary>
    /// Статус задачи
    /// </summary>
    [Required]
    [Display(Name = "Статус")]
    public Status Status { get; set; }
    
    /// <summary>
    /// Дата завершения задачи
    /// </summary>
    [Required]
    [Display(Name = "Дата завершения")]
    [DataType(DataType.Date)]
    public DateTime DeadLine { get; set; }

    /// <summary>
    /// Наблюдатели задачи
    /// </summary>
    [Required]
    [Display(Name = "Наблюдатели")]
    public List<User?> Reviewers { get; set; } = new();
    
    /// <summary>
    /// Исполнители задачи
    /// </summary>
    [Required]
    [Display(Name = "Исполнители")]
    public List<User?> Executors { get; set; } = new();

    /// <summary>
    /// Комментарии задачи
    /// </summary>
    [Display(Name = "Комментарии")]
    public List<Comment?> Comments { get; set; } = new();

    public Task()
    {

    }
  }
}
