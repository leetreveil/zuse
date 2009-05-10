﻿/*
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
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

using log4net;

namespace Zuse.Scrobbler
{
    using Zuse.Core;

    class ScrobSub
    {
        private const int kDefaultPort = 33367;
        private const string mPluginId = "Zuse";
        private Socket fd;
        private NetworkStream ns;
        private StreamWriter sw;
        private StreamReader sr;

        private ILog log;

        public ScrobSub()
        {
            try
            {
                this.log = LogManager.GetLogger("Zuse", typeof(Zuse.Scrobbler.ScrobSub));

                fd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                fd.Connect(new IPEndPoint(IPAddress.Loopback, kDefaultPort));
                ns = new NetworkStream(fd);
                sw = new StreamWriter(ns);
                sr = new StreamReader(ns);
            }
            catch (Exception e)
            {
                this.log.Error("Could not connect to Last.fm software", e);
            }
        }

        public void Start(string artist, string track, string album, string mbId, int length, string filename)
        {
            string osCmd = "START c=" + mPluginId + "&" +
                           "a=" + Escape(artist) + "&" +
                           "t=" + Escape(track) + "&" +
                           "b=" + Escape(album) + "&" +
                           "m=" + Escape(mbId) + "&" +
                           "l=" + length.ToString() + "&" +
                           "p=" + Escape(filename);

            SendMessageToClient(osCmd);
        }

        public void Stop()
        {
            string osCmd = "STOP c=" + mPluginId;

            SendMessageToClient(osCmd);
        }

        public void Pause()
        {
            string osCmd = "PAUSE c=" + mPluginId;

            SendMessageToClient(osCmd);
        }

        public void Resume()
        {
            string osCmd = "RESUME c=" + mPluginId;

            SendMessageToClient(osCmd);
        }

        private void SendMessageToClient(string osCmd)
        {
            try
            {
                sw.WriteLine(osCmd);
                sw.Flush();
            }
            catch (Exception e)
            {
                this.log.Error("Could not send message to Last.fm software", e);
            }
        }

        private string Escape(string str)
        {
            return str.Replace("&", "&&");
        }
    }
}
