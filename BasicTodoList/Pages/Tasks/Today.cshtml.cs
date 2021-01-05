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
    public class TodayModel : PageModel
    {
		public IList<TodoTask> TodaysTasks { get; set; }

		private readonly ApplicationDbContext context;

		public TodayModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
        {
			TodaysTasks = await context.TodoTasks.Where(task=> task.DueAt.Value.Date == DateTime.Now.Date && !task.IsCompleted ).ToListAsync();
        }
    }
}
