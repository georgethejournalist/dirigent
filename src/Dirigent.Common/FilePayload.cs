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
        public string FileName; // name of the file

        [DataMember]
        public byte[] Content; // data content of the file
    };
}
