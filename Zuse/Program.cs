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
using System.Drawing;
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
    using Zuse.Web;

    class Program
    {
        private Manager manager;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ILog log;
        private bool windowMinimized;

        public Program()
        {
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            ClientLoader cl = new ClientLoader();

            if (!cl.IsOpen())
            {
                if (!cl.IsAvailable())
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
                ToolStripMenuItem itemDebugLog = new ToolStripMenuItem("Debug Log");
                itemDebugLog.Click += new EventHandler(this.DebugLog_Click);
                this.contextMenuStrip.Items.Add(itemDebugLog);
                this.contextMenuStrip.Items.Add(new ToolStripSeparator());
            }

            /* Check for Updates system tray menu option */
            ToolStripMenuItem itemCheckUpdates = new ToolStripMenuItem("Check for Updates");
            itemCheckUpdates.Click += new EventHandler(this.CheckForUpdate_Click);
            this.contextMenuStrip.Items.Add(itemCheckUpdates);

            /* About Zuse system tray menu option */
            ToolStripMenuItem itemSettings = new ToolStripMenuItem("Settings");
            itemSettings.Click += new EventHandler(this.Settings_Click);
            this.contextMenuStrip.Items.Add(itemSettings);

            /* About Zuse system tray menu option */
            ToolStripMenuItem itemAbout = new ToolStripMenuItem("About Zuse");
            itemAbout.Click += new EventHandler(this.About_Click);
            this.contextMenuStrip.Items.Add(itemAbout);

            this.contextMenuStrip.Items.Add(new ToolStripSeparator());

            ToolStripMenuItem itemExit = new ToolStripMenuItem("Exit");
            itemExit.Click += new EventHandler(this.Exit_Click);
            this.contextMenuStrip.Items.Add(itemExit);

            // Retrieve the stream of the system tray icon embedded in Zuse.exe
            Assembly asm = Assembly.GetExecutingAssembly();
            Icon icon =  Icon.ExtractAssociatedIcon(asm.Location);
            
            // Create the system tray icon and attach events
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.Icon = icon;
            this.notifyIcon.Visible = true;
            this.notifyIcon.Text = "Zuse";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.BalloonTipClicked += new EventHandler(this.NotifyIcon_BalloonTipClicked);
            this.notifyIcon.BalloonTipClosed += new EventHandler(this.NotifyIcon_BalloonTipClosed);
            this.notifyIcon.DoubleClick += new EventHandler(this.NotifyIcon_DoubleClick);

            this.windowMinimized = true;

            // Initialize main manager
            this.manager = new Manager();
            this.manager.LaunchZune();
        }

        protected void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            switch (windowMinimized)
            {
                case true:
                    this.manager.ShowZuneWindow();
                    break;
                case false:
                    this.manager.HideZuneWindow();
                    break;
            }

            windowMinimized = !windowMinimized;
        }

        protected void CheckForUpdate_Click(object sender, EventArgs e)
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

        protected void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
        }

        protected void NotifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
        }

        protected void DebugLog_Click(object sender, EventArgs e)
        {
            this.manager.ShowDebugWindow();
        }

        protected void Settings_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.ShowDialog();
            frmSettings.Dispose();
        }

        protected void About_Click(object sender, EventArgs e)
        {
            FrmAbout frmAbout = new FrmAbout();
            frmAbout.ShowDialog();
            frmAbout.Dispose();
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            log.Info("Zuse is closing down!");

            this.manager.CloseZune();
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

            if (!File.Exists(settings_path)) ZuseSettings.Save();
            else ZuseSettings.Load();

            if (ZuseSettings.CheckForUpdates)
            {
                UpdateChecker.Check();

                if (UpdateChecker.UpdateAvailable)
                {
                    if (!ZuseSettings.UpdateSkipVersions.Contains(UpdateChecker.LatestVersion.ToString()))
                    {
                        UpdateChecker.ShowUpdateDialog();
                    }
                }
            }

            Program zuse = new Program();
            Application.Run();

            ZuseSettings.Save();
        }
    }
}