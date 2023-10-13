namespace TaskManagement.ViewModels
{
  /// <summary>
  /// Модель представления справочной информации о задаче
  /// </summary>
  public class TaskDetailsViewModel : Models.Task
  {
    public List<CommentDetailsViewModel> CommentDetails { get; set; }

    public TaskDetailsViewModel(Models.Task task)
    {
      this.Id = task.Id;
      this.Name = task.Name;
      this.Description = task.Description;
      this.Priority = task.Priority;
      this.Status = task.Status;
      this.DeadLine = task.DeadLine;
      this.Reviewers = task.Reviewers;
      this.Executors = task.Executors;
      this.Comments = task.Comments;
    }
  }
}
