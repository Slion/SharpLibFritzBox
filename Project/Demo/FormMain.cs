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
            // Add data source
            iComboBoxThermostat.DataSource = Enum.GetValues(typeof(SmartHome.Thermostat.Radiator));
            UpdateControls();
            //
            iClient = new SmartHome.Client();
            // Show version in title bar
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            Text += " - v" + version;

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
                        iToolStripMenuItemUpdate.Visible = true;
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
            if (iTextBoxUrl.Enabled)
            {
                // First time around set our base address
                // Base address can't be changed after a request was issued
                iClient.BaseAddress = new Uri(iTextBoxUrl.Text); // This will throw an exception if a request was already sent
                iTextBoxUrl.Enabled = false; // Tell the user this can't be changed any more
            }

            await iClient.Authenticate(iTextBoxLogin.Text, iTextBoxPassword.Text);
            iLabelSessionId.Text = "Session ID: " + iClient.SessionId;
            await UpdateDeviceList();
        }
        
        /// <summary>
        /// Update our device list
        /// </summary>
        /// <returns></returns>
        async Task UpdateDeviceList()
        {
            SmartHome.DeviceList deviceList = await iClient.GetDeviceList();
            PopulateDevicesTree(deviceList);
        }

        /// <summary>
        /// Update our device list and select the specified device 
        /// </summary>
        /// <param name="aIdentifier"></param>
        /// <returns></returns>
        async Task UpdateDeviceList(string aIdentifier)
        {
            await UpdateDeviceList();
            SelectDevice(aIdentifier);
        }

        /// <summary>
        /// Select the specified device in our tree view.
        /// </summary>
        /// <param name="aIdentifier"></param>
        void SelectDevice(string aIdentifier)
        {
            foreach (TreeNode n in iTreeViewDevices.Nodes)
            {
                SmartHome.Device d = (SmartHome.Device)n.Tag;
                if (d.Identifier==aIdentifier)
                {
                    iTreeViewDevices.SelectedNode = n;
                    n.ExpandAll();
                }
            }
        }

        /// <summary>
        /// Populate our tree view with our devices information.
        /// </summary>
        /// <param name="aDeviceList"></param>
        void PopulateDevicesTree(SmartHome.DeviceList aDeviceList)
        {
            iTreeViewDevices.Nodes.Clear();
            UpdateControls();

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
                            functionNode.Nodes.Add($"Comfort temperature: {device.Thermostat.ComfortTemperatureInCelsius.ToString()} °C");
                            functionNode.Nodes.Add($"Economy temperature: {device.Thermostat.EconomyTemperatureInCelsius.ToString()} °C");
                            functionNode.Nodes.Add($"Current temperature: {device.Thermostat.CurrentTemperatureInCelsius.ToString()} °C");
                            functionNode.Nodes.Add($"Target temperature: {device.Thermostat.TargetTemperatureInCelsius.ToString()} °C");
                            functionNode.Nodes.Add($"Battery {device.Thermostat.Battery.ToString()}");
                            functionNode.Nodes.Add($"Lock: {device.Thermostat.Lock.ToString()}");
                            functionNode.Nodes.Add($"Device lock: {device.Thermostat.DeviceLock.ToString()}");
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

        /// <summary>
        /// Update our UI controls based on application state.
        /// We mostly check the properties of the device currently selected.
        /// </summary>
        private void UpdateControls()
        {
            // Switch Socket
            iGroupBoxSwitchSocket.Enabled = false;
            iButtonSwitchToggle.Enabled = false;
            iButtonSwitchOn.Enabled = false;
            iButtonSwitchOff.Enabled = false;
            // Radiator Thermostat
            iGroupBoxRadiatorThermostat.Enabled = false;
            iNumericUpDownTemperature.Enabled = false;
            iComboBoxThermostat.Enabled = false;

            if (iTreeViewDevices.SelectedNode == null
                || !(iTreeViewDevices.SelectedNode.Tag is SmartHome.Device))
            {
                return;
            }

            SmartHome.Device device = (SmartHome.Device)iTreeViewDevices.SelectedNode.Tag;
            // Enable controls related to switch socket
            if (device.IsSwitchSocket)
            {
                iGroupBoxSwitchSocket.Enabled = true;
                iButtonSwitchToggle.Enabled = true;
                iButtonSwitchOn.Enabled = true;
                iButtonSwitchOff.Enabled = true;
            }

            if (device.IsRadiatorThermostat)
            {
                iGroupBoxRadiatorThermostat.Enabled = true;
                iComboBoxThermostat.Enabled = true;
                if (device.Thermostat.IsOnMax)
                {
                    iComboBoxThermostat.SelectedItem = SmartHome.Thermostat.Radiator.On;
                }
                else if (device.Thermostat.IsOff)
                {
                    iComboBoxThermostat.SelectedItem = SmartHome.Thermostat.Radiator.Off;
                }
                else
                {
                    iComboBoxThermostat.SelectedItem = SmartHome.Thermostat.Radiator.Regulated;
                    iNumericUpDownTemperature.Enabled = true;
                    iNumericUpDownTemperature.Value = (decimal)device.Thermostat.TargetTemperatureInCelsius;
                }
            }

        }

        private void iTreeViewDevices_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateControls();
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
            await device.SwitchToggle();
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
            await device.SwitchOn();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void iButtonSwitchOff_Click(object sender, EventArgs e)
        {
            if (iTreeViewDevices.SelectedNode == null
                || !(iTreeViewDevices.SelectedNode.Tag is SmartHome.Device))
            {
                return;
            }

            // Switch off if valid
            SmartHome.Device device = (SmartHome.Device)iTreeViewDevices.SelectedNode.Tag;
            await device.SwitchOff();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Shown(object sender, EventArgs e)
        {
            SquirrelUpdate();
        }

        private void iToolStripMenuItemUpdate_Click(object sender, EventArgs e)
        {
            SquirrelUpdate();
        }

        private void iToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void iNumericUpDownTemperature_ValueChanged(object sender, EventArgs e)
        {
            if (iTreeViewDevices.SelectedNode == null
            || !(iTreeViewDevices.SelectedNode.Tag is SmartHome.Device))
            {
                return;
            }

            SmartHome.Device device = (SmartHome.Device)iTreeViewDevices.SelectedNode.Tag;
            await device.SetTargetTemperature((float)iNumericUpDownTemperature.Value);
            await UpdateDeviceList(device.Identifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void iComboBoxThermostat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (iTreeViewDevices.SelectedNode == null
            || !(iTreeViewDevices.SelectedNode.Tag is SmartHome.Device))
            {
                return;
            }

            SmartHome.Device device = (SmartHome.Device)iTreeViewDevices.SelectedNode.Tag;

            SmartHome.Thermostat.Radiator thermostat = SmartHome.Thermostat.Radiator.Off;
            Enum.TryParse<SmartHome.Thermostat.Radiator>(iComboBoxThermostat.SelectedValue.ToString(), out thermostat);
            if (thermostat == SmartHome.Thermostat.Radiator.Regulated)
            {
                // Set target temperature
                await device.SetTargetTemperature((float)iNumericUpDownTemperature.Value);
            }
            else
            {
                // Turn it on of off
                await device.SetTargetTemperatureCode((int)thermostat);
            }

            await UpdateDeviceList(device.Identifier);
        }

    }
}
