using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.ViewModels
{
  /// <summary>
  /// Модель для представления регистрационной страницы
  /// </summary>
  public class RegisterViewModel
  {
    [Required(ErrorMessage = "Укажите имя")]
    [MaxLength(50, ErrorMessage = "Имя должно иметь длину менее 50 символов")]
    [MinLength(3, ErrorMessage = "Имя должно иметь длину более 3-х символов")]
    [Display(Name = "Имя")]
    public string Name { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Введите пароль")]
    [MinLength(6, ErrorMessage = "Пароль должен иметь длину не менее 6 символов")]
    [Display(Name = "Пароль")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [Display(Name = "Подтвердите пароль")]
    public string PasswordConifrm { get; set; }

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Введите адрес эл.Почты")]
    [Display(Name = "Эл.Почта")]
    public string Email { get; set; }
  }
}
