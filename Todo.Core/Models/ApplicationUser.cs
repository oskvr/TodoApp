using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BasicTodoList.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ICollection<TodoListUser> TodoListUsers { get; set; }
	}
}
