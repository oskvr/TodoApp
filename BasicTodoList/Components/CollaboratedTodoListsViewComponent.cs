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
	public class CollaboratedTodoListsViewComponent : ViewComponent
	{
		private readonly ITodoListService todoListService;

		public CollaboratedTodoListsViewComponent(ITodoListService todoListService)
		{
			this.todoListService = todoListService;
		}
		public IList<TodoList> TodoLists { get; set; }
		public async Task<IViewComponentResult> InvokeAsync()
		{
			TodoLists = await todoListService.GetAll(User.GetUserId(), Role.Collaborator);
			//TodoLists = TodoLists.OrderByDescending(list => list.Tasks.Count).ToList();
			return View(TodoLists);
		}
	}
}
