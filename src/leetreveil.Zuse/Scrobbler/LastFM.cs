﻿/*
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
using System.IO;
using Microsoft.Win32;
using leetreveil.Zuse.Core;

namespace leetreveil.Zuse.Scrobbler
{
    public static class LastFM
    {
        public static bool IsClientRunning
        {
            get
            {
                bool isRunning = false;

                foreach (Process proc in Process.GetProcesses())
                {
                    if (proc.ProcessName == "LastFM")
                    {
                        isRunning = true;
                        break;
                    }
                }

                return isRunning;
            }
        }
    }
}