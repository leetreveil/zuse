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

namespace Zuse.Core
{
    using Zuse.Properties;
    using Zuse.Scrobbler;
    using Zuse.Utilities;
    using Zuse.Forms;

    class Manager
    {
        private float currentSongPosition;
        private Song currentSong;
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

        private void Zune_PlayingNewTrack(object sender, Song song, float pos, float length)
        {
            if (!currentSong.Equals(song))
            {
                this.scrobbler.Start(song.Artist, song.Title, song.Album, song.MBID, (int)length, "");

                log.Info(string.Format("Starting song '{0:s}'", song.ToString()));

                currentSong = song;
                currentSongPosition = pos;
            }
        }

        private void Zune_PlaybackStopped(object sender, EventArgs e)
        {
            log.Info("Stopped playing");

            this.scrobbler.Stop();
        }

        public void Launch()
        {
            ClientLoader cl = new ClientLoader();
            if (cl.IsAvailable()) cl.Open();

            this.KillZune();

            this.zuneTh = new Thread(new ThreadStart(this.ZuneThread));
            this.zuneTh.Start();

            this.monitorTh = new Thread(new ThreadStart(this.MonitorThread));
            this.monitorTh.Start();
        }

        public void ZuneThread()
        {
            Microsoft.Zune.Shell.ZuneApplication.Launch("", IntPtr.Zero);

            this.monitorTh.Abort();
        }

        public void KillZune()
        {
            foreach (Process proc in Process.GetProcessesByName("Zune"))
            {
                log.Info("Killing existing Zune.exe process with PID of " + proc.Id.ToString());
                proc.Kill();
            }
        }

        private void MonitorThread()
        {
            Thread.Sleep(2000);

            ZuneUI.TransportControls.Instance.PlaybackStopped += new EventHandler(Zune_PlaybackStopped);

            while (true)
            {
                if (ZuneUI.TransportControls.Instance.Playing)
                {
                    if (ZuneUI.TransportControls.Instance.CurrentTrack.IsInCollection)
                    {
                        ZuneUI.LibraryPlaybackTrack track = (ZuneUI.LibraryPlaybackTrack)ZuneUI.TransportControls.Instance.CurrentTrack;

                        MicrosoftZuneLibrary.AlbumMetadata album = ZuneUI.FindAlbumInfoHelper.GetAlbumMetadata(track.AlbumLibraryId);

                        this.Zune_PlayingNewTrack(this, new Song(track.Title, album.AlbumArtist, album.AlbumTitle), ZuneUI.TransportControls.Instance.CurrentTrackPosition, ZuneUI.TransportControls.Instance.CurrentTrackDuration);
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}