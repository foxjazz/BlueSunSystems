using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlueSunSystems
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 //.UseSetting("https_port", "443")
                .UseUrls("http://*:80")
                .UseKestrel()
                .UseStartup<Startup>()
            .ConfigureKestrel((context, options) =>
            {
            options.Limits.MaxConcurrentConnections = 250;
            options.Limits.MaxConcurrentUpgradedConnections = 250;
            options.Limits.MaxRequestBodySize = 10 * 1024;
            options.Limits.MinRequestBodyDataRate =
                new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
            options.Limits.MinResponseDataRate =
                new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(10));
            options.Listen(IPAddress.Loopback, 5000);
            options.Listen(IPAddress.Any, 80);
                //options.Listen(IPAddress.Any, 443, listenOptions =>
                //{
                //    listenOptions.UseHttps("testCert.pfx", "testPassword");
                //});
                //options.Listen(IPAddress.Loopback, 443, listenOptions =>
                //    {
                //        listenOptions.UseHttps("testCert.pfx", "testPassword");
                //    });
            });
    }
}
