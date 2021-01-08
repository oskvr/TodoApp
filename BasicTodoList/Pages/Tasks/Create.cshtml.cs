using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Authorization;

namespace BasicTodoList.Pages.Tasks
{
	[Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

		public CreateModel(ApplicationDbContext context)
        {
            _context = context;
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
