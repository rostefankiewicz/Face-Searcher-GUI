using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Face_Searcher_GUI.Objects;
using System.Diagnostics;

namespace Face_Searcher_GUI.Forms
{
    public partial class LicensePage : Form
    {
        const string machineID = @"C:\Allevate\Face-Searcher\MachineID.exe";

        public LicensePage()
        {
            InitializeComponent();

            this.FormClosing += Form_FormClosing;

            string[] menuOptions = new string[] { "Camera Page", "License Info", "Camera Stream Interruption Settings", "Setup Documentation", "NiFi", "Default Area Of Interest Settings" };
            this.menu.Items.AddRange(menuOptions);
            this.menu.SelectedItem = "License Info";
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
        /// Return to main page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MPButton_Click(object sender, EventArgs e)
        {
            //Program.MP.Show();
            this.Dispose();
        }
        
        /// <summary>
        /// Call the get license info and display it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicensePage_Load(object sender, EventArgs e)
        {
            AureusEdge.GetLicenseInfo(Program.mp_aureus, Program.msg);
            this.mainDIsplay.Text = Program.msg.ToString();
            this.mainDIsplay.Text = this.mainDIsplay.Text.Replace("\n", "\r\n\r\n");

            //string expDate = "";
            //Days
            int a = this.mainDIsplay.Text.IndexOf("Time remaining") + 14;
            int b = this.mainDIsplay.Text.IndexOf("days");
            string days = this.mainDIsplay.Text.Substring(a, b - a).Trim();
            ////Hours
            //a = this.mainDIsplay.Text.IndexOf("days,") + 5;
            //b = this.mainDIsplay.Text.IndexOf("hours");
            //expDate = this.mainDIsplay.Text.Substring(a, b - a).Trim();
            //DT = DT.AddHours(Int32.Parse(expDate));
            ////Minutes
            //a = this.mainDIsplay.Text.IndexOf("hours,") + 6;
            //b = this.mainDIsplay.Text.IndexOf("min");
            //expDate = this.mainDIsplay.Text.Substring(a, b - a).Trim();
            //DT = DT.AddMinutes(Int32.Parse(expDate));
            ////Seconds
            //a = this.mainDIsplay.Text.IndexOf("min,") + 4;
            //b = this.mainDIsplay.Text.IndexOf("sec");
            //expDate = this.mainDIsplay.Text.Substring(a, b - a).Trim();
            //DT = DT.AddSeconds(Int32.Parse(expDate));
            DateTime DT = Program.getLicenseExp(Program.msg.ToString());

            //Save the License Info
            string newLicense = "<licenseInformation><dateIssued>" + DateTime.Now.ToString("yyyyMMddHHmmss") + "</dateIssued><daysLeft>" + days + "</daysLeft><dateExpires>" + DT.ToString("yyyyMMddHHmmss") + "</dateExpires></licenseInformation>";
            Program.save("LICENSE", newLicense);

            this.mainDIsplay.Text += "\r\n\r\nExpiration Date:\r\n\t" + DT.ToString();
            this.mainDIsplay.Text = this.mainDIsplay.Text.Replace(" = ", ":\r\n\t").Replace("Time remaining ", "Time remaining:\r\n\t");
            this.mainDIsplay.Refresh();
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
                    //case "License Info":
                    //    Program.LP = new LicensePage();
                    //    Program.LP.Show();
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

        /// <summary>
        /// Get the Machine ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MchineIDButton_Click(object sender, EventArgs e)
        {
            string output = "";
            //Get the key
            try
            {
                Process proc = new Process();
                proc.StartInfo.FileName = machineID;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                //* Read the output (or the error)
                output = proc.StandardOutput.ReadToEnd();
            }
            catch
            {
                output = "";
            }

            if (output == "")
            {
                output = "No Key Returned";
            }

            //Update the MachineKeyBox and refresh it
            this.MachineKeyBox.Text = output;
            //Make sure that the color fo the text is now black
            this.MachineKeyBox.ForeColor = Color.Black;
            this.MachineKeyBox.Refresh();
        }
    }
}
