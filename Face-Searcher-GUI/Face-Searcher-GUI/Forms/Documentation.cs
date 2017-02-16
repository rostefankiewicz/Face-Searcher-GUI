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
    public partial class Documentation : Form
    {
        const string setupDocumentation = @"C:\Allevate\Face-Searcher\Documents\Face-Searcher_User_Manual.pdf";
        const string eluaDocumentation = @"C:\Allevate\Face-Searcher\Documents\Face-Searcher_EULA.pdf";
        const string videoPlayerDocumentation = @"C:\Allevate\AureusVideoStreamPlayer\README_How_to_use_AureusVideoStreamPlayer.PDF";

        public Documentation()
        {
            InitializeComponent();

            this.FormClosing += Form_FormClosing;

            string[] menuOptions = new string[] { "Camera Page", "License Info", "Camera Stream Interruption Settings", "Setup Documentation", "NiFi", "Default Area Of Interest Settings" };
            this.menu.Items.AddRange(menuOptions);
            this.menu.SelectedItem = "Setup Documentation";
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
                case "Camera Stream Interruption Settings":
                    Program.ES = new EmailSetup();
                    Program.ES.Show();
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
                //case "Setup Documentation":
                //    Program.DT = new Documentation();
                //    Program.DT.Show();
                    break;
            }
        }

        /// <summary>
        /// Open the Allevate website
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Program.allevateURL);
        }

        /// <summary>
        /// Open the setup documentation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setupButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(setupDocumentation);
            }
            catch
            {
                MessageBox.Show(@"Could not find file '" + setupDocumentation + "'", "Warning", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Open the end user license agreement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EULAButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(eluaDocumentation);
            }
            catch
            {
                MessageBox.Show(@"Could not find file '" + eluaDocumentation + "'", "Warning", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Open the video stream player documentation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoPlayerDocButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(videoPlayerDocumentation);
            }
            catch
            {
                MessageBox.Show(@"Could not find file '" + videoPlayerDocumentation + "'", "Warning", MessageBoxButtons.OK);
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
