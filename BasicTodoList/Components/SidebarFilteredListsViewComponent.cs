using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTodoList.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using BasicTodoList.Data;
using BasicTodoList.Models;
using System.Linq;
using BasicTodoList.Helpers;

public class SidebarFilteredListsViewComponent : ViewComponent
{
	public SidebarListItemViewModel Today { get; set; }
	public SidebarListItemViewModel Upcoming { get; set; }
	public SidebarListItemViewModel Important { get; set; }
	public SidebarListItemViewModel Overdue { get; set; }
	public List<SidebarListItemViewModel> SidebarModels { get; set; } = new List<SidebarListItemViewModel>();
	private IEnumerable<TodoTask> UsersTasks { get; set; }

	private readonly ApplicationDbContext context;

	public SidebarFilteredListsViewComponent(ApplicationDbContext context)
	{
		this.context = context;
	}
	public async Task<IViewComponentResult> InvokeAsync()
	{
		UsersTasks = await context.TodoTasks.GetAll(User.GetUserId());

		var todaysTasksCount = UsersTasks.Count(task => task.IsDueToday && !task.IsCompleted);
		var plannedTasksCount = UsersTasks.Count(task => task.IsPlanned && !task.IsCompleted);
		var importantTasksCount = UsersTasks.Count(task => task.IsImportant && !task.IsCompleted);
		var overdueTasksCount = UsersTasks.Count(task => task.IsOverdue && !task.IsCompleted);



		Today = new SidebarListItemViewModel
		{
			Name = "Today",
			AspPage = "/Tasks/Today",
			Icon = "today-list.svg",
			IncompleteTasksCount = todaysTasksCount,
		};
		Upcoming = new SidebarListItemViewModel
		{
			Name = "Upcoming",
			AspPage = "/Tasks/Upcoming",
			Icon = "upcoming-list.svg",
			IncompleteTasksCount = plannedTasksCount,
		};
		Important = new SidebarListItemViewModel
		{
			Name = "Important",
			AspPage = "/Tasks/Important",
			Icon = "important-list.svg",
			IncompleteTasksCount = importantTasksCount,
		};
		Overdue = new SidebarListItemViewModel
		{
			Name = "Overdue",
			AspPage = "/Tasks/Overdue",
			Icon = "overdue-list.svg",
			IncompleteTasksCount = overdueTasksCount,
		};
		SidebarModels.Add(Today);
		SidebarModels.Add(Upcoming);
		SidebarModels.Add(Important);
		SidebarModels.Add(Overdue);
		return View(SidebarModels);
	}
}