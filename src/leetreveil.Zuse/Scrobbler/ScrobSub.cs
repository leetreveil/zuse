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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Zuse.Core;

namespace Zuse.Scrobbler
{
    internal class ScrobSub
    {
        private const int kDefaultPort = 33367;
        private const int kLaunchWait = 60000; // in ms
        private const int kPortsToStep = 5;
        private const string mPluginId = "zuse";
        private Socket fd;
        private NetworkStream ns;
        private StreamReader sr;
        private StreamWriter sw;

        public ScrobSub()
        {
            SendMessageToClient("BOOTSTRAP c=" + mPluginId + "&" + "&u=");
        }

        private void Connect()
        {
            try
            {
                fd = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                fd.Connect(new IPEndPoint(IPAddress.Loopback, kDefaultPort));
                ns = new NetworkStream(fd);
                sw = new StreamWriter(ns);
                sr = new StreamReader(ns);
            }
            catch (Exception e)
            {
                Logger.Send(GetType(), LogLevel.Error, "Could not connect to Last.fm software", e);
            }
        }

        private void Shutdown()
        {
            ns.Close();
            sw.Close();
            sr.Close();
            fd.Shutdown(SocketShutdown.Both);
        }

        public void Start(string artist, string track, string album, int length, string filename)
        {
            string osCmd = "START c=" + mPluginId + "&" +
                           "a=" + Escape(artist) + "&" +
                           "t=" + Escape(track) + "&" +
                           "b=" + Escape(album) + "&" +
                           "m=" + string.Empty + "&" +
                           "l=" + length.ToString() + "&" +
                           "p=" + Escape(filename);

            SendMessageToClient(osCmd);
        }

        public void Start(string artist, string track, string album, float length, string filename)
        {
            Start(artist, track, album, (int) length, filename);
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
            byte[] bys = Encoding.ASCII.GetBytes(osCmd);

            string s = Encoding.UTF8.GetString(Encoding.Convert(Encoding.ASCII, Encoding.UTF8, bys));

            try
            {
                Connect();

                sw.WriteLine(osCmd);
                sw.Flush();

                Shutdown();
            }
            catch (Exception e)
            {
                Logger.Send(GetType(), LogLevel.Error, "Could not send messaege to Last.fm software", e);
            }
        }

        private string Escape(string str)
        {
            return str.Replace("&", "&&");
        }
    }
}