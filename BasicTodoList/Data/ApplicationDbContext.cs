using BasicTodoList.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasicTodoList.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<TodoList> TodoLists { get; set; }
		public DbSet<TodoTask> TodoTasks { get; set; }
		public DbSet<TodoListUser> TodoListUser { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<TodoListUser>()
				.HasKey(listUser => new { listUser.ApplicationUserId, listUser.TodoListId });
		}
	}
}
