using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ICollection<TodoListUser> TodoListUsers { get; set; }
	}
}
