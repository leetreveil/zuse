using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Zuse.Setup.Forms
{
    using Zuse.Setup.Core;
    using Zuse.Setup.Core.Methods;
    using Zuse.Setup.Properties;

    public partial class FrmSetup : Form
    {
        private delegate void Blink();

        private IMethod m_method;
        private int m_currentMethod;
        private bool m_installed;
        private Thread m_thread;

        public FrmSetup()
        {
            InitializeComponent();

            m_method = null;
            m_currentMethod = 0;
            m_installed = false;
            m_thread = new Thread(new ThreadStart(BetaWarningBlinker));

            this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

            SetupButtons();
        }

        private void FrmSetup_Load(object sender, EventArgs e)
        {
            m_thread.Start();
            WriteStartMessage();
        }

        private void FrmSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_thread.Abort();
        }

        private void BetaWarningBlinker()
        {
            Blink b = new Blink(this.BlinkCallback);

            while (true)
            {
                this.Invoke(b);

                Thread.Sleep(250);
            }
        }

        private void BlinkCallback()
        {
            this.lblRelease.Visible = !this.lblRelease.Visible;
        }

        private void GetRegistrySettings()
        {
            RegistryKey mainKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Zuse");
            RegistryKey setupKey = mainKey.CreateSubKey("Setup");
            RegistryKey settingsKey = mainKey.CreateSubKey("Settings");

            m_currentMethod = (int)setupKey.GetValue("Method", 0);
            bool.TryParse((string)setupKey.GetValue("Installed", bool.FalseString), out m_installed);
        }

        private void SetRegistrySettings(bool did_install)
        {
            RegistryKey mainKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Zuse");
            RegistryKey setupKey = mainKey.CreateSubKey("Setup");
            RegistryKey settingsKey = mainKey.CreateSubKey("Settings");

            if (!did_install)
            {
                setupKey.SetValue("Method", 0);
            }
            else
            {
                if (this.radioButtonMethod1.Checked)
                {
                    setupKey.SetValue("Method", 1);
                }

                if (this.radioButtonMethod2.Checked)
                {
                    setupKey.SetValue("Method", 2);
                }
            }

            setupKey.SetValue("Installed", did_install.ToString());
        }

        private void SetupButtons()
        {
            GetRegistrySettings();

            if (m_installed)
            {
                this.AcceptButton = this.btnUninstall;
                this.btnInstall.Enabled = false;
                this.btnUninstall.Enabled = true;

                switch (m_currentMethod)
                {
                    case 1:
                        this.radioButtonMethod1.Checked = true;
                        break;
                    case 2:
                        this.radioButtonMethod2.Checked = true;
                        break;
                    default:
                        MessageBox.Show("Zuse's registry setup keys appear to be corrupt, please do not modify them manually!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        break;
                }

                this.radioButtonMethod1.Enabled = false;
                this.radioButtonMethod2.Enabled = false;
            }
            else
            {
                this.AcceptButton = this.btnInstall;
                this.btnInstall.Enabled = true;
                this.btnUninstall.Enabled = false;

                this.radioButtonMethod1.Enabled = true;
                this.radioButtonMethod2.Enabled = true;
            }
        }

        private bool SetupMethod()
        {
            if (this.radioButtonMethod1.Checked)
            {
                m_method = new Method1();
            }

            if (this.radioButtonMethod2.Checked)
            {
                m_method = new Method2();
            }

            if (m_method != null)
            {
                m_method.MethodMessage += new MethodMessage(WriteMessage);
                m_method.MethodEvent += new MethodEvent(WriteEvent);
                m_method.MethodError += new MethodError(WriteError);

                return true;
            }
            else
            {
                MessageBox.Show("Please select an installation method.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private void WriteWithColor(string msg, Color color)
        {
            rtfLog.SelectionColor = color;
            int start = -1, end = -1;

            start = rtfLog.Text.Length;
            rtfLog.Text += msg;
            end = rtfLog.Text.Length;

            rtfLog.Select(start, end - start);
            rtfLog.SelectionColor = Color.White;

            rtfLog.ResumeLayout();
            //rtfLog.SelectionColor = Color.White;
        }

        private void WriteError(object sender, string msg)
        {
            WriteWithColor("*--> ", Color.Blue);
            WriteWithColor(msg, Color.White);
            rtfLog.Text += Environment.NewLine;
            rtfLog.ScrollToCaret();
        }

        private void WriteEvent(object sender, string msg)
        {
            WriteWithColor(msg, Color.White);
            rtfLog.Text += Environment.NewLine;
            rtfLog.ScrollToCaret();
        }

        private void WriteMessage(object sender, string msg)
        {
            WriteWithColor("*--> ", Color.Green);
            WriteWithColor(msg, Color.White);
            rtfLog.Text += Environment.NewLine;
            rtfLog.ScrollToCaret();
        }

        private void WriteStartMessage()
        {
            rtfLog.Rtf = string.Empty;
            string msg = string.Empty;

            msg += string.Format("Zuse Setup v{0:s}", Assembly.GetExecutingAssembly().GetName().Version.ToString()) + Environment.NewLine;
            msg += "Licensed by the GNU General Public License v3" + Environment.NewLine;
            msg += "Copyright (C) Zachary Howe 2010" + Environment.NewLine;

            WriteWithColor(msg, Color.Red);
            rtfLog.ScrollToCaret();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            FrmTerms frmTerms = new FrmTerms();

            if (frmTerms.ShowDialog() == DialogResult.OK)
            {
                if (SetupMethod())
                {
                    WriteStartMessage();
                    this.btnInstall.Enabled = false;
                    WriteWithColor(Environment.NewLine + "Go Go Gadget Zuse!", Color.White);
                    WriteWithColor(Environment.NewLine + Environment.NewLine, Color.White);
                    m_method.Install();
                    WriteWithColor(Environment.NewLine + "Done!", Color.White);
                    this.btnUninstall.Enabled = true;
                    SetRegistrySettings(true);
                    SetupButtons();
                }
            }
            else
            {
                MessageBox.Show("Terms were not agreed upon, canceling patching and installing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (SetupMethod())
            {
                WriteStartMessage();
                this.btnUninstall.Enabled = false;
                WriteWithColor(Environment.NewLine + "Uninstalling...", Color.White);
                WriteWithColor(Environment.NewLine + Environment.NewLine, Color.White);
                m_method.Uninstall();
                WriteWithColor(Environment.NewLine + "Done!", Color.White);
                this.btnInstall.Enabled = true;
                SetRegistrySettings(false);
                SetupButtons();
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;

            msg += string.Format("Zuse Setup v{0:s}", Assembly.GetExecutingAssembly().GetName().Version.ToString()) + Environment.NewLine;
            msg += "Licensed by the GNU General Public License v3" + Environment.NewLine;
            msg += "Copyright (C) Zachary Howe 2010" + Environment.NewLine;

            MessageBox.Show(msg, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
