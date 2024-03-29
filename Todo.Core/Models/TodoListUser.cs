﻿using System;

namespace Todo.Core.Models
{
	public enum Role
	{
		Creator,
		Collaborator
	}
    public class TodoListUser
    {
		public Guid TodoListId { get; set; }
		public TodoList TodoList { get; set; }
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public Role Role { get; set; }

		// Computed properties
		public bool UserIsCreator(string userId) => ApplicationUserId == userId && Role == Role.Creator;
	}
}
