using System;
using System.ComponentModel.DataAnnotations;

namespace BasicTodoList.Models
{
	public class TodoTask : BaseEntity
	{
		[Required]
		public string Description { get; set; }
		public DateTime? DueAt { get; set; }
		public bool IsImportant { get; set; }
		public bool IsCompleted { get; set; }
		public Guid TodoListId { get; set; }
		public TodoList TodoList { get; set; }

		//TODO: Den här ger ett invalid modelstate när man sätter due date före dagens datum
		public bool IsOverdue => DueAt != null && DueAt.Value.Date < DateTime.Today;
	}
}
