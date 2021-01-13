using System.Collections.Generic;

namespace Todo.Core.Models.ViewModels
{
	public class TodoListViewModel
    {
		public IEnumerable<TodoTask> TodoList { get; set; }
		public bool ShowLinksOnTasks { get; set; }
	}
}
