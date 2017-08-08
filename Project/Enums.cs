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
    public enum State
    {
        [XmlEnum(Name = "0")]
        Off,
        [XmlEnum(Name = "1")]
        On
    }

    public enum Mode
    {
        [XmlEnum(Name = "manuell")]
        Manuel,
        [XmlEnum(Name = "auto")]
        Auto
    }

    public enum Lock
    {
        [XmlEnum(Name = "0")]
        Unlocked,
        [XmlEnum(Name = "1")]
        Locked
    }

    public enum Battery
    {
        [XmlEnum(Name = "0")]
        Ok,
        [XmlEnum(Name = "1")]
        Low
    }
}
