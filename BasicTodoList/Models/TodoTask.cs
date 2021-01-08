using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public bool IsOverdue => DueAt != null && DueAt.Value.Date < DateTime.Today;
	}
}
