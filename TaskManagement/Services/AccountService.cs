using System.Security.Claims;
using TaskManagement.Data.AppData;
using TaskManagement.Data.Interfaces;
using TaskManagement.Data.Repositories;
using TaskManagement.Models;
using TaskManagement.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Principal;

namespace TaskManagement.Services
{
  /// <summary>
  /// Сервис аутентификации и авторизации пользователя
  /// </summary>
  public class AccountService : IAccountService
  {
    private readonly UsersRepository _usersRepository;

    public AccountService(ApplicationDatabaseContext context)
    {
      this._usersRepository = new UsersRepository(context);
    }

    /// <summary>
    /// Регистраця нового пользователя
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public ClaimsIdentity? Register(RegisterViewModel model)
    {
      var newUser = new User(model);
      var response = _usersRepository.Add(newUser);
      if (response != false)
      {
        var result = Authenticate(newUser);
        return result;
      }
      return null;
    }

    /// <summary>
    /// Вход в аккаунт пользователя
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async System.Threading.Tasks.Task<ClaimsIdentity?> Login(string username, string password)
    {
      var users = await _usersRepository.GetAllAsync();
      var possibleUsers = users.ToList().Where(x => x.Username == username.Trim(' '));
      if (possibleUsers == null)
      {
        return null;
      }

      foreach(var user in possibleUsers)
      {
        if ((user != null) 
          && (user.Password == _usersRepository.HashPassword(password)))
        {
          var result = Authenticate(user);
          return result;
        }
      }

      return null;
    }

    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private ClaimsIdentity Authenticate(User user)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
        new Claim("Id",user.Id.ToString())
      };
      return new ClaimsIdentity(claims, "ApplicationCookie",
        ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
  }
}
