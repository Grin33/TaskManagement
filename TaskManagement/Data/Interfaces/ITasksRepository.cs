using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.Data.Interfaces
{
  /// <summary>
  /// Интерфейс репозитория для работы с задачами
  /// </summary>
  public interface ITasksRepository: IRepository<Task>
  {
    /// <summary>
    /// Получить все задачи исполнителя.
    /// </summary>
    /// <param name="executorId">Id исполнителя.</param>
    /// <returns>Список задач. </returns>
    Task<IEnumerable<Task?>> GetAllByExecutorIdAsync(Guid executorId);

    /// <summary>
    /// Получить все задачи рецензента.
    /// </summary>
    /// <param name="reviewerId">Id рецензента.</param>
    /// <returns>Список задач.</returns>
    Task<IEnumerable<Task?>> GetAllByReviewerIdAsync(Guid reviewerId);

    /// <summary>
    /// Получить список Executor выбранного Task.
    /// </summary>
    /// <param name="taskId">Id задачи.</param>
    /// <returns>Список пользователей.</returns>
    Task<List<User?>> GetAllExeByIdAsync(Guid taskId);

    /// <summary>
    /// Получить список Reviewer выбранного Task.
    /// </summary>
    /// <param name="taskId">Id задачи.</param>
    /// <returns>Список пользователей.</returns>
    Task<List<User?>> GetAllRevByIdAsync(Guid taskId);

    /// <summary>
    /// Получить весь список Пользователей, связанных с данной задачей
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    Task<List<User?>> GetLinkedUsers(Guid taskId);
  }
}
