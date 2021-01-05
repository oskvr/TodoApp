using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Models.ViewModels
{
    public class TasksHeaderViewModel
    {
		public string Title { get; set; }
		public string Creator { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
