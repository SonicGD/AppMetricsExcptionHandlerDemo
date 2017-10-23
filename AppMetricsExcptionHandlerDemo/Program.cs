using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AppMetricsExcptionHandlerDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureMetricsWithDefaults((whBuilder, metricsBuilder) =>
                {
                    metricsBuilder.Configuration.Configure(options =>
                    {
                        options.DefaultContextLabel = "Test";
                        options.GlobalTags["env"] = whBuilder.HostingEnvironment.IsStaging() ? "stage" :
                            whBuilder.HostingEnvironment.IsProduction() ? "prod" : "dev";
                    });
                })
                .UseMetrics()
                .UseStartup<Startup>()
                .Build();
    }
}