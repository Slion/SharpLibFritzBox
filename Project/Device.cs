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
    [DataContract(Namespace = "", Name ="device"), XmlSerializerFormat, XmlRoot("device")]
    public class Device
    {
        [DataMember, XmlAttribute(Namespace ="", AttributeName = "identifier")]
        public string Identifier { get; set; }

        [DataMember, XmlAttribute(Namespace = "", AttributeName = "id")]
        public string Id { get; set; }

        [DataMember, XmlAttribute(Namespace = "", AttributeName = "functionbitmask")]
        public uint FunctionBitmask { get; set; }

        [DataMember, XmlAttribute(Namespace = "", AttributeName = "fwversion")]
        public string FirmwareVersion { get; set; }

        [DataMember, XmlAttribute(Namespace = "", AttributeName = "manufacturer")]
        public string Manufacturer { get; set; }

        [DataMember, XmlAttribute(Namespace = "", AttributeName = "productname")]
        public string ProductName { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "present")]
        public string Present { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "name")]
        public string Name { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "switch")]
        public Switch Switch { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "powermeter")]
        public PowerMeter PowerMeter { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "temperature")]
        public Temperature Temperature { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "hkr")]
        public Radiator Radiator { get; set; }

        

    }

}
