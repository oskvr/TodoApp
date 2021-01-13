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
	public class OverdueModel : BasePageModel
	{
		public IEnumerable<TodoTask> OverdueTasks { get; set; }

		private readonly ApplicationDbContext context;

		public OverdueModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
		{
			OverdueTasks = await context.TodoTasks.GetOverdue(UserId);
		}
	}
}
