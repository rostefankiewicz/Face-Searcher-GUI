using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Face_Searcher_GUI.Objects
{
    public class CameraMonitor
    {
        /// <summary>
        /// Monitor the cameras and check if the running ones are still running or not
        /// </summary>
        public void monitor()
        {
            //Need to continually do this until the program exits
            while (true)
            {
                string camErrors = "";
                int sleepTime = AureusEdge.GetRestartInterval();
                //Attempt to retart all cameras
                AureusEdge.RestartCameras();
                //Check each camera
                foreach(CameraValues CV in Program.TempCAMs.cameras)
                {
                    //Only care if we marked it as running
                    if (CV.running)
                    {
                        //Now lets check if it is actually running
                        if (!AureusEdge.IsRunning(Int32.Parse(CV.cameraIndex)))
                        {
                            if (CV.restartsTried >= 5)
                            {
                                //It is not anymore. Lets follow protocol and do what we need to
                                CV.running = false;
                                camErrors = "Camera " + CV.cameraIndex + " with URL " + CV.URL + " unexpectly stopped.\r\n\r\n";
                            }
                            else
                            {
                                //Don't kill the camera until after the 5th attempt
                                CV.restartsTried += 1;
                            }
                        }
                        else
                        {
                            //Just to make sure everything is running still anothing happened in between
                            CV.running = true;
                        }
                    }
                }

                //If something failed then display the message
                if (camErrors.Trim() != "")
                {
                    MessageBox.Show(camErrors, "Warning", MessageBoxButtons.OK);
                    camErrors = "";
                }

                if (Program.CF != null)
                {
                    //Reload the display
                    Program.CF.invokeStartButton();
                }

                //Sleep for 1 minute
                System.Threading.Thread.Sleep(sleepTime * 1000);
            }
        }
    }
}
