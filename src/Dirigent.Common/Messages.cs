using System;
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
    [KnownType(typeof(PlansStateMessage))]
    [KnownType(typeof(LaunchAppMessage))]
    [KnownType(typeof(KillAppMessage))]
    [KnownType(typeof(RestartAppMessage))]
    //[KnownType(typeof(SelectPlanMessage))]
    [KnownType(typeof(StartPlanMessage))]
    [KnownType(typeof(StopPlanMessage))]
    [KnownType(typeof(KillPlanMessage))]
    [KnownType(typeof(RestartPlanMessage))]
    [KnownType(typeof(CurrentPlanMessage))]
    [KnownType(typeof(PlanRepoMessage))]
    [KnownType(typeof(ProblemSnapshotRequest))]
    [KnownType(typeof(ProblemSnapshotResponse))]
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
    public class PlansStateMessage : Message
    {
        [DataMember]
        public Dictionary<string, PlanState> plansState;

        public PlansStateMessage( Dictionary<string, PlanState> plansState )
        {
            this.plansState = new Dictionary<string, PlanState>(plansState);
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

    //[DataContract]
    //public class SelectPlanMessage : Message
    //{
    //    [DataMember]
    //    public string planName;

    //    public SelectPlanMessage( String planName )
    //    {
    //        this.plan = plan
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("SelectPlan {0}", plan.Name);
    //    }

    //}

    [DataContract]
    public class StartPlanMessage : Message
    {
        [DataMember]
        public String planName;

        public StartPlanMessage( String planName )
        {
            this.planName = planName;
        }

		public override string ToString() { return string.Format("StartPlan {0}", planName); }
    }

     [DataContract]
    public class StopPlanMessage : Message
    {
        [DataMember]
        public string planName;

        public StopPlanMessage( String planName )
        {
            this.planName = planName;
        }

		public override string ToString() { return string.Format("StopPlan {0}", planName); }
    }

    [DataContract]
    public class KillPlanMessage : Message
    {
        [DataMember]
        public string planName;

        public KillPlanMessage( String planName )
        {
            this.planName = planName;
        }

		public override string ToString() { return string.Format("KillPlan {0}", planName); }
    }

    [DataContract]
    public class RestartPlanMessage : Message
    {
        [DataMember]
        public string planName;

        public RestartPlanMessage( String planName )
        {
            this.planName = planName;
        }

		public override string ToString() { return string.Format("RestartPlan {0}", planName); }
    }

	/// <summary>
	/// Master tells new client about the current launch plan
	/// </summary>
	[DataContract]
	public class CurrentPlanMessage : Message
	{
		[DataMember]
		public string planName;

		public CurrentPlanMessage(String planName)
		{
			this.planName = planName;
		}

		public override string ToString()
		{
			return string.Format("CurrentPlan {0}", planName);
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

    /// <summary>
    /// Master tells new client about existing plans
    /// </summary>
    [DataContract]
    public class ProblemSnapshotRequest : Message
    {

        /// <summary>
        /// A unique identifier of the request
        /// </summary>
        [DataMember]
        public string RequestUuid;

        /// <summary>
        /// What concrete staion are we interested in
        /// Empty = any station of given type
        /// If both Type and Id are empty, all stations in the system are processed
        /// </summary>
        [DataMember]
        public String MachineId;

        /// <summary>
        /// What application type are we intested in
        /// Empty = any application type
        /// </summary>
        [DataMember]
        public String ApplicationType;

        /// <summary>
        /// What concrete application are we intested in
        /// Empty = any application of given type
        /// If both Type and Id are empty, all apps as configured for the agents are processed
        /// </summary>
        [DataMember]
        public String ApplicationId;

        /// <summary>
        /// Whatever parameters specifying the type of the data we are interested in;
        /// Empty list = default
        /// </summary>
        [DataMember]
        public Dictionary<String, String> Options;

        public override string ToString()
        {
            return string.Format("ProblemSnapshotRequest (MachId {0} AppType {1} AppId {2})", MachineId, ApplicationType, ApplicationId);
        }
    }


    /// <summary>
    /// This is sent in response to ProblemSnapshotRequest
    /// </summary>
    [DataContract]
    public class ProblemSnapshotResponse : Message
    {

        /// <summary>
        /// what request this snapshot belongs to
        /// </summary>
        [DataMember]
        public string RequestUuid;

        [DataMember]
        public String MachineId;

        [DataMember]
        public String ApplicationType;

        [DataMember]
        public String ApplicationId;

        [DataMember]
        public List<FilePayload> Files;
    };

}
