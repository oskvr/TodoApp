using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Core.Data;
using Todo.Core.Helpers;
using Todo.Core.Models;
using Todo.Core.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Todo.Core.Pages.Tasks
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

				SearchResults = await context.TodoTasks.GetSearchResults(query, UserId);
			}
		}
	}
}
