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
using Application=Microsoft.Iris.Application;
using leetreveil.Zuse.Core;
using leetreveil.Zuse.Properties;
using Lpfm.LastFmScrobbler;
using Lpfm.LastFmScrobbler.Api;

namespace Zuse.Core
{
    internal class Manager
    {
        private ZuneTrack _currentTrack;
        private float _lastTrackPosition;
        private Thread _monitorThread;
        private Thread _zuneThread;
        private TrackWatcher _trackWatcher;
        private Scrobbler _scrobbler;

        public Manager(Scrobbler scrobbler)
        {
            _scrobbler = scrobbler;
            _currentTrack = new ZuneTrack();
            _trackWatcher = new TrackWatcher();

            _trackWatcher.TrackIsReadyToBeScrobbled += new Action<ScrobbleMe>(_trackWatcher_TrackIsReadyToBeScrobbled);
            _trackWatcher.TrackHasStartedPlaying += new Action<ZuneTrack>(trackWatcher_TrackHasStartedPlaying);
        }

        void _trackWatcher_TrackIsReadyToBeScrobbled(ScrobbleMe scrobbleMe)
        {
            if (CheckForSession())
            {
                try
                {
                    var scrobTrack = new Track 
                    { 
                        TrackName = scrobbleMe.ZuneTrack.Title, 
                        ArtistName = scrobbleMe.ZuneTrack.Artist,
                        WhenStartedPlaying = scrobbleMe.TimeStarted, 
                        Duration = TimeSpan.FromSeconds(scrobbleMe.ZuneTrack.Length)
                    };

                    var resp = _scrobbler.Scrobble(scrobTrack);

                    Debug.WriteLine("successfully scrobbled: " + resp.Track.TrackName);
                    Logger.Send(GetType(), LogLevel.Info, "Successfully scrobbled: " + resp.Track.TrackName);
                }
                catch (LastFmApiException exception)
                {
                    if (exception.ErrorCode == 9) // re-authenticate
                    {
                        Reauthenticate();
                        return;
                    }

                    Logger.Send(GetType(), LogLevel.Error, exception.Message);
                }
                catch (InvalidOperationException exception)
                {
                    //occurs when the scrobble has been sent before it should have been
                    Logger.Send(GetType(), LogLevel.Warning, exception.Message);
                }
            }
        }

        void trackWatcher_TrackHasStartedPlaying(ZuneTrack track)
        {
            if (CheckForSession())
            {
                try
                {
                    var scrobTrack = new Track
                    { 
                        TrackName = track.Title, 
                        ArtistName = track.Artist, 
                        Duration = TimeSpan.FromSeconds(track.Length) 
                    };

                    var resp = _scrobbler.NowPlaying(scrobTrack);
                    Debug.WriteLine("successfully sent now playing: " + resp.Track.TrackName);
                }
                catch (LastFmApiException exception)
                {
                    if (exception.ErrorCode == 9) // re-authenticate
                    {
                        Reauthenticate();
                        return;
                    }

                    //log unsuccessfull now playing
                    Logger.Send(GetType(), LogLevel.Error, exception.Message);
                }
            }
        }

        private bool CheckForSession()
        {
            if (String.IsNullOrEmpty(Settings.Default.SessionKey))
            {
                try
                {
                    var sessionKey = _scrobbler.GetSession();
                    Settings.Default.SessionKey = sessionKey;
                    Settings.Default.Save();

                    return true;
                }
                catch (LastFmApiException exception)
                {
                    if (exception.ErrorCode == 9) // re-authenticate
                    {
                        Reauthenticate();
                        return false;
                    }

                    Logger.Send(GetType(), LogLevel.Error, exception.Message);
                    return false;
                }
            }

            return true;
        }

        private void Reauthenticate()
        {
            MessageBox.Show("We need to re-authorize Zuse with last.fm, " +
                "please follow the instructions in your web browser. Thank you.",
                "Zuse", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // get a url to authenticate this application
            string url = _scrobbler.GetAuthorisationUri();

            // open the URL in the default browser
            Process.Start(url);
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
                    _trackWatcher.Playing(track);
                    Logger.Send(GetType(), LogLevel.Info,
                                    string.Format("Playback started - {0:s}", track.ToString()));
                    break;
                case MCTransportState.Paused:
                    _trackWatcher.Paused(ZuneTrack.GetFromCurrentTrack());
                    _lastTrackPosition = GetCurrentTrackPosition();
                    Logger.Send(GetType(), LogLevel.Info, "Playback paused");
                    break;
                case MCTransportState.Stopped:
                    _trackWatcher.Stopped(ZuneTrack.GetFromCurrentTrack());
                    Logger.Send(GetType(), LogLevel.Info, "Playback stopped");
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