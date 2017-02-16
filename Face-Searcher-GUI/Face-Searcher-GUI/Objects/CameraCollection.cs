using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Face_Searcher_GUI.Objects
{
    class CameraCollection
    {
        public List<CameraValues> cameras = new List<CameraValues>();

        /// <summary>
        /// Take the given XML string and parse it into each Camera
        /// </summary>
        /// <param name="xml">string of xml that contains everything</param>
        /// <returns></returns>
        public void newList(string xml)
        {
            try
            {
                //Declare the xmlDocument variable
                XmlDocument xmlDoc = new XmlDocument();
                //Load the XML string into the xmlDoc variable
                xmlDoc.LoadXml(xml);
                //Sort all of the main XML tags into a collection
                XmlNodeList cam = xmlDoc.GetElementsByTagName("recordNumber");
                //Loop through out colletion and use the values that we currenctly need
                for (int i = 0; i < cam.Count; i++)
                {
                    //Declare the camera
                    CameraValues CV = new CameraValues();
                    //Fill the camera with out values
                    CV.loadCam(cam[i].OuterXml);
                    //Add the camera to the list
                    cameras.Add(CV);
                }
            }
            catch
            {
                if (cameras.Count <= 0)
                {
                    CameraValues CV = new CameraValues();
                    cameras.Add(CV);
                }
            }
            //Make sure that all of the numbers are in order
            fixNumbers();
            //Make sure that the list is in order
            sortList();

            Program.save("CAM");
        }

        /// <summary>
        /// Take all of the cameras and return them as XML
        /// </summary>
        /// <returns>XML string of all Cameras</returns>
        public string toXML(bool includeTag = false)
        {
            StringBuilder ret = new StringBuilder();

            if (includeTag) { ret.AppendFormat("<{0}>", "cameraConfiguration"); }
            foreach (CameraValues CV in cameras)
            {
                //Call the toXML function for each camera and append it to the output string
                ret.Append(CV.toXML());
            }
            if (includeTag) { ret.AppendFormat("</{0}>", "cameraConfiguration"); }

            //Return the value
            return ret.ToString();
        }

        /// <summary>
        /// Add a new Camera. This will use all default values then the user will update them accordingly
        /// </summary>
        public void addCam()
        {

        }

        /// <summary>
        /// Remove a camera at a given index
        /// </summary>
        /// <param name="camIndex">index of the camera to be removed</param>
        public void removeCam(int camIndex)
        {
            //Remove the selected camera by index
            //Not sure if this is going to be the camera number or the camera index just yet
            cameras.RemoveAt(camIndex);
            //Cal fixNumbers() to make sure that we do not have a gap in the numbers of cameras
            fixNumbers();
        }

        /// <summary>
        /// Take the collection and sort by the current Cam Number
        /// </summary>
        public void sortList()
        {
            try
            {
                cameras = cameras.OrderBy(x => x.cameraIndex).ToList();
            }catch
            {
                //Do nothing
            }
        }

        /// <summary>
        /// Loop through the list of cameras and assign then numbers as needed
        /// </summary>
        public void fixNumbers()
        {
            int tempCamNum = 1;
            foreach (CameraValues CV in cameras)
            {
                //Set the value now
                CV.cameraIndex = tempCamNum.ToString();
                CV.save_xml_folder = "C:/Allevate/Face-Searcher/POST/Camera" + CV.cameraIndex + "/";
                CV.save_images_folder = "C:/Allevate/Face-Searcher/Results/Images/Camera" + CV.cameraIndex + "/";
                //Increment
                tempCamNum++;
            }
        }

        /// <summary>
        /// Create a clone of the collection
        /// </summary>
        /// <returns></returns>
        public CameraCollection clone(bool keepRunning = false)
        {
            CameraCollection C = new CameraCollection();
            foreach (CameraValues CV in this.cameras)
            {
                CameraValues CVNEW = new CameraValues();
                C.cameras.Add(CV.clone(keepRunning));
            }
            return C;
        }
    }
}
