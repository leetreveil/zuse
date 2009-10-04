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
        private string title;
        private string artist;
        private string album;
        private float length;
        private string zuneid;
        
        public Song()
        {
            this.title = string.Empty;
            this.artist = string.Empty;
            this.album = string.Empty;
            this.zuneid = string.Empty;
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

        public Song(string title, string artist, string album)
        {
            this.title = title;
            this.artist = artist;
            this.album = album;
            this.zuneid = string.Empty;
        }

        public override string ToString()
        {
            return string.Format("{0:s} - {1:s} - {2:s}", this.title, this.artist, this.album);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
    }
}