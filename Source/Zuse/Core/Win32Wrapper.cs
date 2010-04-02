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
using System.Diagnostics;

namespace Zuse.Core
{
    public static class Win32Wrapper
    {
        #region Windows 64-bit Programatic Detection

        /* Windows 64-bit Programatic Detection code pulled from:
         *   http://stackoverflow.com/questions/336633/how-to-detect-windows-64-bit-platform-with-net/681542#681542 */

        public static bool Is64Bit()
        {
            if (IntPtr.Size == 8 || (IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Is32BitProcessOn64BitProcessor()
        {
            bool retVal;

            Win32.IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);

            return retVal;
        }

        #endregion
    }
}