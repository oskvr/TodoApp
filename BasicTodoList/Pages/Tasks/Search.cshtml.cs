using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Helpers;
using BasicTodoList.Models;
using BasicTodoList.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicTodoList.Pages.Tasks
{
	public class SearchModel : BasePageModel
	{
		public string SearchString { get; set; }
		public IEnumerable<TodoTask> SearchResults { get; set; } = new List<TodoTask>();

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

				SearchResults = await context.TodoTasks.GetSearchResults(query, User.GetUserId());
			}
		}
	}
}
