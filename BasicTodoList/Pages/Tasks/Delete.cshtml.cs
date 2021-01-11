using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BasicTodoList.Models;
using BasicTodoList.Pages.Shared;

namespace BasicTodoList.Pages.Tasks
{
	public class DeleteModel : BasePageModel
    {
        private readonly BasicTodoList.Data.ApplicationDbContext _context;

        public DeleteModel(BasicTodoList.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(Guid? id)
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

            return RedirectToPage("./Index", new {id=TodoTask.TodoListId });
        }
    }
}
