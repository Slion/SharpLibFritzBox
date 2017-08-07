using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FritzBox = SharpLib.FritzBox;

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
            FritzBox.Client client = new FritzBox.Client();
            FritzBox.SessionInfo info = await client.GetSessionInfoAsync();
        }
    }
}
