﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Todo.Core.Data;
using Todo.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Todo.Core.Helpers;
using Todo.Core.Pages.Shared;

namespace Todo.Core.Pages.Lists
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
