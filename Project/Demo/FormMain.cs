using Squirrel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        SmartHome.Client iClient;

        public FormMain()
        {
            InitializeComponent();
            iClient = new SmartHome.Client();
        }

        /// <summary>
        /// Check for application update and ask the user to proceed if any.
        /// </summary>
        async void SquirrelUpdate()
        {
            // Check for Squirrel application update
#if !DEBUG
            ReleaseEntry release = null;
            using (var mgr = new UpdateManager("http://publish.slions.net/FritzBoxDemo"))
            {
                //
                UpdateInfo updateInfo = await mgr.CheckForUpdate();
                if (updateInfo.ReleasesToApply.Any()) // Check if we have any update
                {
                    // We have an update ask our user if he wants it
                    System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    string msg =    "New version available!" +
                                    "\n\nCurrent version: " + updateInfo.CurrentlyInstalledVersion.Version +
                                    "\nNew version: " + updateInfo.FutureReleaseEntry.Version +
                                    "\n\nUpdate application now?";
                    DialogResult dialogResult = MessageBox.Show(msg, fvi.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // User wants it, do the update
                        release = await mgr.UpdateApp();
                    }
                    else
                    {
                        // User cancel an update enable manual update option
                        //iToolStripMenuItemUpdate.Visible = true;
                    }
                }
            }

            // Restart the app
            if (release!=null)
            {
                UpdateManager.RestartApp();
            }           
#endif
        }

        private async void iButtonLogin_Click(object sender, EventArgs e)
        {
            //FritzBox.SessionInfo info = await client.GetSessionInfoAsync();
            await iClient.AuthenticateAsync(iTextBoxLogin.Text, iTextBoxPassword.Text);
            iLabelSessionId.Text = "Session ID: " + iClient.SessionId;
            SmartHome.DeviceList deviceList = await iClient.GetDeviceListAsync();
            PopulateDevicesTree(deviceList);
            //await client.SetSwitchToggle("08761 0250071");
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
                foreach (SmartHome.Function f in Enum.GetValues(typeof(SmartHome.Function)))
                {
                    if (device.Has(f))
                    {
                        // Add a new node for each supported function
                        TreeNode functionNode = deviceNode.Nodes.Add(f.ToString());
                        if (f == SmartHome.Function.TemperatureSensor)
                        {
                            // Add temperature sensor data
                            functionNode.Nodes.Add($"{device.Temperature.Reading} °C");
                            functionNode.Nodes.Add($"Offset: {device.Temperature.OffsetReading} °C");
                        }
                        else if (f == SmartHome.Function.SwitchSocket)
                        {
                            // Add switch socket data
                            functionNode.Nodes.Add($"Mode: {device.Switch.Mode.ToString()}");
                            functionNode.Nodes.Add($"Switched {device.Switch.State.ToString()}");
                            functionNode.Nodes.Add($"Lock: {device.Switch.Lock.ToString()}");
                            functionNode.Nodes.Add($"Device lock: {device.Switch.DeviceLock.ToString()}");
                        }
                        else if (f == SmartHome.Function.RadiatorThermostat)
                        {
                            // Add radiator thermostat data
                            functionNode.Nodes.Add($"Battery {device.Radiator.Battery.ToString()}");
                            functionNode.Nodes.Add($"Lock: {device.Radiator.Lock.ToString()}");
                            functionNode.Nodes.Add($"Device lock: {device.Radiator.DeviceLock.ToString()}");
                        }
                        else if (f == SmartHome.Function.PowerMeter)
                        {
                            // Add power meter data
                            functionNode.Nodes.Add($"Power: {device.PowerMeter.PowerInWatt}W");
                            functionNode.Nodes.Add($"Energy: {device.PowerMeter.EnergyInKiloWattPerHour}kWh");
                        }

                    }
                }
            }
        }

        private void iTreeViewDevices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            iButtonSwitchToggle.Enabled = false;
            iButtonSwitchOn.Enabled = false;
            iButtonSwitchOff.Enabled = false;

            if (e.Node == null
                || !(e.Node.Tag is SmartHome.Device))
            {
                return;
            }

            SmartHome.Device device = (SmartHome.Device)iTreeViewDevices.SelectedNode.Tag;
            // Enable controls related to switch socket
            if (device.Has(SmartHome.Function.SwitchSocket))
            {
                iButtonSwitchToggle.Enabled = true;
                iButtonSwitchOn.Enabled = true;
                iButtonSwitchOff.Enabled = true;
            }

        }

        private void iTreeViewDevices_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private async void iButtonSwitchToggle_Click(object sender, EventArgs e)
        {
            if (iTreeViewDevices.SelectedNode==null
                || !(iTreeViewDevices.SelectedNode.Tag is SmartHome.Device))
            {
                return;
            }

            // Toggle our switch if valid
            SmartHome.Device device = (SmartHome.Device)iTreeViewDevices.SelectedNode.Tag;
            if (device.Has(SmartHome.Function.SwitchSocket))
            {
                await iClient.SetSwitchToggle(device.Identifier);
            }
        }

        private async void iButtonSwitchOn_Click(object sender, EventArgs e)
        {
            if (iTreeViewDevices.SelectedNode == null
                || !(iTreeViewDevices.SelectedNode.Tag is SmartHome.Device))
            {
                return;
            }

            // Switch on if valid
            SmartHome.Device device = (SmartHome.Device)iTreeViewDevices.SelectedNode.Tag;
            if (device.Has(SmartHome.Function.SwitchSocket))
            {
                await iClient.SetSwitchOn(device.Identifier);
            }
        }

        private async void iButtonSwitchOff_Click(object sender, EventArgs e)
        {
            if (iTreeViewDevices.SelectedNode == null
                || !(iTreeViewDevices.SelectedNode.Tag is SmartHome.Device))
            {
                return;
            }

            // Switch off if valid
            SmartHome.Device device = (SmartHome.Device)iTreeViewDevices.SelectedNode.Tag;
            if (device.Has(SmartHome.Function.SwitchSocket))
            {
                await iClient.SetSwitchOff(device.Identifier);
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            SquirrelUpdate();
        }
    }
}
