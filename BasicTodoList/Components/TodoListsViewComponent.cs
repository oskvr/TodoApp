using BasicTodoList.Models;
using BasicTodoList.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Helpers;

namespace BasicTodoList.Components
{
	public class TodoListsViewComponent : ViewComponent
	{
		private readonly TodoListService todoListService;

		public TodoListsViewComponent(TodoListService todoListService)
		{
			this.todoListService = todoListService;
		}
		public IList<TodoList> TodoLists { get; set; }
		public async Task<IViewComponentResult> InvokeAsync()
		{
			TodoLists = await todoListService.GetAll(User.GetUserId(), Role.Creator);
			TodoLists = TodoLists.OrderBy(list => list.CreatedAt).ToList();
			return View(TodoLists);
		}
	}
}
