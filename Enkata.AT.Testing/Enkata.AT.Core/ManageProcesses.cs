using System.Management;
using System;
using NUnit.Framework;

namespace Enkata.ActivityTracker.Core
{
    public static class Process
    {
        public static bool Exists(string name)
        {
            return GetProcess(name).Length > 0;
        }

        public static System.Diagnostics.Process[] GetProcess(string name)
        {
            return System.Diagnostics.Process.GetProcessesByName(name);
        }

        public static System.Diagnostics.Process[] GetAllProcess()
        {
            return System.Diagnostics.Process.GetProcesses();
        }
        
        /// <summary>
        /// This function was copied from Internet and not verified yet.
        /// </summary>
        /// <param name="pid"></param>
        public static void KillProcessAndChildren(int pid)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                System.Diagnostics.Process proc = System.Diagnostics.Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }
        
        public static void CheckProcess()
        {
            Assert.IsTrue(AT.WatchdogExists(), "Watch Dog service isn't started.");
            Assert.IsTrue(AT.DttExists(), "DTT service isn't started.");
            Assert.IsTrue(AT.OsrExists(), "OSR process isn't started.");
        }

    }

}
