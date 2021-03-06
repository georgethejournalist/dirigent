﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Dirigent.Common;

namespace Dirigent.Agent.Core
{
    public interface ILauncher
    {
        void Launch();
        void Kill();
        bool Running { get; }
        int ExitCode { get; }
        int ProcessId { get; }
    }

    public interface ILauncherFactory
    {
        ILauncher createLauncher( AppDef appDef, string rootForRelativePaths );
    }
}
