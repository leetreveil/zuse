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
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Zuse.Forms
{
    internal partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();

            Text = String.Format("About {0}", AssemblyTitle);
            labelCopyright.Text = AssemblyCopyright;
            labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            labelProductName.Text = AssemblyProduct;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute) attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute) attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute) attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute) attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute) attributes[0]).Company;
            }
        }

        #endregion

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Icon = Icon.ExtractAssociatedIcon(asm.Location);

            StreamReader sr = null;

            Stream s1 = asm.GetManifestResourceStream("Zuse.Resources.LICENSE.rtf");
            Stream s2 = asm.GetManifestResourceStream("Zuse.Resources.THIRDPARTIES.rtf");

            sr = new StreamReader(s1);
            rtfLicense.Rtf = sr.ReadToEnd();
            sr.Close();

            sr = new StreamReader(s2);
            rtfThirdParties.Rtf = sr.ReadToEnd();
            sr.Close();

            webBrowserDonors.Navigate("http://zusefm.org/donors/");
        }

        private void linkLabelJoinGroup1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("http://www.last.fm/group/Zuse");
        }

        private void linkLabelJoinGroup2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("http://www.last.fm/group/Zune-Online/");
        }

        private void OpenLink(string url)
        {
            var proc = new Process();
            proc.StartInfo = new ProcessStartInfo(url);
            proc.Start();
        }
    }
}