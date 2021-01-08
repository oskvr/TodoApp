﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BasicTodoList.Models
{
	public class TodoList : BaseEntity
    {
		[Required]
		public string Name { get; set; }
		public ICollection<TodoTask> Tasks { get; set; }
		public ICollection<TodoListUser> TodoListUsers { get; set; }

		// Computed properties
		public int? TaskCount => Tasks?.Count;
		public int? IncompleteCount => Tasks?.Count(task => !task.IsCompleted);
		//public IEnumerable<TodoTask> TodaysTasks => Tasks.Where(task => task.DueAt == DateTime.Now || task.DueAt == null);
	}
}
