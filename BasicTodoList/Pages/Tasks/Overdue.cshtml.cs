using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Models;
using BasicTodoList.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BasicTodoList.Pages.Tasks
{
	public class OverdueModel : PageModel
	{
		private readonly UserManager<ApplicationUser> userManager;

		public OverdueModel(ITaskService taskService, UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
		}
		public async Task OnGet()
		{
		}
	}
}
