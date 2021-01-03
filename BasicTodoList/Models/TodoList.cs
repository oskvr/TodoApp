using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Models
{
    public class TodoList
    {
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public ICollection<TodoTask> Tasks { get; set; }
		public ICollection<TodoListUser> TodoListUsers { get; set; }

		// Computed properties
		public int? TaskCount => Tasks?.Count;
		public int? IncompleteCount => Tasks?.Count(task => !task.IsCompleted);
		//public IEnumerable<TodoTask> TodaysTasks => Tasks.Where(task => task.DueAt == DateTime.Now || task.DueAt == null);
	}
}
