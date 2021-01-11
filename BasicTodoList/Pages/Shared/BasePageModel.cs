using BasicTodoList.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Pages.Shared
{
    [Authorize]
    public class BasePageModel : PageModel
    {
		public string UserId
		{
			get { return User.GetUserId(); }
		}
	}
}
