﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

using Dirigent.Common;

namespace Dirigent.Net
{
    /// <summary>
    /// Base class for all messages.
    /// </summary>
    [DataContract]
    [KnownType(typeof(RemoteOperationErrorMessage))]
    [KnownType(typeof(AppsStateMessage))]
    [KnownType(typeof(LaunchAppMessage))]
    [KnownType(typeof(KillAppMessage))]
    [KnownType(typeof(RestartAppMessage))]
    [KnownType(typeof(SelectPlanMessage))]
    [KnownType(typeof(StartPlanMessage))]
    [KnownType(typeof(StopPlanMessage))]
    [KnownType(typeof(KillPlanMessage))]
    [KnownType(typeof(RestartPlanMessage))]
    [KnownType(typeof(CurrentPlanMessage))]
    [KnownType(typeof(PlanRepoMessage))]
    public class Message
    {
        [DataMember]
        public string Sender { get; set; }
    }

    [DataContract]
    public class RemoteOperationErrorMessage : Message
    {
        [DataMember]
        public string Requestor;
        
        [DataMember]
        public string Message; // Error description 
        
        [DataMember]
        public Dictionary<string, string> Attributes; // additional attribute pairs (name, value)

        public RemoteOperationErrorMessage(string requestor, string msg, Dictionary<string, string> attribs = null)
        {
            this.Requestor = requestor;
            this.Message = msg;
            if( attribs != null )
            {
                this.Attributes = attribs;
            }
        }
    }

    [DataContract]
    public class AppsStateMessage : Message
    {
        [DataMember]
        public Dictionary<AppIdTuple, AppState> appsState;

        public AppsStateMessage( Dictionary<AppIdTuple, AppState> appsState )
        {
            this.appsState = new Dictionary<AppIdTuple, AppState>(appsState);
        }
    }

    [DataContract]
    public class LaunchAppMessage : Message
    {
        [DataMember]
        public AppIdTuple appIdTuple;

        public LaunchAppMessage( AppIdTuple appIdTuple )
        {
            this.appIdTuple = appIdTuple;
        }

        public override string ToString()
        {
            return string.Format("StartApp {0}", appIdTuple.ToString());
        }

    }

    [DataContract]
    public class KillAppMessage : Message
    {
        [DataMember]
        public AppIdTuple appIdTuple;

        public KillAppMessage( AppIdTuple appIdTuple )
        {
            this.appIdTuple = appIdTuple;
        }

        public override string ToString()
        {
            return string.Format("StopApp {0}", appIdTuple.ToString());
        }

    }

    [DataContract]
    public class RestartAppMessage : Message
    {
        [DataMember]
        public AppIdTuple appIdTuple;

        public RestartAppMessage( AppIdTuple appIdTuple )
        {
            this.appIdTuple = appIdTuple;
        }
        public override string ToString()
        {
            return string.Format("RestartApp {0}", appIdTuple.ToString());
        }

    }

    [DataContract]
    public class SelectPlanMessage : Message
    {
        [DataMember]
        public ILaunchPlan plan;

        public SelectPlanMessage( ILaunchPlan plan )
        {
            this.plan = plan;
        }

        public override string ToString()
        {
            return string.Format("SelectPlan {0}", plan.Name);
        }

    }

    [DataContract]
    public class StartPlanMessage : Message
    {
        public override string ToString() { return "StartPlan"; }
    }

     [DataContract]
    public class StopPlanMessage : Message
    {
        public override string ToString() { return "StopPlan"; }
    }

    [DataContract]
    public class KillPlanMessage : Message
    {
        public override string ToString() { return "KillPlan"; }
    }

    [DataContract]
    public class RestartPlanMessage : Message
    {
        public override string ToString() { return "RestartPlan"; }
    }

    /// <summary>
    /// Master tells new client about the current launch plan
    /// </summary>
    [DataContract]
    public class CurrentPlanMessage : Message
    {
        [DataMember]
        public ILaunchPlan plan;

        public CurrentPlanMessage(ILaunchPlan plan)
        {
            this.plan = plan;
        }

        public override string ToString()
        {
            return string.Format("CurrentPlan {0}", plan.Name);
        }
    }

    /// <summary>
    /// Master tells new client about existing plans
    /// </summary>
    [DataContract]
    public class PlanRepoMessage : Message
    {
        [DataMember]
        public IEnumerable<ILaunchPlan> repo;

        public PlanRepoMessage(IEnumerable<ILaunchPlan> repo)
        {
            this.repo = repo;
        }

        public override string ToString()
        {
            return string.Format("PlanRepo ({0} plans)", repo.Count());
        }
    }

}
