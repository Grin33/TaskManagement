using TaskManagement.Models;

namespace TaskManagement.Data.Interfaces
{
	/// <summary>
	/// Интерфейс репозитория для работы с Пользователями
	/// </summary>
  public interface IUsersRepository : IRepository<User>
  {
    string HashPassword(string Password);

		/// <summary>
		/// Получить список Executor выбранного Пользователя.
		/// </summary>
		/// <param name="userId">Id Пользователя.</param>
		/// <returns>Список пользователей.</returns>
		Task<List<Models.Task?>> GetAllExeByIdAsync(Guid userId);

		/// <summary>
		/// Получить список Reviewer выбранного Пользователя.
		/// </summary>
		/// <param name="userId">Id пользователя.</param>
		/// <returns>Список пользователей.</returns>
		Task<List<Models.Task?>> GetAllRevByIdAsync(Guid userId);

		/// <summary>
		/// Получить все связанные с пользователем задачи
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<IEnumerable<Models.Task?>> GetLinkedTasksAsync(Guid userId);
  }
}
