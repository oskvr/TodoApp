using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Core.Data;
using Todo.Core.Helpers;
using Todo.Core.Models;
using Todo.Core.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Todo.Core.Pages.Tasks
{
	public class TodayModel : BasePageModel
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
			UserListCount = await context.TodoLists.GetListCount(UserId);
			TodaysTasks = await context.TodoTasks.GetDueToday(UserId);
		}
	}
}
