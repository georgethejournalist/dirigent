using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Dirigent.Common
{
    /// <summary>
    /// File content to be transferred
    /// </summary>
    [DataContract]
    public struct FilePayload
    {
        [DataMember]
        public string FullPath; // name of the file; full path

        [DataMember]
        public List<string> Types; // types as defined for the concrete file in the plan

        [DataMember]
        public UInt64 Size; // types as defined for the concrete file in the plan

        [DataMember]
        public DateTime LastWriteTime; // types as defined for the concrete file in the plan

        [DataMember]
        public string UNCPath; // path to access the file from the asking computer (used instead of file content for large files)

        [DataMember]
        public byte[] Content; // data content of the file (usable just for small files up to several KBytes because of limited message size)
    };
}
