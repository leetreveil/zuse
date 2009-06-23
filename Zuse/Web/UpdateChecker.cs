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
		private const string m_BaseUpdateCheckUrl = "http://zusefm.org/updates/version.txt";

		private ILog log;
		
		public UpdateChecker()
		{
            this.log = LogManager.GetLogger("Zuse", typeof(Zuse.Web.UpdateChecker));
		}

        public bool IsUpdateAvailable()
        {
            string current_version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            WebClient wc = new WebClient();
            string version_info = wc.DownloadString(m_BaseUpdateCheckUrl);
            string[] version_s = version_info.Split(new char[] {'\n'});

            foreach (string v in version_s)
            {
                string vs = v.Replace("\r", "");

                if (!vs.StartsWith("#"))
                {
                    string[] vxs = vs.Split('|');

                    /* Format: [0] <AppName>
                               [1] <CurrentVersion>
                               [2] <RootUpdateUrl>
                               [3] <ChangeLogPath>
                               [4] <UpdateInstallerPath> */


                }
                else continue;
            }

            return false;
        }

        public void DownloadUpdate()
        {
            if (!this.IsUpdateAvailable())
            {
                throw new Exception("No new update available.");
            }
        }
	}
}