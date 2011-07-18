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
using System.Xml;
using log4net;
using log4net.Appender;

namespace Zuse.Forms
{
    public partial class FrmDebug : Form
    {
        private string log_path;

        public FrmDebug()
        {
            InitializeComponent();
        }

        private void FrmDebug_Load(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            Icon = Icon.ExtractAssociatedIcon(asm.Location);

            string appdata_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            log_path = appdata_path + "\\Zuse\\Logs\\";

            fileSystemWatcher.Changed += new FileSystemEventHandler(FileSystemWatcher_Changed);
            fileSystemWatcher.Path = log_path;
            fileSystemWatcher.Filter = "*";
            fileSystemWatcher.EnableRaisingEvents = true;

            RefreshView();
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            var modFile = new FileInfo(e.FullPath);
            var logFile = (FileInfo) cmbLogFile.SelectedItem;

            if (modFile.FullName == logFile.FullName)
            {
                RefreshCurrentLog();
            }
        }

        public void RefreshView()
        {
            foreach (string fn in Directory.GetFiles(log_path, "*", SearchOption.AllDirectories))
            {
                var fi = new FileInfo(fn);

                cmbLogFile.Items.Add(fi);
            }

            var rfa = (RollingFileAppender) LogManager.GetAllRepositories()[0].GetAppenders()[0];

            foreach (FileInfo fi in cmbLogFile.Items)
            {
                if (fi.FullName == new FileInfo(rfa.File).FullName)
                {
                    cmbLogFile.SelectedItem = fi;
                }
            }
        }

        public void RefreshCurrentLog()
        {
            lstMessages.Items.Clear();

            while (true)
            {
                try
                {
                    var currentlog = (FileInfo) cmbLogFile.SelectedItem;

                    var xdoc = new XmlDocument();
                    XmlElement rootel = xdoc.CreateElement("Root");
                    rootel.InnerXml = File.ReadAllText(currentlog.FullName);

                    foreach (object obj in rootel)
                    {
                        if (obj is XmlElement)
                        {
                            var el = (XmlElement) obj;

                            var lvi = new ListViewItem();
                            lvi.Text = DateTime.Parse(el.Attributes["timestamp"].Value).ToLongTimeString();
                            lvi.SubItems.Add(el.Attributes["level"].Value);
                            lvi.SubItems.Add(el["message"].InnerText);
                            lvi.Tag = el;

                            lstMessages.Items.Add(lvi);
                        }
                    }

                    break;
                }
                catch
                {
                }
            }
        }

        private void cmbLogFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCurrentLog();
        }

        private void lstMessages_DoubleClick(object sender, EventArgs e)
        {
            ILog log = LogManager.GetLogger("Zuse", typeof (FrmDebug));
            log.Info("Testing FrmDebug automatic refreshing");
        }

        private void btnOpenLogsDirectory_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe",
                          "/root," + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                          "\\Zuse\\Logs");
        }
    }
}