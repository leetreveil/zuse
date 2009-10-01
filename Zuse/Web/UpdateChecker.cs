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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;

using log4net;

namespace Zuse.Web
{
	using Zuse.Properties;
	using Zuse.Scrobbler;
	using Zuse.Utilities;
	using Zuse.Forms;

	public class UpdateChecker
    {
        private static string baseUpdateCheckUrl = "http://zusefm.org/updates/version.txt";
        private static string updateChangelog;
        private static Version updateVersion;
        private static bool updateAvailable;
		private static ILog log;

        public static bool UpdateAvailable
        {
            get
            {
                return updateAvailable;
            }
        }

		static UpdateChecker()
		{
            log = LogManager.GetLogger("Zuse", typeof(Zuse.Web.UpdateChecker));
		}

        public static void Check()
        {
            Version current_version = Assembly.GetExecutingAssembly().GetName().Version;
            Version newest_version;

            WebClient wc = new WebClient();
            string version_info = wc.DownloadString(baseUpdateCheckUrl);
            string[] version_s = version_info.Split(new char[] {'\n'});

            foreach (string v in version_s)
            {
                if (v.Trim() == string.Empty) continue;

                if (!v.StartsWith("#"))
                {
                    string[] vs = v.Split('|');

                    /* Format: [0] <CurrentVersion>
                               [1] <RootUpdateUrl>
                               [2] <ChangeLogFile> */

                    newest_version = new Version(vs[0]);
                    updateVersion = (Version)newest_version.Clone();
                    updateChangelog = vs[1] + vs[2];

                    if (newest_version > current_version) updateAvailable = true;
                    else if (newest_version == current_version) updateAvailable = false;
                    else updateAvailable = false; //could have a pre-release version...
                }
                else continue;
            }

            wc.Dispose();
        }

        public static void DownloadUpdate()
        {
            if (!updateAvailable)
            {
                throw new Exception("No new update available.");
            }
        }

        public static void ShowUpdateDialog()
        {
            WebClient wc = new WebClient();

            FrmUpdate frmUpdate = new FrmUpdate();
            frmUpdate.SetDetails(updateVersion);
            frmUpdate.SetChangeLog(wc.DownloadString(updateChangelog));
            frmUpdate.ShowDialog();

            wc.Dispose();
        }
	}
}