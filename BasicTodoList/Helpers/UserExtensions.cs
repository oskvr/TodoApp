using System.Security.Claims;
using System.Security.Principal;

namespace BasicTodoList.Helpers
{
	public static class UserExtensions
	{
		/// <summary>
		/// Returns the Id of the current signed-in user
		/// </summary>
		public static string GetUserId(this IPrincipal principal)
		{
			var claimsIdentity = (ClaimsIdentity)principal.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			return claim.Value;
		}
	}
}
