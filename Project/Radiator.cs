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
    [DataContract(Namespace = "", Name = "hkr"), XmlSerializerFormat, XmlRoot("hkr")]
    public class Radiator
    {
        [DataMember, XmlElement(Namespace = "", ElementName = "tist")]
        public string CurrentTemperature { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "tsoll")]
        public string TargetTemperature { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "absenk")]
        public string LowestTemperature { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "komfort")]
        public string ComfortTemperature { get; set; }

        /// <summary>
        /// Locked from UI/API
        /// </summary>
        [DataMember, XmlElement(Namespace = "", ElementName = "lock")]
        public Lock Lock { get; set; }

        /// <summary>
        /// Lock from device.
        /// </summary>
        [DataMember, XmlElement(Namespace = "", ElementName = "devicelock")]
        public Lock DeviceLock { get; set; }

        public enum Error
        {
            [XmlEnum(Name = "0")]
            None,
            [XmlEnum(Name = "1")]
            BadAdaptation, // Check mounting
            [XmlEnum(Name = "2")]
            WeakBatteries, // Or valve too short, check mounting
            [XmlEnum(Name = "3")]
            ValveStuck, //
            [XmlEnum(Name = "4")]
            Preparation,
            [XmlEnum(Name = "5")]
            Installation,
            [XmlEnum(Name = "6")]
            Adaptation
        }

        [DataMember, XmlElement(Namespace = "", ElementName = "errorcode")]
        public Error ErrorCode { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "batterylow")]
        public Battery Battery { get; set; }

        [DataMember, XmlElement(Namespace = "", ElementName = "nextchange")]
        public NextChange NextChange { get; set; }


    }

}
