using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Face_Searcher_GUI.Objects;

namespace Face_Searcher_GUI.Forms
{
    public partial class CameraForm : Form
    {
        public List<Panel> cameraControls = new List<Panel>();
        const string videoPlayer = @"C:\Allevate\AureusVideoStreamPlayer\AureusVideoStreamPlayer.exe";

        public CameraForm()
        {
            InitializeComponent();

            this.FormClosing += Form_FormClosing;

            string[] menuOptions = new string[] { "Camera Page", "License Info", "Camera Stream Interruption Settings", "Setup Documentation", "NiFi", "Default Area Of Interest Settings" };
            this.menu.Items.AddRange(menuOptions);
            this.menu.SelectedItem = "Camera Page";

            loadControls();
        }

        /// <summary>
        /// Load all of the controls
        /// </summary>
        public void loadControls()
        {
            this.mainDisplay.Controls.Clear();
            cameraControls = new List<Panel>();
            int camNumber = 1;
            int Position = 0;
            foreach(CameraValues CV in Program.TempCAMs.cameras)
            {
                Panel P = new Panel();
                P.Size = new System.Drawing.Size(1354, 50);
                P.Location = new Point(0, Position);
                P.BorderStyle = BorderStyle.None;
                P.TabIndex = 0;

                CheckBox CB = new CheckBox();
                CB.Text = "";
                CB.AutoSize = true;
                CB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                CB.Location = new System.Drawing.Point(3, 20);
                CB.Name = "checkBox";
                CB.Size = new System.Drawing.Size(18, 17);
                CB.TabIndex = 0;
                CB.UseVisualStyleBackColor = true;

                Label NUM = new Label();
                NUM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                NUM.Location = new System.Drawing.Point(27, 0);//move over 25
                NUM.Name = "number";
                NUM.Size = new System.Drawing.Size(25, 50);
                NUM.TabIndex = 1;
                NUM.Text = CV.cameraIndex;
                NUM.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

                Label L = new Label();
                L.UseMnemonic = false;
                L.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                L.Location = new System.Drawing.Point(52, 0);//move over 25
                L.Name = "Location";
                L.Size = new System.Drawing.Size(150, 50);
                L.TabIndex = 1;
                L.Text = CV.location;
                L.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                Label ID = new Label();
                ID.UseMnemonic = false;
                ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ID.Location = new System.Drawing.Point(208, 0);//move over 25
                ID.Name = "CameraID";
                ID.Size = new System.Drawing.Size(150, 50);
                ID.TabIndex = 2;
                ID.Text = CV.CameraID;
                ID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                Label RTSP = new Label();
                RTSP.UseMnemonic = false;
                RTSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                RTSP.Location = new System.Drawing.Point(364, 0); //move over 25
                RTSP.Name = "Rtsp";
                RTSP.Size = new System.Drawing.Size(669, 50); //reduce 25
                RTSP.TabIndex = 3;
                RTSP.Text = CV.URL;
                RTSP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                Button set = new Button();
                set.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                set.Location = new System.Drawing.Point(1037, 3);
                set.Name = "Edit" +  CV.cameraIndex;
                set.Size = new System.Drawing.Size(141, 43);
                set.TabIndex = 5;
                set.Text = "Edit Camera";
                set.UseVisualStyleBackColor = true;
                set.Click += new EventHandler((sender, e) => loadCamOverlay(CV));

                Button START = new Button();
                START.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                START.Location = new System.Drawing.Point(1184, 3);
                START.Name = "Start" + CV.cameraIndex;
                START.Size = new System.Drawing.Size(141, 43);
                START.TabIndex = 5;
                START.UseVisualStyleBackColor = true;
                START.Click += new EventHandler((sender, e) => startStopIndivCam(CV, START));
                if (CV.running)
                {
                    START.Text = "STOP";
                    START.BackColor = Color.Red;
                }
                else
                {
                    START.Text = "START";
                    START.BackColor = Color.MediumSeaGreen;
                }
                
                //Add the controls to the panel
                P.Controls.Add(CB);
                P.Controls.Add(NUM);
                P.Controls.Add(L);
                P.Controls.Add(ID);
                P.Controls.Add(RTSP);
                P.Controls.Add(set);
                P.Controls.Add(START);

                //Add the panel to the list
                cameraControls.Add(P);

                camNumber += 1;
                Position += 50;
            }

            for (int i = 0; i < cameraControls.Count; i++)
            {
                this.mainDisplay.Controls.Add(cameraControls[i]);
            }
            this.mainDisplay.Refresh();
            //Make sure that we refresh the link label since all buttons will become unchecked
            this.linkLabel1.Text = "Check All";
            this.linkLabel1.Refresh();
        }

        /// <summary>
        /// Load controls from another thread
        /// </summary>
        public void invokeLoad()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(loadControls));
            }else
            {
                loadControls();
            }
        }

        /// <summary>
        /// Only reload the start/stop buttons
        /// </summary>
        public void reloadStartButtons()
        {
            for (int i = 0; i < Program.TempCAMs.cameras.Count; i++)
            {
                foreach (Control C in cameraControls[i].Controls)
                {
                    if (C is Button)
                    {
                        if (((Button)C).Text == "START" || ((Button)C).Text == "STOP")
                        {
                            if (Program.TempCAMs.cameras[i].running)
                            {
                                ((Button)C).Text = "STOP";
                                ((Button)C).BackColor = Color.Red;
                            }
                            else
                            {
                                ((Button)C).Text = "START";
                                ((Button)C).BackColor = Color.MediumSeaGreen;
                            }
                            ((Button)C).Refresh();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Reload the start button from another thread
        /// </summary>
        public void invokeStartButton()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(reloadStartButtons));
            }
            else
            {
                reloadStartButtons();
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
                Program.exitApp();
            }
        }

        /// <summary>
        /// Return to the main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainPageButton_Click(object sender, EventArgs e)
        {
            //Program.MP.Show();
            this.Dispose();
        }

        /// <summary>
        /// Display the NiFi page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nifiButton_Click(object sender, EventArgs e)
        {
            NiFi N = new NiFi();
            N.Show();
            this.Hide();
        }

        /// <summary>
        /// Add a new camera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCamera_Click(object sender, EventArgs e)
        {
            CameraValues newCV = new CameraValues();
            newCV.location = "Location";
            newCV.CameraID = "ID";
            newCV.URL = "RTSP";
            newCV.save_xml_folder = newCV.save_xml_folder.Replace("[CamNUMBER]", Program.TempCAMs.cameras.Count.ToString());
            newCV.save_images_folder = newCV.save_images_folder.Replace("[CamNUMBER]", Program.TempCAMs.cameras.Count.ToString());
            Program.TempCAMs.cameras.Add(newCV);

            Program.TempCAMs.fixNumbers();
            //Save everything
            Program.CAMS = Program.TempCAMs.clone();
            Program.save("CAM");
            loadControls();
        }

        /// <summary>
        /// Remove the selected cameras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeCamera_Click(object sender, EventArgs e)
        {
            for (int i = Program.TempCAMs.cameras.Count-1; i >= 0; i--)
            {
                foreach (Control C in cameraControls[i].Controls)
                {
                    if (C is CheckBox)
                    {
                        if (((CheckBox)C).Checked)
                        {
                            //Remove the selected cameras
                            cameraControls.RemoveAt(i);
                            Program.TempCAMs.cameras.RemoveAt(i);
                        }
                    }
                }
            }

            Program.TempCAMs.fixNumbers();
            //Save everything
            Program.CAMS = Program.TempCAMs.clone();
            Program.save("CAM");
            loadControls();
        }

        /// <summary>
        /// Display he camera overlay that lets you edit what needs to be edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadCamOverlay(CameraValues CV)
        {
            indivCamOverlay ICO = new indivCamOverlay();
            ICO.CV = CV;
            ICO.loadValues();
            ICO.ShowDialog();
            loadControls();
        }

        /// <summary>
        /// Start and stop the inividual camera
        /// </summary>
        /// <param name="CV"></param>
        private void startStopIndivCam(CameraValues CV, Button START)
        {
            string action = START.Text;

            START.Text = "....";
            START.BackColor = Color.Yellow;
            START.Refresh();
            try
            {
                if (action == "START")
                {
                    if (Program.usedLicense < Program.numberOfLicense)
                    {
                        //Add camera by index
                        try
                        {
                            AureusEdge.CreateCamera(Program.mp_aureus, Int32.Parse(CV.cameraIndex.Trim()), 2, Program.msg);
                        }
                        catch
                        {
                            //Do nothing. Camera was already added
                        }
                        //Start Camera
                        AureusEdge.StartCameraByIndex(Int32.Parse(CV.cameraIndex.Trim()), Program.msg);
                        //Check the status
                        if (AureusEdge.IsRunning(Int32.Parse(CV.cameraIndex.Trim())))
                        {
                            //Good to go. Update the display
                            CV.restartsTried = 0;
                            CV.running = true;
                            START.Text = "STOP";
                            START.BackColor = Color.Red;
                            Program.usedLicense += 1;
                        }
                        else
                        {
                            //Camera did not start
                            MessageBox.Show("Camera " + CV.cameraIndex + " Failed to start\r\n" + CV.URL, "ERROR", MessageBoxButtons.OK);
                        }
                    }else
                    {
                        MessageBox.Show("Cannot start camera. All allowed cameras are currently in use", "Warning", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    AureusEdge.StopCameraByIndex(Int32.Parse(CV.cameraIndex), Program.msg);
                    //AureusEdge.DestroyCameraByIndex(Int32.Parse(CV.cameraIndex));
                    //Check the status
                    CV.running = false;
                    START.Text = "START";
                    START.BackColor = Color.MediumSeaGreen;
                    Program.usedLicense -= 1;
                }
            }
            catch
            {
                if (action == "START")
                {
                    START.Text = "START";
                    START.BackColor = Color.MediumSeaGreen;
                }
                else
                {
                    START.Text = "STOP";
                    START.BackColor = Color.Red;
                }
                START.Refresh();
            }

            if (Program.usedLicense < 0) { Program.usedLicense = 0; }

            loadControls();
        }

        /// <summary>
        /// Start all selected cameras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startAllBut_Click(object sender, EventArgs e)
        {
            string failedCam = "";
            this.startAllBut.Text = "....";
            this.startAllBut.BackColor = Color.Yellow;
            this.startAllBut.Refresh();

            //for (int i = Program.TempCAMs.cameras.Count - 1; i >= 0; i--)
            for (int i = 0; i < Program.TempCAMs.cameras.Count; i++)
            {
                foreach (Control C in cameraControls[i].Controls)
                {
                    if (C is CheckBox)
                    {
                        if (((CheckBox)C).Checked)
                        {
                            if (Program.usedLicense < Program.numberOfLicense)
                            {
                                AureusEdge.CreateCamera(Program.mp_aureus, Int32.Parse(Program.TempCAMs.cameras[i].cameraIndex.Trim()), 2, Program.msg);
                                //Start Camera
                                AureusEdge.StartCameraByIndex(Int32.Parse(Program.TempCAMs.cameras[i].cameraIndex.Trim()), Program.msg);
                                //Check the status
                                if (AureusEdge.IsRunning(Int32.Parse(Program.TempCAMs.cameras[i].cameraIndex.Trim())))
                                {
                                    Program.TempCAMs.cameras[i].restartsTried = 0;
                                    Program.TempCAMs.cameras[i].running = true;
                                    Program.usedLicense += 1;
                                }
                                else
                                {
                                    failedCam += formatError(Program.TempCAMs.cameras[i], "Reason: System Error.         ");
                                }
                            }
                            else
                            {
                                failedCam += formatError(Program.TempCAMs.cameras[i], "Reason: Camera Limit Reached. ");
                            }
                        }
                    }
                }
            }

            if (failedCam.Trim() != "")
            {
                MessageBox.Show("The following cameras have filed to start\r\n\r\n" + failedCam, "ERROR", MessageBoxButtons.OK);
            }
            loadControls();

            this.startAllBut.Text = "Start All Selected";
            this.startAllBut.UseVisualStyleBackColor = true;
            this.startAllBut.Refresh();
        }

        /// <summary>
        /// Format the camera error
        /// </summary>
        /// <param name="CV"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        private string formatError(CameraValues CV, string reason)
        {
            string ret = "";

            if (Int32.Parse(CV.cameraIndex.Trim()) < 10)
            {
                ret = reason + "Camera   " + Int32.Parse(CV.cameraIndex) + "   RTSP: " + CV.URL + "\r\n";
            }
            else if (Int32.Parse(CV.cameraIndex.Trim()) >= 10 && Int32.Parse(CV.cameraIndex) < 100)
            {
                ret = reason + "Camera   " + Int32.Parse(CV.cameraIndex) + "   RTSP: " + CV.URL + "\r\n";
            }
            else
            {
                ret = reason + "Camera   " + Int32.Parse(CV.cameraIndex) + "   RTSP: " + CV.URL + "\r\n";
            }

            return ret;
        }

        /// <summary>
        /// Stop all selected cameras that are running
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopAllBut_Click(object sender, EventArgs e)
        {
            this.stopAllBut.Text = "....";
            this.stopAllBut.BackColor = Color.Yellow;
            this.stopAllBut.Refresh();
            
            for (int i = Program.TempCAMs.cameras.Count - 1; i >= 0; i--)
            {
                foreach (Control C in cameraControls[i].Controls)
                {
                    if (C is CheckBox)
                    {
                        if (((CheckBox)C).Checked)
                        {
                            //Stop the camera
                            AureusEdge.StopCameraByIndex(Int32.Parse(Program.TempCAMs.cameras[i].cameraIndex.Trim()), Program.msg);
                            //Set the values on the camera level
                            Program.TempCAMs.cameras[i].running = false;
                            Program.usedLicense -= 1;
                        }
                    }
                }
            }

            if (Program.usedLicense < 0) { Program.usedLicense = 0; }

            loadControls();

            this.stopAllBut.Text = "Stop All Selected";
            this.stopAllBut.UseVisualStyleBackColor = true;
            this.stopAllBut.Refresh();
        }

        /// <summary>
        /// Loop through the collection and check/uncheck all
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Text == "Check All")
            {
                for (int i = Program.TempCAMs.cameras.Count - 1; i >= 0; i--)
                {
                    foreach (Control C in cameraControls[i].Controls)
                    {
                        if (C is CheckBox)
                        {
                            ((CheckBox)C).Checked = true;
                        }
                    }
                }
                linkLabel1.Text = "Uncheck\r\nAll";
                linkLabel1.Refresh();
            }
            else
            {
                for (int i = Program.TempCAMs.cameras.Count - 1; i >= 0; i--)
                {
                    foreach (Control C in cameraControls[i].Controls)
                    {
                        if (C is CheckBox)
                        {
                            if (((CheckBox)C).Checked)
                            {
                                ((CheckBox)C).Checked = false;
                            }
                        }
                    }
                }
                linkLabel1.Text = "Check All";
                linkLabel1.Refresh();
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
                case "NiFi":
                    Program.NF = new NiFi();
                    Program.NF.Show();
                    this.Hide();
                    break;
                case "Default Area Of Interest Settings":
                    Program.DA = new DefaultAOI();
                    Program.DA.Show();
                    this.Hide();
                    break;
                default:
                    //This is this page. Do nothing
                    //case "Camera Page":
                    //Program.CF = new CameraForm();
                    //Program.CF.Show();
                    break;
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
                    }else
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
        /// Open the video player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoPlayerButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(videoPlayer);
            }
            catch
            {
                MessageBox.Show(@"Could not find file '" + videoPlayer + "'", "Warning", MessageBoxButtons.OK);
            }
        }
    }
}