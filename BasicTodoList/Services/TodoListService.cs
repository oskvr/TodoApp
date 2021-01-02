using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Services
{
	public class TodoListService : ITodoListService
	{
		private readonly ApplicationDbContext _context;

		public TodoListService(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task<List<TodoList>> GetAll(string userId, Role role)
		{
			return await _context.TodoListUser
			.Include(t => t.TodoList)
			.ThenInclude(t => t.Tasks)
			.Where(t => t.ApplicationUserId == userId && t.Role == role)
			.Select(t => t.TodoList)
			.ToListAsync();
		}
		public async Task<List<TodoList>> GetAll()
		{
			return await _context.TodoListUser
			.Include(t => t.TodoList)
			.ThenInclude(t => t.Tasks)
			.Select(t => t.TodoList)
			.ToListAsync();
		}

		public Task<List<TodoList>> GetById(string Id)
		{
			throw new NotImplementedException();
		}
	}
}
