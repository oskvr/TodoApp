using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Models
{
	public enum Role
	{
		Creator,
		Collaborator
	}
    public class TodoListUser
    {
		public Guid TodoListId { get; set; }
		public TodoList TodoList { get; set; }
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public Role Role { get; set; }
	}
}
