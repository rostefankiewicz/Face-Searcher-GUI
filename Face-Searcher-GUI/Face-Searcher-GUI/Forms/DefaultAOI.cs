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
    public partial class DefaultAOI : Form
    {
        public DefaultAOI()
        {
            InitializeComponent();

            this.FormClosing += Form_FormClosing;

            string[] menuOptions = new string[] { "Camera Page", "License Info", "Camera Stream Interruption Settings", "Setup Documentation", "NiFi", "Default Area Of Interest Settings" };
            this.menu.Items.AddRange(menuOptions);
            this.menu.SelectedItem = "Default Area Of Interest Settings";

            loadDisplay();
        }

        /// <summary>
        /// Load the correct values into the displays
        /// </summary>
        public void loadDisplay()
        {
            //AOI Settings
            this.aoiTOP.Text = Program.defaultAOI.face_detect_top;
            this.aoiLEFT.Text = Program.defaultAOI.face_detect_left;
            this.aoiHeight.Text = Program.defaultAOI.face_detect_height;
            this.aoiWIDTH.Text = Program.defaultAOI.face_detect_width;
            this.aoiMinHead.Text = Program.defaultAOI.face_detect_min_height_prop;
            this.aoiMaxHead.Text = Program.defaultAOI.face_detect_max_height_prop;
            this.aoiTOP.Refresh();
            this.aoiLEFT.Refresh();
            this.aoiHeight.Refresh();
            this.aoiWIDTH.Refresh();
            this.aoiMinHead.Refresh();
            this.aoiMaxHead.Refresh();
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
        /// Save the default AOI settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            Program.defaultAOI.face_detect_top = this.aoiTOP.Text;
            Program.defaultAOI.face_detect_left = this.aoiLEFT.Text;
            Program.defaultAOI.face_detect_height = this.aoiHeight.Text;
            Program.defaultAOI.face_detect_width = this.aoiWIDTH.Text;
            Program.defaultAOI.face_detect_min_height_prop = this.aoiMinHead.Text;
            Program.defaultAOI.face_detect_max_height_prop = this.aoiMaxHead.Text;

            Program.save("AOI");

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
        /// Open allevate webpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.allevateURL);
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
                case "Camera Stream Interruption Settings":
                    Program.ES = new EmailSetup();
                    Program.ES.Show();
                    this.Hide();
                    break;
                case "Setup Documentation":
                    Program.DT = new Documentation();
                    Program.DT.Show();
                    this.Hide();
                    break;
                case "NiFi":
                    Program.NF = new NiFi();
                    Program.NF.Show();
                    this.Hide();
                    break;
                case "Camera Page":
                    Program.CF = new CameraForm();
                    Program.CF.Show();
                    this.Hide();
                    break;
                default:
                    //This is this page. Do nothing
                    //case "Default Area Of Interest Settings":
                    //Program.DA = new DefaultAOI();
                    //Program.DA.Show();
                    //this.Hide();
                    break;
            }
        }
    }
}
