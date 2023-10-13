using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManagement.Data.Comparers;
using TaskManagement.Data.Interfaces;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
  /// <summary>
  /// Контроллер домашней страницы
  /// </summary>
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
		private readonly ITasksRepository _tasksRepos;
		private readonly IUsersRepository _usersRepos;

		public HomeController(ILogger<HomeController> logger, ITasksRepository tasks, IUsersRepository users)
    {
      _logger = logger;
      _tasksRepos = tasks;
      _usersRepos = users;
    }

    /// <summary>
    /// Метод вызывающий окно домашней страницы сайта
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
      if (!User.Identity.IsAuthenticated)
      {
        return View("Index");
      }
      var currentUserId = Guid.Parse(User.Claims.Where(k => k.Type == "Id").FirstOrDefault().Value);
      var userTasks = await _usersRepos.GetLinkedTasksAsync(currentUserId);
      userTasks.Sort(new TaskPriorityComparer<Models.Task?>());
      return View(userTasks);
    }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

  }
}