﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Dirigent.Common.Commands
{
	public class DirigentControlCommand : ICommand
	{
		protected IDirigentControl ctrl;

		string name;

		public List<string> args;

		public IList<string> Args
		{
			get { return args; }
			set { args = new List<string>( value ); }
		}

		public event WriteResponseDeleg Response; // to be set externally by command class instance creator and to be called through WriteRespose from command handler


		public DirigentControlCommand( IDirigentControl ctrl )
		{
			this.name = this.GetType().Name;
			this.ctrl = ctrl;
		}

		public string Name { get { return name; } }

		public virtual void Execute()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			Response = null;
		}

		// txt = just the response body, i.e. no request id prefix and no LF at the end
		public void WriteResponse( string txt )
		{
			if( Response != null )
			{
				Response( txt );
			}
		}

	}


	public class StartPlan : DirigentControlCommand
	{
		public StartPlan( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			ctrl.StartPlan( Tools.FindPlanByName( ctrl.GetPlanRepo(), args[0] ).Name );
			WriteResponse( "ACK" );
		}
	}

	public class StopPlan : DirigentControlCommand
	{
		public StopPlan( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			ctrl.StopPlan( Tools.FindPlanByName( ctrl.GetPlanRepo(), args[0] ).Name );
			WriteResponse( "ACK" );
		}
	}

	public class KillPlan : DirigentControlCommand
	{
		public KillPlan( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			ctrl.KillPlan( Tools.FindPlanByName( ctrl.GetPlanRepo(), args[0] ).Name );
			WriteResponse( "ACK" );
		}
	}

	public class RestartPlan : DirigentControlCommand
	{
		public RestartPlan( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			ctrl.RestartPlan( Tools.FindPlanByName( ctrl.GetPlanRepo(), args[0] ).Name );
			WriteResponse( "ACK" );
		}
	}


	public class LaunchApp : DirigentControlCommand
	{
		public LaunchApp( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			if( args.Count == 0 ) throw new MissingArgumentException( "appIdTuple", "AppIdTuple expected." );
			var t = new AppIdTuple( args[0] );
			if( t.AppId == "" ) throw new ArgumentSyntaxErrorException( "appIdTuple", args[0], "\"machineId.appId\" expected" );
			ctrl.LaunchApp( t );
			WriteResponse( "ACK" );
		}
	}

	public class KillApp : DirigentControlCommand
	{
		public KillApp( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			if( args.Count == 0 ) throw new MissingArgumentException( "appIdTuple", "AppIdTuple expected." );
			var t = new AppIdTuple( args[0] );
			if( t.AppId == "" ) throw new ArgumentSyntaxErrorException( "appIdTuple", args[0], "\"machineId.appId\" expected" );
			ctrl.KillApp( t );
			WriteResponse( "ACK" );
		}
	}

	public class RestartApp : DirigentControlCommand
	{
		public RestartApp( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			if( args.Count == 0 ) throw new MissingArgumentException( "appIdTuple", "AppIdTuple expected." );
			var t = new AppIdTuple( args[0] );
			if( t.AppId == "" ) throw new ArgumentSyntaxErrorException( "appIdTuple", args[0], "\"machineId.appId\" expected" );
			ctrl.RestartApp( t );
			WriteResponse( "ACK" );
		}
	}

	//public class SelectPlan : DirigentControlCommand
	//{
	//    public SelectPlan(IDirigentControl ctrl)
	//        : base(ctrl)
	//    {
	//    }

	//    public override void Execute(IList<string> args)
	//    {
	//        if (args.Count == 0) throw new MissingArgumentException("planName", "plan name expected.");

	//        // find plan in the repository
	//        ILaunchPlan plan = Tools.FindPlanByName( ctrl.GetPlanRepo(), args[0]) ;

	//        ctrl.SelectPlan(plan);
	//    }
	//}

	public class GetPlanState : DirigentControlCommand
	{
		public GetPlanState( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			if( args.Count == 0 ) throw new MissingArgumentException( "args[0]", "Plan name expected." );
			var planName = args[0];
			var planState = ctrl.GetPlanState( planName );
			var stateStr = Tools.GetPlanStateString( planName, planState );
			WriteResponse( stateStr );
		}
	}

	public class GetAppState : DirigentControlCommand
	{
		public GetAppState( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			if( args.Count == 0 ) throw new MissingArgumentException( "appIdTuple", "AppIdTuple expected." );
			var t = new AppIdTuple( args[0] );
			if( t.AppId == "" ) throw new ArgumentSyntaxErrorException( "appIdTuple", args[0], "\"machineId.appId\" expected" );

			var appState = ctrl.GetAppState( t );
			var stateStr = Tools.GetAppStateString( t, appState );

			WriteResponse( stateStr );
		}
	}


	public class GetAllPlansState : DirigentControlCommand
	{
		public GetAllPlansState( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{
			foreach( var p in ctrl.GetPlanRepo() )
			{
				var planState = ctrl.GetPlanState( p.Name );
				var stateStr = Tools.GetPlanStateString( p.Name, planState );
				WriteResponse( stateStr );
			}
			WriteResponse( "END" );
		}
	}


	public class GetAllAppsState : DirigentControlCommand
	{
		public GetAllAppsState( IDirigentControl ctrl )
			: base( ctrl )
		{
		}

		public override void Execute()
		{

			foreach( var pair in ctrl.GetAllAppsState() )
			{
				var stateStr = Tools.GetAppStateString( pair.Key, pair.Value );
				WriteResponse( stateStr );
			}
			WriteResponse( "END" );
		}
	}
}
