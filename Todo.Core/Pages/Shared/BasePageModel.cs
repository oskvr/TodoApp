using Todo.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Pages.Shared
{
    [Authorize]
    public class BasePageModel : PageModel
    {
		/// <summary>
		/// Id of the currently signed in user
		/// </summary>
		public string UserId
		{
			get { return User.GetUserId(); }
		}
	}
}
