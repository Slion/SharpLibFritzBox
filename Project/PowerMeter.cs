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

namespace SharpLib.FritzBox.SmartHome
{
    /// <summary>
    /// Not sure why we need the data contract here since we use the XML serializer anyway.
    /// </summary>
    [DataContract(Namespace = "", Name ="powermeter"), XmlSerializerFormat, XmlRoot("powermeter")]
    public class PowerMeter
    {
        [DataMember, XmlElement(Namespace ="", ElementName = "power")]
        public string Power { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "energy")]
        public string Energy { get; set; }

    }

}
