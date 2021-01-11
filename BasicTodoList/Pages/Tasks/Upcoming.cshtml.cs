using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Helpers;
using BasicTodoList.Models;
using BasicTodoList.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicTodoList.Pages.Tasks
{
	public class UpcomingModel : BasePageModel
    {
		public IEnumerable<TodoTask> PlannedTasks { get; set; }

		private readonly ApplicationDbContext context;

		public UpcomingModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
		{
			PlannedTasks = await context.TodoTasks.GetPlanned(UserId);
		}
	}
}
