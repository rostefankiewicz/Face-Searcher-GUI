using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Face_Searcher_GUI.Objects
{
    public class EmailSettings
    {
        //Email Settings
        public string emailAccountName = "";
        public string sender = "";
        public string password = "";
        public string contents = "";
        public List<string> receiver = new List<string>();
        public string emailNotifications = "true";
        //Camera Restart Info
        public string stream_timeout = "5"; //How long Aureus waits before saying this stream is bad
        public string camera_restart_interval = "5"; //Try to restart every X seconds
        public string camera_restart_attempts = "10"; //Total retry attempts before killing the camera

        /// <summary>
        /// Take the XML content and load the email settings
        /// </summary>
        /// <param name="xml"></param>
        public void loadEmail(string xml)
        {
            try
            {
                //Do not parse an empty string. This will break things
                if (xml.Trim() != "")
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    //Load the XML string into the xmlDoc variable
                    xmlDoc.LoadXml(xml);
                    this.emailAccountName = xmlDoc.GetElementsByTagName("emailAccountName")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.sender = xmlDoc.GetElementsByTagName("sender")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.password = xmlDoc.GetElementsByTagName("password")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.contents = xmlDoc.GetElementsByTagName("contents")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    string rec = xmlDoc.GetElementsByTagName("receiver")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    receiver = rec.Split(',').ToList();
                    this.emailNotifications = xmlDoc.GetElementsByTagName("emailNotifications")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    try
                    {
                        this.stream_timeout = xmlDoc.GetElementsByTagName("stream_timeout")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    }
                    catch
                    {
                        this.stream_timeout = "5";
                    }
                    this.camera_restart_interval = xmlDoc.GetElementsByTagName("camera_restart_interval")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                    this.camera_restart_attempts = xmlDoc.GetElementsByTagName("camera_restart_attempts")[0].InnerText.Trim().Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&");
                }
            }
            catch
            {
                //Do Nothing
            }
        }

        /// <summary>
        /// Take all the values for this class and put them into an XML string
        /// </summary>
        /// <returns></returns>
        public string toXML()
        {
            StringBuilder ret = new StringBuilder();
            //Build the string to return
            ret.AppendFormat("<{0}>", "emailConfiguration");
            ret.AppendFormat("<{0}>", "emailSettings");
            ret.AppendFormat("<{0}>{1}</{0}>", "emailAccountName", this.emailAccountName.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "sender", this.sender.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "password", this.password.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "contents", this.contents.Replace("<", "&lt;").Replace(">", "&gt;"));
            //Convert from a list to a comma separted string
            string rec = "";
            for (int x =0; x< receiver.Count; x++)
            {
                rec += receiver[x] + ",";
            }
            rec = rec.Trim(',');
            ret.AppendFormat("<{0}>{1}</{0}>", "receiver", rec.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("<{0}>{1}</{0}>", "emailNotifications", this.emailNotifications.Replace("<", "&lt;").Replace(">", "&gt;"));
            ret.AppendFormat("</{0}>", "emailSettings");
            ret.AppendFormat("</{0}>", "emailConfiguration");

            //Restart Settings
            ret.AppendFormat("<{0}>", "automaticCameraRestart");
            ret.AppendFormat("<{0}>{1}</{0}>", "camera_restart_interval", this.camera_restart_interval);
            ret.AppendFormat("<{0}>{1}</{0}>", "camera_restart_attempts", this.camera_restart_attempts);
            ret.AppendFormat("<{0}>{1}</{0}>", "stream_timeout", this.stream_timeout);
            ret.AppendFormat("</{0}>", "automaticCameraRestart");

            //Return it
            return ret.ToString();
        }
    }
}
