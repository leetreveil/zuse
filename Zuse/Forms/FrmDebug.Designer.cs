namespace Zuse.Forms
{
    partial class FrmDebug
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
            this.cmbLogFile = new System.Windows.Forms.ComboBox();
            this.lstMessages = new System.Windows.Forms.ListView();
            this.columnTime = new System.Windows.Forms.ColumnHeader();
            this.columnLevel = new System.Windows.Forms.ColumnHeader();
            this.columMessage = new System.Windows.Forms.ColumnHeader();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbLogFile
            // 
            this.cmbLogFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLogFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogFile.FormattingEnabled = true;
            this.cmbLogFile.Location = new System.Drawing.Point(65, 14);
            this.cmbLogFile.Name = "cmbLogFile";
            this.cmbLogFile.Size = new System.Drawing.Size(667, 21);
            this.cmbLogFile.TabIndex = 0;
            this.cmbLogFile.SelectedIndexChanged += new System.EventHandler(this.cmbLogFile_SelectedIndexChanged);
            // 
            // lstMessages
            // 
            this.lstMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnTime,
            this.columnLevel,
            this.columMessage});
            this.lstMessages.FullRowSelect = true;
            this.lstMessages.Location = new System.Drawing.Point(13, 41);
            this.lstMessages.MultiSelect = false;
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(719, 321);
            this.lstMessages.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.lstMessages.TabIndex = 3;
            this.lstMessages.UseCompatibleStateImageBehavior = false;
            this.lstMessages.View = System.Windows.Forms.View.Details;
            this.lstMessages.DoubleClick += new System.EventHandler(this.lstMessages_DoubleClick);
            // 
            // columnTime
            // 
            this.columnTime.Text = "Time";
            this.columnTime.Width = 115;
            // 
            // columnLevel
            // 
            this.columnLevel.Text = "Level";
            this.columnLevel.Width = 68;
            // 
            // columMessage
            // 
            this.columMessage.Text = "Message";
            this.columMessage.Width = 516;
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.IncludeSubdirectories = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Log File:";
            // 
            // FrmDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 374);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstMessages);
            this.Controls.Add(this.cmbLogFile);
            this.Name = "FrmDebug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debug Log";
            this.Load += new System.EventHandler(this.FrmDebug_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbLogFile;
        private System.Windows.Forms.ListView lstMessages;
        private System.Windows.Forms.ColumnHeader columnTime;
        private System.Windows.Forms.ColumnHeader columnLevel;
        private System.Windows.Forms.ColumnHeader columMessage;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.Label label1;
    }
}