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
using System.Net;
using System.Reflection;
using System.Xml;
using leetreveil.Zuse.Core;
using leetreveil.Zuse.Forms;

namespace leetreveil.Zuse.Web
{
    public class UpdateChecker
    {
        private static readonly Version m_currentVersion;
        private static readonly WebClient m_webClient;
        private static string m_baseUpdateUrl = "http://zusefm.org/updates/";
        private static string m_changelogFile = "changelog.php";
        private static string m_downloadUrl = "";
        private static Version m_latestVersion;
        private static string m_updateCheckFile = "check.php";

        static UpdateChecker()
        {
            m_currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

            m_webClient = new WebClient();
            m_webClient.Headers.Add("ZuseVersion", m_currentVersion.ToString());

            m_latestVersion = null;
        }

        public static bool UpdateAvailable
        {
            get
            {
                if (m_latestVersion > m_currentVersion) return true;
                else return false;
            }
        }

        public static Version LatestVersion
        {
            get { return m_latestVersion; }
        }

        public static void Check()
        {
            var xdoc = new XmlDocument();

            try
            {
                string updateXml = m_webClient.DownloadString(m_baseUpdateUrl + m_updateCheckFile);
                xdoc.LoadXml(updateXml);

                Version v1 = m_currentVersion;

                foreach (XmlNode node in xdoc.GetElementsByTagName("Update"))
                {
                    var v = new Version(node.Attributes["Version"].Value);

                    if (v1 < v)
                    {
                        v1 = v;
                        m_downloadUrl = node.Attributes["Root"].Value;
                    }
                    else continue;
                }

                m_latestVersion = v1;
            }
            catch (Exception e)
            {
                Logger.Send(typeof (UpdateChecker), LogLevel.Error, e.Message, e);
            }
        }
    }
}