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
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Zuse.Core;

namespace Zuse.Forms
{
    public partial class FrmUpdate : Form
    {
        private string displayedUpdate;
        private string updateDownloadUrl;

        public FrmUpdate()
        {
            InitializeComponent();
        }

        public string UpdateDownloadUrl
        {
            get { return updateDownloadUrl; }
            set { updateDownloadUrl = value; }
        }

        public string DisplayingUpdate
        {
            get { return displayedUpdate; }
            set { displayedUpdate = value; }
        }

        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Icon = Icon.ExtractAssociatedIcon(asm.Location);

            if (ZuseSettings.UpdateSkipVersions.Contains(displayedUpdate))
            {
                lblSkipThisUpdate.Visible = true;
            }
            else
            {
                lblSkipThisUpdate.Visible = false;
            }
        }

        public void SetDetails(Version new_version)
        {
            Version current_version = Assembly.GetExecutingAssembly().GetName().Version;

            lblDetails.Text = string.Format("You have version {0:s}, and {1:s} is available!",
                                            current_version.ToString(), new_version.ToString());
        }

        public void SetChangeLog(string url)
        {
            webBrowserChangelog.Navigate(url);

            webBrowserChangelog.Navigating += new WebBrowserNavigatingEventHandler(webBrowserChangelog_Navigating);
        }

        private void btnInstallUpdate_Click(object sender, EventArgs e)
        {
            Process.Start(updateDownloadUrl);
        }

        private void btnSkipUpdate_Click(object sender, EventArgs e)
        {
            if (!lblSkipThisUpdate.Visible)
            {
                string msg =
                    "If you skip this update, it will no longer appear as Zuse checks for updates on startup. Are you sure?";

                if (MessageBox.Show(msg, "Zuse", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ZuseSettings.UpdateSkipVersions.Add(displayedUpdate);

                    ZuseSettings.Save();
                }
            }

            DialogResult = DialogResult.Cancel;
        }

        private void webBrowserChangelog_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Process.Start(e.Url.ToString());
            e.Cancel = true;
        }
    }
}