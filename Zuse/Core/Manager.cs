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
using System.Threading;
using System.Windows.Forms;

using log4net;

using Microsoft.Iris;
using ZuneUI;
using MicrosoftZuneLibrary;

namespace Zuse.Core
{
    using Zuse.Properties;
    using Zuse.Scrobbler;
    using Zuse.Utilities;
    using Zuse.Forms;

    class Manager
    {
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

        public void Launch()
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

        private void ZuneNoShow(object sender)
        {
        }

        public void NotifyClient()
        {
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
                    }
                    log.Info(string.Format("Playback started - '{0:s}'", song.ToString()));
                    break;
                case MicrosoftZunePlayback.MCTransportState.Paused:
                    this.lastSongPosition = this.GetCurrentSongPosition();
                    log.Info("Playback paused");
                    this.scrobbler.Pause();
                    break;
                case MicrosoftZunePlayback.MCTransportState.Stopped:
                    log.Info("Playback stopped");
                    //this.scrobbler.Stop();
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

            Microsoft.Iris.Application.DeferredInvoke(new DeferredInvokeHandler(ZuneNoShow), null, DeferredInvokePriority.Normal);

            MicrosoftZunePlayback.PlayerInterop.Instance.TransportStatusChanged += foo;

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