using TaskManagement.Models;

namespace TaskManagement.ViewModels
{
  /// <summary>
  /// View-Модель комментария 
  /// </summary>
  public class CommentDetailsViewModel : Comment
  {
    public User Author { get; set; }

    public CommentDetailsViewModel(Comment comment)
      : base(comment.UserId.Value, comment.Message, comment.Task)
    {
    }
  }
}
