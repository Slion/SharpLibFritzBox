using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharpLib.FritzBox
{
    /// <summary>
    /// Not sure why we need the data contract here since we use the XML serializer anyway.
    /// </summary>
    [DataContract(Namespace = "", Name ="switch"), XmlSerializerFormat, XmlRoot("switch")]
    public class Switch
    {
        [DataMember, XmlElement(Namespace ="", ElementName = "state")]
        public string State { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "mode")]
        public string Mode { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "devicelock")]
        public string DeviceLock { get; set; }

    }

}
