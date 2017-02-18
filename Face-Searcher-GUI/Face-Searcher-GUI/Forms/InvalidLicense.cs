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
    public partial class InvalidLicense : Form
    {
        public string errorMessage = "";
        const string machineID = @"C:\Allevate\Face-Searcher\MachineID.exe";

        public InvalidLicense()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load what needs to be displayed
        /// </summary>
        public void loadDisplay()
        {
            //Generate the machine key
            this.errorReason.Text = getErrorMessage();
            //Get the machine key
            this.machineKey.Text = getMachineID();
            //Refresh everything
            Application.DoEvents();
        }

        /// <summary>
        /// Build the error message based on the errorMessage code above
        /// </summary>
        /// <returns></returns>
        private string getErrorMessage()
        {
            string message = "";
            message = "The license retrieved for this machine is invalid. Please contact Allevate or your supplier for a valid license.\r\n\r\nError message: ";
            switch (errorMessage)
            {
                case "FILE-EMPTY":
                    message += "The correct file has been saved to your machine, but it is empty.";
                    break;
                case "INVALID-CONTENT":
                    message += "The license saved on your machine is invalid. Please obtain a new key and update the file 'C:\\Allevate\\Face-Searcher\\AureusSDK_License.txt' to continue.";
                    break;
                case "EXPIRED":
                    message += "Your license has expired. Please obtain a new key and update the file 'C:\\Allevate\\Face-Searcher\\AureusSDK_License.txt' to continue.";
                    break;
                case "FILE-NOT-FOUND":
                    message += "The license file 'C:\\Allevate\\Face-Searcher\\AureusSDK_License.txt' could not be found";
                    break;
                default:
                    message += "Something unexpected happened. Please notify your contacts of this error";
                    break;
            }
            //Return it
            return message;
        }

        /// <summary>
        /// Get the machine key
        /// </summary>
        /// <returns></returns>
        private string getMachineID()
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
            //Return it
            return output;
        }

        /// <summary>
        /// Close this form and resume the initial loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continueButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Program.killApplication = true;
            this.Dispose();
        }
    }
}
