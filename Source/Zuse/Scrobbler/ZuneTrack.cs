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
using MicrosoftZuneLibrary;
using ZuneUI;
using Zuse.Core;

namespace Zuse.Scrobbler
{
    public class ZuneTrack
    {
        private string album;
        private string artist;
        private string coverUrl;
        private float length;
        private string title;
        private string zuneid;

        public ZuneTrack()
        {
            title = string.Empty;
            artist = string.Empty;
            album = string.Empty;
            zuneid = string.Empty;
        }

        public ZuneTrack(string title, string artist, string album)
        {
            this.title = title;
            this.artist = artist;
            this.album = album;
            zuneid = string.Empty;
            coverUrl = string.Empty;
        }

        public string CoverUrl
        {
            get { return coverUrl; }
            set { coverUrl = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }

        public string Album
        {
            get { return album; }
            set { album = value; }
        }

        public float Length
        {
            get { return length; }
            set { length = value; }
        }

        public string ZuneID
        {
            get { return zuneid; }
            set { zuneid = value; }
        }

        public override string ToString()
        {
            var fmt = (string) ZuseSettings.TrackDisplayFormat.Clone();
            string str = null;

            try
            {
                fmt = fmt.Replace("%artist%", "{0:s}");
                fmt = fmt.Replace("%album%", "{1:s}");
                fmt = fmt.Replace("%title%", "{2:s}");

                str = string.Format(fmt, artist, album, title);
                return str;
            }
            catch (Exception e)
            {
                Logger.Send(GetType(), LogLevel.Error, "An error occured while formatting the track display string", e);
                return str;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ZuneTrack)
            {
                var s = (ZuneTrack) obj;
                return (s.Artist == artist) && (s.Album == album) && (s.Title == title);
            }
            else
            {
                return false;
            }
        }

        public static ZuneTrack GetFromCurrentTrack()
        {
            if (TransportControls.Instance.CurrentTrack is LibraryPlaybackTrack)
            {
                var track = (LibraryPlaybackTrack) TransportControls.Instance.CurrentTrack;
                AlbumMetadata album = FindAlbumInfoHelper.GetAlbumMetadata(track.AlbumLibraryId);

                var s = new ZuneTrack(track.Title, album.AlbumArtist, album.AlbumTitle);
                s.Length = TransportControls.Instance.CurrentTrackDuration;
                s.coverUrl = album.CoverUrl;

                return s;
            }
            else if (TransportControls.Instance.CurrentTrack is MarketplacePlaybackTrack)
            {
                var track = (MarketplacePlaybackTrack) TransportControls.Instance.CurrentTrack;

                var s = new ZuneTrack(track.Title, track.Artist, track.Album);
                s.Length = TransportControls.Instance.CurrentTrackDuration;

                return s;
            }
            else if (TransportControls.Instance.CurrentTrack is StreamingPlaybackTrack)
            {
            }
            else if (TransportControls.Instance.CurrentTrack is StreamingRadioPlaybackTrack)
            {
            }
            else if (TransportControls.Instance.CurrentTrack is VideoPlaybackTrack)
            {
            }

            return null;
        }
    }
}