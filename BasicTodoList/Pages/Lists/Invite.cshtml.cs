using System;
using System.Linq;
using System.Threading.Tasks;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BasicTodoList.Pages.Lists
{
	public class InviteModel : PageModel
	{
		[BindProperty]
		public string Email { get; set; }
		public Guid Id { get; set; }

		private readonly ApplicationDbContext _context;

		public InviteModel(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult OnGet(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Id = (Guid)id;
			return Page();
		}
		public async Task<IActionResult> OnPostAsync(Guid id)
		{
			Id = id;

			if (!ModelState.IsValid)
			{
				return Page();
			}
			var user = _context.Users.FirstOrDefault(user => user.Email.ToLower() == Email.ToLower());
			if (!_context.Users.Any(user => user.Email.ToLower() == Email.ToLower()))
			{
				ModelState.AddModelError("Email", "Email doesn't exist");
				return Page();
			}
			if (_context.TodoListUser.Any(tlu => tlu.ApplicationUserId == user.Id && tlu.TodoListId == id))
			{
				ModelState.AddModelError("Email", "User has already been added to list");
				return Page();
			}

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
