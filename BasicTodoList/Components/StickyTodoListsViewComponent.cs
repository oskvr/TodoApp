using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class StickyTodoListsViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}