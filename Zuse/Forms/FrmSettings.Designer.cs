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

namespace Zuse.Forms
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
            this.grpImportantNote = new System.Windows.Forms.GroupBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
            this.chkUseGrowl = new System.Windows.Forms.CheckBox();
            this.btnClearSkips = new System.Windows.Forms.Button();
            this.grpImportantNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(197, 287);
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
            this.chkDebugMode.Location = new System.Drawing.Point(12, 129);
            this.chkDebugMode.Name = "chkDebugMode";
            this.chkDebugMode.Size = new System.Drawing.Size(256, 17);
            this.chkDebugMode.TabIndex = 1;
            this.chkDebugMode.Text = "Enable Debug Mode (Requires Program Restart)";
            this.chkDebugMode.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeToTray
            // 
            this.chkMinimizeToTray.AutoSize = true;
            this.chkMinimizeToTray.Location = new System.Drawing.Point(12, 152);
            this.chkMinimizeToTray.Name = "chkMinimizeToTray";
            this.chkMinimizeToTray.Size = new System.Drawing.Size(188, 17);
            this.chkMinimizeToTray.TabIndex = 2;
            this.chkMinimizeToTray.Text = "Let Zune Minimize To System Tray";
            this.chkMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // grpImportantNote
            // 
            this.grpImportantNote.Controls.Add(this.lblNote);
            this.grpImportantNote.Location = new System.Drawing.Point(12, 13);
            this.grpImportantNote.Name = "grpImportantNote";
            this.grpImportantNote.Size = new System.Drawing.Size(260, 87);
            this.grpImportantNote.TabIndex = 3;
            this.grpImportantNote.TabStop = false;
            this.grpImportantNote.Text = "Note";
            // 
            // lblNote
            // 
            this.lblNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNote.Location = new System.Drawing.Point(3, 17);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(254, 67);
            this.lblNote.TabIndex = 4;
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkCheckUpdates
            // 
            this.chkCheckUpdates.AutoSize = true;
            this.chkCheckUpdates.Location = new System.Drawing.Point(12, 106);
            this.chkCheckUpdates.Name = "chkCheckUpdates";
            this.chkCheckUpdates.Size = new System.Drawing.Size(208, 17);
            this.chkCheckUpdates.TabIndex = 4;
            this.chkCheckUpdates.Text = "Check for Updates to Zuse on Startup";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            // 
            // chkUseGrowl
            // 
            this.chkUseGrowl.AutoSize = true;
            this.chkUseGrowl.Location = new System.Drawing.Point(12, 175);
            this.chkUseGrowl.Name = "chkUseGrowl";
            this.chkUseGrowl.Size = new System.Drawing.Size(136, 17);
            this.chkUseGrowl.TabIndex = 5;
            this.chkUseGrowl.Text = "Use Growl Notifications";
            this.chkUseGrowl.UseVisualStyleBackColor = true;
            // 
            // btnClearSkips
            // 
            this.btnClearSkips.Location = new System.Drawing.Point(12, 287);
            this.btnClearSkips.Name = "btnClearSkips";
            this.btnClearSkips.Size = new System.Drawing.Size(152, 23);
            this.btnClearSkips.TabIndex = 6;
            this.btnClearSkips.Text = "Clear Skipped Updates";
            this.btnClearSkips.UseVisualStyleBackColor = true;
            this.btnClearSkips.Click += new System.EventHandler(this.btnClearSkips_Click);
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 322);
            this.ControlBox = false;
            this.Controls.Add(this.btnClearSkips);
            this.Controls.Add(this.chkUseGrowl);
            this.Controls.Add(this.chkCheckUpdates);
            this.Controls.Add(this.grpImportantNote);
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
            this.grpImportantNote.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkDebugMode;
        private System.Windows.Forms.CheckBox chkMinimizeToTray;
        private System.Windows.Forms.GroupBox grpImportantNote;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.CheckBox chkCheckUpdates;
        private System.Windows.Forms.CheckBox chkUseGrowl;
        private System.Windows.Forms.Button btnClearSkips;
    }
}