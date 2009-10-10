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

            ZuseSettings.Save();
        }
    }
}
