using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BasicTodoList.Pages.Tasks
{
    public class ImportantModel : PageModel
    {
		public IList<TodoTask> ImportantTasks { get; set; }

		private readonly ApplicationDbContext context;
		private readonly UserManager<ApplicationUser> userManager;

		public ImportantModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}
		public async Task OnGet()
		{
			ImportantTasks = await context.TodoTasks
					.Include(task => task.TodoList)
					.ThenInclude(list => list.TodoListUsers)
					.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == userManager.GetUserId(User)))
					.Where(task => task.IsImportant && !task.IsCompleted)
					.OrderBy(task => task.DueAt).ToListAsync();
		}
	}
}
