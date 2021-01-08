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
	public class TodayModel : PageModel
	{
		public IList<TodoTask> TodaysTasks { get; set; }
		public IList<TodoTask> UsersTasks { get; set; }
		public int UserListCount { get; set; }
		public int MyProperty { get; set; }

		private readonly ApplicationDbContext context;
		private readonly UserManager<ApplicationUser> userManager;

		public TodayModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}
		public async Task OnGet()
		{
			var userId = userManager.GetUserId(User);
			if (userId != null)
			{
				UserListCount = context.TodoListUser.Count(tlu => tlu.ApplicationUserId == userId);
				UsersTasks = await context.TodoTasks
					.Include(task => task.TodoList)
					.ThenInclude(list => list.TodoListUsers)
					.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId)).ToListAsync();
				TodaysTasks = UsersTasks
					.Where(task => task.DueAt != null
						&& task.DueAt.Value.Date == DateTime.Now.Date
						&& !task.IsCompleted)
					.ToList();
			}
		}
	}
}
