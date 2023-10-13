﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagement.Data.Interfaces;
using TaskManagement.Models;
using TaskManagement.ViewModels;

namespace TaskManagement.Controllers
{
  public class UsersController : Controller
  {
    private readonly IUsersRepository _usersRepos;

    public UsersController(IUsersRepository userRepos)
    {
      this._usersRepos = userRepos;
    }

    /// <summary>
    /// Отображение списка пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var allUsers = await _usersRepos.GetAllAsync();
      return View(allUsers);
    }


    /// <summary>
    /// Получения окна информации о пользователе
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    public async Task<IActionResult> Details(Guid? id)
    {
      if (id == null)
        throw new Exception("Передан Null Параметр");

      var user = await _usersRepos.GetByIdAsync(id.Value);
      if (user == null)
        return NotFound();
      return View(user);
    }

    /// <summary>
    /// Получение окна изменения пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    [HttpGet]
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (User.Identity?.IsAuthenticated != true)
        return RedirectToAction("Index", "Home");

      if (id == null)
        throw new NullReferenceException("Передан Null Параметр");

      var currentUser = await _usersRepos.GetByIdAsync(id.Value);
      if (currentUser is null)
        return NotFound();

      return View(currentUser);
    }

    /// <summary>
    /// Post - Изменение Пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(User user)
    {
      var updateResult = false;
      if (user != null)
        updateResult = _usersRepos.Update(user);

      if (!updateResult)
        return View("Error");

      return RedirectToAction("Index");
    }

    /// <summary>
    /// Метод вызывающий удаление пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
        throw new NullReferenceException("Передан Null Параметр");

      var userToDelete = await _usersRepos.GetByIdAsync(id.Value);

      if (userToDelete == null)
        return NotFound();

      return View("Index");
    }

    /// <summary>
    /// Окно подтверждения удаления пользователя
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      var userToDelete = await _usersRepos.GetByIdAsync(id);

      if (userToDelete == null)
        return NotFound();

      var res = _usersRepos.Delete(userToDelete);
      if (!res)
        throw new Exception("Произошла ошибка при удалении записи");
      return View("Index");
    }
  }
}
