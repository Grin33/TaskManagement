using static NuGet.Packaging.PackagingConstants;

namespace TaskManagement.Structs
{
  /// <summary>
  /// Структура фильтров в поиске задач
  /// </summary>
  public struct TaskFilterStruct
  {
    public string FilterText { get; set; }
    public bool HighPriorityEnabled { get; set; }
    public bool MediumPriorityEnabled { get; set; }
    public bool LowPriorityEnabled { get; set; }
    public bool isReviewEnabled { get; set; }
    public bool isInProgressEnabled { get; set; }
    public bool isExecReqEnabled { get; set; }
    public bool isFinished { get; set; }
    public bool isUserLinked { get; set; }

    public TaskFilterStruct()
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

    public TaskFilterStruct(string FilterText, bool HighPriorityEnabled,
      bool MediumPriorityEnabled, bool LowPriorityEnabled, bool isReviewEnabled,
      bool isInProgressEnabled, bool isExecReqEnabled, bool isFinished, bool isUserLinked)
    {
      this.FilterText = FilterText == null ? string.Empty : FilterText;
      this.HighPriorityEnabled = HighPriorityEnabled;
      this.MediumPriorityEnabled = MediumPriorityEnabled;
      this.LowPriorityEnabled = LowPriorityEnabled;
      this.isReviewEnabled = isReviewEnabled;
      this.isInProgressEnabled = isInProgressEnabled;
      this.isExecReqEnabled = isExecReqEnabled;
      this.isFinished = isFinished;
      this.isUserLinked = isUserLinked;
    }

    public IEnumerable<Models.Task?> Filter(IEnumerable<Models.Task?> tasks)
		{
      var filtername = this.FilterText;
			if ((FilterText != null) && (FilterText.Trim(' ') != string.Empty))
				tasks = tasks.Where((k) => k.Name.Contains(filtername.Trim(' ')));
			if (!HighPriorityEnabled)
				tasks = tasks.Where((k) => k.Priority != Data.Enums.Priority.High);
			if (!MediumPriorityEnabled)
				tasks = tasks.Where((k) => k.Priority != Data.Enums.Priority.Medium);
			if (!LowPriorityEnabled)
				tasks = tasks.Where((k) => k.Priority != Data.Enums.Priority.Low);
			if (!isInProgressEnabled)
				tasks = tasks.Where((k) => k.Status != Data.Enums.Status.InProgress);
			if (!isExecReqEnabled)
				tasks = tasks.Where((k) => k.Status != Data.Enums.Status.ExecutorRequired);
			if (!isReviewEnabled)
				tasks = tasks.Where((k) => k.Status != Data.Enums.Status.Review);
			if (!isFinished)
				tasks = tasks.Where((k) => k.Status != Data.Enums.Status.Finished);

      return tasks;
		}
  }
}
