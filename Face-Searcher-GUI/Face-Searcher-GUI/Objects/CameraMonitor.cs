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
                foreach(CameraValues CV in Program.TempCAMs.cameras)
                {
                    //Only care if we marked it as running
                    if (CV.running)
                    {
                        //Now lets check if it is actually running
                        if (AureusEdge.RunningStatus(Int32.Parse(CV.cameraIndex)) != 0)
                        {
                            //Camera should be running but is not running
                            if (CV.restartInitiated)
                            {
                                //Check if the thread has finished processing
                                if (CV.CR.isDone)
                                {
                                    //The process has ended, Take the nessecary actions
                                    if (!CV.CR.hasSucceeded)
                                    {
                                        //Only do this if the camera did not successfully restart.
                                        CV.running = false;
                                        camErrors = "Camera " + CV.cameraIndex + " with URL " + CV.URL + " unexpectly stopped.\r\n\r\n";
                                    }
                                    //Reset all of the values as needed
                                    CV.CR = null;
                                    CV.restartInitiated = false;

                                }else
                                {
                                    //Still running. Just continue along your way :)
                                }
                            }
                            else
                            {
                                //We have not tried to restart yet. Lets do that
                                CV.restartInitiated = true;
                                CV.CR = new CameraRestart();
                                CV.CR.cameraIndex = Int32.Parse(CV.cameraIndex);
                                CV.CR.isDone = false;
                                CV.CR.hasSucceeded = false;
                                System.Threading.Thread CRThread = new System.Threading.Thread(new System.Threading.ThreadStart(CV.CR.ProcessRestart));
                                CRThread.Start();
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

                //Sleep for 10 seconds
                System.Threading.Thread.Sleep(10 * 1000);
            }
        }
    }
}
