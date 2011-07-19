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
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using MicrosoftZunePlayback;
using ZuneUI;
using Zuse.Forms;
using Zuse.Scrobbler;
using Application=Microsoft.Iris.Application;

namespace Zuse.Core
{
    internal class Manager
    {
        private ZuneTrack currentTrack;
        private float lastTrackPosition;
        private Thread monitorTh;
        private ScrobSub scrobbler;
        private Thread zuneTh;

        public Manager()
        {
            scrobbler = new ScrobSub();
            currentTrack = new ZuneTrack();

            lastTrackPosition = 300f;
        }

        public void LaunchZune()
        {
            //TODO: what if we set zuse to run at startup and it starts before the lastfm client?
            //we could just let everything load because the scrobbler uses sockets and it isn't 
            //a requirement that the lastfm client is running before zuse is
            //if we go to scrobble a track and the client isn't running then we should warn the user
            if (!LastFM.IsClientRunning)
            {
                MessageBox.Show("Zuse couldn't detect Last.fm. Can you please make sure Last.fm is running before you start Zuse");
            }

            KillZune();

            zuneTh = new Thread(new ThreadStart(ZuneThread));
            zuneTh.Start();

            monitorTh = new Thread(new ThreadStart(MonitorThread));
            monitorTh.Start();
        }

        private void CloseZune_Do(object sender)
        {
            Application.Window.Close();
        }

        public void CloseZune()
        {
            Application.DeferredInvoke(new DeferredInvokeHandler(CloseZune_Do), null, DeferredInvokePriority.Normal);
        }

        public void ZuneThread()
        {
            ZuneApplication.Launch("", IntPtr.Zero);
            System.Windows.Forms.Application.Exit();
        }

        public void KillZune()
        {
            foreach (Process proc in Process.GetProcessesByName("Zune"))
            {
                string msg =
                    "The Zune software is already running without Zuse loaded, would you like to close the existing open instance of Zune?";

                if (MessageBox.Show(msg, "Zuse", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Logger.Send(GetType(), LogLevel.Info, "Killing Zune.exe process with PID of " + proc.Id.ToString());
                    proc.Kill();
                }
            }
        }

        private void ZuneWindow_DoShow(object sender)
        {
            Application.Window.AlwaysOnTop = true;
            Application.Window.WindowState = WindowState.Normal;
            Application.Window.AlwaysOnTop = false;
        }

        private void ZuneWindow_DoHide(object sender)
        {
            Application.Window.WindowState = WindowState.Minimized;
        }

        public void ShowZuneWindow()
        {
            Application.DeferredInvoke(new DeferredInvokeHandler(ZuneWindow_DoShow), null, DeferredInvokePriority.Normal);
        }

        public void HideZuneWindow()
        {
            Application.DeferredInvoke(new DeferredInvokeHandler(ZuneWindow_DoHide), null, DeferredInvokePriority.Normal);
        }

        private void ZuneWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var window = (Window) sender;

            if (e.PropertyName == "WindowState")
            {
                if (Properties.Settings.Default.MinimizeToTray)
                {
                    switch (window.WindowState)
                    {
                        case WindowState.Maximized:
                            window.ShowInTaskbar = true;
                            break;
                        case WindowState.Minimized:
                            window.ShowInTaskbar = false;
                            break;
                        case WindowState.Normal:
                            window.ShowInTaskbar = true;
                            break;
                    }
                }
            }
        }

        private void ZuneWindow_Setup(object sender)
        {
            Application.Window.PropertyChanged += new PropertyChangedEventHandler(ZuneWindow_PropertyChanged);
        }

        public float GetCurrentTrackPosition()
        {
            return TransportControls.Instance.CurrentTrackPosition;
        }

        private void UpdateClient()
        {
            string current_uri = PlayerInterop.Instance.CurrentUri;

            if (current_uri == null) return;

            switch (PlayerInterop.Instance.TransportState)
            {
                case MCTransportState.Playing:
                    ZuneTrack track = ZuneTrack.GetFromCurrentTrack();
                    if (track == null) return;
                    float theshold = GetCurrentTrackPosition() - lastTrackPosition;
                    if (theshold < 1 && theshold >= 0)
                    {
                        lastTrackPosition = 300f;
                        Logger.Send(GetType(), LogLevel.Info,
                                    string.Format("Playback resumed - {0:s}", track.ToString()));
                        scrobbler.Resume();
                        return;
                    }
                    else
                    {
                        if (!currentTrack.Equals(track))
                        {
                            scrobbler.Start(track.Artist, track.Title, track.Album, track.Length, current_uri);
                            currentTrack = track;
                        }
                        Logger.Send(GetType(), LogLevel.Info,
                                    string.Format("Playback started - {0:s}", track.ToString()));
                    }
                    break;
                case MCTransportState.Paused:
                    lastTrackPosition = GetCurrentTrackPosition();
                    Logger.Send(GetType(), LogLevel.Info, "Playback paused");
                    scrobbler.Pause();
                    break;
                case MCTransportState.Stopped:
                    Logger.Send(GetType(), LogLevel.Info, "Playback stopped");
                    scrobbler.Stop();
                    break;
                default:
                    break;
            }
        }

        private void ZunePlayer_UriSet(object sender, EventArgs e)
        {
            Logger.Send(GetType(), LogLevel.Info, "Playback URI changed to " + PlayerInterop.Instance.CurrentUri);
            UpdateClient();
        }

        private void ZunePlayer_StatusChanged(object sender, EventArgs e)
        {
            UpdateClient();
        }

        private void MonitorThread()
        {
            while (ZuneShell.DefaultInstance == null)
            {
                Thread.Sleep(500);
            }

            Application.DeferredInvoke(new DeferredInvokeHandler(ZuneWindow_Setup), null, DeferredInvokePriority.Normal);

            var foo = new EventHandler(ZunePlayer_StatusChanged);

            PlayerInterop.Instance.TransportStatusChanged += foo;
            PlayerInterop.Instance.UriSet += new EventHandler(ZunePlayer_UriSet);

            while (true)
            {
                if (ZuneShell.DefaultInstance == null)
                {
                    PlayerInterop.Instance.StatusChanged -= foo;
                    return;
                }

                Thread.Sleep(1000);
            }
        }
    }
}