using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TaskManagement.Data.AppData;
using TaskManagement.Data.Comparers;
using TaskManagement.Data.Interfaces;
using TaskManagement.Models;

namespace TaskManagement.Data.Repositories
{
  public class UsersRepository : IUsersRepository
  {
    private readonly ApplicationDatabaseContext context;

    public UsersRepository(ApplicationDatabaseContext context)
    {
      this.context = context;
    }

    /// <summary>
    /// Получение всех пользователей
    /// </summary>
    /// <returns>Коллекция пользователей</returns>
    public async Task<IEnumerable<User?>> GetAllAsync()
    {
      return await context.Users.ToListAsync();
    }

    /// <summary>
    /// Получение пользователя по Id
    /// </summary>
    /// <param name="id">id пользователя</param>
    /// <returns>сущность пользователя</returns>
    public async Task<User?> GetByIdAsync(Guid id)
    {
      return await context.Users.FirstOrDefaultAsync(item => item.Id == id);
    }

		/// <summary>
		/// Получение всех задач, в которых пользователь является исполнителем
		/// </summary>
		/// <param name="userID">Id пользователя</param>
		/// <returns>Список задач</returns>
		public async Task<List<Models.Task?>> GetAllExeByIdAsync(Guid userID)
    {
      var user = await context.Users
        .Include(item => item.Executors)
        .FirstOrDefaultAsync(item => item.Id == userID);
      return user.Executors;
    }

    /// <summary>
    /// Получение всех задач, в которых пользователь является наблюдателем
    /// </summary>
    /// <param name="userID">Id Пользователя</param>
    /// <returns>Список задач</returns>
    public async Task<List<Models.Task?>> GetAllRevByIdAsync(Guid userID)
    {
      var user = await context.Users
        .Include(item => item.Reviewers)
        .FirstOrDefaultAsync(item => item.Id == userID);
      return user.Reviewers;
    }

    /// <summary>
    /// Получить все связанные задачи с пользователем
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <returns>Список задач</returns>
		public async Task<IEnumerable<Models.Task?>> GetLinkedTasksAsync(Guid userId)
		{
      var tasks = await GetAllExeByIdAsync(userId);
      tasks.AddRange(await GetAllRevByIdAsync(userId));
      return tasks.Distinct(new TaskEqualityComparer<Models.Task?>()).ToList();
		}

    /// <summary>
    /// Добавление сущности в БД
    /// </summary>
    /// <param name="entity">Сущность для добавления</param>
    /// <returns>Результат обновления</returns>
		public bool Add(User entity)
    {
      entity.Password = HashPassword(entity.Password);
      context.Users.Add(entity);
      return Save();
    }

		/// <summary>
		/// Обновление задачи в БД
		/// </summary>
		/// <param name="entity">Сущность для обновления</param>
		/// <returns>результат обновления</returns>
		public bool Update(User entity)
    {
      context.Users.Update(entity);
      return Save();
    }

    /// <summary>
    /// Удаление сущности из БД
    /// </summary>
    /// <param name="entity">Сущность для удаления</param>
    /// <returns>Результат удаления</returns>
    public bool Delete(User entity)
    {
      context.Users.Remove(entity);
      return Save();
    }

    /// <summary>
    /// Сохранение БД
    /// </summary>
    /// <returns>Результат сохранения</returns>
    public bool Save()
    {
      var saved = context.SaveChanges();
      return saved > 0;
    }

    /// <summary>
    /// Метод для хеширования пароля
    /// </summary>
    /// <param name="password">строка с паролем</param>
    /// <returns></returns>
    public string HashPassword(string password)
    {
      using (var sha = SHA256.Create())
      {
        var HashedBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        var hash = BitConverter.ToString(HashedBytes).Replace("-", "").ToLower();
        return hash;
      }
    }

	}
}
