using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Face_Searcher_GUI.Forms
{
    public partial class EmailSetup : Form
    {
        public EmailSetup()
        {
            InitializeComponent();

            this.FormClosing += Form_FormClosing;

            string[] menuOptions = new string[] { "Camera Page", "License Info", "Camera Stream Interruption Settings", "Setup Documentation", "NiFi", "Default Area Of Interest Settings" };
            this.menu.Items.AddRange(menuOptions);
            this.menu.SelectedItem = "Camera Stream Interruption Settings";

            loadDisplay();
        }

        public void loadDisplay()
        {
            //To emails
            string toEmailString = "";
            foreach (string TE in Program.email.receiver)
            {
                toEmailString += TE.ToString() + ",\r\n";
            }
            toEmailString = toEmailString.Substring(0, toEmailString.Length - 3);
            this.toEmails.Text = toEmailString;
            this.toEmails.Refresh();
            
            this.interruptionTime.Text = Program.email.stream_timeout;
            this.restartInterval.Text = Program.email.camera_restart_interval;
            this.restartAttempts.Text = Program.email.camera_restart_attempts;
            this.accountName.Text = Program.email.emailAccountName;
            this.fromEmail.Text = Program.email.sender;
            this.accountPassword.Text = Program.email.password;

            if (Program.email.emailNotifications == "true")
            {
                this.sendEmail.Checked = true;
            }

            //Refresh everthing
            this.interruptionTime.Refresh();
            this.restartInterval.Refresh();
            this.restartAttempts.Refresh();
            this.accountName.Refresh();
            this.fromEmail.Refresh();
            this.accountPassword.Refresh();
        }

        /// <summary>
        /// Change to the selected form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.menu.SelectedItem.ToString())
            {
                //{ "Camera Page", "License Info", "Camera Stream Interruption Settings", "Setup Documentation", "NiFi", "Default Area Of Interest Settings" };
                case "License Info":
                    Program.LP = new LicensePage();
                    Program.LP.Show();
                    this.Hide();
                    break;
                case "NiFi":
                    Program.NF = new NiFi();
                    Program.NF.Show();
                    this.Hide();
                    break;
                case "Setup Documentation":
                    Program.DT = new Documentation();
                    Program.DT.Show();
                    this.Hide();
                    break;
                case "Camera Page":
                    Program.CF = new CameraForm();
                    Program.CF.Show();
                    this.Hide();
                    break;
                case "Default Area Of Interest Settings":
                    Program.DA = new DefaultAOI();
                    Program.DA.Show();
                    this.Hide();
                    break;
                default:
                    //This is this page. Do nothing
                    //case "Camera Stream Interruption Settings":
                    //    Program.ES = new EmailSetup();
                    //    Program.ES.Show();
                    break;
            }
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("If you exit now, any unsaved changes will be lost.\r\nAre you sure you would like to exit?", "EXIT", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //ToDo
                Program.exitApp();
            }
        }

        /// <summary>
        /// Handle what happens when the user hits the x button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_FormClosing(Object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    if (MessageBox.Show("If you exit now, any unsaved changes will be lost.\r\nAre you sure you would like to exit?", "EXIT", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Program.exitApp();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// Return to the camera page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CPButton_Click(object sender, EventArgs e)
        {
            Program.CF = new CameraForm();
            Program.CF.Show();
            this.Hide();
        }

        /// <summary>
        /// Save the settings on this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            Program.email.receiver.Clear();
            foreach (string TE in this.toEmails.Text.Split(','))
            {
                Program.email.receiver.Add(TE.Replace("\r\n", "").Replace(",", ""));
            }
            Program.email.emailAccountName = this.accountName.Text;
            Program.email.sender = this.fromEmail.Text;
            Program.email.password = this.accountPassword.Text;

            if (this.sendEmail.Checked)
            {
                Program.email.emailNotifications = "true";
            }else
            {
                Program.email.emailNotifications = "false";
            }

            Program.email.stream_timeout = this.interruptionTime.Text;
            Program.email.camera_restart_interval = this.restartInterval.Text;
            Program.email.camera_restart_attempts = this.restartAttempts.Text;

            Program.save("EMAIL");
        }

        /// <summary>
        /// Open allevate webpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.allevateURL);
        }
    }
}
