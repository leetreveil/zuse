/*
 * Zuse - A Zune Last.fm plugin
 * Copyright (C) 2007-2010 Zachary Howe
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
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Zuse.Core;

namespace Zuse.Forms
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Icon = Icon.ExtractAssociatedIcon(asm.Location);

            lblNote.Text = "There is no need to save these settings. They will be saved automatically.";

            chkCheckUpdates.Checked = ZuseSettings.CheckForUpdates;
            chkDebugMode.Checked = ZuseSettings.DebugMode;
            chkMinimizeToTray.Checked = ZuseSettings.MinimizeToTray;
            cmbGrowlLevel.Items.Add("All messages");
            cmbGrowlLevel.Items.Add("Warning and error messages");
            cmbGrowlLevel.Items.Add("Only error messages");
            cmbTrackDisplayFmt.Items.Add("%artist% - %album% - %title%");
            cmbTrackDisplayFmt.Items.Add("%artist% - %title%");
            cmbTrackDisplayFmt.Items.Add("\"%title%\" by %artist%");
            cmbTrackDisplayFmt.Items.Add("\"%title%\" by %artist% in \"%album%\"");
            if (!cmbTrackDisplayFmt.Items.Contains(ZuseSettings.TrackDisplayFormat))
            {
                cmbTrackDisplayFmt.Items.Add(ZuseSettings.TrackDisplayFormat);
            }
            cmbTrackDisplayFmt.SelectedText = ZuseSettings.TrackDisplayFormat;
        }

        private void btnClearSkips_Click(object sender, EventArgs e)
        {
            ZuseSettings.UpdateSkipVersions.Clear();
            ZuseSettings.Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ZuseSettings.CheckForUpdates = chkCheckUpdates.Checked;
            ZuseSettings.DebugMode = chkDebugMode.Checked;
            ZuseSettings.MinimizeToTray = chkMinimizeToTray.Checked;
            ZuseSettings.TrackDisplayFormat = cmbTrackDisplayFmt.Text;

            ZuseSettings.Save();
        }

        private void chkMinimizeToTray_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMinimizeToTray.Checked)
            {
                string msg =
                    "Zuse may cause the Zune software to become buggy if you use the Zune Software's built-in Mini Player. Are you sure?";

                if (MessageBox.Show(msg, "Zuse", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    chkMinimizeToTray.Checked = false;
                }
            }
        }
    }
}