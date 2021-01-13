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
		public IEnumerable<TodoTask> UpcomingTasks { get; set; }

		private readonly ApplicationDbContext context;

		public UpcomingModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
		{
			UpcomingTasks = await context.TodoTasks.GetUpcoming(UserId);
		}
	}
}
