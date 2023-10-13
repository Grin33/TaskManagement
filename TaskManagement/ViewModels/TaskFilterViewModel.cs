using Task = TaskManagement.Models.Task;
namespace TaskManagement.ViewModels
{
	/// <summary>
	/// Отображаемая модель для окна с фильтрацией
	/// </summary>
	public class TaskFilterViewModel
	{
		public IEnumerable<Task?> Tasks { get; set; }

		public string FilterText { get; set; }
		public bool HighPriorityEnabled { get; set; }
		public bool MediumPriorityEnabled { get; set; }
		public bool LowPriorityEnabled { get; set; }
		public bool isReviewEnabled { get; set; }
		public bool isInProgressEnabled { get; set; }
		public bool isExecReqEnabled { get; set; }
		public bool isFinished { get; set; }

		public bool isUserLinked { get; set; }
		public TaskFilterViewModel(IEnumerable<Task?> tasks,string FilterText, bool HighPriorityEnabled
			, bool MediumPriorityEnabled, bool LowPriorityEnabled, bool isReviewEnabled
			, bool isInProgressEnabled, bool isExecReqEnabled, bool isFinished, bool isUserLinked)
			: this(tasks)
		{
			this.FilterText = FilterText;
			this.HighPriorityEnabled = HighPriorityEnabled;
			this.MediumPriorityEnabled = MediumPriorityEnabled;
			this.LowPriorityEnabled = LowPriorityEnabled;
			this.isReviewEnabled = isReviewEnabled;
			this.isInProgressEnabled = isInProgressEnabled;
			this.isExecReqEnabled = isExecReqEnabled;
			this.isFinished = isFinished;
			this.isUserLinked = isUserLinked;
		}

		public TaskFilterViewModel(IEnumerable<Task?> tasks)
			: this()
		{
			Tasks = tasks;
		}

		public TaskFilterViewModel()
		{
			this.FilterText = string.Empty;
			this.HighPriorityEnabled = true;
			this.MediumPriorityEnabled = true;
			this.LowPriorityEnabled = true;
			this.isReviewEnabled = true;
			this.isInProgressEnabled = true;
			this.isFinished = true;
			this.isExecReqEnabled = true;
			this.isUserLinked = false;
		}
	}
}
