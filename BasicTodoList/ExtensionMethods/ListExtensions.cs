using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BasicTodoList.Helpers
{
	// Various extension methods for retrieving lists for a specific user
	public static class ListExtensions
	{
		public static async Task<TodoList> GetById(this DbSet<TodoList> todoList, Guid? listId)
		{
			return await todoList.Include(list => list.Tasks).Include(list => list.TodoListUsers).FirstOrDefaultAsync(list => list.Id == listId);
		}
		public static async Task<IEnumerable<TodoList>> GetAll(this DbSet<TodoList> todoList, string userId, Role role)
		{
			return await todoList
			.Include(list => list.TodoListUsers)
			.Include(list => list.Tasks)
			.Where(list => list.TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId && tlu.Role == role))
			.ToListAsync();
		}
		public static async Task<IEnumerable<TodoList>> GetAll(this DbSet<TodoList> todoList, string userId)
		{
			return await todoList
			.Include(list => list.TodoListUsers)
			.Include(list => list.Tasks)
			.Where(list => list.TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId))
			.ToListAsync();
		}
		public static async Task<int> GetListCount(this DbSet<TodoList> todoList, string userId)
		{
			var lists = await todoList.GetAll(userId);
			return lists.Count();
		}
	}

}
