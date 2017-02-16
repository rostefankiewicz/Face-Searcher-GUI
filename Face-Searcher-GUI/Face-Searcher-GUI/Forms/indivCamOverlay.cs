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
    public partial class indivCamOverlay : Form
    {
        public CameraValues CV;

        public indivCamOverlay()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }
        
        /// <summary>
        /// The CV should be filled by now so this will take those values and fill in the text boxes
        /// </summary>
        public void loadValues()
        {
            //General Info
            this.local.Text = CV.location;
            this.camID.Text = CV.CameraID;
            this.RTSP.Text = CV.URL;
            this.local.Refresh();
            this.camID.Refresh();
            this.RTSP.Refresh();

            //AOI
            this.aoiTOP.Text = CV.face_detect_top;
            this.aoiLEFT.Text = CV.face_detect_left;
            this.aoiHeight.Text = CV.face_detect_height;
            this.aoiWIDTH.Text = CV.face_detect_width;
            this.aoiMinHead.Text = CV.face_detect_min_height_prop;
            this.aoiMaxHead.Text = CV.face_detect_max_height_prop;
            this.aoiTOP.Refresh();
            this.aoiLEFT.Refresh();
            this.aoiHeight.Refresh();
            this.aoiWIDTH.Refresh();
            this.aoiMinHead.Refresh();
            this.aoiMaxHead.Refresh();
            
            if (CV.save_images == "true")
            {
                this.saveImageCheckBox.Checked = true;
            }
        }

        /// <summary>
        /// Cancel Changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            CV.location = this.local.Text;
            CV.CameraID = this.camID.Text;
            CV.URL = this.RTSP.Text;
            CV.face_detect_top = this.aoiTOP.Text;
            CV.face_detect_left = this.aoiLEFT.Text;
            CV.face_detect_height = this.aoiHeight.Text;
            CV.face_detect_width = this.aoiWIDTH.Text;
            CV.face_detect_min_height_prop = this.aoiMinHead.Text;
            CV.face_detect_max_height_prop = this.aoiMaxHead.Text;
            CV.save_images = this.saveImageCheckBox.Checked.ToString();
            //if (this.saveImageCheckBox.Checked == true)
            //{
            //    CV.save_images = "true";
            //}
            //else
            //{
            //    CV.save_images = "false";
            //}
            

            //Save everything
            Program.CAMS = Program.TempCAMs.clone();
            Program.save("CAM");

            this.Dispose();
        }

        /// <summary>
        /// Open the advanced settings overlay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Are you sure that you would like to open the advanced settings page?", "CONFIRM", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //Open new form
                indivCamAdvanced ICA = new indivCamAdvanced();
                ICA.CV = CV;
                ICA.loadAll();
                ICA.ShowDialog();
            }
        }
    }
}