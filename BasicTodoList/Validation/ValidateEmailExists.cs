using BasicTodoList.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Validation
{
	// TODO: Ta bort innan inlämning, används bara som mall nu
	public class ValidateEmailExists : ValidationAttribute
	{
		private readonly ApplicationDbContext context;

		public ValidateEmailExists(ApplicationDbContext context)
		{
			this.context = context;
		}
		public override bool IsValid(object value)
		{
			return context.Users.Any(user => user.Email.ToLower() == value.ToString().ToLower());
		}
	}
}
