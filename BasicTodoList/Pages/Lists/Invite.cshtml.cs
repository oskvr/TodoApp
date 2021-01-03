using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicTodoList.Pages.Lists
{
    public class InviteModel : PageModel
    {
        [BindProperty]
		public string Email { get; set; }

		private readonly ApplicationDbContext _context;

		public InviteModel(ApplicationDbContext context)
        {
			_context = context;
		}
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = _context.Users.FirstOrDefault(user => user.Email.ToLower() == Email.ToLower());
            _context.TodoListUser.Add(new TodoListUser
            {
                ApplicationUserId = user.Id,
                TodoListId = id,
                Role = Role.Collaborator
            });
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tasks/Index", new { id });
        }
    }
}
