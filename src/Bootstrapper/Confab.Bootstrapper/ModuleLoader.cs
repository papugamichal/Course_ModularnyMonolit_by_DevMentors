using Confab.Shared.Abstraction.Modules;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Confab.Bootstrapper
{
    internal static class ModuleLoader 
    {
        internal static List<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            const string modulePart = "Confab.Modules.";
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToList();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            var disabledModules = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                {
                    continue;
                }

                var moduleName = file.Split(modulePart)[1].Split(".")[0];
                var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");

                if (!enabled)
                {
                    disabledModules.Add(file);
                }
            }

            foreach (var disableModule in disabledModules)
            {
                files.Remove(disableModule);
            }

            files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

            return assemblies;
        }

        internal static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
        {
            return assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .OrderBy(x => x.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>()
                .ToList()
                ;
        }
    }
}
