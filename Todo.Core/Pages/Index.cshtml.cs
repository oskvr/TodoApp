using Todo.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Todo.Core.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly SignInManager<ApplicationUser> signInManager;

		public IndexModel(ILogger<IndexModel> logger, SignInManager<ApplicationUser> signInManager)
		{
			_logger = logger;
			this.signInManager = signInManager;
		}

		public IActionResult OnGet()
		{
			if (signInManager.IsSignedIn(User))
			{
				return RedirectToPage("/Tasks/Today");
			}
			else
			{
				return Page();
			}
		}
	}
}
