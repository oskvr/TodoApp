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

namespace BasicTodoList.Components
{
	public class CollaboratedTodoListsViewComponent : ViewComponent
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly ITodoListService todoListService;

		public CollaboratedTodoListsViewComponent(ITodoListService todoListService, UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
			this.todoListService = todoListService;
		}
		public IList<TodoList> TodoLists { get; set; }
		public async Task<IViewComponentResult> InvokeAsync()
		{
			string userId = userManager.GetUserId((ClaimsPrincipal)User);
			TodoLists = await todoListService.GetAll(userId, Role.Collaborator);
			//TodoLists = TodoLists.OrderByDescending(list => list.Tasks.Count).ToList();
			return View(TodoLists);
		}
	}
}
