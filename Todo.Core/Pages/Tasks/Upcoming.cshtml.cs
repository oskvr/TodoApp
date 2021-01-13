using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Core.Data;
using Todo.Core.Helpers;
using Todo.Core.Models;
using Todo.Core.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Todo.Core.Pages.Tasks
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
