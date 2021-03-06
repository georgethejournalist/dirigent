﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Dirigent.Common;

namespace Dirigent.Agent.CmdLineCtrl
{
    public class MyCommandRepo : CommandRepository
    {
        IDirigentControl ctrl;

        public MyCommandRepo(IDirigentControl ctrl)
        {
            this.ctrl = ctrl;
            //var cmdClasses = Assembly.GetExecutingAssembly().GetTypes().Where(t =>  t.IsSubclassOf(typeof(Commands.DirigentControlCommand)));
            //foreach( cmdClass in cmdClasses )
            //{
            //    Register( new cmd ????
            //}

            Register(new Commands.StartPlan(ctrl));
            Register(new Commands.StopPlan(ctrl));
            Register(new Commands.KillPlan(ctrl));
            Register(new Commands.RestartPlan(ctrl));
            Register(new Commands.LaunchApp(ctrl));
            Register(new Commands.KillApp(ctrl));
            Register(new Commands.RestartApp(ctrl));
            //Register(new Commands.SelectPlan(ctrl));
        }

    }
}
