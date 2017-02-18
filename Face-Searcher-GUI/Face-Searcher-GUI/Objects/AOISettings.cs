using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Face_Searcher_GUI.Objects
{
    class AOISettings
    {
        public string face_detect_top = "0.001";
        public string face_detect_left = "0.001";
        public string face_detect_height = "0.998";
        public string face_detect_width = "0.998";
        public string face_detect_min_height_prop = "0.200";
        public string face_detect_max_height_prop = "0.600";
        public string frame_interval = "0";
        public string reduce_frame_step = "0";
        public string generate_templates = "false";
        public string perform_ranking = "false";
        public string verification_thresh = "0.000";
        //public string force_every_frame = "true";
        //public string save_local = "false";
        //public string write_debug = "false";

        /// <summary>
        /// Take the XML content and load the email settings
        /// </summary>
        /// <param name="xml"></param>
        public void loadAOI(string xml)
        {
            try
            {
                if (xml.Trim() != "")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    //Load the XML string into the xmlDoc variable
                    xmlDoc.LoadXml(xml);
                    this.face_detect_top = xmlDoc.GetElementsByTagName("def_face_detect_top")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_left = xmlDoc.GetElementsByTagName("def_face_detect_left")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_height = xmlDoc.GetElementsByTagName("def_face_detect_height")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_width = xmlDoc.GetElementsByTagName("def_face_detect_width")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_min_height_prop = xmlDoc.GetElementsByTagName("def_face_detect_min_height_prop")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.face_detect_max_height_prop = xmlDoc.GetElementsByTagName("def_face_detect_max_height_prop")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.frame_interval = xmlDoc.GetElementsByTagName("def_frame_interval")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.reduce_frame_step = xmlDoc.GetElementsByTagName("def_reduce_frame_step")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.generate_templates = xmlDoc.GetElementsByTagName("def_generate_templates")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.perform_ranking = xmlDoc.GetElementsByTagName("def_perform_ranking")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.verification_thresh = xmlDoc.GetElementsByTagName("def_verification_thresh")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    //this.force_every_frame = xmlDoc.GetElementsByTagName("def_force_every_frame")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    //this.save_local = xmlDoc.GetElementsByTagName("def_save_local")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    //this.write_debug = xmlDoc.GetElementsByTagName("def_write_debug")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                }
            }
            catch
            {
                //do nothing
            }
        }

        /// <summary>
        /// Take all the values for this class and put them into an XML string
        /// </summary>
        /// <returns></returns>
        public string toXML()
        {
            StringBuilder ret = new StringBuilder().Replace("&lt;", "<").Replace("&gt;", ">");
            //Pretty simple here
            ret.AppendFormat("<{0}>", "defaultVideoStreamControlSettings");
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
            ret.AppendFormat("</{0}>", "defaultVideoStreamControlSettings");

            return ret.ToString().Replace("&lt;", "<").Replace("&gt;", ">");
        }
    }
}
