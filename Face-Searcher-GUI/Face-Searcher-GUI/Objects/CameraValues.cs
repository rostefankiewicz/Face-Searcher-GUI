using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Face_Searcher_GUI.Objects
{
    public class CameraValues
    {
        public bool running = false;
        public int restartsTried = 0;
        //=================== cameraIndex
        public string cameraIndex = "0";
        //=================== cameraMetaDataSettings
        public string location = "Location";
        public string CameraID = "CameraID";
        public string URL = "RTSP";
        public string numberImages = "1";
        //=================== videoStreamControlSettings
        public string version = "1";
        public string face_detect_top = Program.defaultAOI.face_detect_top;
        public string face_detect_left = Program.defaultAOI.face_detect_left;
        public string face_detect_height = Program.defaultAOI.face_detect_height;
        public string face_detect_width = Program.defaultAOI.face_detect_width;
        public string face_detect_min_height_prop = Program.defaultAOI.face_detect_min_height_prop;
        public string face_detect_max_height_prop = Program.defaultAOI.face_detect_max_height_prop;
        public string frame_interval = Program.defaultAOI.frame_interval;
        public string reduce_frame_step = Program.defaultAOI.reduce_frame_step;
        public string generate_templates = Program.defaultAOI.generate_templates;
        public string perform_ranking = Program.defaultAOI.perform_ranking;
        public string verification_thresh = Program.defaultAOI.verification_thresh;
        /*
        public string force_every_frame = Program.defaultAOI.force_every_frame;
        public string save_local = Program.defaultAOI.save_local;
        public string write_debug = Program.defaultAOI.write_debug;
        */
        //=================== videoStreamResultsSettings
        public string save_xml_results = "true";
        public string save_xml_folder = "C:/Allevate/Face-Searcher/POST/Camera[CamNUMBER]/";
        public string remove_files = "false";
        public string post_xml = "false";
        public string post_url = "http://localhost:81";
        public string save_images = "false";
        public string save_images_folder = "C:/Allevate/Face-Searcher/Results/Images/Camera[CamNUMBER]/";
        public string save_only_verified = "false";
        public string root_tag = "CustomerInfoRequest";
        public string person_name = "false";
        public string person_name_tag = "externalId";
        public string person_id = "false";
        public string person_id_tag = "person_id";
        public string processed_frames = "true";
        public string processed_frames_tag = "ProcessedFrames";
        public string head_id = "true";
        public string head_id_tag = "head_id";
        public string stream_type = "true";
        public string stream_type_tag = "stream_type";
        public string stream_connection_info = "true";
        public string stream_connection_info_tag = "conn_info";
        public string stream_index = "true";
        public string stream_index_tag = "stream_index";
        public string verification_threshold = "true";
        public string verification_threshold_tag = "VerificationThresh";
        public string frame_number = "true";
        public string frame_number_tag = "FrameNumber";
        public string utc_time = "true";
        public string utc_time_tag = "utcTime";
        public string ranked_results = "true";
        public string ranked_results_n = "1";
        public string ranked_results_tag = "NumOfRankedResults";
        public string matched_person_name = "false";
        public string matched_person_name_tag = "externalId";
        public string matched_person_id = "false";
        public string matched_person_id_tag = "person_id";
        public string matched_image_id = "false";
        public string matched_image_id_tag = "image_id";
        public string date_time_stamp = "true";
        public string date_time_stamp_tag = "DateTime";
        public string confidence_measure = "true";
        public string confidence_measure_tag = "Confidence";
        public string focus_measure = "true";
        public string focus_measure_tag = "Focus";
        public string eye_positions = "true";
        public string eye_positions_tag = "EyePositions";
        public string face_box = "true";
        public string face_box_tag = "FaceBox";
        public string match_score = "false";
        public string match_score_tag = "MatchScore";
        public string matched_status = "false";
        public string matched_status_tag = "matchStatus";
        public string tracked_image = "true";
        public string tracked_image_tag = "capturedImage";
        public string matched_person_thumbnail = "false";
        public string matched_person_thumbnail_tag = "MatchedPersonImage";
        public string matched_thumbnail = "false";
        public string matched_thumbnail_tag = "MatchedImage";

        /// <summary>
        /// Take the given XML string and parse it into each Camera
        /// </summary>
        /// <param name="xml">string of xml that contains everything</param>
        /// <returns></returns>
        public void loadCam(string xml)
        {
            try
            {
                if (xml.Trim() != "")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    //Load the XML string into the xmlDoc variable
                    xmlDoc.LoadXml(xml);

                    //=================== cameraIndex
                    this.cameraIndex = xmlDoc.GetElementsByTagName("cameraIndex")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    //=================== cameraMetaDataSettings
                    this.location = xmlDoc.GetElementsByTagName("location")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.CameraID = xmlDoc.GetElementsByTagName("CameraID")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.URL = xmlDoc.GetElementsByTagName("URL")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.numberImages = xmlDoc.GetElementsByTagName("numberImages")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    //=================== videoStreamControlSettings
                    this.face_detect_top = xmlDoc.GetElementsByTagName("face_detect_top")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_left = xmlDoc.GetElementsByTagName("face_detect_left")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_height = xmlDoc.GetElementsByTagName("face_detect_height")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_width = xmlDoc.GetElementsByTagName("face_detect_width")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_min_height_prop = xmlDoc.GetElementsByTagName("face_detect_min_height_prop")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_max_height_prop = xmlDoc.GetElementsByTagName("face_detect_max_height_prop")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.frame_interval = xmlDoc.GetElementsByTagName("frame_interval")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.reduce_frame_step = xmlDoc.GetElementsByTagName("reduce_frame_step")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.generate_templates = xmlDoc.GetElementsByTagName("generate_templates")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.perform_ranking = xmlDoc.GetElementsByTagName("perform_ranking")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.verification_thresh = xmlDoc.GetElementsByTagName("verification_thresh")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    //=================== videoStreamResultsSettings
                    this.save_xml_results = xmlDoc.GetElementsByTagName("save_xml_results")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.save_xml_folder = xmlDoc.GetElementsByTagName("save_xml_folder")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.remove_files = xmlDoc.GetElementsByTagName("remove_files")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.post_xml = xmlDoc.GetElementsByTagName("post_xml")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.post_url = xmlDoc.GetElementsByTagName("post_url")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    //this.save_images = xmlDoc.GetElementsByTagName("save_images")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.save_images_folder = xmlDoc.GetElementsByTagName("save_images_folder")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.save_only_verified = xmlDoc.GetElementsByTagName("save_only_verified")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.root_tag = xmlDoc.GetElementsByTagName("root_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.person_name = xmlDoc.GetElementsByTagName("person_name")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.person_name_tag = xmlDoc.GetElementsByTagName("person_name_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.person_id = xmlDoc.GetElementsByTagName("person_id")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.person_id_tag = xmlDoc.GetElementsByTagName("person_id_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.processed_frames = xmlDoc.GetElementsByTagName("processed_frames")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.processed_frames_tag = xmlDoc.GetElementsByTagName("processed_frames_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.head_id = xmlDoc.GetElementsByTagName("head_id")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.head_id_tag = xmlDoc.GetElementsByTagName("head_id_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.stream_type = xmlDoc.GetElementsByTagName("stream_type")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.stream_type_tag = xmlDoc.GetElementsByTagName("stream_type_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.stream_connection_info = xmlDoc.GetElementsByTagName("stream_connection_info")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.stream_connection_info_tag = xmlDoc.GetElementsByTagName("stream_connection_info_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.stream_index = xmlDoc.GetElementsByTagName("stream_index")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.stream_index_tag = xmlDoc.GetElementsByTagName("stream_index_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.verification_threshold = xmlDoc.GetElementsByTagName("stream_index_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.verification_threshold_tag = xmlDoc.GetElementsByTagName("verification_threshold_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.frame_number = xmlDoc.GetElementsByTagName("frame_number")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.frame_number_tag = xmlDoc.GetElementsByTagName("frame_number_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.utc_time = xmlDoc.GetElementsByTagName("utc_time")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.utc_time_tag = xmlDoc.GetElementsByTagName("utc_time_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.ranked_results = xmlDoc.GetElementsByTagName("ranked_results")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.ranked_results_n = xmlDoc.GetElementsByTagName("ranked_results_n")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.ranked_results_tag = xmlDoc.GetElementsByTagName("ranked_results_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_person_name = xmlDoc.GetElementsByTagName("matched_person_name")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_person_name_tag = xmlDoc.GetElementsByTagName("matched_person_name_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_person_id = xmlDoc.GetElementsByTagName("matched_person_id")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_person_id_tag = xmlDoc.GetElementsByTagName("matched_person_id_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_image_id = xmlDoc.GetElementsByTagName("matched_image_id")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_image_id_tag = xmlDoc.GetElementsByTagName("matched_image_id_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.date_time_stamp = xmlDoc.GetElementsByTagName("date_time_stamp")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.date_time_stamp_tag = xmlDoc.GetElementsByTagName("date_time_stamp_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.confidence_measure = xmlDoc.GetElementsByTagName("confidence_measure")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.confidence_measure_tag = xmlDoc.GetElementsByTagName("confidence_measure_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.focus_measure = xmlDoc.GetElementsByTagName("focus_measure")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.focus_measure_tag = xmlDoc.GetElementsByTagName("focus_measure_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.eye_positions = xmlDoc.GetElementsByTagName("eye_positions")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.eye_positions_tag = xmlDoc.GetElementsByTagName("eye_positions_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_box = xmlDoc.GetElementsByTagName("face_box")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_box_tag = xmlDoc.GetElementsByTagName("face_box_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.match_score = xmlDoc.GetElementsByTagName("match_score")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.match_score_tag = xmlDoc.GetElementsByTagName("match_score_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_status = xmlDoc.GetElementsByTagName("matched_status")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_status_tag = xmlDoc.GetElementsByTagName("matched_status_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.tracked_image = xmlDoc.GetElementsByTagName("tracked_image")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.tracked_image_tag = xmlDoc.GetElementsByTagName("tracked_image_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_person_thumbnail = xmlDoc.GetElementsByTagName("matched_person_thumbnail")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_person_thumbnail_tag = xmlDoc.GetElementsByTagName("matched_person_thumbnail_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_thumbnail = xmlDoc.GetElementsByTagName("matched_thumbnail")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.matched_thumbnail_tag = xmlDoc.GetElementsByTagName("matched_thumbnail_tag")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                }
            }catch
            {
                //Do nothing
            }
            fillInTheBlanks();
        }

        /// <summary>
        /// After loading all of the values, go through and check if any of them are blank. If so, fill them in
        /// </summary>
        public void fillInTheBlanks()
        {
            //===============cameraIndex
            if (this.cameraIndex == "") { this.cameraIndex = "0"; }
            //===============cameraMetaDataSettings
            if (this.location == "") { this.location = "Local"; }
            if (this.CameraID == "") { this.CameraID = "CamID"; }
            if (this.URL == "") { this.URL = "RTSP"; }
            if (this.numberImages == "") { this.numberImages = "1"; }
            //===============videoStreamControlSettings
            if (this.face_detect_top == "") { this.face_detect_top = Program.defaultAOI.face_detect_top; }
            if (this.face_detect_left == "") { this.face_detect_left = Program.defaultAOI.face_detect_left; }
            if (this.face_detect_height == "") { this.face_detect_height = Program.defaultAOI.face_detect_height; }
            if (this.face_detect_width == "") { this.face_detect_width = Program.defaultAOI.face_detect_width; }
            if (this.face_detect_min_height_prop == "") { this.face_detect_min_height_prop = Program.defaultAOI.face_detect_min_height_prop; }
            if (this.face_detect_max_height_prop == "") { this.face_detect_max_height_prop = Program.defaultAOI.face_detect_max_height_prop; }
            if (this.frame_interval == "") { this.frame_interval = Program.defaultAOI.frame_interval; }
            if (this.reduce_frame_step == "") { this.reduce_frame_step = Program.defaultAOI.reduce_frame_step; }
            if (this.generate_templates == "") { this.generate_templates = Program.defaultAOI.generate_templates; }
            if (this.perform_ranking == "") { this.perform_ranking = Program.defaultAOI.perform_ranking; }
            if (this.verification_thresh == "") { this.verification_thresh = Program.defaultAOI.verification_thresh; }
            //===============videoStreamResultsSettings
            if (this.save_xml_results == "") { this.save_xml_results = "true"; }
            if (this.save_xml_folder == "") { this.save_xml_folder = "C:/Allevate/Face-Searcher/POST/Camera[CamNUMBER]/"; }
            if (this.remove_files == "") { this.remove_files = "false"; }
            if (this.post_xml == "") { this.post_xml = "false"; }
            if (this.post_url == "") { this.post_url = "http://localhost:81"; }
            if (this.save_images_folder == "") { this.save_images_folder = "C:/Allevate/Face-Searcher/Results/Images/Camera[CamNUMBER]/"; }
            if (this.save_only_verified == "") { this.save_only_verified = "false"; }
            if (this.root_tag == "") { this.root_tag = "CustomerInfoRequest"; }
            if (this.person_name == "") { this.person_name = "false"; }
            if (this.person_name_tag == "") { this.person_name_tag = "externalId"; }
            if (this.person_id == "") { this.person_id = "false"; }
            if (this.person_id_tag == "") { this.person_id_tag = "person_id"; }
            if (this.processed_frames == "") { this.processed_frames = "true"; }
            if (this.processed_frames_tag == "") { this.processed_frames_tag = "ProcessedFrames"; }
            if (this.head_id == "") { this.head_id = "true"; }
            if (this.head_id_tag == "") { this.head_id_tag = "head_id"; }
            if (this.stream_type == "") { this.stream_type = "true"; }
            if (this.stream_type_tag == "") { this.stream_type_tag = "stream_type"; }
            if (this.stream_connection_info == "") { this.stream_connection_info = "true"; }
            if (this.stream_connection_info_tag == "") { this.stream_connection_info_tag = "conn_info"; }
            if (this.stream_index == "") { this.stream_index = "true"; }
            if (this.stream_index_tag == "") { this.stream_index_tag = "stream_index"; }
            if (this.verification_threshold == "") { this.verification_threshold = "true"; }
            if (this.verification_threshold_tag == "") { this.verification_threshold_tag = "VerificationThresh"; }
            if (this.frame_number == "") { this.frame_number = "true"; }
            if (this.frame_number_tag == "") { this.frame_number_tag = "FrameNumber"; }
            if (this.utc_time == "") { this.utc_time = "true"; }
            if (this.utc_time_tag == "") { this.utc_time_tag = "utcTime"; }
            if (this.ranked_results == "") { this.ranked_results = "true"; }
            if (this.ranked_results_n == "") { this.ranked_results_n = "1"; }
            if (this.ranked_results_tag == "") { this.ranked_results_tag = "NumOfRankedResults"; }
            if (this.matched_person_name == "") { this.matched_person_name = "false"; }
            if (this.matched_person_name_tag == "") { this.matched_person_name_tag = "externalId"; }
            if (this.matched_person_id == "") { this.matched_person_id = "false"; }
            if (this.matched_person_id_tag == "") { this.matched_person_id_tag = "person_id"; }
            if (this.matched_image_id == "") { this.matched_image_id = "false"; }
            if (this.matched_image_id_tag == "") { this.matched_image_id_tag = "image_id"; }
            if (this.date_time_stamp == "") { this.date_time_stamp = "true"; }
            if (this.date_time_stamp_tag == "") { this.date_time_stamp_tag = "DateTime"; }
            if (this.confidence_measure == "") { this.confidence_measure = "true"; }
            if (this.confidence_measure_tag == "") { this.confidence_measure_tag = "Confidence"; }
            if (this.focus_measure == "") { this.focus_measure = "true"; }
            if (this.focus_measure_tag == "") { this.focus_measure_tag = "Focus"; }
            if (this.eye_positions == "") { this.eye_positions = "true"; }
            if (this.eye_positions_tag == "") { this.eye_positions_tag = "EyePositions"; }
            if (this.face_box == "") { this.face_box = "true"; }
            if (this.face_box_tag == "") { this.face_box_tag = "FaceBox"; }
            if (this.match_score == "") { this.match_score = "false"; }
            if (this.match_score_tag == "") { this.match_score_tag = "MatchScore"; }
            if (this.matched_status == "") { this.matched_status = "false"; }
            if (this.matched_status_tag == "") { this.matched_status_tag = "matchStatus"; }
            if (this.tracked_image == "") { this.tracked_image = "true"; }
            if (this.tracked_image_tag == "") { this.tracked_image_tag = "capturedImage"; }
            if (this.matched_person_thumbnail == "") { this.matched_person_thumbnail = "true"; }
            if (this.matched_person_thumbnail_tag == "") { this.matched_person_thumbnail_tag = "MatchedPersonImage"; }
            if (this.matched_thumbnail == "") { this.matched_thumbnail = "false"; }
            if (this.matched_thumbnail_tag == "") { this.matched_thumbnail_tag = "MatchedImage"; }
        }

        /// <summary>
        /// Take all of the cameras and return them as XML
        /// </summary>
        /// <returns>XML string of all Cameras</returns>
        public string toXML()
        {
            StringBuilder ret = new StringBuilder();
            ret.AppendFormat("<{0}>", "recordNumber");
            ret.AppendFormat("<{0}>{1}</{0}>", "cameraIndex", this.cameraIndex.Replace("<", "&lt;").Replace(">", "&gt;"));

            ret.AppendFormat("<{0}>", "cameraMetaDataSettings");
            ret.AppendFormat("<{0}>{1}</{0}>", "location", this.location.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "CameraID", this.CameraID.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "URL", this.URL.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "numberImages", this.numberImages.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("</{0}>", "cameraMetaDataSettings");

            ret.AppendFormat("<{0}>", "videoStreamControlSettings");
            ret.AppendFormat("<{0}>{1}</{0}>", "version", this.version.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "face_detect_top", this.face_detect_top.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "face_detect_left", this.face_detect_left.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "face_detect_height", this.face_detect_height.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "face_detect_width", this.face_detect_width.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "face_detect_min_height_prop", this.face_detect_min_height_prop.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "face_detect_max_height_prop", this.face_detect_max_height_prop.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "frame_interval", this.frame_interval.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "reduce_frame_step", this.reduce_frame_step.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "generate_templates", this.generate_templates.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "perform_ranking", this.perform_ranking.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "verification_thresh", this.verification_thresh.Replace("<", "&lt;").Replace(">", "&gt;"));
            //ret.AppendFormat("<{0}>{1}</{0}>", "force_every_frame", this.force_every_frame.Replace("<", "&lt;").Replace(">", "&gt;"));
            //ret.AppendFormat("<{0}>{1}</{0}>", "save_local", this.save_local.Replace("<", "&lt;").Replace(">", "&gt;"));
            //ret.AppendFormat("<{0}>{1}</{0}>", "write_debug", this.write_debug.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("</{0}>", "videoStreamControlSettings");

            ret.AppendFormat("<{0}>", "videoStreamResultsSettings");
            ret.AppendFormat("<{0}>{1}</{0}>", "save_xml_results", this.save_xml_results.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "save_xml_folder", this.save_xml_folder.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "remove_files", this.remove_files.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "post_xml", this.post_xml.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "post_url", this.post_url.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "save_images", this.save_images.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "save_images_folder", this.save_images_folder.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "save_only_verified", this.save_only_verified.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "root_tag", this.root_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "person_name", this.person_name.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "person_name_tag", this.person_name_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "person_id", this.person_id.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "person_id_tag", this.person_id_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "processed_frames", this.processed_frames.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "processed_frames_tag", this.processed_frames_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "head_id", this.head_id.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "head_id_tag", this.head_id_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "stream_type", this.stream_type.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "stream_type_tag", this.stream_type_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "stream_connection_info", this.stream_connection_info.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "stream_connection_info_tag", this.stream_connection_info_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "stream_index", this.stream_index.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "stream_index_tag", this.stream_index_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "verification_threshold", this.verification_threshold.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "verification_threshold_tag", this.verification_threshold_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "frame_number", this.frame_number.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "frame_number_tag", this.frame_number_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "utc_time", this.utc_time.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "utc_time_tag", this.utc_time_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "ranked_results", this.ranked_results.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "ranked_results_n", this.ranked_results_n.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "ranked_results_tag", this.ranked_results_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_person_name", this.matched_person_name.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_person_name_tag", this.matched_person_name_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_person_id", this.matched_person_id.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_person_id_tag", this.matched_person_id_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_image_id", this.matched_image_id.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_image_id_tag", this.matched_image_id_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "date_time_stamp", this.date_time_stamp.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "date_time_stamp_tag", this.date_time_stamp_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "confidence_measure", this.confidence_measure.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "confidence_measure_tag", this.confidence_measure_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "focus_measure", this.focus_measure.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "focus_measure_tag", this.focus_measure_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "eye_positions", this.eye_positions.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "eye_positions_tag", this.eye_positions_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "face_box", this.face_box.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "face_box_tag", this.face_box_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "match_score", this.match_score.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "match_score_tag", this.match_score_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_status", this.matched_status.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_status_tag", this.matched_status_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "tracked_image", this.tracked_image.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "tracked_image_tag", this.tracked_image_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_person_thumbnail", this.matched_person_thumbnail.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_person_thumbnail_tag", this.matched_person_thumbnail_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_thumbnail", this.matched_thumbnail.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "matched_thumbnail_tag", this.matched_thumbnail_tag.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("</{0}>", "videoStreamResultsSettings");

            ret.AppendFormat("</{0}>", "recordNumber");
            //Return the value
            return ret.ToString();
        }

        /// <summary>
        /// When you set a collection = another collection, when you change any values, it will be reflected in the other collection
        /// This gives us the option to make a safe copy
        /// </summary>
        /// <param name="CV">CameraValues that we want to copy</param>
        /// <returns></returns>
        public CameraValues clone(bool keepRunning = false)
        {
            CameraValues newCV = new CameraValues();
            //Start assigning values
            if (keepRunning) { newCV.running = this.running; }
            newCV.cameraIndex = this.cameraIndex;
            newCV.location = this.location;
            newCV.CameraID = this.CameraID;
            newCV.URL = this.URL;
            newCV.numberImages = this.numberImages;
            newCV.face_detect_top = this.face_detect_top;
            newCV.face_detect_left = this.face_detect_left;
            newCV.face_detect_height = this.face_detect_height;
            newCV.face_detect_width = this.face_detect_width;
            newCV.face_detect_min_height_prop = this.face_detect_min_height_prop;
            newCV.face_detect_max_height_prop = this.face_detect_max_height_prop;
            newCV.frame_interval = this.frame_interval;
            newCV.reduce_frame_step = this.reduce_frame_step;
            newCV.generate_templates = this.generate_templates;
            newCV.perform_ranking = this.perform_ranking;
            newCV.verification_thresh = this.verification_thresh;
            newCV.save_xml_results = this.save_xml_results;
            newCV.save_xml_folder = this.save_xml_folder;
            newCV.remove_files = this.remove_files;
            newCV.post_xml = this.post_xml;
            newCV.post_url = this.post_url;
            newCV.save_images = this.save_images;
            newCV.save_images_folder = this.save_images_folder;
            newCV.save_only_verified = this.save_only_verified;
            newCV.root_tag = this.root_tag;
            newCV.person_name = this.person_name;
            newCV.person_name_tag = this.person_name_tag;
            newCV.person_id = this.person_id;
            newCV.person_id_tag = this.person_id_tag;
            newCV.processed_frames = this.processed_frames;
            newCV.processed_frames_tag = this.processed_frames_tag;
            newCV.head_id = this.head_id;
            newCV.head_id_tag = this.head_id_tag;
            newCV.stream_type = this.stream_type;
            newCV.stream_type_tag = this.stream_type_tag;
            newCV.stream_connection_info = this.stream_connection_info;
            newCV.stream_connection_info_tag = this.stream_connection_info_tag;
            newCV.stream_index = this.stream_index;
            newCV.stream_index_tag = this.stream_index_tag;
            newCV.verification_threshold = this.verification_threshold;
            newCV.verification_threshold_tag = this.verification_threshold_tag;
            newCV.frame_number = this.frame_number;
            newCV.frame_number_tag = this.frame_number_tag;
            newCV.utc_time = this.utc_time;
            newCV.utc_time_tag = this.utc_time_tag;
            newCV.ranked_results = this.ranked_results;
            newCV.ranked_results_n = this.ranked_results_n;
            newCV.ranked_results_tag = this.ranked_results_tag;
            newCV.matched_person_name = this.matched_person_name;
            newCV.matched_person_name_tag = this.matched_person_name_tag;
            newCV.matched_person_id = this.matched_person_id;
            newCV.matched_person_id_tag = this.matched_person_id_tag;
            newCV.matched_image_id = this.matched_image_id;
            newCV.matched_image_id_tag = this.matched_image_id_tag;
            newCV.date_time_stamp = this.date_time_stamp;
            newCV.date_time_stamp_tag = this.date_time_stamp_tag;
            newCV.confidence_measure = this.confidence_measure;
            newCV.confidence_measure_tag = this.confidence_measure_tag;
            newCV.focus_measure = this.focus_measure;
            newCV.focus_measure_tag = this.focus_measure_tag;
            newCV.eye_positions = this.eye_positions;
            newCV.eye_positions_tag = this.eye_positions_tag;
            newCV.face_box = this.face_box;
            newCV.face_box_tag = this.face_box_tag;
            newCV.match_score = this.match_score;
            newCV.match_score_tag = this.match_score_tag;
            newCV.matched_status = this.matched_status;
            newCV.matched_status_tag = this.matched_status_tag;
            newCV.tracked_image = this.tracked_image;
            newCV.tracked_image_tag = this.tracked_image_tag;
            newCV.matched_person_thumbnail = this.matched_person_thumbnail;
            newCV.matched_person_thumbnail_tag = this.matched_person_thumbnail_tag;
            newCV.matched_thumbnail = this.matched_thumbnail;
            newCV.matched_thumbnail_tag = this.matched_thumbnail_tag;
            //Return the new CameraValues
            return newCV;
        }
    }
}
