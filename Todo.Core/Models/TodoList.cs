using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Todo.Core.Models
{
	public class TodoList : BaseEntity
    {
		[Required]
		[MaxLength(100, ErrorMessage = "List name can't be more than 100 characters long")]
		public string Name { get; set; }
		public ICollection<TodoTask> Tasks { get; set; }
		public ICollection<TodoListUser> TodoListUsers { get; set; }

		// Computed properties
		public int? TaskCount => Tasks?.Count;
		public int? IncompleteCount => Tasks?.Count(task => !task.IsCompleted);
		public bool IsUserCreator(string userId) => TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId && tlu.Role == Role.Creator);
		public bool IsUserInList(string userId) => TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId);
	}
}
