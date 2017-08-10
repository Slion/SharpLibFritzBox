namespace FritzBoxDemo
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.iButtonLogin = new System.Windows.Forms.Button();
            this.iTextBoxLogin = new System.Windows.Forms.TextBox();
            this.iTextBoxPassword = new System.Windows.Forms.TextBox();
            this.iLabelPassword = new System.Windows.Forms.Label();
            this.iLabelLogin = new System.Windows.Forms.Label();
            this.iTreeViewDevices = new System.Windows.Forms.TreeView();
            this.iLabelSessionId = new System.Windows.Forms.Label();
            this.iButtonSwitchToggle = new System.Windows.Forms.Button();
            this.iButtonSwitchOn = new System.Windows.Forms.Button();
            this.iButtonSwitchOff = new System.Windows.Forms.Button();
            this.iMenuStrip = new System.Windows.Forms.MenuStrip();
            this.iToolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.iToolStripMenuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.iNumericUpDownTemperature = new System.Windows.Forms.NumericUpDown();
            this.iLabelTemperature = new System.Windows.Forms.Label();
            this.iComboBoxThermostat = new System.Windows.Forms.ComboBox();
            this.thermostatBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.thermostatBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iThermostatLabel = new System.Windows.Forms.Label();
            this.iMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iNumericUpDownTemperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermostatBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermostatBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // iButtonLogin
            // 
            this.iButtonLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.iButtonLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.iButtonLogin.Location = new System.Drawing.Point(12, 447);
            this.iButtonLogin.Name = "iButtonLogin";
            this.iButtonLogin.Size = new System.Drawing.Size(75, 23);
            this.iButtonLogin.TabIndex = 2;
            this.iButtonLogin.Text = "Login";
            this.iButtonLogin.UseVisualStyleBackColor = true;
            this.iButtonLogin.Click += new System.EventHandler(this.iButtonLogin_Click);
            // 
            // iTextBoxLogin
            // 
            this.iTextBoxLogin.Location = new System.Drawing.Point(74, 33);
            this.iTextBoxLogin.Name = "iTextBoxLogin";
            this.iTextBoxLogin.Size = new System.Drawing.Size(100, 20);
            this.iTextBoxLogin.TabIndex = 0;
            // 
            // iTextBoxPassword
            // 
            this.iTextBoxPassword.Location = new System.Drawing.Point(74, 59);
            this.iTextBoxPassword.Name = "iTextBoxPassword";
            this.iTextBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.iTextBoxPassword.TabIndex = 1;
            this.iTextBoxPassword.UseSystemPasswordChar = true;
            // 
            // iLabelPassword
            // 
            this.iLabelPassword.AutoSize = true;
            this.iLabelPassword.Location = new System.Drawing.Point(12, 62);
            this.iLabelPassword.Name = "iLabelPassword";
            this.iLabelPassword.Size = new System.Drawing.Size(56, 13);
            this.iLabelPassword.TabIndex = 3;
            this.iLabelPassword.Text = "Password:";
            // 
            // iLabelLogin
            // 
            this.iLabelLogin.AutoSize = true;
            this.iLabelLogin.Location = new System.Drawing.Point(12, 36);
            this.iLabelLogin.Name = "iLabelLogin";
            this.iLabelLogin.Size = new System.Drawing.Size(36, 13);
            this.iLabelLogin.TabIndex = 4;
            this.iLabelLogin.Text = "Login:";
            // 
            // iTreeViewDevices
            // 
            this.iTreeViewDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iTreeViewDevices.HideSelection = false;
            this.iTreeViewDevices.Location = new System.Drawing.Point(198, 36);
            this.iTreeViewDevices.Name = "iTreeViewDevices";
            this.iTreeViewDevices.Size = new System.Drawing.Size(477, 436);
            this.iTreeViewDevices.TabIndex = 3;
            this.iTreeViewDevices.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.iTreeViewDevices_AfterSelect);
            this.iTreeViewDevices.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.iTreeViewDevices_NodeMouseDoubleClick);
            // 
            // iLabelSessionId
            // 
            this.iLabelSessionId.AutoSize = true;
            this.iLabelSessionId.Location = new System.Drawing.Point(15, 100);
            this.iLabelSessionId.Name = "iLabelSessionId";
            this.iLabelSessionId.Size = new System.Drawing.Size(61, 13);
            this.iLabelSessionId.TabIndex = 14;
            this.iLabelSessionId.Text = "Session ID:";
            // 
            // iButtonSwitchToggle
            // 
            this.iButtonSwitchToggle.Enabled = false;
            this.iButtonSwitchToggle.Location = new System.Drawing.Point(15, 132);
            this.iButtonSwitchToggle.Name = "iButtonSwitchToggle";
            this.iButtonSwitchToggle.Size = new System.Drawing.Size(102, 23);
            this.iButtonSwitchToggle.TabIndex = 15;
            this.iButtonSwitchToggle.Text = "Switch Toggle";
            this.iButtonSwitchToggle.UseVisualStyleBackColor = true;
            this.iButtonSwitchToggle.Click += new System.EventHandler(this.iButtonSwitchToggle_Click);
            // 
            // iButtonSwitchOn
            // 
            this.iButtonSwitchOn.Enabled = false;
            this.iButtonSwitchOn.Location = new System.Drawing.Point(15, 161);
            this.iButtonSwitchOn.Name = "iButtonSwitchOn";
            this.iButtonSwitchOn.Size = new System.Drawing.Size(102, 23);
            this.iButtonSwitchOn.TabIndex = 16;
            this.iButtonSwitchOn.Text = "Switch On";
            this.iButtonSwitchOn.UseVisualStyleBackColor = true;
            this.iButtonSwitchOn.Click += new System.EventHandler(this.iButtonSwitchOn_Click);
            // 
            // iButtonSwitchOff
            // 
            this.iButtonSwitchOff.Enabled = false;
            this.iButtonSwitchOff.Location = new System.Drawing.Point(15, 190);
            this.iButtonSwitchOff.Name = "iButtonSwitchOff";
            this.iButtonSwitchOff.Size = new System.Drawing.Size(102, 23);
            this.iButtonSwitchOff.TabIndex = 17;
            this.iButtonSwitchOff.Text = "Switch Off";
            this.iButtonSwitchOff.UseVisualStyleBackColor = true;
            this.iButtonSwitchOff.Click += new System.EventHandler(this.iButtonSwitchOff_Click);
            // 
            // iMenuStrip
            // 
            this.iMenuStrip.BackColor = System.Drawing.SystemColors.Window;
            this.iMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iToolStripMenuItemAbout,
            this.iToolStripMenuItemUpdate});
            this.iMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.iMenuStrip.Name = "iMenuStrip";
            this.iMenuStrip.Size = new System.Drawing.Size(687, 24);
            this.iMenuStrip.TabIndex = 21;
            this.iMenuStrip.Text = "Menu";
            // 
            // iToolStripMenuItemAbout
            // 
            this.iToolStripMenuItemAbout.Name = "iToolStripMenuItemAbout";
            this.iToolStripMenuItemAbout.Size = new System.Drawing.Size(52, 20);
            this.iToolStripMenuItemAbout.Text = "About";
            this.iToolStripMenuItemAbout.Click += new System.EventHandler(this.iToolStripMenuItemAbout_Click);
            // 
            // iToolStripMenuItemUpdate
            // 
            this.iToolStripMenuItemUpdate.Name = "iToolStripMenuItemUpdate";
            this.iToolStripMenuItemUpdate.Size = new System.Drawing.Size(57, 20);
            this.iToolStripMenuItemUpdate.Text = "Update";
            this.iToolStripMenuItemUpdate.Visible = false;
            this.iToolStripMenuItemUpdate.Click += new System.EventHandler(this.iToolStripMenuItemUpdate_Click);
            // 
            // iNumericUpDownTemperature
            // 
            this.iNumericUpDownTemperature.DecimalPlaces = 1;
            this.iNumericUpDownTemperature.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.iNumericUpDownTemperature.Location = new System.Drawing.Point(108, 237);
            this.iNumericUpDownTemperature.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
            this.iNumericUpDownTemperature.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.iNumericUpDownTemperature.Name = "iNumericUpDownTemperature";
            this.iNumericUpDownTemperature.Size = new System.Drawing.Size(53, 20);
            this.iNumericUpDownTemperature.TabIndex = 22;
            this.iNumericUpDownTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.iNumericUpDownTemperature.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.iNumericUpDownTemperature.ValueChanged += new System.EventHandler(this.iNumericUpDownTemperature_ValueChanged);
            // 
            // iLabelTemperature
            // 
            this.iLabelTemperature.AutoSize = true;
            this.iLabelTemperature.Location = new System.Drawing.Point(12, 239);
            this.iLabelTemperature.Name = "iLabelTemperature";
            this.iLabelTemperature.Size = new System.Drawing.Size(90, 13);
            this.iLabelTemperature.TabIndex = 23;
            this.iLabelTemperature.Text = "Temperature (°C):";
            // 
            // iComboBoxThermostat
            // 
            this.iComboBoxThermostat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iComboBoxThermostat.FormattingEnabled = true;
            this.iComboBoxThermostat.Location = new System.Drawing.Point(108, 274);
            this.iComboBoxThermostat.Name = "iComboBoxThermostat";
            this.iComboBoxThermostat.Size = new System.Drawing.Size(53, 21);
            this.iComboBoxThermostat.TabIndex = 24;
            this.iComboBoxThermostat.SelectedIndexChanged += new System.EventHandler(this.iComboBoxThermostat_SelectedIndexChanged);
            // 
            // iThermostatLabel
            // 
            this.iThermostatLabel.AutoSize = true;
            this.iThermostatLabel.Location = new System.Drawing.Point(39, 277);
            this.iThermostatLabel.Name = "iThermostatLabel";
            this.iThermostatLabel.Size = new System.Drawing.Size(63, 13);
            this.iThermostatLabel.TabIndex = 25;
            this.iThermostatLabel.Text = "Thermostat:";
            // 
            // FormMain
            // 
            this.AcceptButton = this.iButtonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(687, 484);
            this.Controls.Add(this.iThermostatLabel);
            this.Controls.Add(this.iComboBoxThermostat);
            this.Controls.Add(this.iLabelTemperature);
            this.Controls.Add(this.iNumericUpDownTemperature);
            this.Controls.Add(this.iMenuStrip);
            this.Controls.Add(this.iButtonSwitchOff);
            this.Controls.Add(this.iButtonSwitchOn);
            this.Controls.Add(this.iButtonSwitchToggle);
            this.Controls.Add(this.iLabelSessionId);
            this.Controls.Add(this.iTreeViewDevices);
            this.Controls.Add(this.iLabelLogin);
            this.Controls.Add(this.iLabelPassword);
            this.Controls.Add(this.iTextBoxPassword);
            this.Controls.Add(this.iTextBoxLogin);
            this.Controls.Add(this.iButtonLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Fritz!Box Demo";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.iMenuStrip.ResumeLayout(false);
            this.iMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iNumericUpDownTemperature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermostatBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thermostatBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button iButtonLogin;
        private System.Windows.Forms.TextBox iTextBoxLogin;
        private System.Windows.Forms.TextBox iTextBoxPassword;
        private System.Windows.Forms.Label iLabelPassword;
        private System.Windows.Forms.Label iLabelLogin;
        private System.Windows.Forms.TreeView iTreeViewDevices;
        private System.Windows.Forms.Label iLabelSessionId;
        private System.Windows.Forms.Button iButtonSwitchToggle;
        private System.Windows.Forms.Button iButtonSwitchOn;
        private System.Windows.Forms.Button iButtonSwitchOff;
        private System.Windows.Forms.MenuStrip iMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem iToolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem iToolStripMenuItemUpdate;
        private System.Windows.Forms.NumericUpDown iNumericUpDownTemperature;
        private System.Windows.Forms.Label iLabelTemperature;
        private System.Windows.Forms.ComboBox iComboBoxThermostat;
        private System.Windows.Forms.BindingSource thermostatBindingSource;
        private System.Windows.Forms.BindingSource thermostatBindingSource1;
        private System.Windows.Forms.Label iThermostatLabel;
    }
}

