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
    [DataContract(Namespace = "", Name ="device"), XmlSerializerFormat, XmlRoot("device")]
    public class Device
    {
        #region XML Attributes
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
        #endregion

        #region XML Elements
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
        public Thermostat Thermostat { get; set; }
        #endregion

        public bool Has(Function aFunction) { return ((Function)FunctionBitmask).HasFlag(aFunction); }
        public bool IsSwitchSocket { get { return Has(Function.SwitchSocket); } }
        public bool IsRadiatorThermostat { get { return Has(Function.RadiatorThermostat); } }

        #region Switch Socket functionality
        //
        public async Task SwitchToggle()
        {
            AssertSwitchSocket();
            await Client.SwitchToggle(Identifier);
        }

        //
        public async Task SwitchOn()
        {
            AssertSwitchSocket();
            await Client.SwitchOn(Identifier);
        }

        //
        public async Task SwitchOff()
        {
            AssertSwitchSocket();
            await Client.SwitchOff(Identifier);
        }
        #endregion

        #region Radiator Thermostat functionality
        //
        public async Task SetTargetTemperature(float aTemperatureInCelsius)
        {
            AssertRadiatorThermostat();
            await Client.SetTargetTemperature(Identifier, Thermostat.CelsiusToTemperatureCode(aTemperatureInCelsius));
        }

        public async Task SetTargetTemperatureCode(int aCode)
        {
            AssertRadiatorThermostat();
            await Client.SetTargetTemperature(Identifier, aCode);
        }

        public async Task RadiatorOn()
        {
            AssertRadiatorThermostat();
            await Client.SetTargetTemperature(Identifier, (int)Thermostat.Radiator.On);
        }

        public async Task RadiatorOff()
        {
            AssertRadiatorThermostat();
            await Client.SetTargetTemperature(Identifier, (int)Thermostat.Radiator.Off);
        }

        #endregion

        #region Internals
        private void AssertSwitchSocket()
        {
            if (!IsSwitchSocket)
            {
                throw new NotSupportedException("This device is not a switch socket!");
            }
        }

        private void AssertRadiatorThermostat()
        {
            if (!IsRadiatorThermostat)
            {
                throw new NotSupportedException("This device is not a radiator thermostat!");
            }
        }

        [XmlIgnore]
        public Client Client { get; set; }
        #endregion
    }

}