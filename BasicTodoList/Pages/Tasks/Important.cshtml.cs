using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Helpers;
using BasicTodoList.Models;
using BasicTodoList.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicTodoList.Pages.Tasks
{
	public class ImportantModel : BasePageModel
    {
		public IEnumerable<TodoTask> ImportantTasks { get; set; }

		private readonly ApplicationDbContext context;

		public ImportantModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
		{
			ImportantTasks = await context.TodoTasks.GetImportant(User.GetUserId());
		}
	}
}
