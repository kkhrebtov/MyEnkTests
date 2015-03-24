using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using NUnit.Framework;
using White.Core;
using White.Core.UIItems.Finders;

namespace Enkata.ActivityTracker.Core
{
    public static class AT
    {

        private const int DelayAfterKeyEvent = 1000;
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MouseClickDelay = 2000;
        private static string _date;

        [DllImport("msi.dll")]
        private static extern int MsiInstallProduct(string packagePath,
        string commandLine);
        
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "GetWindow")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        static public void Install()
        {            
            string path = Path.Combine(NunitSettings.InstallFileLocation, NunitSettings.InstallFileName);
            Console.WriteLine("Installing AT from " + path + " ...");
            MsiInstallProduct(path, "SYSTEMSETTINGSPATH=" + SystemSettings.FilePath);
            for (var i = 0; i < 100; i++)
            {
                Thread.Sleep(500);
                if (WatchdogExists() && OsrExists())
                {
                    Thread.Sleep(20000);
                    break;
                }
            }
        }

        static public void InstallMsiFile(string pathToInstallationFile, string msi)
        {
            MsiInstallProduct(Path.Combine(pathToInstallationFile, msi), "SYSTEMSETTINGSPATH=" + SystemSettings.FilePath);
        }

        public static bool WindowExists(string nameWindow, string elementInWindow)
        {
            var enkataWindow = FindWindowByCaption(IntPtr.Zero, nameWindow);
            if (FindWindowEx(enkataWindow, IntPtr.Zero, "Static", elementInWindow) != IntPtr.Zero)
                return true;

            if (Environment.OSVersion.Version.Major != 5)
            {
                UserInputs.PressEnter();
                return enkataWindow != IntPtr.Zero;
            }

            return false;
        }

        static public void UninstallAt(string pathToInstallationFile)
        {
            string path = Path.Combine(pathToInstallationFile, NunitSettings.InstallFileName);
            Console.WriteLine("Uninstalling AT. Path " + Path.Combine(pathToInstallationFile, NunitSettings.InstallFileName + " ..."));
            MsiInstallProduct(path, "REMOVE=ALL");
        }
        
        public static bool DttExists()
        {
            return Service.Exists(NunitSettings.ServiceDttName);
        }

        public static bool WatchdogExists()
        {
            return Service.Exists(NunitSettings.ServiceWdName);
        }

        public static bool OsrExists()
        {
            return Process.Exists(NunitSettings.OsRunTimeName);
        }

        public static bool OpenSpanServiceExists()
        {
            return Service.Exists(NunitSettings.OpenSpanService);
        }

        public static bool OpenSpanDriverServiceExists()
        {
            return Service.Exists(NunitSettings.OpenSpanDriverService);
        }
       
        public static List<FileInfo> GetContentOfFolder(string pathToFolder, string mask)
        {
            try
            {
                var openWith = new List<FileInfo>();
                var di = new DirectoryInfo(pathToFolder);
                var rgFiles = di.GetFiles(mask);
                foreach (var fi in rgFiles)
                {
                    openWith.Add(fi);
                }
                return openWith;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<FileInfo>();
            }
        }

        /// <summary>
        /// Private methods
        /// </summary>
        /// <returns></returns>
        public static string GetDate()
        {
            var curentDate = DateTime.Now;
            return curentDate.ToString("yyyy-MM-dd");
        }

        public static void CheckOutPutFolder(string dttPath)
        {
            _date = GetDate();
            //Check folders
            var pathCombine = Path.Combine(dttPath, _date);
            Assert.IsTrue(DirectoryExists(pathCombine + @"\PACKETS"));
            Assert.IsTrue(DirectoryExists(pathCombine + @"\LOGS"));

            //check packets
            Assert.IsTrue(GetContentOfFolder(pathCombine + @"\PACKETS", "*.packet").Count >= 1);
        }

        public static void DeleteFolder(string pathToDirectory)
        {
            for (var i = 0; i < 10; i++)
            {
                try
                {
                    Thread.Sleep(1000);
                    Directory.Delete(pathToDirectory, true);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static bool DirectoryExists(string path)
        {
            return new DirectoryInfo(path).Exists;
        }

        public static void CheckRuntimeError()
        {
            var all = Desktop.Instance.GetMultiple(SearchCriteria.All);

            foreach (var element in all)
            {
                if (element.Name.Contains("Microsoft Visual C++ Runtime Library"))
                {
                    Console.WriteLine("Microsoft Visual C++ Runtime Library Error detected");
                    element.Click();
                    Thread.Sleep(2000);
                    UserInputs.PressKey((int)VirtualKeys.Return);
                    Thread.Sleep(30000);
                    return;
                }
            }
            return;
        }

        public static bool CheckMessageBox()
        {
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name == "Information" && element.Get(SearchCriteria.ByText("OpenSpan project is loaded")) != null)
                {
                    element.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("OK")).RaiseClickEvent();
                    Thread.Sleep(2000);
                    return true;
                }
            }

            return false;
        }
        
        public static void CleanMachine()
        {
           try
            {
                AT.DeleteFolder(Path.Combine(Path.Combine(NunitSettings.DttPath.ToString(), AT.GetDate()),"PACKETS"));
                Directory.CreateDirectory(Path.Combine(Path.Combine(NunitSettings.DttPath.ToString(), AT.GetDate()), "PACKETS"));

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            try
            {
               AT.DeleteFolder(NunitSettings.TempFolder);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
                                    
        }

        public static void UpdateSystemSettings()
        {
            using (var systemSettings = new SystemSettings())
            {
                //Set value packet-duration = 20
                systemSettings.packetDuration = 1800;
                //data-transfer-settings poll-period = ‘20’
                systemSettings.dttPollPeriod = 20;
            }

        }
    }
}
