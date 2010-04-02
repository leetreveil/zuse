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
    partial class FrmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.pictureBoxZuneLogo = new System.Windows.Forms.PictureBox();
            this.linkLabelJoinGroup1 = new System.Windows.Forms.LinkLabel();
            this.linkLabelJoinGroup2 = new System.Windows.Forms.LinkLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonDonate = new System.Windows.Forms.Button();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.rtfWhyDonate = new System.Windows.Forms.RichTextBox();
            this.tabPageLic = new System.Windows.Forms.TabPage();
            this.rtfLicense = new System.Windows.Forms.RichTextBox();
            this.tabPageThirdParties = new System.Windows.Forms.TabPage();
            this.rtfThirdParties = new System.Windows.Forms.RichTextBox();
            this.tabPageDonars = new System.Windows.Forms.TabPage();
            this.webBrowserDonors = new System.Windows.Forms.WebBrowser();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZuneLogo)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.tabPageLic.SuspendLayout();
            this.tabPageThirdParties.SuspendLayout();
            this.tabPageDonars.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxZuneLogo
            // 
            this.pictureBoxZuneLogo.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxZuneLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxZuneLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxZuneLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxZuneLogo.Image")));
            this.pictureBoxZuneLogo.Location = new System.Drawing.Point(9, 9);
            this.pictureBoxZuneLogo.Name = "pictureBoxZuneLogo";
            this.pictureBoxZuneLogo.Size = new System.Drawing.Size(456, 258);
            this.pictureBoxZuneLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxZuneLogo.TabIndex = 1;
            this.pictureBoxZuneLogo.TabStop = false;
            // 
            // linkLabelJoinGroup1
            // 
            this.linkLabelJoinGroup1.AutoSize = true;
            this.linkLabelJoinGroup1.Location = new System.Drawing.Point(54, 270);
            this.linkLabelJoinGroup1.Name = "linkLabelJoinGroup1";
            this.linkLabelJoinGroup1.Size = new System.Drawing.Size(176, 13);
            this.linkLabelJoinGroup1.TabIndex = 2;
            this.linkLabelJoinGroup1.TabStop = true;
            this.linkLabelJoinGroup1.Text = "Join the Zuse Users Last.fm Group!";
            this.linkLabelJoinGroup1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelJoinGroup1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelJoinGroup1_LinkClicked);
            // 
            // linkLabelJoinGroup2
            // 
            this.linkLabelJoinGroup2.AutoSize = true;
            this.linkLabelJoinGroup2.Location = new System.Drawing.Point(244, 270);
            this.linkLabelJoinGroup2.Name = "linkLabelJoinGroup2";
            this.linkLabelJoinGroup2.Size = new System.Drawing.Size(181, 13);
            this.linkLabelJoinGroup2.TabIndex = 3;
            this.linkLabelJoinGroup2.TabStop = true;
            this.linkLabelJoinGroup2.Text = "Join the Zune-Online Last.fm Group!";
            this.linkLabelJoinGroup2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelJoinGroup2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelJoinGroup2_LinkClicked);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageAbout);
            this.tabControl.Controls.Add(this.tabPageLic);
            this.tabControl.Controls.Add(this.tabPageThirdParties);
            this.tabControl.Controls.Add(this.tabPageDonars);
            this.tabControl.Location = new System.Drawing.Point(9, 286);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(456, 287);
            this.tabControl.TabIndex = 26;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.BackColor = System.Drawing.Color.Transparent;
            this.tabPageAbout.Controls.Add(this.tableLayoutPanel);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(448, 261);
            this.tabPageAbout.TabIndex = 0;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.buttonDonate, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.labelCopyright, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelVersion, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelProductName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.rtfWhyDonate, 0, 3);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 6);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(442, 252);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // buttonDonate
            // 
            this.buttonDonate.AutoSize = true;
            this.buttonDonate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonDonate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDonate.Location = new System.Drawing.Point(3, 225);
            this.buttonDonate.Name = "buttonDonate";
            this.buttonDonate.Size = new System.Drawing.Size(436, 24);
            this.buttonDonate.TabIndex = 28;
            this.buttonDonate.Text = "Donate";
            this.buttonDonate.UseVisualStyleBackColor = true;
            // 
            // labelCopyright
            // 
            this.labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCopyright.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.Location = new System.Drawing.Point(6, 17);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(433, 17);
            this.labelCopyright.TabIndex = 19;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVersion
            // 
            this.labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVersion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(6, 34);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(433, 17);
            this.labelVersion.TabIndex = 0;
            this.labelVersion.Text = "Version";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelProductName
            // 
            this.labelProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.Location = new System.Drawing.Point(6, 0);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(433, 17);
            this.labelProductName.TabIndex = 21;
            this.labelProductName.Text = "Product Name";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rtfWhyDonate
            // 
            this.rtfWhyDonate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfWhyDonate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfWhyDonate.Location = new System.Drawing.Point(3, 54);
            this.rtfWhyDonate.Name = "rtfWhyDonate";
            this.rtfWhyDonate.ReadOnly = true;
            this.rtfWhyDonate.Size = new System.Drawing.Size(436, 165);
            this.rtfWhyDonate.TabIndex = 22;
            this.rtfWhyDonate.Text = resources.GetString("rtfWhyDonate.Text");
            // 
            // tabPageLic
            // 
            this.tabPageLic.Controls.Add(this.rtfLicense);
            this.tabPageLic.Location = new System.Drawing.Point(4, 22);
            this.tabPageLic.Name = "tabPageLic";
            this.tabPageLic.Size = new System.Drawing.Size(448, 261);
            this.tabPageLic.TabIndex = 2;
            this.tabPageLic.Text = "License Agreement";
            this.tabPageLic.UseVisualStyleBackColor = true;
            // 
            // rtfLicense
            // 
            this.rtfLicense.BackColor = System.Drawing.SystemColors.Control;
            this.rtfLicense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtfLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfLicense.Location = new System.Drawing.Point(0, 0);
            this.rtfLicense.Name = "rtfLicense";
            this.rtfLicense.ReadOnly = true;
            this.rtfLicense.Size = new System.Drawing.Size(448, 261);
            this.rtfLicense.TabIndex = 26;
            this.rtfLicense.Text = "";
            // 
            // tabPageThirdParties
            // 
            this.tabPageThirdParties.BackColor = System.Drawing.Color.Transparent;
            this.tabPageThirdParties.Controls.Add(this.rtfThirdParties);
            this.tabPageThirdParties.Location = new System.Drawing.Point(4, 22);
            this.tabPageThirdParties.Name = "tabPageThirdParties";
            this.tabPageThirdParties.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageThirdParties.Size = new System.Drawing.Size(448, 261);
            this.tabPageThirdParties.TabIndex = 1;
            this.tabPageThirdParties.Text = "Third Parties";
            this.tabPageThirdParties.UseVisualStyleBackColor = true;
            // 
            // rtfThirdParties
            // 
            this.rtfThirdParties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfThirdParties.Location = new System.Drawing.Point(3, 3);
            this.rtfThirdParties.Name = "rtfThirdParties";
            this.rtfThirdParties.ReadOnly = true;
            this.rtfThirdParties.Size = new System.Drawing.Size(442, 255);
            this.rtfThirdParties.TabIndex = 0;
            this.rtfThirdParties.Text = "";
            // 
            // tabPageDonars
            // 
            this.tabPageDonars.Controls.Add(this.webBrowserDonors);
            this.tabPageDonars.Location = new System.Drawing.Point(4, 22);
            this.tabPageDonars.Name = "tabPageDonars";
            this.tabPageDonars.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDonars.Size = new System.Drawing.Size(448, 261);
            this.tabPageDonars.TabIndex = 3;
            this.tabPageDonars.Text = "Donors";
            this.tabPageDonars.UseVisualStyleBackColor = true;
            // 
            // webBrowserDonors
            // 
            this.webBrowserDonors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserDonors.Location = new System.Drawing.Point(3, 3);
            this.webBrowserDonors.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserDonors.Name = "webBrowserDonors";
            this.webBrowserDonors.Size = new System.Drawing.Size(442, 255);
            this.webBrowserDonors.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(386, 579);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 27;
            this.okButton.Text = "&OK";
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 614);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.linkLabelJoinGroup2);
            this.Controls.Add(this.linkLabelJoinGroup1);
            this.Controls.Add(this.pictureBoxZuneLogo);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmAbout";
            this.Load += new System.EventHandler(this.FrmAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZuneLogo)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageAbout.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tabPageLic.ResumeLayout(false);
            this.tabPageThirdParties.ResumeLayout(false);
            this.tabPageDonars.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxZuneLogo;
        private System.Windows.Forms.LinkLabel linkLabelJoinGroup1;
        private System.Windows.Forms.LinkLabel linkLabelJoinGroup2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.TabPage tabPageThirdParties;
        private System.Windows.Forms.TabPage tabPageLic;
        private System.Windows.Forms.RichTextBox rtfLicense;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.RichTextBox rtfThirdParties;
        private System.Windows.Forms.RichTextBox rtfWhyDonate;
        private System.Windows.Forms.Button buttonDonate;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.TabPage tabPageDonars;
        private System.Windows.Forms.WebBrowser webBrowserDonors;
    }
}
