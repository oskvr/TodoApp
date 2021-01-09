using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
		
		// Computed properties
		public bool IsOverdue => DueAt != null && DueAt.Value.Date < DateTime.Today;

		public bool IsDueToday => DueAt != null && DueAt.Value.Date == DateTime.Now.Date;

		public bool IsPlanned => DueAt != null && DueAt.Value.Date >= DateTime.Today;
	}
}
