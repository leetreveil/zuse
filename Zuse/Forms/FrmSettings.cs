/*
 * Zuse - A Zune Last.fm plugin
 * Copyright (C) 2007-2009 Zachary Howe
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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

            this.chkCheckUpdates.Checked = ZuseSettings.CheckForUpdates;
            this.chkDebugMode.Checked = ZuseSettings.DebugMode;
            this.chkMinimizeToTray.Checked = ZuseSettings.MinimizeToTray;
            this.chkUseGrowl.Checked = ZuseSettings.UseGrowl;
            this.cmbGrowlLevel.Items.Add("All messages");
            this.cmbGrowlLevel.Items.Add("Warning and error messages");
            this.cmbGrowlLevel.Items.Add("Only error messages");
            this.cmbGrowlLevel.SelectedIndex = ZuseSettings.LoggerGrowlLevel;
            this.cmbTrackDisplayFmt.Items.Add("%artist% - %album% - %title%");
            this.cmbTrackDisplayFmt.Items.Add("%artist% - %title%");
            this.cmbTrackDisplayFmt.Items.Add("\"%title%\" by %artist%");
            this.cmbTrackDisplayFmt.Items.Add("\"%title%\" by %artist% in \"%album%\"");
            if (!this.cmbTrackDisplayFmt.Items.Contains(ZuseSettings.TrackDisplayFormat))
            {
                this.cmbTrackDisplayFmt.Items.Add(ZuseSettings.TrackDisplayFormat);
            }
            this.cmbTrackDisplayFmt.SelectedText = ZuseSettings.TrackDisplayFormat;
        }

        private void btnClearSkips_Click(object sender, EventArgs e)
        {
            ZuseSettings.UpdateSkipVersions.Clear();
            ZuseSettings.Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ZuseSettings.CheckForUpdates = this.chkCheckUpdates.Checked;
            ZuseSettings.DebugMode = this.chkDebugMode.Checked;
            ZuseSettings.MinimizeToTray = this.chkMinimizeToTray.Checked;
            ZuseSettings.UseGrowl = this.chkUseGrowl.Checked;
            ZuseSettings.LoggerGrowlLevel = this.cmbGrowlLevel.SelectedIndex;
            ZuseSettings.TrackDisplayFormat = this.cmbTrackDisplayFmt.Text;

            ZuseSettings.Save();
        }

        private void chkMinimizeToTray_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkMinimizeToTray.Checked)
            {
                string msg = "Zuse may cause the Zune software to become buggy if you use the Zune Software's built-in Mini Player. Are you sure?";

                if (MessageBox.Show(msg, "Zuse", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    this.chkMinimizeToTray.Checked = false;
                }
            }
        }
    }
}
