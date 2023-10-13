using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Data.Interfaces;
using TaskManagement.ViewModels;

namespace TaskManagement.Controllers
{
  /// <summary>
  /// Контроллер отвечающий за действия с аккаунтом
  /// </summary>
  public class AccountController : Controller
  {

    private readonly IUsersRepository _usersRepos;
    private readonly IAccountService _accountService;

    public AccountController(IUsersRepository users, IAccountService accService)
    {
      this._usersRepos = users;
      this._accountService = accService;
    }

    /// <summary>
    /// Вызов окна регистрации
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Register()
    {
      return View();
    }

    /// <summary>
    /// Метод, осуществляющий регистрацию нового пользователя
    /// </summary>
    /// <param name="model">Регистрационная модель</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
      if (ModelState.IsValid)
      {
        var response = _accountService.Register(model);
        if (response != null)
        {
          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
            , new ClaimsPrincipal(response));
          return RedirectToAction("Index", "Home");
        }

      }
      return View(model);
    }

    /// <summary>
    /// Метод, осуществляющий вход в аккаун
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="password">Пароль</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
      var temp = username;
      var temp2 = password;
      if (ModelState.IsValid)
      {
        var response = await _accountService.Login(username,password);
        if (response != null)
        {
          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme
            , new ClaimsPrincipal(response));
          return RedirectToAction("Index", "Home");
        }
        password = string.Empty;
        return RedirectToAction("Index","Home");
      }
      return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// Отображение профиля залогинившегося пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
      if(User.Identity.IsAuthenticated)
      {
        var users = await _usersRepos.GetAllAsync();
        var currentId = Guid.Parse(User.Claims.Where(k => k.Type == "Id").FirstOrDefault().Value);
        var currentUser = users.Where(x => x.Id == currentId).FirstOrDefault();
        if(currentUser != null) 
        {
          //не передается параметр
          return RedirectToAction("Details", "Users", routeValues: new { id = (Guid?)currentUser.Id });
        }
      }
      return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult BackToHome()
    {
      return RedirectToAction("Index", "Home");
    }

  }
}
