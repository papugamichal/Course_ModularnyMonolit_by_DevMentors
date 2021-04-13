﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    public interface IModuleRegistry
    {
        IEnumerable<ModuleBradcastRegistration> GetModuleBradcastRegistrations(string key);
        void AddBroadcastAction(Type requestType, Func<object, Task> action);
    }
}
