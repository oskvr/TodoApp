using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Helpers;
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

		public TodayModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet()
		{
			UserListCount = context.TodoListUser.Count(tlu => tlu.ApplicationUserId == User.GetUserId());
			UsersTasks = await context.TodoTasks
				.Include(task => task.TodoList)
				.ThenInclude(list => list.TodoListUsers)
				.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == User.GetUserId())).ToListAsync();
			TodaysTasks = UsersTasks
				.Where(task => task.DueAt != null
					&& task.DueAt.Value.Date == DateTime.Now.Date
					&& !task.IsCompleted)
				.ToList();
		}
	}
}
