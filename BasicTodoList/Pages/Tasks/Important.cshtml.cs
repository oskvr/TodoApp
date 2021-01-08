using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Helpers;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BasicTodoList.Pages.Tasks
{
	public class ImportantModel : PageModel
    {
		public IList<TodoTask> ImportantTasks { get; set; }

		private readonly ApplicationDbContext context;

		public ImportantModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
		{
			ImportantTasks = await context.TodoTasks
					.Include(task => task.TodoList)
					.ThenInclude(list => list.TodoListUsers)
					.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == User.GetUserId()))
					.Where(task => task.IsImportant && !task.IsCompleted)
					.OrderBy(task => task.DueAt).ToListAsync();
		}
	}
}
