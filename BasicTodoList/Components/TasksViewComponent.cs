using BasicTodoList.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BasicTodoList.Components
{
	public class TasksViewComponent : ViewComponent
    {
		public TodoTask TodoTask { get; set; }
		public async Task<IViewComponentResult> InvokeAsync(Guid id)
		{
			return View(new TodoTask {TodoListId = id });
		}
	}
}
