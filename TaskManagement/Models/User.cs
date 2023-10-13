using System.ComponentModel.DataAnnotations;
using TaskManagement.Data.Enums;
using TaskManagement.Data.Interfaces;
using TaskManagement.ViewModels;

namespace TaskManagement.Models
{
  /// <summary>
  /// Модель пользователя в БД
  /// </summary>
  public class User : IEntity
  {
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    [Display(Name = "Имя")]
    public string Username { get; set; }

    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    /// <summary>
    /// Информация о пользователе
    /// </summary>
    [Display(Name = "О себе")]
    [MaxLength(150)]
    public string? Information { get; set; }
    
    /// <summary>
    /// Адрес почты пользователя
    /// </summary>
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Адрес электронной почты")]
    public string Email { get; set; }

    /// <summary>
    /// Роль пользователя
    /// </summary>
    [Required]
    [Display(Name = "Роль")]
    public Role Role { get; set; }

    /// <summary>
    /// Дата создания пользователя
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Дата создания")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Задачи, в которых пользователь является наблюдателем
    /// </summary>
    [Required]
    [Display(Name = "Наблюдатель")]
    public List<Task?> Reviewers { get; set; } = new();

    /// <summary>
    /// Задачи, в которых пользователь является 
    /// </summary>
    [Required]
    [Display(Name = "Исполнители")]
    public List<Task?> Executors { get; set; } = new();

    /// <summary>
    /// Уведомления пользователя
    /// </summary>
    public List<Notification?> Notifications { get; set; } = new();

    public User()
    {

    }

    public User(RegisterViewModel model)
    {
      this.Id = Guid.NewGuid();
      this.CreatedAt = DateTime.Now;
      this.Role = Role.Default;
      this.Username = model.Name; 
      this.Password = model.Password;
      this.Email = model.Email;
    }

  }
}
