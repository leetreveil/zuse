/*
 * Zuse - A Zune Last.fm plugin
 * Copyright (C) 2007-2008 Zachary Howe
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
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Zuse.Forms
{
    partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();

            this.Text = String.Format("About {0}", this.AssemblyTitle);
            this.labelCopyright.Text = this.AssemblyCopyright;
            this.labelVersion.Text = String.Format("Version {0}", this.AssemblyVersion);
            this.labelProductName.Text = this.AssemblyProduct;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            StreamReader sr = null;

            Stream s1 = asm.GetManifestResourceStream("Zuse.Resources.LICENSE.rtf");
            Stream s2 = asm.GetManifestResourceStream("Zuse.Resources.THIRDPARTIES.rtf");

            sr = new StreamReader(s1);
            this.rtfLicense.Rtf = sr.ReadToEnd();
            sr.Close();

            sr = new StreamReader(s2);
            this.rtfThirdParties.Rtf = sr.ReadToEnd();
            sr.Close();

            this.webBrowserDonors.Navigate("http://zuse.nfshost.com/donors/");
        }

        private void linkLabelJoinGroup1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.OpenLink("http://www.last.fm/group/Zuse");
        }

        private void linkLabelJoinGroup2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.OpenLink("http://www.last.fm/group/Zune-Online/");
        }

        private void OpenLink(string url)
        {
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo(url);
            proc.Start();
        }
    }
}
