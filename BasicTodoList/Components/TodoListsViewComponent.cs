using BasicTodoList.Models;
using BasicTodoList.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using BasicTodoList.Helpers;

namespace BasicTodoList.Components
{
	public class TodoListsViewComponent : ViewComponent
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly ITodoListService todoListService;

		public TodoListsViewComponent(ITodoListService todoListService, UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
			this.todoListService = todoListService;
		}
		public IList<TodoList> TodoLists { get; set; }
		public async Task<IViewComponentResult> InvokeAsync()
		{
			string userId = userManager.GetUserId((ClaimsPrincipal)User);
			//TodoLists = await todoListService.GetAll(userId);
			TodoLists = await todoListService.GetAll(userId, Role.Creator);
			TodoLists = TodoLists.OrderBy(list => list.CreatedAt).ToList();
			return View(TodoLists);
		}
	}
}
