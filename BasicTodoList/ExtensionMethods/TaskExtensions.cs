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
	// Various extension methods for retrieving tasks for a specific user
	public static class TaskExtensions
	{
		public static async Task<IEnumerable<TodoTask>> GetAll(this DbSet<TodoTask> tasks, string userId)
		{
			return await tasks
				.Include(task => task.TodoList)
				.ThenInclude(list => list.TodoListUsers)
				.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId)).ToListAsync();
		}
		public static async Task<IEnumerable<TodoTask>> GetImportant(this DbSet<TodoTask> tasks, string userId)
		{
			var usersTasks = await tasks.GetAll(userId);
			return usersTasks
				.Where(task => task.IsImportant && !task.IsCompleted)
				.OrderBy(task => task.DueAt == null);
		}
		public static async Task<IEnumerable<TodoTask>> GetOverdue(this DbSet<TodoTask> tasks, string userId)
		{
			var usersTasks = await tasks.GetAll(userId);
			return usersTasks.Where(task => task.IsOverdue && !task.IsCompleted);
		}
		public static async Task<IEnumerable<TodoTask>> GetPlanned(this DbSet<TodoTask> tasks, string userId)
		{
			var usersTasks = await tasks.GetAll(userId);
			return usersTasks.Where(task => task.IsPlanned && !task.IsCompleted).OrderBy(task => task.DueAt);
		}
		public static async Task<IEnumerable<TodoTask>> GetSearchResults(this DbSet<TodoTask> tasks, string query, string userId)
		{
			var usersTasks = await tasks.GetAll(userId);
			return usersTasks.Where(task => task.Description.ToLower().Contains(query));
		}
		public static async Task<IEnumerable<TodoTask>> GetDueToday(this DbSet<TodoTask> tasks, string userId)
		{
			var usersTasks = await tasks.GetAll(userId);
			return usersTasks.Where(task => task.IsDueToday && !task.IsCompleted);
		}

	}

}
