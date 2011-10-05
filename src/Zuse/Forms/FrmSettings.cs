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
using leetreveil.Zuse.Properties;

namespace Zuse.Forms
{
    public partial class FrmSettings : Form
    {
        private leetreveil.Zuse.Properties.Settings _zuseSettings = Settings.Default;

        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Icon = Icon.ExtractAssociatedIcon(asm.Location);

            chkCheckUpdates.Checked = _zuseSettings.CheckForUpdates;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _zuseSettings.CheckForUpdates = chkCheckUpdates.Checked;
            _zuseSettings.Save();
        }
    }
}