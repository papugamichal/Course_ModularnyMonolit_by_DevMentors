﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("Confab.Bootstrapper")]
namespace Confab.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        internal static IHostBuilder ConfigureModules(
            this IHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach (var settings in GetSettings("*"))
                {
                    cfg.AddJsonFile(settings);
                }

                foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}"))
                {
                    cfg.AddJsonFile(settings);
                }

                IEnumerable<string> GetSettings(string pattern)
                    => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath,
                    $"module.{pattern}.json", SearchOption.AllDirectories);
            });
        }
    }
}
