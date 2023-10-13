using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagement.Models;
using Task = TaskManagement.Models.Task;

namespace TaskManagement.ViewModels
{
  /// <summary>
  /// Модель представления изменения задачи
  /// </summary>
  public class TaskEditViewModel : Task
  {
    public IEnumerable<SelectListItem>? possibleExecutors { get; set; }
    public IEnumerable<SelectListItem>? possibleReviewers { get; set; }

    public string[]? currentExecutors { get; set; }
    public string[]? currentReviewers { get; set; }

    public TaskEditViewModel()
    {

    }

    public TaskEditViewModel(IEnumerable<User?> allUsers, IEnumerable<User?> oldExecs, IEnumerable<User?> oldRevs)
    {

      this.possibleExecutors = from item in allUsers
                               select new SelectListItem { Text = item.Username, Value = item.Id.ToString() };
      this.possibleReviewers = from item in allUsers
                               select new SelectListItem { Text = item.Username, Value = item.Id.ToString() };

      this.currentExecutors = (from item in oldExecs
                               select item.Id.ToString()).ToArray();
      this.currentReviewers = (from item in oldRevs
                               select item.Id.ToString()).ToArray();
    }

    public TaskEditViewModel(Task task, IEnumerable<User?> allUsers, IEnumerable<User?> oldExecs, IEnumerable<User?> oldRevs)
    {
      base.Id = task.Id;
      base.Name = task.Name;
      base.Description = task.Description;
      base.Priority = task.Priority;
      base.Status = task.Status;
      this.DeadLine = task.DeadLine;
      base.Comments = task.Comments;
      base.Reviewers = task.Reviewers;
      base.Executors = task.Executors;


      this.possibleExecutors = from item in allUsers
                               select new SelectListItem { Text = item.Username, Value = item.Id.ToString() };
      this.possibleReviewers = from item in allUsers
                               select new SelectListItem { Text = item.Username, Value = item.Id.ToString() };

      this.currentExecutors = (from item in oldExecs
                               select item.Id.ToString()).ToArray();
      this.currentReviewers = (from item in oldRevs
                               select item.Id.ToString()).ToArray();
    }
  }
}
