using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace Face_Searcher_GUI.Objects
{
    public class AureusEdge
    {
        const string aureusedge_path = @"C:\Allevate\Face-Searcher\x64\Release\AureusEdge.dll";

        [DllImport(aureusedge_path, EntryPoint = "LEON_CreateAndInitializeAureus", CharSet = CharSet.Ansi)]
        public static extern IntPtr LEON_CreateAndInitializeAureus([Out] StringBuilder message);

        [DllImport(aureusedge_path, EntryPoint = "LEON_FreeAureus", CharSet = CharSet.Ansi)]
        public static extern bool LEON_FreeAureus(IntPtr p_aureus, [Out] StringBuilder msg);

        // Returns false if fails - e.g., you try to create more cameras than you are licensed for
        // If licensed number of streams is maxed out, must reuse a cameraindex
        [DllImport(aureusedge_path, EntryPoint = "LEON_CreateCamera", CharSet = CharSet.Ansi)]
        public static extern bool LEON_CreateCamera(IntPtr p_aureus, int cameraindex, int mediatype, [Out] StringBuilder msg);

        [DllImport(aureusedge_path, EntryPoint = "LEON_RunningStatus", CharSet = CharSet.Ansi)]
        public static extern int LEON_RunningStatus(int cameraindex);

        [DllImport(aureusedge_path, EntryPoint = "LEON_StartCameraByStream", CharSet = CharSet.Ansi)]
        public static extern bool LEON_StartCameraByStream(int stream, [Out] StringBuilder msg);

        [DllImport(aureusedge_path, EntryPoint = "LEON_StartCameraByIndex", CharSet = CharSet.Ansi)]
        public static extern bool LEON_StartCameraByIndex(int cameraindex, [Out] StringBuilder msg);

        [DllImport(aureusedge_path, EntryPoint = "LEON_StartAllCameras", CharSet = CharSet.Ansi)]
        public static extern void LEON_StartAllCameras([Out] StringBuilder msg);

        [DllImport(aureusedge_path, EntryPoint = "LEON_StopCameraByStream", CharSet = CharSet.Ansi)]
        public static extern bool LEON_StopCameraByStream(int stream, [Out] StringBuilder msg);

        [DllImport(aureusedge_path, EntryPoint = "LEON_StopCameraByIndex", CharSet = CharSet.Ansi)]
        public static extern bool LEON_StopCameraByIndex(int cameraindex, [Out] StringBuilder msg);

        [DllImport(aureusedge_path, EntryPoint = "LEON_StopAllCameras", CharSet = CharSet.Ansi)]
        public static extern void LEON_StopAllCameras([Out] StringBuilder msg);

        [DllImport(aureusedge_path, EntryPoint = "LEON_DestroyAllCameras", CharSet = CharSet.Ansi)]
        public static extern void LEON_DestroyAllCameras();

        [DllImport(aureusedge_path, EntryPoint = "LEON_GetLicenseInfo", CharSet = CharSet.Ansi)]
        public static extern void LEON_GetLicenseInfo(IntPtr p_aureus, [Out] StringBuilder msg);

        [DllImport(aureusedge_path, EntryPoint = "LEON_GetCamerasInfo", CharSet = CharSet.Ansi)]
        public static extern void LEON_GetCamerasInfo(IntPtr p_aureus, [Out] StringBuilder msg);

        [DllImport(aureusedge_path, EntryPoint = "LEON_GetRestartInterval", CharSet = CharSet.Ansi)]
        public static extern int LEON_GetRestartInterval();

        [DllImport(aureusedge_path, EntryPoint = "LEON_RestartCamera", CharSet = CharSet.Ansi)]
        public static extern void LEON_RestartCamera(int cameraindex);


        // AureusEdge C++ functions defined for C# use
        public static IntPtr CreateAndInitializeAureus([Out] StringBuilder msg)
        {
            return LEON_CreateAndInitializeAureus(msg);
        }

        public static bool FreeAureus(IntPtr p_aureus, [Out] StringBuilder msg)
        {
            return LEON_FreeAureus(p_aureus, msg);
        }

        public static bool CreateCamera(IntPtr p_aureus, int cameraindex, int mediatype, [Out] StringBuilder msg)
        {
            return LEON_CreateCamera(p_aureus, cameraindex, mediatype, msg);
        }

        // running: 0, stopped: 1, unexpectedly terminated: 2, restart failed: 3
        public static int RunningStatus(int cameraindex)
        {
            return LEON_RunningStatus(cameraindex);
        }

        public static bool StartCameraByStream(int stream, [Out] StringBuilder msg)
        {
            return LEON_StartCameraByStream(stream, msg);
        }

        public static bool StartCameraByIndex(int cameraindex, [Out] StringBuilder msg)
        {
            return LEON_StartCameraByIndex(cameraindex, msg);
        }

        public static void StartAllCameras([Out] StringBuilder msg)
        {
            LEON_StartAllCameras(msg);
        }

        public static bool StopCameraByStream(int stream, [Out] StringBuilder msg)
        {
            return LEON_StopCameraByStream(stream, msg);
        }

        public static bool StopCameraByIndex(int cameraindex, [Out] StringBuilder msg)
        {
            return LEON_StopCameraByIndex(cameraindex, msg);
        }

        public static void StopAllCameras([Out] StringBuilder msg)
        {
            LEON_StopAllCameras(msg);
        }

        public static void DestroyAllCameras()
        {
            LEON_DestroyAllCameras();
        }

        public static void GetLicenseInfo(IntPtr p_aureus, [Out] StringBuilder msg)
        {
            LEON_GetLicenseInfo(p_aureus, msg);
        }

        public static void GetCamerasInfo(IntPtr p_aureus, [Out] StringBuilder msg)
        {
            LEON_GetCamerasInfo(p_aureus, msg);
        }

        public static int GetRestartInterval()
        {
            return LEON_GetRestartInterval();
        }

        public static void RestartCamera(int cameraindex)
        {
            LEON_RestartCamera(cameraindex);
        }
        
        // Other functions
        // Woody, ignore CheckForUnexpectedTerminations. That's just for me for testing. This is the function for you to use. It creates a new thread to run RestartCamera.
        public static void RestartCameraInBackground(int cameraindex)
        {
            Thread thread = new Thread(() => RestartCamera(cameraindex));
            thread.Start();
        }

        public static void CheckForUnexpectedTerminations()
        {
            while (true)
            {
                for (int index = 1; index <= 3; index++)
                {
                    if (RunningStatus(index) == 2)
                    {
                        RestartCameraInBackground(index);
                    }
                }
            }
        }
    }
}
