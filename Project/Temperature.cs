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
    [DataContract(Namespace = "", Name = "temperature"), XmlSerializerFormat, XmlRoot("temperature")]
    public class Temperature
    {
        [DataMember, XmlElement(Namespace = "", ElementName = "celsius")]
        public string Celcius { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "offset")]
        public string Offset { get; set; }

    }

}
