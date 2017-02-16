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
        const string aureus_path = @"C:\Allevate\Face-Searcher\x64\Release\AureusEdge.dll";
        private static Thread m_restartingCamerasThread;

        [DllImport(aureus_path, EntryPoint = "LEON_CreateAndInitializeAureus", CharSet = CharSet.Ansi)]
        public static extern IntPtr LEON_CreateAndInitializeAureus([Out] StringBuilder message);

        [DllImport(aureus_path, EntryPoint = "LEON_CreateCamera", CharSet = CharSet.Ansi)]
        public static extern bool LEON_CreateCamera(IntPtr p_aureus, int cameraindex, int mediatype, [Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_IsRunning", CharSet = CharSet.Ansi)]
        public static extern bool LEON_IsRunning(int cameraindex);

        [DllImport(aureus_path, EntryPoint = "LEON_StartCameraByStream", CharSet = CharSet.Ansi)]
        public static extern bool LEON_StartCameraByStream(int stream, [Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_StartCameraByIndex", CharSet = CharSet.Ansi)]
        public static extern bool LEON_StartCameraByIndex(int cameraindex, [Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_StartAllCameras", CharSet = CharSet.Ansi)]
        public static extern void LEON_StartAllCameras([Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_StopCameraByStream", CharSet = CharSet.Ansi)]
        public static extern bool LEON_StopCameraByStream(int stream, [Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_StopCameraByIndex", CharSet = CharSet.Ansi)]
        public static extern bool LEON_StopCameraByIndex(int cameraindex, [Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_StopAllCameras", CharSet = CharSet.Ansi)]
        public static extern void LEON_StopAllCameras([Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_DestroyCameraByIndex", CharSet = CharSet.Ansi)]
        public static extern void LEON_DestroyCameraByIndex(int cameraindex);

        [DllImport(aureus_path, EntryPoint = "LEON_DestroyCameraByStream", CharSet = CharSet.Ansi)]
        public static extern void LEON_DestroyCameraByStream(int stream);

        [DllImport(aureus_path, EntryPoint = "LEON_DestroyAllCameras", CharSet = CharSet.Ansi)]
        public static extern void LEON_DestroyAllCameras();

        [DllImport(aureus_path, EntryPoint = "LEON_GetLicenseInfo", CharSet = CharSet.Ansi)]
        public static extern void LEON_GetLicenseInfo(IntPtr p_aureus, [Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_GetCamerasInfo", CharSet = CharSet.Ansi)]
        public static extern void LEON_GetCamerasInfo(IntPtr p_aureus, [Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_FreeAureus", CharSet = CharSet.Ansi)]
        public static extern bool LEON_FreeAureus(IntPtr p_aureus, [Out] StringBuilder msg);

        [DllImport(aureus_path, EntryPoint = "LEON_RestartCameras", CharSet = CharSet.Ansi)]
        public static extern void LEON_RestartCameras();

        [DllImport(aureus_path, EntryPoint = "LEON_HelloWorld", CharSet = CharSet.Ansi)]
        public static extern void LEON_HelloWorld();

        [DllImport(aureus_path, EntryPoint = "LEON_GetRestartInterval", CharSet = CharSet.Ansi)]
        public static extern int LEON_GetRestartInterval();
        
        // AureusEdge C++ functions defined for C# use
        public static IntPtr CreateAndInitializeAureus([Out] StringBuilder msg)
        {
            return LEON_CreateAndInitializeAureus(msg);
        }

        public static bool CreateCamera(IntPtr p_aureus, int cameraindex, int mediatype, [Out] StringBuilder msg)
        {
            bool ret = false;
            try
            {
                ret = LEON_CreateCamera(p_aureus, cameraindex, mediatype, msg);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        
        public static bool IsRunning(int cameraindex)
        {
            bool ret = false;
            try
            {
                ret = LEON_IsRunning(cameraindex);
            }catch
            {
                ret = false;
            }
            return ret;
        }

        public static bool StartCameraByStream(int stream, [Out] StringBuilder msg)
        {
            bool ret = false;
            try
            {
                ret = LEON_StartCameraByStream(stream, msg);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        public static bool StartCameraByIndex(int cameraindex, [Out] StringBuilder msg)
        {
            bool ret = false;
            try
            {
                ret = LEON_StartCameraByIndex(cameraindex, msg);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        public static void StartAllCameras([Out] StringBuilder msg)
        {
            try
            {
                LEON_StartAllCameras(msg);
            }
            catch
            {
                //Do Nothing
            }
        }

        public static bool StopCameraByStream(int stream, [Out] StringBuilder msg)
        {
            bool ret = false;
            try
            {
                ret = LEON_StopCameraByStream(stream, msg);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        public static bool StopCameraByIndex(int cameraindex, [Out] StringBuilder msg)
        {
            bool ret = false;
            try
            {
                ret = LEON_StopCameraByIndex(cameraindex, msg);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        public static void StopAllCameras([Out] StringBuilder msg)
        {
            try
            {
                LEON_StopAllCameras(msg);
            }
            catch
            {
                //Do Nothing
            }
        }

        public static void DestroyCameraByIndex(int cameraindex)
        {
            try
            {
                LEON_DestroyCameraByIndex(cameraindex);
            }
            catch
            {
                //Do Nothing
            }
        }

        public static void DestroyCameraByStream(int stream)
        {             
            try
            {
                LEON_DestroyCameraByStream(stream);
            }
            catch
            {
                //Do Nothing
            }
        }

        public static void DestroyAllCameras()
        {
            try
            {
                LEON_DestroyAllCameras();
            }
            catch
            {
                //Do Nothing
            }
        }

        public static void GetLicenseInfo(IntPtr p_aureus, [Out] StringBuilder msg)
        {
            try
            {
                LEON_GetLicenseInfo(p_aureus, msg);
            }
            catch
            {
                //Do Nothing
            }
        }

        public static void GetCamerasInfo(IntPtr p_aureus, [Out] StringBuilder msg)
        {
            try
            {
                LEON_GetCamerasInfo(p_aureus, msg);
            }
            catch
            {
                //Do Nothing
            }
        }

        public static bool FreeAureus(IntPtr p_aureus, [Out] StringBuilder msg)
        {
            bool ret = false;
            try
            {
                ret = LEON_FreeAureus(p_aureus, msg);
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        public static int GetRestartInterval()
        {
            return LEON_GetRestartInterval();
        }

        public static void RestartCameras()
        {
            try
            {
                LEON_RestartCameras();
            }catch
            {
                //do Nothing
            }
        }

        public static void HelloWorld()
        {
            LEON_HelloWorld();
        }

        // Other functions
        public static void RestartCamerasPeriodically()
        {
            while (true)
            {
                Console.WriteLine("Check if any cameras need restarting");
                RestartCameras();
                int sleepTime = GetRestartInterval();
                Console.WriteLine("Going to sleep for {0} seconds then check for unexpectedly restarted cameras again", sleepTime);
                Thread.Sleep(sleepTime * 1000);
            }
        }

        public static void MakeThreadForRestartingCameras()
        {
            m_restartingCamerasThread = new Thread(() => RestartCamerasPeriodically());
            m_restartingCamerasThread.Start();
        }
    }
}
