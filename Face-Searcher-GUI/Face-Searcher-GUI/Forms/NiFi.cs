using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Face_Searcher_GUI.Forms
{
    public partial class NiFi : Form
    {
        const string startNifi = @"C:\Allevate\NiFi\bin\run-nifi.bat";
        const string stopNiFi = @"C:\Allevate\NiFi\bin\stop-nifi.bat";
        const string statusNiFi = @"C:\Allevate\NiFi\bin\status-nifi.bat";
        const string workingDir = @"C:\Allevate\NiFi\bin";

        public NiFi()
        {
            InitializeComponent();

            this.FormClosing += Form_FormClosing;

            string[] menuOptions = new string[] { "Camera Page", "License Info", "Camera Stream Interruption Settings", "Setup Documentation", "NiFi", "Default Area Of Interest Settings" };
            this.menu.Items.AddRange(menuOptions);
            this.menu.SelectedItem = "NiFi";

            if (checkNifiStatus())
            {
                //Process is still running. Update everything that we need to
                nifiStartButton.Text = "STOP";
                nifiStartButton.BackColor = Color.Red;
                nifiStartButton.Refresh();
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
                    //case "NiFi":
                    //Program.NF = new NiFi();
                    //Program.NF.Show();
                    break;
            }
        }

        /// <summary>
        /// Start/Stop the Nifi executable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nifiStartButton_Click(object sender, EventArgs e)
        {
            if (nifiStartButton.Text == "START")
            {
                //Display ... as yellow meaning that this is processing
                nifiStartButton.Text = "...";
                nifiStartButton.BackColor = Color.Yellow;
                nifiStartButton.Refresh();

                //Start the NiFi application
                int procID = startBackgroundProc(startNifi, false);

                if (procID != 0)
                {
                    //Make sure that we save the proc ID
                    //Process is now running. Update everything that we need to
                    nifiStartButton.Text = "STOP";
                    nifiStartButton.BackColor = Color.Red;
                    nifiStartButton.Refresh();
                }
                else
                {
                    //Warn the user that this failed to run for some reason
                    MessageBox.Show("Failed to start NiFi Interface", "Error!", MessageBoxButtons.OK);
                    nifiStartButton.Text = "START";
                    nifiStartButton.BackColor = Color.MediumSeaGreen;
                    nifiStartButton.Refresh();
                }
            }
            else
            {
                //Display ... as yellow meaning that this is processing
                nifiStartButton.Text = "...";
                nifiStartButton.BackColor = Color.Yellow;
                nifiStartButton.Refresh();

                //Stop the NiFi application
                int procID = startBackgroundProc(stopNiFi, true);

                if (procID != 0)
                {
                    nifiStartButton.Text = "START";
                    nifiStartButton.BackColor = Color.MediumSeaGreen;
                    nifiStartButton.Refresh();                    
                }
                else
                {
                    nifiStartButton.Text = "STOP";
                    nifiStartButton.BackColor = Color.Red;
                    nifiStartButton.Refresh();
                }

               
            }
        }

        /// <summary>
        /// Open the NiFi web interface. This does not handle closing because it is a webpage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webInterfaceButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Open the default browser and go to the NiFi link
                System.Diagnostics.Process.Start("http://localhost:8080/nifi");
            }
            catch
            {
                //Warn the user that this failed to run for some reason
                MessageBox.Show("Failed to start NiFi Web Interface", "Error!", MessageBoxButtons.OK);
            }
        }
        
        /// <summary>
        /// Start the NiFi process
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="blnWaitForExit"></param>
        /// <returns></returns>
        private static int startBackgroundProc(string filePath, bool blnWaitForExit)
        {
            int procID = 0;
            try
            {
                //Set all of the variables needed to start the background process
                Process proc = new Process();
                proc.StartInfo.FileName = filePath;
                proc.StartInfo.UseShellExecute = false;
                //Make sure to hide the window
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.CreateNoWindow = true;
                //Add the working directory
                //This needs to be done for bat files apparently
                proc.StartInfo.WorkingDirectory = workingDir;
                //Start the process
                proc.Start();
                //Get the process ID to return
                procID = proc.Id;

                if (blnWaitForExit)
                {
                    //Make sure that we wait for an exit before continuing
                    proc.WaitForExit();
                }
            }
            catch(Exception x)
            {
                procID = 0;
                MessageBox.Show("Cannot perform desired action. The file '" + filePath + "' cannot be found.", "ERROR", MessageBoxButtons.OK);
            }
            return procID;
        }

        /// <summary>
        /// Check if Nifi is still running on our computer or not
        /// </summary>
        /// <returns></returns>
        private bool checkNifiStatus()
        {
            Process proc = new Process();
            proc.StartInfo.FileName = statusNiFi;
            proc.StartInfo.UseShellExecute = false;
            //Make sure to hide the window
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;

            //Add the working directory
            //This needs to be done for bat files apparently
            proc.StartInfo.WorkingDirectory = workingDir;

            //Start the process
            proc.Start();
            proc.WaitForExit();

            bool running = false;

            string output = proc.StandardOutput.ReadToEnd().ToUpper();
            if (output.Contains("CURRENTLY RUNNING"))
            {
                running = true;
            }
            return running;
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
