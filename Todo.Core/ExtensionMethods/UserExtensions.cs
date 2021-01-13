using Todo.Core.Data;
using Todo.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Todo.Core.Helpers
{
	public static class UserExtensions
	{
		/// <summary>
		/// Returns the Id of the currently signed-in user
		/// </summary>
		public static string GetUserId(this IPrincipal principal)
		{
			var claimsIdentity = (ClaimsIdentity)principal.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			return claim.Value;
		}
	}

}
