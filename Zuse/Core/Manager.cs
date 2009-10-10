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
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using log4net;
using Microsoft.Iris;
using ZuneUI;
using MicrosoftZuneLibrary;
using MicrosoftZunePlayback;

using Growl.Connector;

namespace Zuse.Core
{
    using Zuse.Properties;
    using Zuse.Scrobbler;
    using Zuse.Forms;

    class Manager
    {
        private Growl.Connector.Application growlZuseApp;
        private GrowlConnector growl;
        private Song currentSong;
        private float lastSongPosition;
        private Thread monitorTh;
        private Thread zuneTh;
        private ScrobSub scrobbler;
        private FrmDebug frmDebug;
        private ILog log;

        public Manager()
        {
            this.log = LogManager.GetLogger("Zuse", typeof(Zuse.Core.Manager));

            this.frmDebug = new FrmDebug();

            this.scrobbler = new ScrobSub();

            this.currentSong = new Song();

            this.lastSongPosition = 300f;

            this.InitGrowler();
        }

        public void InitGrowler()
        {
            this.growl = new GrowlConnector();

            if (growl.IsGrowlRunning())
            {
                this.log.Info("Growl is running");

                this.growlZuseApp = new Growl.Connector.Application("Zune");
                this.growlZuseApp.Icon = Icon.ExtractAssociatedIcon("Zune.exe").ToBitmap();
                
                NotificationType notifyTypeOpen = new NotificationType("Zune Opening");
                NotificationType notifyTypeClose = new NotificationType("Zune Closing");
                NotificationType notifyTypePlaying = new NotificationType("Zune Now Playing");
                NotificationType[] notificationTypes = new NotificationType[] { notifyTypeOpen, notifyTypeClose, notifyTypePlaying };

                this.growl.Register(this.growlZuseApp, notificationTypes);
            }
            else
            {
                this.log.Info("Growl is not running, disabling support temporarily.");
            }
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

            this.KillZune();

            this.zuneTh = new Thread(new ThreadStart(this.ZuneThread));
            this.zuneTh.Start();

            this.monitorTh = new Thread(new ThreadStart(this.MonitorThread));
            this.monitorTh.Start();
        }

        private void CloseZune_Do(object sender)
        {
            Microsoft.Iris.Application.Window.Close();
        }

        public void CloseZune()
        {
            if (ZuseSettings.UseGrowl && this.growlZuseApp != null)
            {
                this.growl.Notify(new Growl.Connector.Notification(this.growlZuseApp.Name, "Zune Closing", "0", "Zune", "Zune is closing"));
            }
            Microsoft.Iris.Application.DeferredInvoke(new DeferredInvokeHandler(CloseZune_Do), null, DeferredInvokePriority.Normal);
        }

        public void ZuneThread()
        {
            Microsoft.Zune.Shell.ZuneApplication.Launch("", IntPtr.Zero);

            System.Windows.Forms.Application.Exit();
        }

        public void KillZune()
        {
            foreach (Process proc in Process.GetProcessesByName("Zune"))
            {
                log.Info("Killing Zune.exe process with PID of " + proc.Id.ToString());
                proc.Kill();
            }
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
            if (ZuseSettings.UseGrowl && this.growlZuseApp != null)
            {
                this.growl.Notify(new Growl.Connector.Notification(this.growlZuseApp.Name, "Zune Opening", "0", "Zune", "Zune is opening"));
            }

            Microsoft.Iris.Application.Window.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ZuneWindow_PropertyChanged);
        }

        public Song GetCurrentSong()
        {
            LibraryPlaybackTrack track = (LibraryPlaybackTrack)TransportControls.Instance.CurrentTrack;

            AlbumMetadata album = FindAlbumInfoHelper.GetAlbumMetadata(track.AlbumLibraryId);

            Song s = new Song(track.Title, album.AlbumArtist, album.AlbumTitle);
            s.Length = ZuneUI.TransportControls.Instance.CurrentTrackDuration;

            return s;
        }

        public float GetCurrentSongPosition()
        {
            return ZuneUI.TransportControls.Instance.CurrentTrackPosition;
        }

        private void ZunePlayer_StatusChanged(object sender, EventArgs e)
        {
            string current_uri = MicrosoftZunePlayback.PlayerInterop.Instance.CurrentUri;

            if (current_uri == null) return;

            switch (MicrosoftZunePlayback.PlayerInterop.Instance.TransportState)
            {
                case MicrosoftZunePlayback.MCTransportState.Playing:
                    Song song = GetCurrentSong();
                    float theshold = this.GetCurrentSongPosition() - this.lastSongPosition;
                    if (theshold < 1 && theshold >= 0)
                    {
                        this.lastSongPosition = 300f;
                        log.Info(string.Format("Playback resumed - '{0:s}'", song.ToString()));
                        this.scrobbler.Resume();
                        return;
                    }
                    else
                    {
                        if (!currentSong.Equals(song))
                        {
                            this.scrobbler.Start(song.Artist, song.Title, song.Album, song.Length, current_uri);
                            currentSong = song;
                        }
                        log.Info(string.Format("Playback started - '{0:s}'", song.ToString()));
                    }
                    if (ZuseSettings.UseGrowl && this.growlZuseApp != null)
                    {
                        this.growl.Notify(new Growl.Connector.Notification(this.growlZuseApp.Name, "Zune Now Playing", "0", "Now Playing", song.ToString()));
                    }
                    break;
                case MicrosoftZunePlayback.MCTransportState.Paused:
                    this.lastSongPosition = this.GetCurrentSongPosition();
                    log.Info("Playback paused");
                    this.scrobbler.Pause();
                    break;
                case MicrosoftZunePlayback.MCTransportState.Stopped:
                    log.Info("Playback stopped");
                    this.scrobbler.Stop();
                    break;
                default:
                    break;
            }
        }

        private void MonitorThread()
        {
            while (ZuneUI.ZuneShell.DefaultInstance == null)
            {
                Thread.Sleep(500);
            }

            EventHandler foo = new EventHandler(ZunePlayer_StatusChanged);

            Microsoft.Iris.Application.DeferredInvoke(new DeferredInvokeHandler(ZuneWindow_Setup), null, DeferredInvokePriority.Normal);

            PlayerInterop.Instance.TransportStatusChanged += foo;

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