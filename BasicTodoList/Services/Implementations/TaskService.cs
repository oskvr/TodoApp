using BasicTodoList.Data;
using BasicTodoList.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace BasicTodoList.Services
{
	public class TaskService : ITaskService
	{
		private readonly ApplicationDbContext _context;
		public TaskService(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<TodoTask>> GetTodaysTasks(string userId)
		{
			return await _context.TodoTasks
					.Include(task => task.TodoList)
					.ThenInclude(list => list.TodoListUsers)
					.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId))
					.Where(task => task.DueAt != null
					&& task.DueAt.Value.Date == DateTime.Now.Date
					&& !task.IsCompleted).ToListAsync();
		}
		public async Task<List<TodoTask>> GetImportantTasksAsync(string userId)
		{
			return await _context.TodoTasks
					.Include(task => task.TodoList)
					.ThenInclude(list => list.TodoListUsers)
					.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId))
					.Where(task => task.IsImportant && !task.IsCompleted)
					.OrderBy(task => task.DueAt).ToListAsync();
		}

		public async Task<List<TodoTask>> GetPlannedTasksAsync(string userId)
		{
			return await _context.TodoTasks
					.Include(task => task.TodoList)
					.ThenInclude(list => list.TodoListUsers)
					.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId))
					.Where(task => task.DueAt != null && task.DueAt.Value.Date >= DateTime.Today && !task.IsCompleted)
					.OrderBy(task => task.DueAt)
					.ToListAsync();
		}

		public async Task<List<TodoTask>> GetOverdueTasks(string userId)
		{
			return await _context.TodoTasks
					.Include(task => task.TodoList)
					.ThenInclude(list => list.TodoListUsers)
					.Where(task => task.TodoList.TodoListUsers.Any(tlu => tlu.ApplicationUserId == userId))
					.Where(task => task.DueAt != null && task.DueAt.Value.Date >= DateTime.Today && !task.IsCompleted)
					.OrderBy(task => task.DueAt)
					.ToListAsync();
		}
	}
}
