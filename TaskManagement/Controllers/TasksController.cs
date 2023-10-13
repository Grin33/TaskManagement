using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagement.Data.Comparers;
using TaskManagement.Data.Interfaces;
using TaskManagement.Models;
using TaskManagement.Services;
using TaskManagement.ViewModels;

namespace TaskManagement.Controllers
{
	/// <summary>
	/// Контроллер Задач
	/// </summary>
	public class TasksController : Controller
	{
		private readonly IUsersRepository _usersRepos;
		private readonly ITasksRepository _tasksRepos;
		private readonly ICommentRepository _commentsRepos;

		public TasksController(IUsersRepository users, ITasksRepository tasks
														, ICommentRepository comments)
		{
			_usersRepos = users;
			_tasksRepos = tasks;
			_commentsRepos = comments;
		}

		/// <summary>
		/// Метод для получения всех задач
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				var tasks = await _tasksRepos.GetAllAsync();
				var taskFilterVM = new TaskFilterViewModel(tasks);
				return View(taskFilterVM);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}

		/// <summary>
		/// Фильтрация коллекции задач
		/// </summary>
		/// <param name="FilterText">Фильтрация по названию</param>
		/// <param name="highPriority">Включение задач с высоким приоритетом</param>
		/// <param name="mediumPriority">Включение задач с средним приоритетом</param>
		/// <param name="LowPriority">Включение задач с низким приоритетом</param>
		/// <param name="isReview">Включение задач со статусом "Review"</param>
		/// <param name="isInProgress">Включение задач со статусом "InProgress"</param>
		/// <param name="isExecRequired">Включение задач со статусом "isExecRequired"</param>
		/// <param name="isFinished">Включение задач со статусом "isFinished"</param>
		/// <param name="isUserLinked">Включение только тех задач, которые связаны с пользователем</param>
		/// <returns>Отфильтрованная коллекция задач</returns>
		[HttpPost]
		public async Task<IActionResult> Index(string FilterText, bool highPriority, bool mediumPriority
								, bool LowPriority, bool isReview, bool isInProgress, bool isExecRequired, bool isFinished, bool isUserLinked)
		{
			var tasks = await GetTasksWithFilter(FilterText, highPriority, mediumPriority
								, LowPriority, isReview, isInProgress, isExecRequired, isFinished, isUserLinked);

			var taskFilterVM = new TaskFilterViewModel
				(tasks, FilterText, highPriority, mediumPriority,
				LowPriority, isReview, isInProgress, isExecRequired, isFinished, isUserLinked);
			return View(taskFilterVM);
		}

		/// <summary>
		/// Отображение справочной информации задачи
		/// </summary>
		/// <param name="id">Id задачи</param>
		/// <returns>Модель, представляющая справочную информацию</returns>
		[HttpGet]
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
				return RedirectToAction("Error", "Home");

			var currentTask = await _tasksRepos.GetByIdAsync(id.Value);

			if (currentTask == null)
				return RedirectToAction("Error", "Home");

			var TaskView = new TaskDetailsViewModel(currentTask);

			var CommentsView = new List<CommentDetailsViewModel>();

			var comments = await _commentsRepos.GetAllByTaskID(id.Value);
			foreach (var comm in comments)
			{
				var author = await _usersRepos.GetByIdAsync(comm.UserId.Value);
				var newCommentVM = new CommentDetailsViewModel(comm);
				newCommentVM.Author = author;
				CommentsView.Add(newCommentVM);
			}
			TaskView.CommentDetails = CommentsView;

			var currentExecs = await _tasksRepos.GetAllExeByIdAsync(id.Value);
			TaskView.Executors = currentExecs.ToList();

			var currentRevs = await _tasksRepos.GetAllRevByIdAsync(id.Value);
			TaskView.Reviewers = currentRevs.ToList();

			return View(TaskView);
		}

		/// <summary>
		/// Отображение окна для создания задачи
		/// </summary>
		/// <returns>Модель для создания</returns>
		public async Task<IActionResult> Create()
		{
			var vm = new TaskEditViewModel();

			var execs = await _usersRepos.GetAllAsync();
			vm.possibleExecutors = from item in execs
														 select new SelectListItem { Text = item.Username, Value = item.Id.ToString() };

			var reviews = await _usersRepos.GetAllAsync();
			vm.possibleReviewers = from item in reviews
														 select new SelectListItem { Text = item.Username, Value = item.Id.ToString() };
			return View(vm);
		}

		/// <summary>
		/// Post - создание задачи по входной модели
		/// </summary>
		/// <param name="model">Входная модель</param>
		/// <returns>Возврат к списку задач</returns>
		/// <exception cref="NullReferenceException"></exception>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(TaskEditViewModel model)
		{
			var toAdd = new Models.Task();
			//toAdd = model.currentTask;
			if (toAdd == null)
				throw new NullReferenceException();

			foreach (var item in model.currentExecutors)
			{
				var tempUser = await _usersRepos.GetByIdAsync(Guid.Parse(item));
				if (tempUser == null)
					return RedirectToAction("Error", "Home");
				tempUser.Executors.Add(toAdd);
				_usersRepos.Update(tempUser);
				toAdd.Executors.Add(tempUser);
			}
			foreach (var item in model.currentReviewers)
			{
				var tempUser = await _usersRepos.GetByIdAsync(Guid.Parse(item));
				if (tempUser == null)
					return RedirectToAction("Error", "Home");
				tempUser.Reviewers.Add(toAdd);
				_usersRepos.Update(tempUser);
				toAdd.Reviewers.Add(tempUser);
			}
			var add = _tasksRepos.Update(toAdd);

			if (!add) return View("Error");

			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// Отображение окна изменения задачи
		/// </summary>
		/// <param name="id">Id задачи</param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (User.Identity?.IsAuthenticated != true)
				return RedirectToAction("Index", "Home");

			if (id == null)
				return RedirectToAction("Error", "Home");

			var currentTask = await _tasksRepos.GetByIdAsync(id.Value);
			if (currentTask is null)
				return NotFound();

			var allUsers = await _usersRepos.GetAllAsync();

			var oldExecs = await _tasksRepos.GetAllExeByIdAsync(id.Value);
			var oldRevs = await _tasksRepos.GetAllRevByIdAsync(id.Value);

			var taskView = new TaskEditViewModel
				(
					currentTask,
					allUsers == null ? new List<User?>() : allUsers,
					oldExecs == null ? new List<User?>() : oldExecs,
					oldRevs == null ? new List<User?>() : oldRevs
				);

			return View(taskView);
		}

		/// <summary>
		/// Post - изменение задачи по входной модели
		/// </summary>
		/// <param name="model">Входная модель</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Edit(TaskEditViewModel model)
		{
			var toUpdate = await _tasksRepos.GetByIdAsync(model.Id);

			if (toUpdate == null)
				return RedirectToAction("Error", "Home");

			//весь код ниже определяет выбранных пользователей в наблюдатели и исполнители
			var newRevList = new List<User?>();
			if (model.currentReviewers == null)
				newRevList = new List<User?>();
			else
				foreach (var item in model.currentReviewers)
				{
					var tempRev = await _usersRepos.GetByIdAsync(Guid.Parse(item));
					newRevList.Add(tempRev);
				}

			var newExecList = new List<User?>();
			if (model.currentExecutors == null)
				newExecList = new List<User?>();
			else
				foreach (var item in model.currentExecutors)
				{
					var tempExec = await _usersRepos.GetByIdAsync(Guid.Parse(item));
					newExecList.Add(tempExec);
				}

			//весь последующий код реализован для избежания ошибочных отправлений писем
			var oldLinkedUsers = await _tasksRepos.GetLinkedUsers(model.Id);
			var newLinkedUsers = new List<User?>(newRevList);
			newLinkedUsers.AddRange(newExecList);

			var fullUsers = oldLinkedUsers;
			fullUsers.AddRange(newLinkedUsers);
			var toEmail = fullUsers.Distinct(new UserEqualityComparer<User?>()).ToList();

			EmailService.sendEmailToUsers(toEmail, toUpdate, Data.Enums.Reason.Edit);

			//если закидывать в update model.currentTask - выкидывает ошибку, что таска с данным ID уже отслеживается
			//Если изменится ссылка, то выкидывает ошибку. не могу придумать как сделать
			toUpdate.Name = model.Name;
			toUpdate.Description = model.Description;
			toUpdate.Status = model.Status;
			toUpdate.Priority = model.Priority;
			toUpdate.DeadLine = model.DeadLine;
			toUpdate.Executors = newExecList == null ? new List<User?>() : newExecList;
			toUpdate.Reviewers = newRevList == null ? new List<User?>() : newRevList;

			var update = _tasksRepos.Update(toUpdate);

			if (!update) return View("Error");

			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// Отображение окна удаления задачи
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
				return RedirectToAction("Error", "Home");

			var byIdAsync = await _tasksRepos.GetByIdAsync(id.Value);



			if (byIdAsync == null) return NotFound();

			return View(byIdAsync);
		}

		/// <summary>
		/// Подтверждение удаления задачи
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			if (User.Identity?.IsAuthenticated != true)
				return RedirectToAction("Index", "Home");

			var toDelete = await _tasksRepos.GetByIdAsync(id);
			if (toDelete == null)
				return RedirectToAction("Error", "Home");

			var usersToEmail = await _tasksRepos.GetLinkedUsers(id);

			EmailService.sendEmailToUsers(usersToEmail, toDelete, Data.Enums.Reason.Delete);

			var delete = _tasksRepos.Delete(toDelete);

			if (!delete) return View("Error");

			return RedirectToAction("Index", "Home");
		}

		/// <summary>
		/// Метод добавления комментария
		/// </summary>
		/// <param name="taskId">Id задачи</param>
		/// <param name="commentText">текст комментария</param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> AddComment(Guid? taskId, string commentText)
		{
			var currentUserId = Guid.Parse(User.Claims.Where(k => k.Type == "Id").FirstOrDefault().Value);
			var currentTask = await _tasksRepos.GetByIdAsync(taskId.Value);

			if (currentTask == null)
				return RedirectToAction("Error", "Home");

			var newComment = new Comment(currentUserId, commentText, currentTask);

			currentTask.Comments.Add(newComment);
			_tasksRepos.Update(currentTask);

			return RedirectToAction("Details", new { id = taskId });
		}


		/// <summary>
		/// Post-запрос получения отчета
		/// </summary> 
		/// <param name="filterText">Фильтрация по названию</param>
		/// <param name="highPriority">Включение задач с высоким приоритетом</param>
		/// <param name="mediumPriority">Включение задач с средним приоритетом</param>
		/// <param name="LowPriority">Включение задач с низким приоритетом</param>
		/// <param name="isReview">Включение задач со статусом "Review"</param>
		/// <param name="isInProgress">Включение задач со статусом "InProgress"</param>
		/// <param name="isExecRequired">Включение задач со статусом "isExecRequired"</param>
		/// <param name="isFinished">Включение задач со статусом "isFinished"</param>
		/// <param name="isUserLinked">Включение только тех задач, которые связаны с пользователем</param>
		/// <returns>Скачивание отчета из браузера</returns>
		public async Task<IActionResult> FormReportUser(string filterText, bool highPriority, bool mediumPriority
								, bool LowPriority, bool isReview, bool isInProgress, bool isExecRequired, bool isFinished, bool isUserLinked)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home");
			}
			var filteredTasks = await GetTasksWithFilter(filterText, highPriority, mediumPriority
								, LowPriority, isReview, isInProgress, isExecRequired, isFinished, isUserLinked);
			if (filteredTasks == null)
			{
				return View("SearchTask");
			}

			return File(
					 PresentationService.toPDF(filteredTasks, User.Identity.Name)
					 , "application/pdf"
					 , $"{User.Identity.Name}_TasksReport_{DateTime.Now.ToString("mmHHMMyyyy")}.pdf");

		}

		//[HttpGet]
		//public async Task<IActionResult> SearchTask()
		//{
		//	if (User.Identity.IsAuthenticated)
		//	{
		//		var tasks = await _tasksRepos.GetAllAsync();
		//		var taskFilterVM = new TaskFilterViewModel(tasks);
		//		return View(taskFilterVM);
		//	}
		//	else
		//	{
		//		return RedirectToAction("Index", "Home");
		//	}
		//}


		//public async Task<IActionResult> SearchTask(string FilterText, bool highPriority, bool mediumPriority
		//						, bool LowPriority, bool isReview, bool isInProgress, bool isExecRequired, bool isFinished, bool isUserLinked)
		//{

		//	var tasks = await GetTasksWithFilter(FilterText, highPriority, mediumPriority
		//						, LowPriority, isReview, isInProgress, isExecRequired, isFinished, isUserLinked);

		//	var taskFilterVM = new TaskFilterViewModel
		//		(tasks, FilterText, highPriority, mediumPriority,
		//		LowPriority, isReview, isInProgress, isExecRequired, isFinished, isUserLinked);
		//	return View(taskFilterVM);
		//}


		/// <summary>
		/// Метод, позволяющий отфильтровать задачи по входным фильтрам
		/// </summary>
		/// <param name="FilterText">Фильтрация по названию</param>
		/// <param name="highPriority">Включение задач с высоким приоритетом</param>
		/// <param name="mediumPriority">Включение задач с средним приоритетом</param>
		/// <param name="LowPriority">Включение задач с низким приоритетом</param>
		/// <param name="isReview">Включение задач со статусом "Review"</param>
		/// <param name="isInProgress">Включение задач со статусом "InProgress"</param>
		/// <param name="isExecRequired">Включение задач со статусом "isExecRequired"</param>
		/// <param name="isFinished">Включение задач со статусом "isFinished"</param>
		/// <param name="isUserLinked">Включение только тех задач, которые связаны с пользователем</param>
		/// <returns>Коллекция задач</returns>
		public async Task<IEnumerable<Models.Task?>> GetTasksWithFilter(string FilterText, bool highPriority, bool mediumPriority
								, bool LowPriority, bool isReview, bool isInProgress, bool isExecRequired, bool isFinished, bool isUserLinked)
		{

			var currentUserId = Guid.Parse(User.Claims.Where(k => k.Type == "Id").FirstOrDefault().Value);
			var temptasks = new object();
			if(isUserLinked)
			{
				temptasks = await _usersRepos.GetLinkedTasksAsync(currentUserId);
			}
			else
			{
				temptasks = await _tasksRepos.GetAllAsync();
			}

			var tasks = temptasks as IEnumerable<Models.Task?>;

			if ((FilterText != null) && (FilterText.Trim(' ') != string.Empty))
				tasks = tasks.Where((k) => k.Name.Contains(FilterText.Trim(' ')));
			if (!highPriority)
				tasks = tasks.Where((k) => k.Priority != Data.Enums.Priority.High);
			if (!mediumPriority)
				tasks = tasks.Where((k) => k.Priority != Data.Enums.Priority.Medium);
			if (!LowPriority)
				tasks = tasks.Where((k) => k.Priority != Data.Enums.Priority.Low);
			if (!isInProgress)
				tasks = tasks.Where((k) => k.Status != Data.Enums.Status.InProgress);
			if (!isExecRequired)
				tasks = tasks.Where((k) => k.Status != Data.Enums.Status.ExecutorRequired);
			if (!isReview)
				tasks = tasks.Where((k) => k.Status != Data.Enums.Status.Review);
			if (!isFinished)
				tasks = tasks.Where((k) => k.Status != Data.Enums.Status.Finished);

			return tasks;
		}
	}
}
