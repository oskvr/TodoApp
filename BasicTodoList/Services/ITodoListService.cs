using BasicTodoList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicTodoList.Services
{
	public interface ITodoListService
	{
		Task<List<TodoList>> GetById(string Id);
		Task<List<TodoList>> GetAll(string userId, Role role);
		Task<List<TodoList>> GetAll();
	}
}