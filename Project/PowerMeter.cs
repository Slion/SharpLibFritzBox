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
        public int Power { get; set; }

        public float PowerInWatt { get { return Power * 0.001f; } }

        [DataMember, XmlElement(Namespace = "", ElementName = "energy")]
        public int Energy { get; set; }

        public float EnergyInKiloWattPerHour { get { return Energy * 0.001f; } }

    }

}
