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
    public partial class indivCamAdvanced : Form
    {
        public CameraValues CV;
        public indivCamAdvanced()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// Load all values
        /// </summary>
        public void loadAll()
        {
            //Drop downs
            string[] installs = new string[] { "true", "false" };
            this.save_xml_results.Items.AddRange(installs);
            this.remove_files.Items.AddRange(installs);
            this.post_xml.Items.AddRange(installs);
            this.save_only_verified.Items.AddRange(installs);
            this.person_name.Items.AddRange(installs);
            this.person_id.Items.AddRange(installs);
            this.processed_frames.Items.AddRange(installs);
            this.head_id.Items.AddRange(installs);
            this.stream_type.Items.AddRange(installs);
            this.stream_connection_info.Items.AddRange(installs);
            this.stream_index.Items.AddRange(installs);
            this.frame_number.Items.AddRange(installs);
            this.ranked_results.Items.AddRange(installs);
            this.utc_time.Items.AddRange(installs);
            this.matched_person_name.Items.AddRange(installs);
            this.matched_person_id.Items.AddRange(installs);
            this.matched_image_id.Items.AddRange(installs);
            this.date_time_stamp.Items.AddRange(installs);
            this.confidence_measure.Items.AddRange(installs);
            this.focus_measure.Items.AddRange(installs);
            this.eye_positions.Items.AddRange(installs);
            this.face_box.Items.AddRange(installs);
            this.match_score.Items.AddRange(installs);
            this.matched_status.Items.AddRange(installs);
            this.tracked_image.Items.AddRange(installs);
            this.matched_person_thumbnail.Items.AddRange(installs);
            this.matched_thumbnail.Items.AddRange(installs);
            this.aoiTemplates.Items.AddRange(installs);
            this.aoiRanking.Items.AddRange(installs);

            this.save_xml_results.SelectedItem = CV.save_xml_results;
            this.remove_files.SelectedItem = CV.remove_files;
            this.post_xml.SelectedItem = CV.post_xml;
            this.save_only_verified.SelectedItem = CV.save_only_verified;
            this.person_name.SelectedItem = CV.person_name;
            this.person_id.SelectedItem = CV.person_id;
            this.processed_frames.SelectedItem = CV.processed_frames;
            this.head_id.SelectedItem = CV.head_id;
            this.stream_type.SelectedItem = CV.stream_type;
            this.stream_connection_info.SelectedItem = CV.stream_connection_info;
            this.stream_index.SelectedItem = CV.stream_index;
            this.frame_number.SelectedItem = CV.frame_number;
            this.ranked_results.SelectedItem = CV.ranked_results;
            this.utc_time.SelectedItem = CV.utc_time;
            this.matched_person_name.SelectedItem = CV.matched_person_name;
            this.matched_person_id.SelectedItem = CV.matched_person_id;
            this.matched_image_id.SelectedItem = CV.matched_image_id;
            this.date_time_stamp.SelectedItem = CV.date_time_stamp;
            this.confidence_measure.SelectedItem = CV.confidence_measure;
            this.focus_measure.SelectedItem = CV.focus_measure;
            this.eye_positions.SelectedItem = CV.eye_positions;
            this.face_box.SelectedItem = CV.face_box;
            this.match_score.SelectedItem = CV.match_score;
            this.matched_status.SelectedItem = CV.matched_status;
            this.tracked_image.SelectedItem = CV.tracked_image;
            this.matched_person_thumbnail.SelectedItem = CV.matched_person_thumbnail;
            this.matched_thumbnail.SelectedItem = CV.matched_thumbnail;
            this.aoiTemplates.SelectedItem = CV.generate_templates;
            this.aoiRanking.SelectedItem = CV.perform_ranking;

            //Textboxes
            this.save_xml_folder.Text = CV.save_xml_folder;
            this.post_url.Text = CV.post_url;
            this.root_tag.Text = CV.root_tag;
            this.person_name_tag.Text = CV.person_name_tag;
            this.person_id_tag.Text = CV.person_id_tag;
            this.processed_frames_tag.Text = CV.processed_frames_tag;
            this.head_id_tag.Text = CV.head_id_tag;
            this.stream_type_tag.Text = CV.stream_type_tag;
            this.stream_connection_info_tag.Text = CV.stream_connection_info_tag;
            this.stream_index_tag.Text = CV.stream_index_tag;
            this.verification_threshold.Text = CV.verification_threshold;
            this.verification_threshold_tag.Text = CV.verification_threshold_tag;
            this.frame_number_tag.Text = CV.frame_number_tag;
            this.ranked_results_n.Text = CV.ranked_results_n;
            this.ranked_results_tag.Text = CV.ranked_results_tag;
            this.utc_time_tag.Text = CV.utc_time_tag;
            this.matched_person_name_tag.Text = CV.matched_person_name_tag;
            this.matched_person_id_tag.Text = CV.matched_person_id_tag;
            this.matched_image_id_tag.Text = CV.matched_image_id_tag;
            this.date_time_stamp_tag.Text = CV.date_time_stamp_tag;
            this.confidence_measure_tag.Text = CV.confidence_measure_tag;
            this.focus_measure_tag.Text = CV.focus_measure_tag;
            this.eye_positions_tag.Text = CV.eye_positions_tag;
            this.face_box_tag.Text = CV.face_box_tag;
            this.match_score_tag.Text = CV.match_score_tag;
            this.matched_status_tag.Text = CV.matched_status_tag;
            this.tracked_image_tag.Text = CV.tracked_image_tag;
            this.matched_person_thumbnail_tag.Text = CV.matched_person_thumbnail_tag;
            this.matched_thumbnail_tag.Text = CV.matched_thumbnail_tag;
            this.aoiFramInterval.Text = CV.frame_interval;
            this.aoiReduceFramInterval.Text = CV.reduce_frame_step;
            this.aoiVerThresh.Text = CV.verification_thresh;
            this.save_images_folder.Text = CV.save_images_folder;
        }

        /// <summary>
        /// Close the overlay and do not save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        /// <summary>
        /// Save all changes and close the overlay
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            CV.save_xml_results = this.save_xml_results.SelectedItem.ToString();
            CV.remove_files = this.remove_files.SelectedItem.ToString();
            CV.post_xml = this.post_xml.SelectedItem.ToString();
            CV.save_only_verified = this.save_only_verified.SelectedItem.ToString();
            CV.person_name = this.person_name.SelectedItem.ToString();
            CV.person_id = this.person_id.SelectedItem.ToString();
            CV.processed_frames = this.processed_frames.SelectedItem.ToString();
            CV.head_id = this.head_id.SelectedItem.ToString();
            CV.stream_type = this.stream_type.SelectedItem.ToString();
            CV.stream_connection_info = this.stream_connection_info.SelectedItem.ToString();
            CV.stream_index = this.stream_index.SelectedItem.ToString();
            CV.frame_number = this.frame_number.SelectedItem.ToString();
            CV.ranked_results = this.ranked_results.SelectedItem.ToString();
            CV.utc_time = this.utc_time.SelectedItem.ToString();
            CV.matched_person_name = this.matched_person_name.SelectedItem.ToString();
            CV.matched_person_id = this.matched_person_id.SelectedItem.ToString();
            CV.matched_image_id = this.matched_image_id.SelectedItem.ToString();
            CV.date_time_stamp = this.date_time_stamp.SelectedItem.ToString();
            CV.confidence_measure = this.confidence_measure.SelectedItem.ToString();
            CV.focus_measure = this.focus_measure.SelectedItem.ToString();
            CV.eye_positions = this.eye_positions.SelectedItem.ToString();
            CV.face_box = this.face_box.SelectedItem.ToString();
            CV.match_score = this.match_score.SelectedItem.ToString();
            CV.matched_status = this.matched_status.SelectedItem.ToString();
            CV.tracked_image = this.tracked_image.SelectedItem.ToString();
            CV.matched_person_thumbnail = this.matched_person_thumbnail.SelectedItem.ToString();
            CV.matched_thumbnail = this.matched_thumbnail.SelectedItem.ToString();
            CV.save_xml_folder = this.save_xml_folder.Text;
            CV.post_url = this.post_url.Text;
            CV.root_tag = this.root_tag.Text;
            CV.person_name_tag = this.person_name_tag.Text;
            CV.person_id_tag = this.person_id_tag.Text;
            CV.processed_frames_tag = this.processed_frames_tag.Text;
            CV.head_id_tag = this.head_id_tag.Text;
            CV.stream_type_tag = this.stream_type_tag.Text;
            CV.stream_connection_info_tag = this.stream_connection_info_tag.Text;
            CV.stream_index_tag = this.stream_index_tag.Text;
            CV.verification_threshold = this.verification_threshold.Text;
            CV.verification_threshold_tag = this.verification_threshold_tag.Text;
            CV.frame_number_tag = this.frame_number_tag.Text;
            CV.ranked_results_n = this.ranked_results_n.Text;
            CV.ranked_results_tag = this.ranked_results_tag.Text;
            CV.utc_time_tag = this.utc_time_tag.Text;
            CV.matched_person_name_tag = this.matched_person_name_tag.Text;
            CV.matched_person_id_tag = this.matched_person_id_tag.Text;
            CV.matched_image_id_tag = this.matched_image_id_tag.Text;
            CV.date_time_stamp_tag = this.date_time_stamp_tag.Text;
            CV.confidence_measure_tag = this.confidence_measure_tag.Text;
            CV.focus_measure_tag = this.focus_measure_tag.Text;
            CV.eye_positions_tag = this.eye_positions_tag.Text;
            CV.face_box_tag = this.face_box_tag.Text;
            CV.match_score_tag = this.match_score_tag.Text;
            CV.matched_status_tag = this.matched_status_tag.Text;
            CV.tracked_image_tag = this.tracked_image_tag.Text;
            CV.matched_person_thumbnail_tag = this.matched_person_thumbnail_tag.Text;
            CV.matched_thumbnail_tag = this.matched_thumbnail_tag.Text;
            CV.save_images_folder = this.save_images_folder.Text;

            CV.frame_interval = this.aoiFramInterval.Text;
            CV.reduce_frame_step = this.aoiReduceFramInterval.Text;
            CV.verification_thresh = this.aoiVerThresh.Text;
            CV.generate_templates = this.aoiTemplates.SelectedItem.ToString();
            CV.perform_ranking = this.aoiRanking.SelectedItem.ToString();

            //Save everything
            Program.CAMS = Program.TempCAMs.clone();
            Program.save("CAM");

            this.Dispose();
        }

        /// <summary>
        /// Restore everything back to normal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you would like to restore all settings to their default values?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CameraValues tempCV = new CameraValues();
                tempCV.location = CV.location;
                tempCV.CameraID = CV.CameraID;
                tempCV.URL = CV.URL;
                tempCV.face_detect_top = CV.face_detect_top;
                tempCV.face_detect_left = CV.face_detect_left;
                tempCV.face_detect_height = CV.face_detect_height;
                tempCV.face_detect_width = CV.face_detect_width;
                tempCV.face_detect_min_height_prop = CV.face_detect_min_height_prop;
                tempCV.face_detect_max_height_prop = CV.face_detect_max_height_prop;
                tempCV.save_images = CV.save_images;

                CV = tempCV.clone();

                loadAll();
            }
        }
    }
}
