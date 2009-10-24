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
using System.Text;
using System.Xml;
using System.Windows.Forms;

using MicrosoftZuneLibrary;
using MicrosoftZunePlayback;
using ZuneUI;

namespace Zuse.Scrobbler
{
    using Zuse.Core;
    using Zuse.Properties;

    public class ZuneTrack
    {
        private string title;
        private string artist;
        private string album;
        private string zuneid;
        private string coverUrl;
        private float length;

        public string CoverUrl
        {
            get { return this.coverUrl; }
            set { this.coverUrl = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string Artist
        {
            get { return this.artist; }
            set { this.artist = value; }
        }

        public string Album
        {
            get { return this.album; }
            set { this.album = value; }
        }

        public float Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        public string ZuneID
        {
            get { return this.zuneid; }
            set { this.zuneid = value; }
        }

        public ZuneTrack()
        {
            this.title = string.Empty;
            this.artist = string.Empty;
            this.album = string.Empty;
            this.zuneid = string.Empty;
        }

        public ZuneTrack(string title, string artist, string album)
        {
            this.title = title;
            this.artist = artist;
            this.album = album;
            this.zuneid = string.Empty;
            this.coverUrl = string.Empty;
        }

        public override string ToString()
        {
            string fmt = (string)ZuseSettings.TrackDisplayFormat.Clone();
            string str = null;

            try
            {
                fmt = fmt.Replace("%artist%", "{0:s}");
                fmt = fmt.Replace("%album%", "{1:s}");
                fmt = fmt.Replace("%title%", "{2:s}");

                str = string.Format(fmt, this.artist, this.album, this.title);
                return str;
            }
            catch (Exception e)
            {
                Logger.Send(this.GetType(), LogLevel.Error, "An error occured while formatting the track display string", e);
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
                ZuneTrack s = (ZuneTrack)obj;
                return (s.Artist == this.artist) && (s.Album == this.album) && (s.Title == this.title);
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
                LibraryPlaybackTrack track = (LibraryPlaybackTrack)TransportControls.Instance.CurrentTrack;
                AlbumMetadata album = FindAlbumInfoHelper.GetAlbumMetadata(track.AlbumLibraryId);

                ZuneTrack s = new ZuneTrack(track.Title, album.AlbumArtist, album.AlbumTitle);
                s.Length = ZuneUI.TransportControls.Instance.CurrentTrackDuration;
                s.coverUrl = album.CoverUrl;

                return s;
            }
            else if (TransportControls.Instance.CurrentTrack is MarketplacePlaybackTrack)
            {
                MarketplacePlaybackTrack track = (MarketplacePlaybackTrack)TransportControls.Instance.CurrentTrack;

                ZuneTrack s = new ZuneTrack(track.Title, track.Artist, track.Album);
                s.Length = ZuneUI.TransportControls.Instance.CurrentTrackDuration;

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