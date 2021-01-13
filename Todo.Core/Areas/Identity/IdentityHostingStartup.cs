using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BasicTodoList.Areas.Identity.IdentityHostingStartup))]
namespace BasicTodoList.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}