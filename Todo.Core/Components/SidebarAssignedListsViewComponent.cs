using Todo.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Core.Helpers;
using Todo.Core.Data;

namespace Todo.Core.Components
{
	public class SidebarAssignedListsViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext context;

		public SidebarAssignedListsViewComponent(ApplicationDbContext context)
		{
			this.context = context;
		}
		public IEnumerable<TodoList> TodoLists { get; set; }
		public async Task<IViewComponentResult> InvokeAsync()
		{
			TodoLists = await context.TodoLists.GetAll(User.GetUserId(), Role.Collaborator);
			return View(TodoLists);
		}
	}
}
