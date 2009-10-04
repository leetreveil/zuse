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
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Repository;

namespace Zuse
{
    using Zuse.Core;
    using Zuse.Forms;
    using Zuse.Scrobbler;
    using Zuse.Utilities;
    using Zuse.Web;

    class Program
    {
        private Manager manager;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ILog log;

        public Program()
        {
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            ClientLoader cl = new ClientLoader();

            if (!cl.IsOpen())
            {
                if (cl.IsAvailable())
                {
                }
                else
                {
                    if (MessageBox.Show("Zuse has detected that the Last.fm software is not installed on this computer, would you like to install it?", "Install Last.fm Software?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start("http://www.last.fm/download");

                        MessageBox.Show("Zuse will now close.", "Thank you!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            log = LogManager.GetLogger("Zuse", typeof(Zuse.Program));
            log.Info("Zuse is starting up!");

            /* Build the context menu for the system tray icon */
            this.contextMenuStrip = new ContextMenuStrip();

            /* Add the debug log option if the debug mode is on */
            if (ZuseSettings.DebugMode)
            {
                ToolStripMenuItem item1 = new ToolStripMenuItem("Debug Log");
                item1.Click += new EventHandler(this.DebugLog_Click);
                this.contextMenuStrip.Items.Add(item1);
                this.contextMenuStrip.Items.Add(new ToolStripSeparator());
            }

            /* Check for Updates system tray menu option */
            ToolStripMenuItem item2 = new ToolStripMenuItem("Check for Updates");
            item2.Click += new EventHandler(this.CheckForUpdate_Click);
            this.contextMenuStrip.Items.Add(item2);

            /* About Zuse system tray menu option */
            ToolStripMenuItem item3 = new ToolStripMenuItem("About Zuse");
            item3.Click += new EventHandler(this.About_Click);
            this.contextMenuStrip.Items.Add(item3);

            this.contextMenuStrip.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem item4 = new ToolStripMenuItem("Exit");
            item4.Click += new EventHandler(this.Exit_Click);
            this.contextMenuStrip.Items.Add(item4);

            // Retrieve the stream of the system tray icon embedded in Zuse.exe
            Assembly asm = Assembly.GetExecutingAssembly();
            Stream stream = asm.GetManifestResourceStream("Zuse.Resources.ZuseIcon.ico");

            // Create the system tray icon and attach events
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.Icon = new System.Drawing.Icon(stream);
            this.notifyIcon.Visible = true;
            this.notifyIcon.Text = "Zuse";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.BalloonTipClicked += new EventHandler(this.NotifyIcon_BalloonTipClicked);
            this.notifyIcon.BalloonTipClosed += new EventHandler(this.NotifyIcon_BalloonTipClosed);

            // Initialize main manager
            this.manager = new Manager();
            this.manager.Launch();
        }

        private void CheckForUpdate_Click(object sender, EventArgs e)
        {
            UpdateChecker.Check();

            if (UpdateChecker.UpdateAvailable)
            {
                UpdateChecker.ShowUpdateDialog();
            }
            else
            {
                MessageBox.Show("No update is available.", "Zuse", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
        }

        protected void NotifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
        }

        protected void Launch_Click(object sender, EventArgs e)
        {
            this.manager.Launch();
        }

        protected void DebugLog_Click(object sender, EventArgs e)
        {
            this.manager.ShowDebugWindow();
        }

        protected void About_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog();
            frmAbout.Dispose();
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
            }
            catch { }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string appdata_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Zuse";
            if (!Directory.Exists(appdata_path))
            {
                Directory.CreateDirectory(appdata_path);
            }
            string log_path = appdata_path + "\\Logs\\" + DateTime.Today.ToShortDateString().Replace('/', '-') + ".xml";
            string settings_path = appdata_path + "\\Settings.xml";

            if (!File.Exists(settings_path)) ZuseSettings.Save();
            else ZuseSettings.Load();

            RollingFileAppender rfa = new RollingFileAppender();
            rfa.AppendToFile = true;
            rfa.File = log_path;
            rfa.StaticLogFileName = true;
            rfa.Layout = new log4net.Layout.XmlLayout();
            rfa.MaxFileSize = 1024 * 1024;
            rfa.Threshold = Level.All;
            rfa.LockingModel = new FileAppender.MinimalLock();
            rfa.ActivateOptions();

            ILoggerRepository repo = LogManager.CreateRepository("Zuse");
            BasicConfigurator.Configure(repo, rfa);

            UpdateChecker.Check();

            if (UpdateChecker.UpdateAvailable)
            {
                UpdateChecker.ShowUpdateDialog();
            }

            Program zuse = new Program();
            Application.Run();

            ZuseSettings.Save();
        }
    }
}