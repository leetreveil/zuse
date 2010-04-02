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
using System.Threading;
using Microsoft.Iris;
using MicrosoftZunePlayback;

namespace Zuse.Core
{
    using Zuse.Scrobbler;
    using Zuse.Forms;

    class Manager
    {
        private ZuneTrack currentTrack;
        private float lastTrackPosition;
        private Thread monitorTh;
        private ScrobSub scrobbler;
        private FrmDebug frmDebug;

        public Manager()
        {
            this.frmDebug = new FrmDebug();

            this.scrobbler = new ScrobSub();

            this.currentTrack = new ZuneTrack();

            this.lastTrackPosition = 300f;
        }

        public void ShowDebugWindow()
        {
            this.frmDebug = new FrmDebug();
            this.frmDebug.Show();
        }
        
        public void RefreshDebugView()
        {
            this.frmDebug.RefreshView();
        }

        public void LaunchZune()
        {
            ClientLoader cl = new ClientLoader();
            if (cl.IsAvailable())
            {
                if (!cl.IsOpen()) cl.Open();
            }

            this.monitorTh = new Thread(new ThreadStart(this.MonitorThread));
            this.monitorTh.Start();
        }

        private void CloseZune_Do(object sender)
        {
            Microsoft.Iris.Application.Window.Close();
        }

        public void CloseZune()
        {
            this.scrobbler.Stop();

            Microsoft.Iris.Application.DeferredInvoke(new DeferredInvokeHandler(CloseZune_Do), null, DeferredInvokePriority.Normal);
        }

        public void ZuneThread()
        {
            Microsoft.Zune.Shell.ZuneApplication.Launch("", IntPtr.Zero);

            Growler.Notify("Program Closing", "Zune", "Zune is closing");

            System.Windows.Forms.Application.Exit();
        }

        private void ZuneWindow_DoShow(object sender)
        {
            Microsoft.Iris.Application.Window.AlwaysOnTop = true;
            Microsoft.Iris.Application.Window.WindowState = WindowState.Normal;
            Microsoft.Iris.Application.Window.AlwaysOnTop = false;
        }

        private void ZuneWindow_DoHide(object sender)
        {
            Microsoft.Iris.Application.Window.WindowState = WindowState.Minimized;
        }

        public void ShowZuneWindow()
        {
            Microsoft.Iris.Application.DeferredInvoke(new DeferredInvokeHandler(ZuneWindow_DoShow), null, DeferredInvokePriority.Normal);
        }

        public void HideZuneWindow()
        {
            Microsoft.Iris.Application.DeferredInvoke(new DeferredInvokeHandler(ZuneWindow_DoHide), null, DeferredInvokePriority.Normal);
        }

        private void ZuneWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Window window = (Window)sender;

            if (e.PropertyName == "WindowState")
            {
                if (ZuseSettings.MinimizeToTray)
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
            Growler.Notify("Program Opening", "Zune", "Zune is opening");

            Microsoft.Iris.Application.Window.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ZuneWindow_PropertyChanged);
        }

        public float GetCurrentTrackPosition()
        {
            return ZuneUI.TransportControls.Instance.CurrentTrackPosition;
        }

        private void UpdateClient()
        {
            string current_uri = MicrosoftZunePlayback.PlayerInterop.Instance.CurrentUri;

            if (current_uri == null) return;

            switch (MicrosoftZunePlayback.PlayerInterop.Instance.TransportState)
            {
                case MicrosoftZunePlayback.MCTransportState.Playing:
                    ZuneTrack track = ZuneTrack.GetFromCurrentTrack();
                    if (track == null) return;
                    float theshold = this.GetCurrentTrackPosition() - this.lastTrackPosition;
                    if (theshold < 1 && theshold >= 0)
                    {
                        this.lastTrackPosition = 300f;
                        Logger.Send(this.GetType(), LogLevel.Info, string.Format("Playback resumed - {0:s}", track.ToString()));
                        this.scrobbler.Resume();
                        return;
                    }
                    else
                    {
                        if (!currentTrack.Equals(track))
                        {
                            this.scrobbler.Start(track.Artist, track.Title, track.Album, track.Length, current_uri);
                            currentTrack = track;
                        }
                        Logger.Send(this.GetType(), LogLevel.Info, string.Format("Playback started - {0:s}", track.ToString()));
                    }
                    Growler.Notify("Now Playing", "Now Playing", track.ToString(), track.CoverUrl);
                    break;
                case MicrosoftZunePlayback.MCTransportState.Paused:
                    this.lastTrackPosition = this.GetCurrentTrackPosition();
                    Logger.Send(this.GetType(), LogLevel.Info, "Playback paused");
                    this.scrobbler.Pause();
                    break;
                case MicrosoftZunePlayback.MCTransportState.Stopped:
                    Logger.Send(this.GetType(), LogLevel.Info, "Playback stopped");
                    this.scrobbler.Stop();
                    break;
                default:
                    break;
            }
        }

        private void ZunePlayer_UriSet(object sender, EventArgs e)
        {
            //Logger.Send(this.GetType(), LogLevel.Info, "Playback track changed to " + ZuneTrack.GetFromCurrentTrack().ToString());
            Logger.Send(this.GetType(), LogLevel.Info, "Playback URI changed to " + PlayerInterop.Instance.CurrentUri);

            UpdateClient();
        }

        private void ZunePlayer_StatusChanged(object sender, EventArgs e)
        {
            UpdateClient();
        }

        private void MonitorThread()
        {
            while (ZuneUI.ZuneShell.DefaultInstance == null)
            {
                Thread.Sleep(500);
            }

            Microsoft.Iris.Application.DeferredInvoke(new DeferredInvokeHandler(ZuneWindow_Setup), null, DeferredInvokePriority.Normal);

            EventHandler foo = new EventHandler(ZunePlayer_StatusChanged);

            PlayerInterop.Instance.TransportStatusChanged += foo;
            PlayerInterop.Instance.UriSet += new EventHandler(ZunePlayer_UriSet);

            while (true)
            {
                if (ZuneUI.ZuneShell.DefaultInstance == null)
                {
                    MicrosoftZunePlayback.PlayerInterop.Instance.StatusChanged -= foo;
                    return;
                }

                Thread.Sleep(1000);
            }
        }
    }
}