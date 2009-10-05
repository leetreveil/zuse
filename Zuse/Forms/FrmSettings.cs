using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace Zuse.Forms
{
    using Zuse.Core;

    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            this.Icon = Icon.ExtractAssociatedIcon(asm.Location);

            this.lblNote.Text = "There is no need to save these settings. They will be saved automatically.";

            this.chkDebugMode.Checked = ZuseSettings.DebugMode;
            this.chkMinimizeToTray.Checked = ZuseSettings.MinimizeToTray;
        }

        private void chkSettings_CheckedChanged(object sender, EventArgs e)
        {
            ZuseSettings.DebugMode = this.chkDebugMode.Checked;
            ZuseSettings.MinimizeToTray = this.chkMinimizeToTray.Checked;

            ZuseSettings.Save();
        }
    }
}
