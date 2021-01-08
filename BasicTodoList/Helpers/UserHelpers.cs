using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Helpers
{
	public static class UserHelpers
	{
		//Returns the Id of the signed in user
		public static string GetUserId(this IPrincipal principal)
		{
			var claimsIdentity = (ClaimsIdentity)principal.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			return claim.Value;
		}
	}
}
