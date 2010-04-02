namespace Zuse.Setup.Forms
{
    partial class FrmSetup
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
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.grpInstallMethod = new System.Windows.Forms.GroupBox();
            this.radioButtonMethod2 = new System.Windows.Forms.RadioButton();
            this.radioButtonMethod1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtfLog = new System.Windows.Forms.RichTextBox();
            this.lblRelease = new System.Windows.Forms.Label();
            this.grpInstallMethod.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnInstall.Location = new System.Drawing.Point(256, 407);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(337, 407);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(75, 23);
            this.btnUninstall.TabIndex = 1;
            this.btnUninstall.Text = "Uninstall";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 69);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Zuse";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(13, 406);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 3;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // grpInstallMethod
            // 
            this.grpInstallMethod.Controls.Add(this.radioButtonMethod2);
            this.grpInstallMethod.Controls.Add(this.radioButtonMethod1);
            this.grpInstallMethod.Location = new System.Drawing.Point(13, 107);
            this.grpInstallMethod.Name = "grpInstallMethod";
            this.grpInstallMethod.Size = new System.Drawing.Size(400, 71);
            this.grpInstallMethod.TabIndex = 5;
            this.grpInstallMethod.TabStop = false;
            this.grpInstallMethod.Text = "Install Method";
            // 
            // radioButtonMethod2
            // 
            this.radioButtonMethod2.AutoSize = true;
            this.radioButtonMethod2.Location = new System.Drawing.Point(98, 44);
            this.radioButtonMethod2.Name = "radioButtonMethod2";
            this.radioButtonMethod2.Size = new System.Drawing.Size(205, 17);
            this.radioButtonMethod2.TabIndex = 1;
            this.radioButtonMethod2.TabStop = true;
            this.radioButtonMethod2.Text = "Method 2: Embed Zuse into ZuneShell";
            this.radioButtonMethod2.UseVisualStyleBackColor = true;
            // 
            // radioButtonMethod1
            // 
            this.radioButtonMethod1.AutoSize = true;
            this.radioButtonMethod1.Checked = true;
            this.radioButtonMethod1.Location = new System.Drawing.Point(70, 21);
            this.radioButtonMethod1.Name = "radioButtonMethod1";
            this.radioButtonMethod1.Size = new System.Drawing.Size(261, 17);
            this.radioButtonMethod1.TabIndex = 0;
            this.radioButtonMethod1.TabStop = true;
            this.radioButtonMethod1.Text = "Method 1: Install Zuse as Reference to ZuneShell";
            this.radioButtonMethod1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtfLog);
            this.groupBox1.Location = new System.Drawing.Point(13, 185);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 215);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Installation Log";
            // 
            // rtfLog
            // 
            this.rtfLog.BackColor = System.Drawing.Color.DarkSlateGray;
            this.rtfLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtfLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtfLog.DetectUrls = false;
            this.rtfLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfLog.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtfLog.ForeColor = System.Drawing.Color.White;
            this.rtfLog.Location = new System.Drawing.Point(3, 17);
            this.rtfLog.Name = "rtfLog";
            this.rtfLog.ReadOnly = true;
            this.rtfLog.Size = new System.Drawing.Size(393, 195);
            this.rtfLog.TabIndex = 0;
            this.rtfLog.Text = "";
            // 
            // lblRelease
            // 
            this.lblRelease.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRelease.ForeColor = System.Drawing.Color.Red;
            this.lblRelease.Location = new System.Drawing.Point(10, 78);
            this.lblRelease.Name = "lblRelease";
            this.lblRelease.Size = new System.Drawing.Size(403, 26);
            this.lblRelease.TabIndex = 7;
            this.lblRelease.Text = "BETA RELEASE!";
            this.lblRelease.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 442);
            this.Controls.Add(this.lblRelease);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpInstallMethod);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnUninstall);
            this.Controls.Add(this.btnInstall);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zuse Setup";
            this.Load += new System.EventHandler(this.FrmSetup_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSetup_FormClosing);
            this.grpInstallMethod.ResumeLayout(false);
            this.grpInstallMethod.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.GroupBox grpInstallMethod;
        private System.Windows.Forms.RadioButton radioButtonMethod1;
        private System.Windows.Forms.RadioButton radioButtonMethod2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtfLog;
        private System.Windows.Forms.Label lblRelease;
    }
}