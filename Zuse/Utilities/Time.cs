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

namespace Zuse.Utilities
{
    public class Time
    {
        public static long GetUnixTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        public static long GetUnixTimestamp(DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        public static string GetMinutesFromSeconds(int seconds)
        {
            int secs = seconds % 60;
            int mins = (seconds - secs) / 60;

            if (secs < 10) { return string.Format("{0:d}:0{1:d}", mins, secs); }
            else { return string.Format("{0:d}:{1:d}", mins, secs); }
        }
    }
}
