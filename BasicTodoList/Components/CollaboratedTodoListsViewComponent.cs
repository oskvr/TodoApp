using BasicTodoList.Models;
using BasicTodoList.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BasicTodoList.Helpers;

namespace BasicTodoList.Components
{
	public class CollaboratedTodoListsViewComponent : ViewComponent
	{
		private readonly TodoListService todoListService;

		public CollaboratedTodoListsViewComponent(TodoListService todoListService)
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
