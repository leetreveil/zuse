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
using Application=Microsoft.Iris.Application;
using Zuse.Properties;
using Lpfm.LastFmScrobbler;
using Lpfm.LastFmScrobbler.Api;

namespace Zuse.Core
{
    internal class ZuneManager
    {
        private Thread _monitorThread;
        private Thread _zuneThread;
        private readonly ZuneTrack _currentTrack;
        private readonly TrackWatcher _trackWatcher;

        public ZuneManager(Scrobbler scrobbler)
        {
            _currentTrack = new ZuneTrack();
            _trackWatcher = new TrackWatcher();
            var scrobbleManager = new ScrobbleManager(scrobbler);

            _trackWatcher.TrackIsReadyToBeScrobbled += scrobbleManager.Scrobble;
            _trackWatcher.TrackHasStartedPlaying += scrobbleManager.SubmitNowPlaying;
        }

        public void LaunchZune()
        {
            KillZune();

            _zuneThread = new Thread(new ThreadStart(ZuneThread));
            _zuneThread.Start();

            _monitorThread = new Thread(new ThreadStart(MonitorThread));
            _monitorThread.Start();
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
                    proc.Kill();
                }
            }
        }


        private void UpdateClient()
        {
            string current_uri = PlayerInterop.Instance.CurrentUri;
            if (current_uri == null) return;

            switch (PlayerInterop.Instance.TransportState)
            {
                case MCTransportState.Playing:
                    ZuneTrack track = ZuneTrack.GetFromCurrentTrack();
                    _trackWatcher.Playing(track);
                    break;
                case MCTransportState.Paused:
                    _trackWatcher.Paused(ZuneTrack.GetFromCurrentTrack());
                    break;
                case MCTransportState.Stopped:
                    _trackWatcher.Stopped(ZuneTrack.GetFromCurrentTrack());
                    break;
                default:
                    break;
            }
        }

        private void ZunePlayer_UriSet(object sender, EventArgs e)
        {
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