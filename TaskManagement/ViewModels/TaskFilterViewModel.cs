using TaskManagement.Structs;
using Task = TaskManagement.Models.Task;
namespace TaskManagement.ViewModels
{
	/// <summary>
	/// Отображаемая модель для окна с фильтрацией
	/// </summary>
	public class TaskFilterViewModel
	{
		public IEnumerable<Task?> Tasks { get; set; }

		public TaskFilterStruct Filters { get; set; }

		public TaskFilterViewModel(IEnumerable<Task?> tasks, TaskFilterStruct filters)
	: this(tasks)
		{
			this.Filters = filters;
		}

		public TaskFilterViewModel(IEnumerable<Task?> tasks)
			: this()
		{
			Tasks = tasks;
		}

		public TaskFilterViewModel()
		{
			this.Tasks = new List<Task?>();
			this.Filters = new TaskFilterStruct();
		}
	}
}
