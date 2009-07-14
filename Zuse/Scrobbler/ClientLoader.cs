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
using System.Text;
using System.Xml;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Zuse.Scrobbler
{
    using Zuse.Core;
    using Zuse.Properties;

    public class ClientLoader
    {
        private const string m_Key32 = @"SOFTWARE\Last.fm\Client";
        private const string m_Key64 = @"SOFTWARE\Wow6432Node\Last.fm\Client";

        private string m_Key = null;

        public ClientLoader()
        {
            if (!Win32Wrapper.Is64Bit()) m_Key = m_Key32;
            else m_Key = m_Key64;
        }

        private Process GetClientProcess()
        {
            string clientPath = GetClientPath();

            if (clientPath == null) return null;
            else
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    if (proc.ProcessName == Path.GetFileNameWithoutExtension(clientPath))
                    {
                        return proc;
                    }
                    else continue;
                }

                return null;
            }
        }
        
        private string GetClientPath()
        {
            if (!this.IsAvailable()) return null;
            else
            {
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(m_Key);

                if (rk != null)
                {
                    return (string)rk.GetValue("Path");
                }
                else return null;
            }
        }

        public bool IsOpen()
        {
            return (this.GetClientProcess() != null);
        }
        
        public bool IsAvailable()
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(m_Key);

            if (rk != null)
            {
                return true;
            }
            else return false;
        }

        public void Open()
        {
            if (this.IsAvailable())
            {
                Process.Start(this.GetClientPath());
            }
            else throw new Exception("Last.fm client software not available!");
        }

        public void Kill()
        {
            Process clientProc = this.GetClientProcess();

            if (clientProc != null)
            {
                clientProc.Kill();
            }
            else throw new Exception("Last.fm client software not open!");
        }
    }
}
