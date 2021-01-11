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
		public int UserListCount { get; set; }
		public int MyProperty { get; set; }

		private readonly ApplicationDbContext context;

		public OverdueModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
		{
			UserListCount = context.TodoListUser.Count(tlu => tlu.ApplicationUserId == User.GetUserId());
			OverdueTasks = await context.TodoTasks.GetOverdue(User.GetUserId());
		}
	}
}
