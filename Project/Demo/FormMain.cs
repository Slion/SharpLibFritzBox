using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartHome = SharpLib.FritzBox.SmartHome;

namespace FritzBoxDemo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private async void iButtonLogin_Click(object sender, EventArgs e)
        {
            SmartHome.Client client = new SmartHome.Client();
            //FritzBox.SessionInfo info = await client.GetSessionInfoAsync();
            await client.AuthenticateAsync(iTextBoxLogin.Text, iTextBoxPassword.Text);
            iLabelSessionId.Text = "Session ID: " + client.SessionId;
            SmartHome.DeviceList deviceList = await client.GetDeviceListAsync();
            PopulateDevicesTree(deviceList);
        }
        
        /// <summary>
        /// Populate our tree view with our devices information.
        /// </summary>
        /// <param name="aDeviceList"></param>
        void PopulateDevicesTree(SmartHome.DeviceList aDeviceList)
        {
            iTreeViewDevices.Nodes.Clear();

            // For each device
            foreach (SmartHome.Device device in aDeviceList.Devices)
            {
                // Add a new node
                TreeNode deviceNode = iTreeViewDevices.Nodes.Add(device.Id, $"{device.Name} - {device.ProductName} by {device.Manufacturer}");
                deviceNode.Tag = device;

                // Check the functions of that device
                foreach (SmartHome.Device.Function f in Enum.GetValues(typeof(SmartHome.Device.Function)))
                {
                    if (device.Has(f))
                    {
                        // Add a new node for each supported function
                        TreeNode functionNode = deviceNode.Nodes.Add(f.ToString());
                        if (f == SmartHome.Device.Function.TemperatureSensor)
                        {
                            // Add temperature sensor data
                            functionNode.Nodes.Add($"{device.Temperature.Reading} °C");
                            functionNode.Nodes.Add($"Offset: {device.Temperature.OffsetReading} °C");
                        }
                        else if (f == SmartHome.Device.Function.PowerPlugSwitch)
                        {
                            functionNode.Nodes.Add($"Mode: {device.Switch.Mode.ToString()}");
                            functionNode.Nodes.Add($"Switched {device.Switch.State.ToString()}");
                            functionNode.Nodes.Add($"Lock: {device.Switch.Lock.ToString()}");
                            functionNode.Nodes.Add($"Device lock: {device.Switch.DeviceLock.ToString()}");
                        }
                        else if (f == SmartHome.Device.Function.RadiatorRegulator)
                        {
                            functionNode.Nodes.Add($"Battery {device.Radiator.Battery.ToString()}");
                            functionNode.Nodes.Add($"Lock: {device.Radiator.Lock.ToString()}");
                            functionNode.Nodes.Add($"Device lock: {device.Radiator.DeviceLock.ToString()}");
                        }
                    }
                }
            }
        }
    }
}
