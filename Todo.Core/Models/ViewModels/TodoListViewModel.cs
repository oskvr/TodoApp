using System.Collections.Generic;

namespace BasicTodoList.Models.ViewModels
{
	public class TodoListViewModel
    {
		public IEnumerable<TodoTask> TodoList { get; set; }
		public bool ShowLinksOnTasks { get; set; }
	}
}
