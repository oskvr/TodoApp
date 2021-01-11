using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Helpers;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicTodoList.Pages.Tasks
{
	public class TodayModel : PageModel
	{
		public IEnumerable<TodoTask> TodaysTasks { get; set; }
		public int UserListCount { get; set; }

		private readonly ApplicationDbContext context;

		public TodayModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
		{
			UserListCount = context.TodoListUser.Count(tlu => tlu.ApplicationUserId == User.GetUserId());
			TodaysTasks = await context.TodoTasks.GetDueToday(User.GetUserId());
		}
	}
}
