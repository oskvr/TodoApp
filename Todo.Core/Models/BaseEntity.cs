﻿using System;

namespace BasicTodoList.Models
{
	public class BaseEntity
    {
		public Guid Id { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
