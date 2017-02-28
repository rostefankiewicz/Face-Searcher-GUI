using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.IO;
using Face_Searcher_GUI.Forms;
using Face_Searcher_GUI.Objects;
using System.Xml;

namespace Face_Searcher_GUI
{
    static class Program
    {
        //Collection of Cameras
        internal static CameraCollection CAMS;
        internal static CameraCollection TempCAMs;
        internal static int numberOfLicense = 1;
        internal static int usedLicense = 0;
        internal static bool killApplication = false;

        //Default AOI settings
        internal static AOISettings defaultAOI;

        //All Email settings
        internal static EmailSettings email;

        //Forms
        internal static CameraForm CF;
        internal static Documentation DT;
        internal static EmailSetup ES;
        internal static LicensePage LP;
        internal static NiFi NF;
        internal static DefaultAOI DA;
        internal static InvalidLicense IL;

        //Aureus edge
        internal static IntPtr mp_aureus;
        internal static StringBuilder msg = new StringBuilder(1024);
        internal static DateTime licenseEXP = DateTime.Now;

        internal static System.Threading.Thread camMonitorThread;
        internal static CameraMonitor CM;

        //All external PDFs, EXEs and other files
        const string Settings = @"C:\Allevate\Face-Searcher\CameraConfiguration.xml";
        internal static string settingsContent = "";

        internal static string allevateURL = "http://allevate.com?utm_source=FaceSearcher_Edge";
        const string licenseFile = @"C:\Allevate\Face-Searcher\AureusSDK_License.txt";
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mp_aureus = AureusEdge.CreateAndInitializeAureus(msg);
            checkLicense();

            settingsContent = readFromFile(Settings);
            
            //Declare all of the new global variables from above
            CAMS = new CameraCollection();
            defaultAOI = new AOISettings();
            email = new EmailSettings();

            //Call the function to load everything
            loadAll();

            TempCAMs = CAMS.clone(true);
            
            getLicenseNumber();

            CM = new CameraMonitor();
            camMonitorThread = new System.Threading.Thread(new System.Threading.ThreadStart(CM.monitor));
            camMonitorThread.Start();
            
            CF = new CameraForm();
            Application.Run(CF);
        }

        /// <summary>
        /// Check if the user has their license. If not, do not let them continue
        /// This will put the user in essentially an infinite loop until they get their license
        /// </summary>
        internal static void checkLicense()
        {
            bool canContinue = false;
            bool firstLoop = true;
            string failReason = "";
            while (!canContinue)
            {
                if (File.Exists(licenseFile))
                {
                    //File does exist
                    string fileContent = readFromFile(licenseFile).Trim();
                    if (fileContent == "")
                    {
                        //File exists but is empty
                        failReason = "FILE-EMPTY";
                    }
                    else
                    {
                        if (!firstLoop)
                        {
                            //Kill it
                            AureusEdge.FreeAureus(mp_aureus, msg);
                            //Re-initilize it
                            mp_aureus = AureusEdge.CreateAndInitializeAureus(msg);
                        }
                        //Check if the license is valid
                        AureusEdge.GetLicenseInfo(mp_aureus, msg);
                        if (msg.ToString().Contains("Error!"))
                        {
                            //Invalid license
                            failReason = "INVALID-CONTENT";
                        }
                        else
                        {
                            //Do calculation
                            licenseEXP = getLicenseExp(msg.ToString());
                            if (licenseEXP <= DateTime.Now)
                            {
                                //License has expired
                                failReason = "EXPIRED";
                            }
                            else
                            {
                                canContinue = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    //File does not exist.
                    failReason = "FILE-NOT-FOUND";
                }
                if (!canContinue)
                {
                    //Display the error form
                    IL = new InvalidLicense();
                    IL.errorMessage = failReason;
                    IL.loadDisplay();
                    IL.ShowDialog();
                    if (killApplication) { canContinue = true; break; }
                }
                firstLoop = false;
            }

            //Kill the application. We needed to exit the loop first
            if (canContinue && killApplication)
            {
                exitApp(true);
            }

        }

        /// <summary>
        /// Get the license expiration date
        /// </summary>
        /// <param name="licenseInfo"></param>
        /// <returns></returns>
        internal static DateTime getLicenseExp(string licenseInfo)
        {
            string expDate = "";
            DateTime DT = DateTime.Now;
            try
            {
                //Days
                int a = licenseInfo.IndexOf("Time remaining") + 14;
                int b = licenseInfo.IndexOf("days");
                expDate = licenseInfo.Substring(a, b - a).Trim();
                DT = DT.AddDays(Int32.Parse(expDate));
                string days = expDate;
                //Hours
                a = licenseInfo.IndexOf("days,") + 5;
                b = licenseInfo.IndexOf("hours");
                expDate = licenseInfo.Substring(a, b - a).Trim();
                DT = DT.AddHours(Int32.Parse(expDate));
                //Minutes
                a = licenseInfo.IndexOf("hours,") + 6;
                b = licenseInfo.IndexOf("min");
                expDate = licenseInfo.Substring(a, b - a).Trim();
                DT = DT.AddMinutes(Int32.Parse(expDate));
                //Seconds
                a = licenseInfo.IndexOf("min,") + 4;
                b = licenseInfo.IndexOf("sec");
                expDate = licenseInfo.Substring(a, b - a).Trim();
                DT = DT.AddSeconds(Int32.Parse(expDate));
            }catch
            {
                //Something failed. subtracta year and return that
                DT = DateTime.Now.AddYears(-1);
            }
           
            return DT;
        }

        /// <summary>
        /// Load all of our settings into the respected collections
        /// </summary>
        /// <returns>If this part fails, then we want to kill the application</returns>
        internal static void loadAll()
        {
            try
            {
                //Get all of the settings from the one file
                settingsContent = settingsContent.Replace("&quot;", "\"").Replace("&apos;", "'");
                //settingsContent = settingsContent.Replace("&amp;", "&");
                //Load the email settings.
                email.loadEmail(settingsContent);
                //Load the default AOI settings
                defaultAOI.loadAOI(settingsContent);
                //Load the collection of cameras!
                CAMS.newList(settingsContent);
            }
            catch (Exception x)
            {
                //Show that something failed. Can't run the application if we do not have any values
                MessageBox.Show("Something failed while trying to start the application. Please notify your contact with attach the message below.\r\n\r\n" + x.ToString(), "Critical Error!", MessageBoxButtons.OK);
                //Something failed on load. Exit the application
                exitApp();
            }
        }

        /// <summary>
        /// Get the number of cameras that we are allowed to use
        /// </summary>
        internal static void getLicenseNumber()
        {
            try
            {
                AureusEdge.GetLicenseInfo(mp_aureus, msg);
                string info = msg.ToString();
                int a = info.IndexOf("=") + 2;
                int b = info.IndexOf("Time remaining");
                info = info.Substring(a, b - a).Trim();
                //Try to parse the number
                numberOfLicense = Int32.Parse(info);
            }
            catch
            {
                //If it fails, default to 1
                numberOfLicense = 1;
            }
        }

        //=================================== ALL OF THE GLOBAL FUNCTIONS WILL BE BELOW HERE ===================================\\

        /// <summary>
        /// Exit the application and kill all running items
        /// </summary>
        internal static void exitApp(bool exitEnvironment = false)
        {
            //Delete the contents of the "Results" folder
            if (System.IO.Directory.Exists(@"C:\Allevate\Face-Searcher\Results"))
            {
                System.IO.Directory.Delete(@"C:\Allevate\Face-Searcher\Results", true);
                System.IO.Directory.CreateDirectory(@"C:\Allevate\Face-Searcher\Results");
            }
            //Kill the monitor thread
            if (camMonitorThread != null)
            {
                camMonitorThread.Abort();
            }
            //Kil the AureusEdge program
            AureusEdge.FreeAureus(mp_aureus, msg);
            //If anything else needs to be done before exit. Do it here
            try
            {
                if (CF != null) { CF.Dispose(); }
                if (DT != null) { DT.Dispose(); }
                if (ES != null) { ES.Dispose(); }
                if (LP != null) { LP.Dispose(); }
                if (NF != null) { NF.Dispose(); }
                if (DA != null) { DA.Dispose(); }
                if (IL != null) { IL.Dispose(); }

                Application.Exit();
                try
                {
                    Environment.Exit(0);
                }catch
                {
                    Environment.Exit(1);
                }
                //if (exitEnvironment)
                //{
                //    Environment.Exit(0);
                //}
            }
            catch
            {
                //Do Nothing
            }
        }

        /// <summary>
        ///Read all of the text from the given file
        /// </summary>
        internal static string readFromFile(string filePath)
        {
            string ret = "";
            try
            {
                //First, check if the file exists or not
                if (File.Exists(filePath))
                {
                    //File does exists, read it
                    ret = File.ReadAllText(filePath);
                }
                else
                {
                    //File does not exists, Return blank
                    ret = "";
                }
            }
            catch
            {
                //Something caused an error. Return blank
                ret = "";
            }
            return ret;
        }

        /// <summary>
        ///Take the fileContent and write it to the target file
        /// </summary>
        internal static void writeToFile(string filePath, string fileContent)
        {
            try
            {
                //This will automatically overwrite the file's content, not appen to it.
                Encoding utf8WithoutBom = new UTF8Encoding(false);
                File.WriteAllText(filePath, fileContent, utf8WithoutBom);
            }
            catch
            {
                //Do nothing. Just need the catch for the program to work
            }
        }

        /// <summary>
        /// Save the settings
        /// </summary>
        /// <param name="values"></param>
        internal static void save(string values, string newLicense = "")
        {
            try
            {
                settingsContent = settingsContent.Replace("&", "&amp;");
                XmlDocument xmlDoc = new XmlDocument();
                //Load the XML string into the xmlDoc variable
                xmlDoc.LoadXml(settingsContent);

                string aoiSetting = "";
                string emailSetting = "";
                string licenseInfo = "";
                string autoRestart = "";
                string camInfo = "";

                //Make sure that we have something to save for the default AOI settings
                var aoi = xmlDoc.GetElementsByTagName("defaultVideoStreamControlSettings");
                if (aoi.Count > 0)
                {
                    aoiSetting = aoi[0].OuterXml;
                }
                else
                {
                    aoiSetting = defaultAOI.toXML();
                }

                //Make sure that we have something to save for the default email settings
                var tempEmail = xmlDoc.GetElementsByTagName("emailConfiguration");
                if (tempEmail.Count > 0)
                {
                    emailSetting = tempEmail[0].OuterXml;
                }
                else
                {
                    emailSetting = email.toXML();
                }

                //Only care if this it is not returned with the email settings
                if (!emailSetting.Contains("<automaticCameraRestart>"))
                {
                    //Make sure that we have something to save for the default email settings
                    var tempAuto = xmlDoc.GetElementsByTagName("automaticCameraRestart");
                    if (tempEmail.Count > 0)
                    {
                        autoRestart += tempAuto[0].OuterXml;
                    }
                    else
                    {
                        autoRestart += "<automaticCameraRestart><stream_timeout>5</stream_timeout><camera_restart_interval>5</camera_restart_interval><camera_restart_attempts>10</camera_restart_attempts></automaticCameraRestart>";
                    }
                }
                else
                {
                    autoRestart = "";
                }

                //Make sure that we have something to save for the license info
                var LI = xmlDoc.GetElementsByTagName("licenseInformation");
                if (LI.Count > 0)
                {
                    licenseInfo = LI[0].OuterXml;
                }
                else
                {
                    licenseInfo = "<licenseInformation><dateIssued>YYYYMMDDHHMMSS</dateIssued><daysLeft>text</daysLeft><dateExpires>YYYYMMDDHHMMSS</dateExpires></licenseInformation>";
                }

                //Make sure that we have something to save for the camerainfo
                var CI = xmlDoc.GetElementsByTagName("cameraConfiguration");
                if (CI.Count > 0)
                {
                    camInfo = CI[0].OuterXml;
                }
                else
                {
                    CameraValues CV = new CameraValues();
                    camInfo = "<cameraConfiguration>";
                    camInfo = CV.toXML();
                    camInfo = "</cameraConfiguration>";
                }

                string newFile = "";
                switch (values)
                {
                    case "EMAIL":
                        newFile += email.toXML(); //New value
                        newFile += autoRestart;
                        newFile += aoiSetting;
                        newFile += licenseInfo;
                        newFile += camInfo;
                        break;
                    case "AOI":
                        newFile += emailSetting;
                        newFile += autoRestart;
                        newFile += defaultAOI.toXML(); //New value
                        newFile += licenseInfo;
                        newFile += camInfo;
                        break;
                    case "LICENSE":
                        newFile += emailSetting;
                        newFile += autoRestart;
                        newFile += aoiSetting; //New value
                        if (newLicense.Trim() != "")
                        {
                            newFile += newLicense;
                        }
                        else
                        {
                            newFile += licenseInfo;
                        }
                        newFile += camInfo;
                        break;
                    case "CAM":
                        newFile += emailSetting;
                        newFile += autoRestart;
                        newFile += aoiSetting;
                        newFile += licenseInfo;
                        newFile += CAMS.toXML(true); //New value
                        break;
                    default:
                        //Save All
                        newFile += email.toXML(); //New value
                        newFile += autoRestart;
                        newFile += defaultAOI.toXML(); //New value
                        newFile += licenseInfo;
                        newFile += CAMS.toXML(true); //New value
                        break;
                }

                settingsContent = "<?xml version=\"1.0\"?><SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">" + newFile + "</SOAP-ENV:Envelope>";

                newFile = newFile.Replace("&", "&amp;");
                newFile = newFile.Replace("\"", "&quot;").Replace("'", "&apos;");
                newFile = "<?xml version=\"1.0\"?><SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">" + newFile + "</SOAP-ENV:Envelope>";
                //Assign it and save it!
                writeToFile(Settings, newFile);
            }
            catch
            {
                saveAll();
            }
        }

        /// <summary>
        /// We do not have a file most likey. Take everything and write it out
        /// </summary>
        internal static void saveAll()
        {
            string newFile = "";
            newFile += email.toXML(); //New value
            newFile += defaultAOI.toXML(); //New value
            newFile += "<licenseInformation><dateIssued>YYYYMMDDHHMMSS</dateIssued><daysLeft>text</daysLeft><dateExpires>YYYYMMDDHHMMSS</dateExpires></licenseInformation>";
            newFile += CAMS.toXML(true); //New value

            newFile = newFile.Replace("&", "&amp;");
            newFile = newFile.Replace("\"", "&quot;").Replace("'", "&apos;");
            newFile = "<?xml version=\"1.0\"?><SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">" + newFile + "</SOAP-ENV:Envelope>";
            //Assign it and save it!
            writeToFile(Settings, newFile);
        }

        /// <summary>
        /// Create a directory
        /// </summary>
        /// <param name="folderName"></param>
        internal static void createDirectory(string folderName)
        {
            try
            {
                if (!System.IO.Directory.Exists(folderName))
                {
                    System.IO.Directory.CreateDirectory(folderName);
                }
            }
            catch
            {
                //Do nothing
            }
        }
    }
}
