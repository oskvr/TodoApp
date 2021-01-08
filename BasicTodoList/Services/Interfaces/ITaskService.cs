using BasicTodoList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicTodoList.Services
{
	public interface ITaskService
	{
		Task<List<TodoTask>> GetImportantTasksAsync(string userId);
		Task<List<TodoTask>> GetOverdueTasks(string userId);
		Task<List<TodoTask>> GetPlannedTasksAsync(string userId);
		Task<List<TodoTask>> GetTodaysTasks(string userId);
	}
}