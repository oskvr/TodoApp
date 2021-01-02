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
    public class CreateWithSelectList_deprecatedModel : PageModel
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> userManager;

		public CreateWithSelectList_deprecatedModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
			this.userManager = userManager;
		}

        public IActionResult OnGet(Guid id)
        {
        ViewData["TodoListId"] = new SelectList(
            _context.TodoListUser
            .Include(t=>t.TodoList)
            .Where(t=>t.ApplicationUserId==userManager.GetUserId(User))
            .Select(t=>t.TodoList), nameof(TodoList.Id), nameof(TodoList.Name));
            return Page();
        }

        [BindProperty]
        public TodoTask TodoTask { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TodoTask.Id = Guid.NewGuid();
            _context.TodoTasks.Add(TodoTask);
            await _context.SaveChangesAsync();

            return RedirectToPage(nameof(Index), new { id = TodoTask.TodoListId });
        }
    }
}
