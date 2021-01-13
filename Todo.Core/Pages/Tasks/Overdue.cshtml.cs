using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Helpers;
using BasicTodoList.Models;
using BasicTodoList.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicTodoList.Pages.Tasks
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
