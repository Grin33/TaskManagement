using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.AppData;
using TaskManagement.Data;
using TaskManagement.Data.Interfaces;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;
using TaskManagement.Data.Comparers;
using System.Linq;

namespace TaskManagement.Data.Repositories
{
  /// <summary>
  /// Репозиторий для работы с задачами
  /// </summary>
  public class TasksRepository : ITasksRepository
  {
    private readonly ApplicationDatabaseContext Сontext;

    public TasksRepository(ApplicationDatabaseContext context)
    {
      this.Сontext = context;
    }

    /// <summary>
    /// Получение всех задач для исполнителя
    /// </summary>
    /// <param name="executorId">Id исполнителя</param>
    /// <returns>Коллекция Task</returns>
    public async Task<IEnumerable<Task?>> GetAllByExecutorIdAsync(Guid executorId)
    {
      return await Сontext.Tasks
          .Include(t => t.Executors)
          .Where(t => t.Executors.Any(e => e.Id == executorId))
          .ToListAsync();
    }

    /// <summary>
    /// Получение всех задач для наблюдателя
    /// </summary>
    /// <param name="reviewerId">id наблюдателя</param>
    /// <returns>Коллекция Task</returns>
    public async Task<IEnumerable<Task?>> GetAllByReviewerIdAsync(Guid reviewerId)
    {
      return await Сontext.Tasks
          .Include(t => t.Reviewers)
          .Where(t => t.Reviewers.Any(r => r.Id == reviewerId))
          .ToListAsync();
    }

    /// <summary>
    /// Получение всех исполнителей по задаче
    /// </summary>
    /// <param name="taskId">id задачи</param>
    /// <returns>Список User</returns>
    public async Task<List<User?>> GetAllExeByIdAsync(Guid taskId)
    {
      var task = await Сontext.Tasks
        .Include(item => item.Executors)
        .FirstOrDefaultAsync(item => item.Id == taskId);
      return task.Executors;
    }

    /// <summary>
    /// Получение всех наблюдателей по задаче
    /// </summary>
    /// <param name="taskId">id задачи</param>
    /// <returns>Список User</returns>
    public async Task<List<User?>> GetAllRevByIdAsync(Guid taskId)
    {
      var task = await Сontext.Tasks
        .Include(item => item.Reviewers)
        .FirstOrDefaultAsync(item => item.Id == taskId);
      return task.Reviewers;
    }

    /// <summary>
    /// Получение всех задач
    /// </summary>
    /// <returns>Коллекцию Task</returns>
    public async Task<IEnumerable<Task?>> GetAllAsync()
    {
      return await Сontext.Tasks.Include(t=>t.Comments).ToListAsync();
    }

    /// <summary>
    /// Получение задачи
    /// </summary>
    /// <param name="id">id задачи</param>
    /// <returns>экземпляр Task, или null при не нахождении</returns>
    public async Task<Task?> GetByIdAsync(Guid id)
    {
      return await Сontext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <summary>
    /// Добавление задачи в БД
    /// </summary>
    /// <param name="entity">Задача для добавления</param>
    /// <returns>Результат добавления</returns>
    public bool Add(Task entity)
    {
      Сontext.Tasks.Add(entity);
      return Save();
    }

    /// <summary>
    /// Обновления задачи в БД
    /// </summary>
    /// <param name="entity">Задача для обновления</param>
    /// <returns>Результат обновления</returns>
    public bool Update(Task entity)
    {
      Сontext.Tasks.Update(entity);
      return Save();
    }

    /// <summary>
    /// Удаление задачи из БД
    /// </summary>
    /// <param name="entity">Задача для удаления</param>
    /// <returns>Результат удаления</returns>
    public bool Delete(Task entity)
    {
      Сontext.Tasks.Remove(entity);
      return Save();
    }

    /// <summary>
    /// Сохранение БД
    /// </summary>
    /// <returns>Результат сохранения</returns>
    public bool Save()
    {
      var saved = Сontext.SaveChanges();
      return saved > 0;
    }

    /// <summary>
    /// Получение всех пользователей, связанных с задачей
    /// </summary>
    /// <param name="taskId">Id задачи</param>
    /// <returns>Список пользователей</returns>
    public async Task<List<User?>> GetLinkedUsers(Guid taskId)
    {
      var users = await GetAllExeByIdAsync(taskId);
      users.AddRange(await GetAllRevByIdAsync(taskId));
      return users.Distinct(new UserEqualityComparer<User?>()).ToList();
    }
  }
}
