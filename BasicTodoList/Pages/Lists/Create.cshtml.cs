using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BasicTodoList.Pages.Lists
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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TodoList TodoList { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string userId = userManager.GetUserId(User);
            //var currentUser = _context.Users.FirstOrDefault(user => user.Id == userId);
            TodoList.Id = Guid.NewGuid();
            _context.TodoLists.Add(TodoList);
            _context.TodoListUser.Add(new TodoListUser
            {
                ApplicationUserId = userId,
				TodoListId = TodoList.Id,
				Role = Role.Creator
            });
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
