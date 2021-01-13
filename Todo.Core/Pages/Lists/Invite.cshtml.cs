using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Todo.Core.Data;
using Todo.Core.Models;
using Todo.Core.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Todo.Core.Pages.Lists
{
	public class InviteModel : BasePageModel
	{
		[BindProperty]
		[Required]
		public string Email { get; set; }
		public Guid Id { get; set; }

		private readonly ApplicationDbContext _context;

		public InviteModel(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult OnGet(Guid? id)
		{
			if (id == null || !ListExists(id))
			{
				return NotFound();
			}
			var todoList = _context.TodoLists.Include(list=>list.TodoListUsers).FirstOrDefault(list=>list.Id == id);
			if (!todoList.IsUserCreator(UserId))
			{
				return new ForbidResult();
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
			if (!EmailExists())
			{
				ModelState.AddModelError("Email", "Email doesn't exist");
				return Page();
			}
			if (TodoListUserExists(id, user))
			{
				ModelState.AddModelError("Email", "User has already been added to the list");
				return Page();
			}

			_context.TodoListUser.Add(new TodoListUser
			{
				ApplicationUserId = user.Id,
				TodoListId = id,
				Role = Role.Collaborator
			});
			await _context.SaveChangesAsync();
			// A message is created to be displayed on /Tasks/Index
			TempData["ListInviteMessage"] = $"{Email} was successfully invited to the list";
			return RedirectToPage("/Tasks/Index", new { id });
		}

		private bool TodoListUserExists(Guid listId, ApplicationUser user)
		{
			return _context.TodoListUser.Any(tlu => tlu.ApplicationUserId == user.Id && tlu.TodoListId == listId);
		}

		private bool EmailExists()
		{
			return _context.Users.Any(user => user.Email.ToLower() == Email.ToLower());
		}

		private bool ListExists(Guid? id)
		{
			return _context.TodoLists.Any(list => list.Id == id);
		}
	}
}
