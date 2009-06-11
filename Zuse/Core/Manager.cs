﻿/*
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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using log4net;

namespace Zuse.Core
{
    using Zuse.Properties;
    using Zuse.Scrobbler;
    using Zuse.Utilities;
    using Zuse.Forms;

    class Manager
    {
        private Process helperProc;
        private ScrobSub scrobbler;
        private FrmHidden frmHidden;
        private FrmDebug frmDebug;

        private ILog log;

        public Manager()
        {
            this.log = LogManager.GetLogger("Zuse", typeof(Zuse.Core.Manager));

            this.frmDebug = new FrmDebug();

            this.frmHidden = new FrmHidden();
            this.frmHidden.PlayingNewTrack += new PlayingNewTrack(FrmHidden_PlayingNewTrack);
            this.frmHidden.PlayingStopped += new PlayingStopped(FrmHidden_PlayingStopped);
            this.frmHidden.Show();

            this.scrobbler = new ScrobSub();
        }

        public void StartHelper()
        {
            // Check for currently running helpers...
            foreach (Process proc in Process.GetProcessesByName("ZuseHelper"))
            {
                log.Info("Killing existing ZuseHelper.exe process with PID of " + proc.Id.ToString());
                proc.Kill();
            }

            log.Info("Starting up ZuseHelper.exe");
            this.helperProc = Process.Start(Application.StartupPath + "\\ZuseHelper.exe");
        }

        public void ShowDebugWindow()
        {
            this.frmDebug = new FrmDebug();
            this.frmDebug.Show();
        }

        public void StopHelper()
        {
            this.helperProc.Kill();
            this.scrobbler.Stop();
        }

        public void RefreshDebugView()
        {
            this.frmDebug.RefreshView();
        }

        private void FrmHidden_PlayingNewTrack(object sender, Song song)
        {
            /*
             * The following commented code is used to fetch song length without
             * disrupting the user experience. I'm not sure if it's needed.
             
            Thread th = new Thread(new ThreadStart(song.FetchSongLength));
            th.Start();

            while (th.ThreadState != System.Threading.ThreadState.Stopped)
            {
                Thread.Sleep(200);
            }
            
            */

            song.FetchSongLength();

            if (song.Length == 0)
            {
                this.scrobbler.Start(song.Artist, song.Title, song.Album, song.MBID, 500, "");
            }
            else
            {
                this.scrobbler.Start(song.Artist, song.Title, song.Album, song.MBID, song.Length, "");
            }

            log.Info(string.Format("Starting song '{0:s}'", song.ToString())); 
        }

        private void FrmHidden_PlayingStopped(object sender)
        {
            log.Info("Stopped playing"); 

            this.scrobbler.Stop();
        }
    }
}