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
using Zuse.Core;
using Zuse.Forms;
using Zuse.Web;
using leetreveil.Zuse.Properties;
using Lpfm.LastFmScrobbler;
using leetreveil.Zuse.Forms;

namespace Zuse
{
    internal class Program
    {
        private ContextMenuStrip contextMenuStrip;
        private ZuneManager manager;
        private NotifyIcon notifyIcon;
        private bool windowMinimized;

        public Program()
        {
            /* Build the context menu for the system tray icon */
            contextMenuStrip = new ContextMenuStrip();

            /* About Zuse system tray menu option */
            var itemAbout = new ToolStripMenuItem("About");
            itemAbout.Click += delegate { new FrmAbout().ShowDialog(); };
            contextMenuStrip.Items.Add(itemAbout);
            
            /* Settings Zuse system tray menu option */
            var itemSettings = new ToolStripMenuItem("Settings");
            itemSettings.Click += delegate { new FrmSettings().ShowDialog(); };
            contextMenuStrip.Items.Add(itemSettings);

            contextMenuStrip.Items.Add(new ToolStripSeparator());

            var itemExit = new ToolStripMenuItem("Exit");
            itemExit.Click += delegate {
                manager.CloseZune();
                Application.Exit();
            };
            contextMenuStrip.Items.Add(itemExit);

            // Retrieve the stream of the system tray icon embedded in Zuse.exe
            Assembly asm = Assembly.GetExecutingAssembly();
            Icon icon = Icon.ExtractAssociatedIcon(asm.Location);

            // Create the system tray icon and attach events
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = icon;
            notifyIcon.Visible = true;
            notifyIcon.Text = "Zuse";
            notifyIcon.ContextMenuStrip = contextMenuStrip;

            windowMinimized = false;

            string ApiKey = "28c09ec38e4b2fc3d0d9685702065295";
            string ApiSecret = "394e61d41eb59981ca4b4d275073c1d1";
            var scrobbler = new Scrobbler(ApiKey, ApiSecret, Settings.Default.SessionKey);

            if (String.IsNullOrEmpty(Settings.Default.SessionKey))
            {
                MessageBox.Show("Welcome to Zuse! Because this is the first time you have used Zuse " + 
                    "we need to authorize the application with last.fm. " +
                    "Please follow the instructions in your web browser. Thank you.",
                    "Zuse", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // get a url to authenticate this application
                string url = scrobbler.GetAuthorisationUri();

                // open the URL in the default browser
                Process.Start(url);
            }

            // Initialize main manager
            manager = new ZuneManager(scrobbler);
            manager.LaunchZune();
        }

        void itemAbout_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void CheckForUpdate_Click(object sender, EventArgs e)
        {
            UpdateChecker.Check();

            if (UpdateChecker.UpdateAvailable)
            {
                //TODO: display message to user with update
            }
            else
            {
                MessageBox.Show("No update is available.", "Zuse", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string appdata_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Zuse";
            if (!Directory.Exists(appdata_path))
            {
                Directory.CreateDirectory(appdata_path);
            }

            string log_path = appdata_path + "\\Logs\\";
            Logger.Init(log_path);

            if (Settings.Default.CheckForUpdates)
            {
                //TODO: the update checker blocks the UI thread and will delay app load
                //UpdateChecker.Check();

                if (UpdateChecker.UpdateAvailable)
                {
                    //TODO: Display message to user with update
                }
            }

            var zuse = new Program();
            Application.Run();
        }
    }
}