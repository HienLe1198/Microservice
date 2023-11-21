using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Gateway.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            


            //print something here   

            Console.Read();
        }

        private static string GetJwt()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44336");
            client.DefaultRequestHeaders.Clear();
            var res2 = client.GetAsync("/api/auth?name=catcher&pwd=123").Result;
            dynamic jwt = JsonConvert.DeserializeObject(res2.Content.ReadAsStringAsync().Result);
            return jwt.access_token;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostingContext, config)=> {
                    config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
                })
            ;
    }
}
