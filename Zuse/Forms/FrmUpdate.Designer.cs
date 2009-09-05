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
            this.rtfChangelog = new System.Windows.Forms.RichTextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDetails = new System.Windows.Forms.Label();
            this.grpChangeLog = new System.Windows.Forms.GroupBox();
            this.grpChangeLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtfChangelog
            // 
            this.rtfChangelog.BackColor = System.Drawing.SystemColors.Control;
            this.rtfChangelog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtfChangelog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfChangelog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfChangelog.Location = new System.Drawing.Point(3, 16);
            this.rtfChangelog.Name = "rtfChangelog";
            this.rtfChangelog.ReadOnly = true;
            this.rtfChangelog.Size = new System.Drawing.Size(588, 174);
            this.rtfChangelog.TabIndex = 0;
            this.rtfChangelog.Text = "";
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
            this.grpChangeLog.Controls.Add(this.rtfChangelog);
            this.grpChangeLog.Location = new System.Drawing.Point(13, 117);
            this.grpChangeLog.Name = "grpChangeLog";
            this.grpChangeLog.Size = new System.Drawing.Size(594, 193);
            this.grpChangeLog.TabIndex = 3;
            this.grpChangeLog.TabStop = false;
            this.grpChangeLog.Text = "Change Log";
            // 
            // FrmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 322);
            this.Controls.Add(this.grpChangeLog);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.lblTitle);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "FrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zuse Update";
            this.grpChangeLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtfChangelog;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.GroupBox grpChangeLog;
    }
}