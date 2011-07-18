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

namespace leetreveil.Zuse.Forms
{
    partial class FrmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.chkDebugMode = new System.Windows.Forms.CheckBox();
            this.chkMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
            this.lblTrackDisplayFmt = new System.Windows.Forms.Label();
            this.lblProgramRestartRequired = new System.Windows.Forms.Label();
            this.cmbTrackDisplayFmt = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(191, 166);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkDebugMode
            // 
            this.chkDebugMode.AutoSize = true;
            this.chkDebugMode.Location = new System.Drawing.Point(9, 58);
            this.chkDebugMode.Name = "chkDebugMode";
            this.chkDebugMode.Size = new System.Drawing.Size(138, 17);
            this.chkDebugMode.TabIndex = 1;
            this.chkDebugMode.Text = "Enable Debug Mode (*)";
            this.chkDebugMode.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToTray
            // 
            this.chkMinimizeToTray.AutoSize = true;
            this.chkMinimizeToTray.Location = new System.Drawing.Point(9, 35);
            this.chkMinimizeToTray.Name = "chkMinimizeToTray";
            this.chkMinimizeToTray.Size = new System.Drawing.Size(188, 17);
            this.chkMinimizeToTray.TabIndex = 2;
            this.chkMinimizeToTray.Text = "Let Zune Minimize To System Tray";
            this.chkMinimizeToTray.UseVisualStyleBackColor = true;
            this.chkMinimizeToTray.CheckedChanged += new System.EventHandler(this.chkMinimizeToTray_CheckedChanged);
            // 
            // chkCheckUpdates
            // 
            this.chkCheckUpdates.AutoSize = true;
            this.chkCheckUpdates.Location = new System.Drawing.Point(9, 12);
            this.chkCheckUpdates.Name = "chkCheckUpdates";
            this.chkCheckUpdates.Size = new System.Drawing.Size(208, 17);
            this.chkCheckUpdates.TabIndex = 4;
            this.chkCheckUpdates.Text = "Check for Updates to Zuse on Startup";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            // 
            // lblTrackDisplayFmt
            // 
            this.lblTrackDisplayFmt.Location = new System.Drawing.Point(6, 78);
            this.lblTrackDisplayFmt.Name = "lblTrackDisplayFmt";
            this.lblTrackDisplayFmt.Size = new System.Drawing.Size(260, 22);
            this.lblTrackDisplayFmt.TabIndex = 13;
            this.lblTrackDisplayFmt.Text = "Track Display Format:";
            this.lblTrackDisplayFmt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProgramRestartRequired
            // 
            this.lblProgramRestartRequired.Location = new System.Drawing.Point(0, 138);
            this.lblProgramRestartRequired.Name = "lblProgramRestartRequired";
            this.lblProgramRestartRequired.Size = new System.Drawing.Size(266, 16);
            this.lblProgramRestartRequired.TabIndex = 14;
            this.lblProgramRestartRequired.Text = "(*) - Requires Program Restart";
            this.lblProgramRestartRequired.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbTrackDisplayFmt
            // 
            this.cmbTrackDisplayFmt.FormattingEnabled = true;
            this.cmbTrackDisplayFmt.Location = new System.Drawing.Point(6, 103);
            this.cmbTrackDisplayFmt.Name = "cmbTrackDisplayFmt";
            this.cmbTrackDisplayFmt.Size = new System.Drawing.Size(260, 21);
            this.cmbTrackDisplayFmt.TabIndex = 15;
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 200);
            this.ControlBox = false;
            this.Controls.Add(this.cmbTrackDisplayFmt);
            this.Controls.Add(this.lblProgramRestartRequired);
            this.Controls.Add(this.lblTrackDisplayFmt);
            this.Controls.Add(this.chkCheckUpdates);
            this.Controls.Add(this.chkMinimizeToTray);
            this.Controls.Add(this.chkDebugMode);
            this.Controls.Add(this.btnClose);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zuse Settings";
            this.Load += new System.EventHandler(this.FrmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkDebugMode;
        private System.Windows.Forms.CheckBox chkMinimizeToTray;
        private System.Windows.Forms.CheckBox chkCheckUpdates;
        private System.Windows.Forms.Label lblTrackDisplayFmt;
        private System.Windows.Forms.Label lblProgramRestartRequired;
        private System.Windows.Forms.ComboBox cmbTrackDisplayFmt;
    }
}