using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BasicTodoList.Pages.Tasks
{
    public class PlannedModel : PageModel
    {
		public IList<TodoTask> PlannedTasks { get; set; }

		private readonly ApplicationDbContext context;

		public PlannedModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
		{
			PlannedTasks = await context.TodoTasks.Where(task => task.DueAt != null && task.DueAt.Value.Date >= DateTime.Today && !task.IsCompleted).OrderBy(task=>task.DueAt).ToListAsync();
		}
	}
}
