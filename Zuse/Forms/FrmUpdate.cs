using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace Zuse.Forms
{
    public partial class FrmUpdate : Form
    {
        public FrmUpdate()
        {
            InitializeComponent();
        }

        public void SetDetails(Version new_version)
        {
            Version current_version = Assembly.GetExecutingAssembly().GetName().Version;

            this.lblDetails.Text = string.Format("You have version {0:s}, and {1:s} is available!", current_version.ToString(), new_version.ToString());
        }

        public void SetChangeLog(string text)
        {
            this.rtfChangelog.Text = text;
        }
    }
}
