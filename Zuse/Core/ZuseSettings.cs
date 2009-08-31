﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

using System.Windows.Forms;

namespace Zuse.Core
{
    public static class ZuseSettings
    {
        // Where the settings file is:
        private static string m_settingsFile;
        // These are actually settings:
        private static bool m_debugMode;

        public static bool DebugMode
        {
            get { return m_debugMode; }
            set { m_debugMode = value; }
        }

        static ZuseSettings()
        {
            m_settingsFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            m_settingsFile += "\\Zuse\\Settings.xml";

            m_debugMode = true;
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
                        default: continue;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
                xw.WriteAttributeString("Value", "True");
                xw.WriteEndElement();

                xw.WriteEndElement();
                xw.WriteEndElement();

                xw.WriteEndDocument();

                xw.Flush();
                xw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
