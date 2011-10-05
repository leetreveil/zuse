using System;
using System.Collections.Generic;
using System.Text;
using Zuse.Core;
using System.Windows.Forms;
using Lpfm.LastFmScrobbler;
using leetreveil.Zuse.Properties;
using Lpfm.LastFmScrobbler.Api;
using System.Diagnostics;

namespace leetreveil.Zuse.Core
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
    }
}
