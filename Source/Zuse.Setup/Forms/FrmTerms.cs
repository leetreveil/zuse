using System;
using System.Windows.Forms;

namespace Zuse.Setup.Forms
{
    public partial class FrmTerms : Form
    {
        public FrmTerms()
        {
            InitializeComponent();
        }

        private void btnAgree_Click(object sender, EventArgs e)
        {
            if (this.chkTerms.Checked)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
