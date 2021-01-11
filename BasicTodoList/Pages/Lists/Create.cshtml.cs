using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Authorization;
using BasicTodoList.Helpers;
using BasicTodoList.Pages.Shared;

namespace BasicTodoList.Pages.Lists
{
    public class CreateModel : BasePageModel
    {
        private readonly ApplicationDbContext _context;

		public CreateModel(ApplicationDbContext context)
        {
            _context = context;
		}

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TodoList TodoList { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TodoList.Id = Guid.NewGuid();
            _context.TodoLists.Add(TodoList);
            _context.TodoListUser.Add(new TodoListUser
            {
                ApplicationUserId = UserId,
				TodoListId = TodoList.Id,
				Role = Role.Creator
            });
            await _context.SaveChangesAsync();

            return RedirectToPage("/Tasks/Index", new {id=TodoList.Id });
        }
    }
}
