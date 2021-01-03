using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Models.ViewModels
{
    public class AddTaskViewModel
    {
		public string Description { get; set; }
		public DateTime? DueAt { get; set; }
		public bool IsImportant { get; set; }
		public Guid TodoListId { get; set; }
	}
}
