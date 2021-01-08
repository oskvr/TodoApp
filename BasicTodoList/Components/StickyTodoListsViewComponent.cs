using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTodoList.Models;
using BasicTodoList.Models.ViewModels;
using BasicTodoList.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BasicTodoList.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BasicTodoList.Helpers;

public class StickyTodoListsViewComponent : ViewComponent
{
	public SidebarListItemViewModel Today { get; set; }
	public SidebarListItemViewModel Planned { get; set; }
	public SidebarListItemViewModel Important { get; set; }
	public SidebarListItemViewModel Overdue { get; set; }
	public List<SidebarListItemViewModel> SidebarModels { get; set; } = new List<SidebarListItemViewModel>();

	private readonly ApplicationDbContext context;

	public StickyTodoListsViewComponent(ApplicationDbContext context)
	{
		this.context = context;
	}
	public async Task<IViewComponentResult> InvokeAsync()
    {
		Today = new SidebarListItemViewModel
		{
			Name = "Today",
			AspPage = "/Tasks/Today",
			Icon = "today-list.svg",
			IncompleteTasksCount = 9,
		};
		Planned = new SidebarListItemViewModel
		{
			Name = "Planned",
			AspPage = "/Tasks/Planned",
			Icon = "planned-list.svg",
			IncompleteTasksCount = 8,
		};
		Important = new SidebarListItemViewModel
		{
			Name = "Important",
			AspPage = "/Tasks/Important",
			Icon = "important-list.svg",
			IncompleteTasksCount = 5,
		};
		Overdue = new SidebarListItemViewModel
		{
			Name = "Overdue",
			AspPage = "/Tasks/Overdue",
			Icon = "overdue-list.svg",
			IncompleteTasksCount = 0,
		};
		SidebarModels.Add(Today);
		SidebarModels.Add(Planned);
		SidebarModels.Add(Important);
		SidebarModels.Add(Overdue);
		return View(SidebarModels);
    }
}