﻿/*
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

using Zuse.Core;
using System.Timers;
using Timer = System.Timers.Timer;
using System;
using System.Diagnostics;

namespace Zuse.Core
{
    internal class TrackWatcher
    {
        private readonly Timer _timer;
        private ZuneTrack _currentTrack;
        private DateTime _timeStarted;

        public event Action<ScrobbleMe> TrackIsReadyToBeScrobbled = delegate { };
        public event Action<ZuneTrack> TrackHasStartedPlaying = delegate { };

        public TrackWatcher()
        {
            _timer = new Timer(5000);
            _timer.AutoReset = true;
            _timer.Elapsed += _timer_Elapsed;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_currentTrack == null) return;

            Debug.WriteLine("Timer elapsed!");
            //A track should only be scrobbled when the following conditions have been met:
            //The track must be longer than 30 seconds.
            //And the track has been played for at least half its duration, or for 4 minutes (whichever occurs earlier.)

            //We only want to scrobble this track if its the same as the one that started playing
            if (!ZuneTrack.GetFromCurrentTrack().Equals(_currentTrack))
                return;

            if (_currentTrack.Length >= 30f)
            {
                var trackPos = ZuneTrack.CurrentTrackPosition();

                if (trackPos >= (_currentTrack.Length / 2) || trackPos >= 240f)
                {
                    TrackIsReadyToBeScrobbled.Invoke(new ScrobbleMe { ZuneTrack = _currentTrack, TimeStarted = _timeStarted });
                    _timer.Stop();
                }
            }
        }

        public void Playing(ZuneTrack track)
        {
            _currentTrack = track;
            _timeStarted = DateTime.Now;
            _timer.Start();
            TrackHasStartedPlaying.Invoke(track);
        }

        public void Paused(ZuneTrack track)
        {
            _timer.Stop();
        }

        public void Stopped(ZuneTrack track)
        {
            _timer.Stop();
        }
    }
}
