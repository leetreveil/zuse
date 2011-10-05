/*
 * Zuse - A Zune Last.fm plugin
 * Copyright (C) 2011 Lee Treveil
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
using Zuse.Core;
using System.Windows.Forms;
using Lpfm.LastFmScrobbler;
using Zuse.Properties;
using Lpfm.LastFmScrobbler.Api;
using System.Diagnostics;

namespace Zuse.Core
{
    internal class ScrobbleManager
    {
        private readonly Scrobbler _scrobbler;

        public ScrobbleManager(Scrobbler scrobbler)
        {
            _scrobbler = scrobbler;
        }

        public void Scrobble(ScrobbleMe scrobbleMe)
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
                    Logger.Send(LogLevel.Info, "Successfully scrobbled: " + resp.Track.TrackName);
                }
                catch (LastFmApiException exception)
                {
                    if (exception.ErrorCode == 9) // re-authenticate
                    {
                        Reauthenticate();
                        return;
                    }

                    Logger.Send(LogLevel.Error, "Scrobble was unsuccessful", exception);
                }
                catch (InvalidOperationException exception)
                {
                    //occurs when the scrobble has been sent before it should have been
                    Logger.Send(LogLevel.Error, "Scrobble was unsuccessful", exception);
                }
            }
        }

        public void SubmitNowPlaying(ZuneTrack track)
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
                    Logger.Send(LogLevel.Error, "Submit now playing was unsuccessful", exception);
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

                    Logger.Send(LogLevel.Error, "Unable to get session", exception);
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
    }
}
