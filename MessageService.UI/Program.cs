using MessageService.UI.Repos.IRepos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageService.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host= CreateHostBuilder(args).Build();
            using(var scope = host.Services.CreateScope())
            {
                var serviceprovider = scope.ServiceProvider;
                var userService = serviceprovider.GetRequiredService<IUserRepo>();
                var userlist = userService.GetList().Result;
                if (userlist.Count == 0)
                {
                    userService.Create(new Models.User { Name = "Muzaffer", Pass = "as", Mail = "as@as.com" });
                    userService.Create(new Models.User { Name = "Elif", Pass = "asd", Mail = "asd@asd.com" });
                    userService.Create(new Models.User { Name = "Elon", Pass = "asdf", Mail = "asdf@asdf.com" });
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
