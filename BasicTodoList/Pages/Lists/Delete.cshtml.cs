using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BasicTodoList.Data;
using BasicTodoList.Models;

namespace BasicTodoList.Pages.Lists
{
	public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TodoList TodoList { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoList = await _context.TodoLists.FirstOrDefaultAsync(m => m.Id == id);

            if (TodoList == null)
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

            TodoList = await _context.TodoLists.FindAsync(id);

            if (TodoList != null)
            {
                _context.TodoLists.Remove(TodoList);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Tasks/Today");
        }
    }
}
