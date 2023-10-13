using System.Security.Claims;
using TaskManagement.ViewModels;

namespace TaskManagement.Data.Interfaces
{
  /// <summary>
  /// Интерфейс сервиса для работы с аккаунтом
  /// </summary>
  public interface IAccountService
  {
    public ClaimsIdentity? Register(RegisterViewModel model);
    public Task<ClaimsIdentity?> Login(string username, string password);
  }
}
