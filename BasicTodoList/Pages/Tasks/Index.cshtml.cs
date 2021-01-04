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
        public ApplicationUser Creator { get; set; }
        public IList<TodoTask> TodoTasks { get; set; }
        [BindProperty]
        public TodoTask TodoTask { get; set; }
        [BindProperty]
        public bool IsCompleted { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            if (!UserHasPermissions((Guid)id))
            {
                return NotFound("You don't have permissions to view this resource");
            }
            TodoList = await _context.TodoLists.Include(list => list.TodoListUsers).FirstOrDefaultAsync(List => List.Id == id);
            // Creator = TodoList.TodoListUsers.Where(tlu => tlu.Role == Role.Creator).Select(tlu => tlu.ApplicationUser).FirstOrDefault();
            TodoTasks = await _context.TodoTasks.Where(task => task.TodoListId == id)
                .Include(t => t.TodoList).ToListAsync();
            return Page();
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

        public async Task<IActionResult> OnPut(Guid id)
        {
            var task = _context.TodoTasks.Find(id);
            _context.Attach(task).State = EntityState.Modified;
            task.IsCompleted = IsCompleted;
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
            return _context.TodoTasks.Any(task => task.Id == id);
        }

        private bool UserHasPermissions(Guid listId)
        {
            string userId = userManager.GetUserId(User);
            return _context.TodoListUser.Any(tlu => tlu.ApplicationUserId == userId && tlu.TodoListId == listId);
        }
    }
}
