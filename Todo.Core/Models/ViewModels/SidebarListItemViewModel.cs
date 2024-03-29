﻿namespace Todo.Core.Models.ViewModels
{
	public class SidebarListItemViewModel
    {
		public string Name { get; set; }
		public int? Count { get; set; }
		/// <summary>
		/// Add the name of an SVG file found in /wwwroot/img/icons, e.g. "icon.svg"
		/// </summary>
		public string Icon { get; set; }
		public string AspPage { get; set; }
		public string RouteId { get; set; }
	}
}
