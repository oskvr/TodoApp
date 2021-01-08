using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BasicTodoList.Services;
using BasicTodoList.Helpers;

namespace BasicTodoList.Pages.Lists
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ITodoListService todoListService;

		public IndexModel(ITodoListService todoListService)
		{
			this.todoListService = todoListService;
		}
		public IList<TodoList> TodoLists { get; set; }
		public IList<TodoList> CollaboratedLists { get; set; }
		public async Task OnGetAsync()
		{
			TodoLists = await todoListService.GetAll(User.GetUserId(), Role.Creator);
			TodoLists = TodoLists.OrderByDescending(list=>list.Tasks.Count).ToList();
			CollaboratedLists = await todoListService.GetAll(User.GetUserId(), Role.Collaborator);
		}
	}
}
