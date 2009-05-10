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

namespace Zuse.Scrobbler
{
    using Zuse.Core;
    using Zuse.Properties;

    public class Song
    {
        public Song()
        {
        }

        private string title;
        private string artist;
        private string album;
        private int length;
        private string mbid;

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

        public int Length
        {
            get { return this.length; }
            set { this.length = value; }
        }

        public string MBID
        {
            get { return this.mbid; }
            set { this.mbid = value; }
        }

        public void FetchSongLength()
        {
            MusicBrainz.Query<MusicBrainz.Track> tracks = MusicBrainz.Track.Query(this.title, this.artist, this.album);

            if (tracks.Count > 0)
            {
                this.mbid = tracks[0].Id;
                this.length = (int)tracks[0].GetDuration().TotalSeconds;
            }
        }

        public Song(string title, string artist, string album, bool autoFetchLength)
        {
            this.title = title;
            this.artist = artist;
            this.album = album;

            if (autoFetchLength)
            {
                this.FetchSongLength();
            }

            this.mbid = "";
        }

        public override string ToString()
        {
            return string.Format("{0:s} - {1:s}", this.title, this.artist);
        }

        public override bool Equals(object obj)
        {
            if (obj is Song)
            {
                Song s = (Song)obj;

                return (s.Artist == this.artist) && (s.Album == this.album) && (s.Title == this.title);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}