using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

using log4net;
using log4net.Appender;
using log4net.Config;

namespace Zuse.Forms
{
    using Zuse.Properties;

    public partial class FrmDebug : Form
    {
        public FrmDebug()
        {
            InitializeComponent();
        }

        private void FrmDebug_Load(object sender, EventArgs e)
        {
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(FileSystemWatcher_Changed);
            this.fileSystemWatcher.Path = Application.StartupPath + "\\Logs\\";
            this.fileSystemWatcher.Filter = "*.xml";
            this.fileSystemWatcher.EnableRaisingEvents = true;

            this.RefreshView();
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            FileInfo modFile = new FileInfo(e.FullPath);
            FileInfo logFile = (FileInfo)this.cmbLogFile.SelectedItem;

            if (modFile.FullName == logFile.FullName)
            {
                this.RefreshCurrentLog();
            }
        }

        public void RefreshView()
        {
            foreach (string fn in Directory.GetFiles(Application.StartupPath + "\\Logs", "*.xml", SearchOption.AllDirectories))
            {
                FileInfo fi = new FileInfo(fn);

                this.cmbLogFile.Items.Add(fi);
            }
            
            RollingFileAppender rfa = (RollingFileAppender)LogManager.GetAllRepositories()[0].GetAppenders()[0];

            foreach (FileInfo fi in this.cmbLogFile.Items)
            {
                if (fi.FullName == new FileInfo(rfa.File).FullName)
                {
                    this.cmbLogFile.SelectedItem = fi;
                }
            }
        }

        public void RefreshCurrentLog()
        {
            this.lstMessages.Items.Clear();

            FileInfo currentlog = (FileInfo)this.cmbLogFile.SelectedItem;

            XmlDocument xdoc = new XmlDocument();
            XmlElement rootel = xdoc.CreateElement("Root");
            rootel.InnerXml = File.ReadAllText(currentlog.FullName);

            foreach (object obj in rootel)
            {
                if (obj is XmlElement)
                {
                    XmlElement el = (XmlElement)obj;

                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = DateTime.Parse(el.Attributes["timestamp"].Value).ToLongTimeString();
                    lvi.SubItems.Add(el.Attributes["level"].Value);
                    lvi.SubItems.Add(el["message"].InnerText);
                    lvi.Tag = el;

                    this.lstMessages.Items.Add(lvi);
                }
            }
        }

        private void cmbLogFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshCurrentLog();
        }

        private void lstMessages_DoubleClick(object sender, EventArgs e)
        {
            ILog log = LogManager.GetLogger("Zuse", typeof(Zuse.Forms.FrmDebug));
            log.Info("Testing FrmDebug automatic refreshing");
        }

        private void ShowItemsDetailsDialog()
        {
            FrmDetails frmDetails = new FrmDetails();

            frmDetails.ShowDialog();

            frmDetails.Dispose();
        }
    }
}
