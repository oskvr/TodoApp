﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Authorization;
using BasicTodoList.Helpers;

namespace BasicTodoList.Pages.Tasks
{

	[Authorize]
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}
		public TodoList TodoList { get; set; }
		public IEnumerable<TodoTask> Tasks { get; set; }
		public bool UserIsListCreator { get; set; }
		[BindProperty]
		public TodoTask TodoTask { get; set; }
		public async Task<IActionResult> OnGetAsync(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			if (!HasPermission(id))
			{
				return NotFound();
			}
			TodoList = await _context.TodoLists.GetById(id);
			Tasks = TodoList.Tasks.OrderBy(t => t.CreatedAt).OrderBy(t => t.IsCompleted);
			UserIsListCreator = TodoList.IsUserCreator(User.GetUserId());

			return Page();
		}
		public async Task<IActionResult> OnPostAsync(Guid id)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToPage(nameof(Index), new { id = TodoTask.TodoListId });
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
			if (task != null)
			{
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
			}

			// Since checkboxes can be updated in multiple views we need to 
			// redirect the user back to where he came from using the referer URL
			return Redirect(Request.Headers["Referer"].ToString());
		}

		public async Task<IActionResult> OnPostDeleteTaskAsync(Guid? id)
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
		public async Task<IActionResult> OnPostDeleteListAsync(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			TodoList = await _context.TodoLists.GetById(id);
			if (!TodoList.IsUserCreator(User.GetUserId()))
			{
				return NotFound();
			}


			if (TodoList != null)
			{
				_context.TodoLists.Remove(TodoList);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("/Tasks/Today");
		}

		/// <summary>
		/// Removes the current user from an invited list
		/// </summary>
		public async Task<IActionResult> OnPostRemoveCollaborator(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var user = await _context.TodoListUser
				.FirstOrDefaultAsync(tlu => tlu.ApplicationUserId == User.GetUserId()
				&& tlu.TodoListId == id
				&& tlu.Role == Role.Collaborator);
			if (user != null)
			{
				_context.TodoListUser.Remove(user);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("/Tasks/Today");
		}

		private bool TodoTaskExists(Guid id)
		{
			return _context.TodoTasks.Any(task => task.Id == id);
		}

		private bool HasPermission(Guid? listId)
		{
			return _context.TodoListUser.Any(tlu => tlu.ApplicationUserId == User.GetUserId() && tlu.TodoListId == listId);
		}
	}
}
