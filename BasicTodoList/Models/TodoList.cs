using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Models
{
    public class TodoList
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ICollection<TodoTask> Tasks { get; set; }
		public ICollection<TodoListUser> TodoListUsers { get; set; }
		public int TaskCount => Tasks.Count;
		public int CompletedCount => Tasks.Count(task => task.IsCompleted);
	}
}
