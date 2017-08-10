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
    [DataContract(Namespace = "", Name = "temperature"), XmlSerializerFormat, XmlRoot("temperature")]
    public class Temperature
    {
        [DataMember, XmlElement(Namespace = "", ElementName = "celsius")]
        public int Celsius { get; set; }

        public float Reading { get { return Celsius * 0.1f; }  }

        [DataMember, XmlElement(Namespace = "", ElementName = "offset")]
        public int Offset { get; set; }

        public float OffsetReading { get { return Offset * 0.1f; } }

    }

}
