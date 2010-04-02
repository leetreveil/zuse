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

namespace Zuse.Forms
{
    partial class FrmUpdate
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDetails = new System.Windows.Forms.Label();
            this.grpChangeLog = new System.Windows.Forms.GroupBox();
            this.webBrowserChangelog = new System.Windows.Forms.WebBrowser();
            this.btnSkipUpdate = new System.Windows.Forms.Button();
            this.btnInstallUpdate = new System.Windows.Forms.Button();
            this.lblSkipThisUpdate = new System.Windows.Forms.Label();
            this.grpChangeLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(13, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(594, 41);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Zuse update is available!";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDetails
            // 
            this.lblDetails.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetails.Location = new System.Drawing.Point(15, 54);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(592, 45);
            this.lblDetails.TabIndex = 2;
            this.lblDetails.Text = "[Details]";
            this.lblDetails.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpChangeLog
            // 
            this.grpChangeLog.Controls.Add(this.webBrowserChangelog);
            this.grpChangeLog.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpChangeLog.Location = new System.Drawing.Point(13, 111);
            this.grpChangeLog.Name = "grpChangeLog";
            this.grpChangeLog.Size = new System.Drawing.Size(594, 189);
            this.grpChangeLog.TabIndex = 3;
            this.grpChangeLog.TabStop = false;
            this.grpChangeLog.Text = "Change Log";
            // 
            // webBrowserChangelog
            // 
            this.webBrowserChangelog.AllowWebBrowserDrop = false;
            this.webBrowserChangelog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserChangelog.IsWebBrowserContextMenuEnabled = false;
            this.webBrowserChangelog.Location = new System.Drawing.Point(3, 17);
            this.webBrowserChangelog.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserChangelog.Name = "webBrowserChangelog";
            this.webBrowserChangelog.ScriptErrorsSuppressed = true;
            this.webBrowserChangelog.Size = new System.Drawing.Size(588, 169);
            this.webBrowserChangelog.TabIndex = 0;
            // 
            // btnSkipUpdate
            // 
            this.btnSkipUpdate.Location = new System.Drawing.Point(516, 312);
            this.btnSkipUpdate.Name = "btnSkipUpdate";
            this.btnSkipUpdate.Size = new System.Drawing.Size(91, 23);
            this.btnSkipUpdate.TabIndex = 4;
            this.btnSkipUpdate.Text = "Skip this update";
            this.btnSkipUpdate.UseVisualStyleBackColor = true;
            this.btnSkipUpdate.Click += new System.EventHandler(this.btnSkipUpdate_Click);
            // 
            // btnInstallUpdate
            // 
            this.btnInstallUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnInstallUpdate.Location = new System.Drawing.Point(406, 312);
            this.btnInstallUpdate.Name = "btnInstallUpdate";
            this.btnInstallUpdate.Size = new System.Drawing.Size(104, 23);
            this.btnInstallUpdate.TabIndex = 5;
            this.btnInstallUpdate.Text = "Download update";
            this.btnInstallUpdate.UseVisualStyleBackColor = true;
            this.btnInstallUpdate.Click += new System.EventHandler(this.btnInstallUpdate_Click);
            // 
            // lblSkipThisUpdate
            // 
            this.lblSkipThisUpdate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSkipThisUpdate.ForeColor = System.Drawing.Color.Red;
            this.lblSkipThisUpdate.Location = new System.Drawing.Point(10, 314);
            this.lblSkipThisUpdate.Name = "lblSkipThisUpdate";
            this.lblSkipThisUpdate.Size = new System.Drawing.Size(390, 17);
            this.lblSkipThisUpdate.TabIndex = 6;
            this.lblSkipThisUpdate.Text = "You have previously chosen to skip this update.";
            this.lblSkipThisUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmUpdate
            // 
            this.AcceptButton = this.btnInstallUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(619, 347);
            this.Controls.Add(this.lblSkipThisUpdate);
            this.Controls.Add(this.btnInstallUpdate);
            this.Controls.Add(this.btnSkipUpdate);
            this.Controls.Add(this.grpChangeLog);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.lblTitle);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zuse Update";
            this.Load += new System.EventHandler(this.FrmUpdate_Load);
            this.grpChangeLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.GroupBox grpChangeLog;
        private System.Windows.Forms.Button btnSkipUpdate;
        private System.Windows.Forms.Button btnInstallUpdate;
        private System.Windows.Forms.WebBrowser webBrowserChangelog;
        private System.Windows.Forms.Label lblSkipThisUpdate;
    }
}