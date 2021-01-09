using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Helpers;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BasicTodoList.Pages.Tasks
{
	public class SearchModel : PageModel
	{
		public string SearchString { get; set; }
		public List<TodoTask> SearchResults { get; set; } = new List<TodoTask>();

		private readonly ApplicationDbContext context;

		public SearchModel(ApplicationDbContext context)
		{
			this.context = context;
		}
		public async Task OnGet(string query)
		{
			if (!string.IsNullOrEmpty(query))
			{


				SearchString = query;
				query = query.ToLower();
				var UsersTasks = await context.TodoTasks
					.Include(task => task.TodoList)
					.ThenInclude(list => list.TodoListUsers)
					.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == User.GetUserId())).ToListAsync();

				SearchResults = UsersTasks.Where(task => task.Description.ToLower().Contains(query)).ToList();
			}
		}
	}
}
