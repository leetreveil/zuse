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
using System.Windows.Forms;

using log4net;

namespace Zuse.Utilities
{
    using Zuse.Core;
    using Zuse.Scrobbler;

    class MessageUtils
    {
        private static ILog log;

        static MessageUtils()
        {
            log = LogManager.GetLogger("Zuse", typeof(Zuse.Utilities.MessageUtils));
        }

        public static Song ParseMessageString(string ex)
        {
            try
            {
                string[] ps = ex.Split('\\');

                string song = ps[4].Substring(1);
                string artist = ps[5].Substring(1);
                string album = ps[6].Substring(1);

                return new Song(song, artist, album, false);
            }
            catch (Exception e)
            {
                log.Error(ex, e);

                return null;
            }
        }

        public static unsafe string GetMessageString(COPYDATASTRUCT cds)
        {
            string msg = string.Empty;

            unsafe
            {
                char* x = (char*)cds.data.ToPointer();

                for (int i = 0; i < cds.cbData; i++)
                {
                    msg += *x;
                    x++;
                }
            }

            return msg;
        }
    }
}
