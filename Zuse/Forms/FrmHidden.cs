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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Zuse.Forms
{
    using Zuse.Scrobbler;
    using Zuse.Utilities;

    public delegate void PlayingNewTrack(object sender, Song song);
    public delegate void PlayingStopped(object sender);

    public partial class FrmHidden : Form
    {
        public event PlayingNewTrack PlayingNewTrack;
        public event PlayingStopped PlayingStopped;

        public static int WM_COPYDATA = 74;

        public FrmHidden()
        {
            InitializeComponent();
        }

        private void FrmHidden_Load(object sender, EventArgs e)
        {
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_COPYDATA)
            {
                COPYDATASTRUCT cds = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
                string msg = MessageUtils.GetMessageString(cds);

                if (msg.StartsWith("ZUNE"))
                {
                    // @"ZUNE\0Music\00\0{0}\0"
                    if (msg.Split('\\').Length == 5) { this.PlayingStopped(this); return; }

                    Song song = MessageUtils.ParseMessageString(msg);

                    if (this.PlayingNewTrack != null) this.PlayingNewTrack(this, song);
                }
            }

            base.WndProc(ref m);
        }

        private void FrmHidden_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.PlayingStopped(this);
        }
    }
}
