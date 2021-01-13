using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Todo.Core.Areas.Identity.IdentityHostingStartup))]
namespace Todo.Core.Areas.Identity
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