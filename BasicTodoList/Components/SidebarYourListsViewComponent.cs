using BasicTodoList.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Helpers;
using BasicTodoList.Data;

namespace BasicTodoList.Components
{
	public class SidebarYourListsViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext context;

		public SidebarYourListsViewComponent(ApplicationDbContext context)
		{
			this.context = context;
		}
		public IEnumerable<TodoList> TodoLists { get; set; }
		public async Task<IViewComponentResult> InvokeAsync()
		{
			TodoLists = await context.TodoLists.GetAll(User.GetUserId(), Role.Creator);
			TodoLists = TodoLists.OrderBy(list => list.CreatedAt).ToList();
			return View(TodoLists);
		}
	}
}
