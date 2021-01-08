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

		public OverdueModel()
		{
		}
		public async Task OnGet()
		{
		}
	}
}
