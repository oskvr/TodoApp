using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BasicTodoList.Pages.Tasks
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> userManager;

		public CreateModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
			this.userManager = userManager;
		}

		public void OnGet()
        {
        }
		[BindProperty]
        public TodoTask TodoTask { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TodoTask.Id = Guid.NewGuid();
            TodoTask.TodoListId = id;
            _context.TodoTasks.Add(TodoTask);
            await _context.SaveChangesAsync();

            return RedirectToPage(nameof(Index), new { id = TodoTask.TodoListId });
        }
    }
}
