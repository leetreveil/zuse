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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Windows.Forms;

using log4net;

namespace Zuse.Core
{
    public static class ZuseSettings
    {
        public class StringCollection : System.Collections.CollectionBase
        {
            public StringCollection()
            {
            }

            public void Add(string str)
            {
                this.List.Add(str);
            }

            public void Remove(string str)
            {
                this.List.Remove(str);
            }

            public bool Contains(string str)
            {
                return this.List.Contains(str);
            }

            public void Join(StringCollection strcol)
            {
                foreach (string str_x in strcol)
                {
                    this.List.Add(str_x);
                }
            }

            public override string ToString()
            {
                string output = string.Empty;

                foreach (string str in this.List)
                {
                    output += str + ";";
                }

                if (output != string.Empty)
                {
                    return output.Remove(output.Length - 1, 1);
                }
                else return string.Empty;
            }

            public static StringCollection FromString(string str)
            {
                StringCollection output = new StringCollection();
                string[] str_s = str.Split(';');

                foreach (string str_x in str_s)
                {
                    if (str_x != string.Empty) output.Add(str_x);
                }

                return output;
            }
        }

        private static ILog log;
        // Where the settings file is:
        private static string m_settingsFile;
        // These are actually settings:
        private static bool m_debugMode;
        private static bool m_minimizeToTray;
        private static bool m_checkForUpdates;
        private static bool m_useGrowl;
        private static StringCollection m_updateSkipVersions;

        public static bool DebugMode
        {
            get { return m_debugMode; }
            set { m_debugMode = value; }
        }

        public static bool MinimizeToTray
        {
            get { return m_minimizeToTray; }
            set { m_minimizeToTray = value; }
        }

        public static bool CheckForUpdates
        {
            get { return m_checkForUpdates; }
            set { m_checkForUpdates = value; }
        }

        public static bool UseGrowl
        {
            get { return m_useGrowl; }
            set { m_useGrowl = value; }
        }

        public static StringCollection UpdateSkipVersions
        {
            get { return m_updateSkipVersions; }
            set { m_updateSkipVersions = value; }
        }

        static ZuseSettings()
        {
            log = LogManager.GetLogger("Zuse", typeof(Zuse.Core.ZuseSettings));

            m_settingsFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            m_settingsFile += "\\Zuse\\Settings.xml";

            m_debugMode = true;
            m_minimizeToTray = false;
            m_checkForUpdates = true;
            m_useGrowl = false;
            m_updateSkipVersions = new StringCollection();
        }

        public static void Load()
        {
            try
            {
                XmlTextReader x = new XmlTextReader(m_settingsFile);
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(x);

                // Load version of settings file:
                //m_version = xdoc.GetElementsByTagName("Version")[0].InnerText;

                // Now load the actual settings:
                foreach (XmlNode node in xdoc.GetElementsByTagName("Setting"))
                {
                    switch (node.Attributes["Name"].Value)
                    {
                        case "DebugMode":
                            m_debugMode = bool.Parse(node.Attributes["Value"].Value);
                            break;
                        case "MinimizeToTray":
                            m_minimizeToTray = bool.Parse(node.Attributes["Value"].Value);
                            break;
                        case "CheckForUpdates":
                            m_checkForUpdates = bool.Parse(node.Attributes["Value"].Value);
                            break;
                        case "UseGrowl":
                            m_useGrowl = bool.Parse(node.Attributes["Value"].Value);
                            break;
                        case "UpdateSkipVersions":
                            m_updateSkipVersions = StringCollection.FromString(node.Attributes["Value"].Value);
                            break;
                        default: continue;
                    }
                }

                x.Close();
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }
        }

        public static void Save()
        {
            try
            {
                XmlWriterSettings xsets = new XmlWriterSettings();
                xsets.Indent = true;
                xsets.IndentChars = "  ";

                XmlWriter xw = XmlWriter.Create(m_settingsFile, xsets);

                xw.WriteStartDocument();
                xw.WriteStartElement("Zuse");

                xw.WriteElementString("Version", Assembly.GetExecutingAssembly().GetName().Version.ToString());

                xw.WriteStartElement("Settings");

                xw.WriteStartElement("Setting");
                xw.WriteAttributeString("Name", "DebugMode");
                xw.WriteAttributeString("Type", "Boolean");
                xw.WriteAttributeString("Value", m_debugMode.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("Setting");
                xw.WriteAttributeString("Name", "MinimizeToTray");
                xw.WriteAttributeString("Type", "Boolean");
                xw.WriteAttributeString("Value", m_minimizeToTray.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("Setting");
                xw.WriteAttributeString("Name", "CheckForUpdates");
                xw.WriteAttributeString("Type", "Boolean");
                xw.WriteAttributeString("Value", m_checkForUpdates.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("Setting");
                xw.WriteAttributeString("Name", "UseGrowl");
                xw.WriteAttributeString("Type", "Boolean");
                xw.WriteAttributeString("Value", m_useGrowl.ToString());
                xw.WriteEndElement();

                xw.WriteStartElement("Setting");
                xw.WriteAttributeString("Name", "UpdateSkipVersions");
                xw.WriteAttributeString("Type", "StringCollection");
                xw.WriteAttributeString("Value", m_updateSkipVersions.ToString());
                xw.WriteEndElement();

                xw.WriteEndElement();
                xw.WriteEndElement();

                xw.WriteEndDocument();

                xw.Flush();
                xw.Close();
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }
        }
    }
}
