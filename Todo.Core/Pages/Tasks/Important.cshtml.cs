using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Core.Data;
using Todo.Core.Helpers;
using Todo.Core.Models;
using Todo.Core.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Todo.Core.Pages.Tasks
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
			ImportantTasks = await context.TodoTasks.GetImportant(UserId);
		}
	}
}
