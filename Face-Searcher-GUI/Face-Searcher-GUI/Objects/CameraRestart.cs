using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Face_Searcher_GUI.Objects
{
    public class CameraRestart
    {
        public int cameraIndex = 0;
        public bool isDone = false;
        public bool hasSucceeded = false;

        /// <summary>
        /// Send the request to Daniel and wait for it to finish
        /// </summary>
        public void ProcessRestart()
        {
            //Thread starts here

            /*
             * I call
             *      hasSucceeded = AureusEdge.RestartCameraByIndex(cameraIndex)
             * My code will send a request to AureusEdge
             * 
             * AureusEdge will have a loop from 1 to camera_restart_attempts
             *      Attempt a restart
             *      If succeeded
             *          return true
             *          break;
             *      else
             *          sleep for camera_restart_interval seconds
             * The loop ended and the camera did not restart
             *      return false
             */

            isDone = false;

            //Make the correct call and wait for it to finish
            AureusEdge.RestartCamera(cameraIndex);

            //Now that it is finished, check the status of the camera
            int status = AureusEdge.RunningStatus(cameraIndex);

            switch (status)
            {
                case 0: //running
                    hasSucceeded = true;
                    break;
                case 1: //Stopped
                    hasSucceeded = false;
                    break;
                case 2: //Unexpected Termination. Still processing
                    //Still in progress???
                    hasSucceeded = false;
                    break;
                case 3: //Restart Failed
                    hasSucceeded = false;
                    break;
                default:
                    //What happened here I wonder?
                    break;
            }

            isDone = true;
        }
    }
}