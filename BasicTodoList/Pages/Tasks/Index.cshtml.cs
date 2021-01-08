using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BasicTodoList.Pages.Tasks
{
	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> userManager;

		public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			this.userManager = userManager;
		}
		public TodoList TodoList { get; set; }
		public IList<TodoTask> TodoTasks { get; set; }
		[BindProperty]
		public TodoTask TodoTask { get; set; }
		public async Task<IActionResult> OnGetAsync(Guid? id)
		{
			if (id is null)
			{
				return NotFound();
			}
			if (!ListExists(id))
			{
				return NotFound();
			}
			if (!UserHasPermissions((Guid)id))
			{
				return NotFound();
			}
			TodoList = await _context.TodoLists.Include(list => list.TodoListUsers).FirstOrDefaultAsync(List => List.Id == id);
			TodoTasks = await _context.TodoTasks.Where(task => task.TodoListId == id)
				.Include(t => t.TodoList).OrderBy(task => task.CreatedAt).ToListAsync();
			return Page();
		}

		private bool ListExists(Guid? id)
		{
			return _context.TodoLists.Any(list=>list.Id == id);
		}

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


		public async Task<IActionResult> OnPostUpdateCheckbox(Guid id, string checkbox)
		{
			var task = _context.TodoTasks.Find(id);
			if (task == null)
			{
				return NotFound();
			}
			_context.Attach(task).State = EntityState.Modified;

			switch (checkbox)
			{
				case "completed":
					task.IsCompleted = !task.IsCompleted;
					break;
				case "important":
					task.IsImportant = !task.IsImportant;
					break;
				default:
					return NotFound();
			}
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) when (!TodoTaskExists(TodoTask.Id))
			{
				return NotFound();
			}

			//return RedirectToPage("./Index", new { id = todoListId });
			return Redirect(Request.Headers["Referer"].ToString());
		}

		public async Task<IActionResult> OnPostDeleteAsync(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			TodoTask = await _context.TodoTasks.FindAsync(id);

			if (TodoTask != null)
			{
				_context.TodoTasks.Remove(TodoTask);
				await _context.SaveChangesAsync();
			}

			return Redirect(Request.Headers["Referer"].ToString());
		}


		private bool TodoTaskExists(Guid id)
		{
			return _context.TodoTasks.Any(task => task.Id == id);
		}

		private bool UserHasPermissions(Guid listId)
		{
			string userId = userManager.GetUserId(User);
			return _context.TodoListUser.Any(tlu => tlu.ApplicationUserId == userId && tlu.TodoListId == listId);
		}
	}
}
