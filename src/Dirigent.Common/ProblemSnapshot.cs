using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Dirigent.Common
{
    [DataContract]
    public class ProblemSnapshotRequest
    {
        /// <summary>
        /// A unique identifier of the request
        /// </summary>
        [DataMember]
        public string RequestUuid;

        /// <summary>
        /// What concrete stations we are interested in
        /// Empty = all stations
        /// </summary>
        [DataMember]
        public String MachineId;

        /// <summary>
        /// What concrete application are we intested in
        /// Empty = any application
        /// If both MachineId  and ApplicationId are empty, all apps as configured for the agents are processed
        /// </summary>
        [DataMember]
        public String AppId;

        /// <summary>
        /// What concrete snapshot type (as defined in the plan) we are intested in.
        /// Empty = any file type
        /// </summary>
        [DataMember]
        public String SnapshotName;

        /// <summary>
        /// UNC name of a shared folder to to upload the resulting zipped package into
        /// </summary>
        /// <remarks>
        /// The uploaded files must have names begining with <MachineId>.<AppId> to allow for easy sorting
        /// For example
        ///   IG_MX1.IG.zip
        ///   SIMHOST.SimHost_CrashDump.zip
        ///   SIMHOST.SimHost_Logs.zip 
        ///   etc.
        /// </remarks>
        [DataMember]
        public String SharedFolderUncPath;

        /// <summary>
        /// Empty if default credentials to be used
        /// </summary>
        [DataMember]
        public String SharedFolderUserName;

        /// <summary>
        /// Empty if default credentials to be used
        /// </summary>
        [DataMember]
        public String SharedFolderPassword;

        /// <summary>
        /// Whatever parameters specifying the type of the data we are interested in;
        /// Empty list = default
        /// </summary>
        [DataMember]
        public Dictionary<String, String> Options;

        public override string ToString()
        {
            return string.Format("ProblemSnapshotRequest ({0}.{1} \"{2}\")", MachineId, AppId, SnapshotName );
        }
    }


    public class ProblemSnapshotHandler
    {
        public void TakeProblemSnapshot( ProblemSnapshotRequest req )
        {
        }
    }
}
