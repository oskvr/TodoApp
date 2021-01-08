namespace BasicTodoList.Models.ViewModels
{
	public class SidebarListItemViewModel
    {
		public string Name { get; set; }
		public int? IncompleteTasksCount { get; set; }
		/// <summary>
		/// Add the name of the SVG file in /wwwroot/img/icons, e.g. "icon.svg"
		/// </summary>
		public string Icon { get; set; }
		public string AspPage { get; set; }
		public string RouteId { get; set; }
	}
}
