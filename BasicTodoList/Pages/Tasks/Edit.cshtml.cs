using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BasicTodoList.Data;
using BasicTodoList.Models;

namespace BasicTodoList.Pages.Tasks
{
	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public EditModel(ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public TodoTask TodoTask { get; set; }

		public async Task<IActionResult> OnGetAsync(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			TodoTask = await _context.TodoTasks
				.Include(t => t.TodoList).FirstOrDefaultAsync(m => m.Id == id);

			if (TodoTask == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(TodoTask).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) when (!TodoTaskExists(TodoTask.Id))
			{
				return NotFound();
			}

			return RedirectToPage("./Index", new { id = TodoTask.TodoListId });
		}

		private bool TodoTaskExists(Guid id)
		{
			return _context.TodoTasks.Any(e => e.Id == id);
		}
	}
}
