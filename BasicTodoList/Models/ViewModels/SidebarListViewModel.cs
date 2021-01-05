using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTodoList.Models.ViewModels
{
    public class SidebarListViewModel
    {
		public string Name { get; set; }
		public int? IncompleteTasksCount { get; set; }
		public string Icon { get; set; }
	}
}
