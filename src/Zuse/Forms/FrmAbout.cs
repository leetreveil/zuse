using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace leetreveil.Zuse.Forms
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
            lblVersion.Text += Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void lblGithubLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/leetreveil/zuse");
        }
    }
}
