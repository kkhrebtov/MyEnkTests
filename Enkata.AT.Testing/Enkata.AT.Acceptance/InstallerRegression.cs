using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Enkata.ActivityTracker.Acceptance.StressTesting;
using Microsoft.Win32;
using NUnit.Framework;
using Enkata.ActivityTracker.Core;

namespace Enkata.ActivityTracker.Acceptance
{
    [TestFixture]
    public class InstallerRegression
    {
        private static string _locationInstalFile;
        private static string _dttPath;
        private static string _win7LocationData;
        private static string _winXpAnd2003LocationData;
        private const int WaitError = 10000;
        private const string NameWindowWaring = "Enkata Activity Tracker";
        private const string NameWindowWaringXp = "Windows Installer";
        private const string NameAlert64Bit = "This package cannot be installed on 64bit Windows";
        private const string NameAlert32Bit = "This installation package is not supported by this processor type. Contact your product vendor.";
        private const string LogMsiValid32Bit = "validateX32.log";
        private const string LogMsiValid64Bit = "validateX64.log";
        private const int DelayInstall = 5000;
        private const string NameInstaller32 = "ActivityTracker.msi";
        private const string NameInstaller64 = "ActivityTracker64.msi";
        private static int _major;
        private const string LogStore = "C:\\Set";
        private static string _msiValid;
        private static string _environmentVariable;

        public InstallerRegression()
        {
            _locationInstalFile = NunitSettings.InstallFileLocation;
            _dttPath = NunitSettings.DttPath;
            _major = Environment.OSVersion.Version.Major;
            using (var systemSettings = new SystemSettings())
            {
                //Set pro
                systemSettings.projectPath = NunitSettings.ProjectPath;
                //Set value packet-duration = 20
                systemSettings.packetDuration = 1800;
                //data-transfer-settings poll-period = ‘20’
                systemSettings.dttPollPeriod = 20;
            }
            _win7LocationData = DttRegression.PathToPacket;
            _winXpAnd2003LocationData = DttRegression.PathToPacket;

            _environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            Debug.Assert(_environmentVariable != null, "_environmentVariable != null");
            if (_environmentVariable != null | _major != 5 | _environmentVariable.Contains("64"))
            {
                _msiValid = AT.DirectoryExists(@"C:\Program Files (x86)\MsiVal2") ? @"C:\Program Files (x86)\MsiVal2" : @"C:\Program Files\MsiVal2";

            }
            else
            {
                _msiValid = @"C:\Program Files\MsiVal2";
            }

        }

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            SystemSettings.StoreSystemSettings();
        }

        [SetUp]
        public void testSetup()
        {
            SystemSettings.RestoreSystemSetingsFile();
            Console.WriteLine("testSetup");
        }

        [SetUp]
        public void SetUp()
        {
            try
            {
                File.Delete(Path.Combine(LogStore, LogMsiValid32Bit));
                File.Delete(Path.Combine(LogStore, LogMsiValid64Bit));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Data);
            }
        }

        [Test]
        public void TestInstallerCleanUninstall()
        {
            Console.WriteLine("Install AT");
            BehaviorsRegression.CleanMachine();
            SetPacketDurationAT(5);
            AT.Install();

            Console.WriteLine("Check services");
            CheckProcessAndServicesStarted();

            Console.WriteLine("Check folders");
            Thread.Sleep(DelayInstall + 10000);
            Assert.IsTrue(AT.DirectoryExists(_dttPath));

            //Identify win7 or winXp
            Assert.IsTrue(_major != 5 ? AT.DirectoryExists(_win7LocationData) : AT.DirectoryExists(_winXpAnd2003LocationData), "Folder isn't created properly.");

            Console.WriteLine("Uninstall check services and folders");
            AT.UninstallAt(_locationInstalFile);
            CheckProcessAndServicesAbsent();
            Assert.IsFalse(AT.DirectoryExists(DttRegression.PathToPacket), "Folder isn't removed properly.");
        }

        [Test]
        public void InstallerNo3RdpartDependencies()
        {
            Console.WriteLine("Install AT");
            BehaviorsRegression.CleanMachine();
            AT.Install();

            Console.WriteLine("Check services");
            CheckProcessAndServicesStarted();
            Assert.IsTrue(AT.OpenSpanDriverServiceExists(), "OpenSpanDriverService isn't started.");
            Assert.IsTrue(AT.OpenSpanServiceExists(), "OpenSpanService isn't started.");

            Console.WriteLine("Check folders");
            AT.DirectoryExists(_dttPath);

            //Identify win7 or winXp
            Assert.IsTrue(_major != 5 ? AT.DirectoryExists(_win7LocationData) : AT.DirectoryExists(_winXpAnd2003LocationData));

            Console.WriteLine("Uninstall");
            AT.UninstallAt(_locationInstalFile);
        }

        [Test]
        public void InstallerWrongBitness()
        {
            Console.WriteLine("Installer (wrong bitness)");
            BehaviorsRegression.CleanMachine();
            var environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
            if (environmentVariable != null && environmentVariable.Contains("64"))
            {
                var myJob = new ThreadTest();
                var thread = new Thread(myJob.InstallMsi64) { Name = "first" };
                thread.Start();
                Thread.Sleep(WaitError + 60000);
                Assert.IsTrue(AT.WindowExists(NameWindowWaring, NameAlert64Bit));
                Calculator.ClickOnButtonByName(NameWindowWaring, "OK");
            }
            else
            {
                var myJob1 = new ThreadTest();
                var thread1 = new Thread(myJob1.InstallMsi32) { Name = "Second" };
                thread1.Start();
                Thread.Sleep(WaitError);
                Assert.IsTrue(AT.WindowExists(NameWindowWaringXp, NameAlert32Bit), "Incorrect warning message");
                Calculator.ClickOnButtonByName(NameWindowWaringXp, "OK");
            }

        }

        [Test]
        public void InstallerOrcaValidation32Bit()
        {
            Console.WriteLine("Run MsiVal2.exe for 32Bit");
            BehaviorsRegression.CleanMachine();
            var command = "\"" + _msiValid + "\\msival2.exe\" \"" + NunitSettings.InstallFileLocation + "\\ActivityTracker.msi\" \"" + _msiValid + "\\darice.cub\" /l \"" + Path.Combine(LogStore, LogMsiValid32Bit) + "\" /f";
            Program.ExecuteCommandCmd(command);

            Console.WriteLine("Check log 32bit");
            Assert.IsTrue(CountWordInFile(Path.Combine(LogStore, LogMsiValid32Bit), "ERROR") == 2 || CountWordInFile(Path.Combine(LogStore, LogMsiValid32Bit), "ERROR") == 10, "MsiValid32Bit ERROR appears in log.");
            Assert.IsTrue(CountWordInFile(Path.Combine(LogStore, LogMsiValid32Bit), "Failure") == 0, "MsiValid32Bit Failure appears in log.");
            Assert.IsTrue(CountWordInFile(Path.Combine(LogStore, LogMsiValid32Bit), "Fatal") == 0, "MsiValid32Bit Fatal appears in log.");

            Console.WriteLine("Delete log file");
            File.Delete(Path.Combine(LogStore, LogMsiValid32Bit));
            File.Delete(Path.Combine(LogStore, LogMsiValid64Bit));
        }

        [Test]
        public void InstallerOrcaValidation64Bit()
        {
            Console.WriteLine("Run MsiVal2.exe for 64Bit");
            BehaviorsRegression.CleanMachine();
            var command64 = "\"" + _msiValid + "\\msival2.exe\" \"" + NunitSettings.InstallFileLocation + "\\ActivityTracker64.msi\" \"" + _msiValid + "\\darice.cub\" /l \"" + Path.Combine(LogStore, LogMsiValid64Bit) + "\" /f";
            Program.ExecuteCommandCmd(command64);

            Console.WriteLine("Check log 64bit");
            Assert.IsTrue(CountWordInFile(Path.Combine(LogStore, LogMsiValid64Bit), "ERROR") == 17 || CountWordInFile(Path.Combine(LogStore, LogMsiValid64Bit), "ERROR") == 16, "MsiValid64Bit ERROR appears in log.");
            Assert.IsTrue(CountWordInFile(Path.Combine(LogStore, LogMsiValid64Bit), "Failure") == 0, "MsiValid64Bit Failure appears in log.");
            Assert.IsTrue(CountWordInFile(Path.Combine(LogStore, LogMsiValid64Bit), "Fatal") == 0, "MsiValid64Bit Fatal appears in log.");
        }

        [Test]
        public void InstallerUninstallAtServicesStop()
        {
            Console.WriteLine("Installer (uninstall AT services stop)");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT");
            AT.Install();

            Console.WriteLine("Stop services");
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Uninstall");
            AT.UninstallAt(_locationInstalFile);
        }

        [Test]
        public void InstallerSilentModeForGpo()
        {
            Console.WriteLine("Install AT silent");
            BehaviorsRegression.CleanMachine();
            MsiExeInstallWithKey("qb");

            Console.WriteLine("Uninstall");
            MsiExecUnInstallWithKey("uninstall","q");

            Console.WriteLine("Delete log");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void InstalAt10Times()
        {
            BehaviorsRegression.CleanMachine();
            for (var i = 0; i < 11; i++)
            {
                AT.Install();
                CheckProcessAndServicesStarted();
                AT.UninstallAt(_locationInstalFile);
                CheckProcessAndServicesAbsent();
            }
            AT.DeleteFolder(NunitSettings.DttPath);
        }

        [Test]
        public void InstallerInstallInSilentMode()
        {
            Console.WriteLine("Install AT silent");
            BehaviorsRegression.CleanMachine();
            MsiExeInstallWithKey("qn");
            CheckProcessAndServicesStarted();

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void InstallerUninstallInSilentMode()
        {
            Console.WriteLine("Install AT silent");
            BehaviorsRegression.CleanMachine();
            AT.Install();
            CheckProcessAndServicesStarted();

            Console.WriteLine("Uninstall");
            MsiExecUnInstallWithKey("uninstall", "q");

            Console.WriteLine("Delete log");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void InstallerNetworkInstallation()
        {
            Console.WriteLine("Network Installation with /a");
            BehaviorsRegression.CleanMachine();
            RunMsiExecQuiteMode("a", "q");

            Console.WriteLine("Check installed AT");
            if (File.Exists(@"D:\Enkata Technologies Inc\Activity Tracker\Watchdog.exe") || File.Exists(@"E:\Enkata Technologies Inc\Activity Tracker\Watchdog.exe") || File.Exists(@"Z:\Enkata Technologies Inc\Activity Tracker\Watchdog.exe"))
            {
               CheckLogMsi();
            }
            else
            {
                Assert.Fail("Installation works incorrectly with '/a' key");
            }

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void InstallerInstallUninstallKeyX()
        {
            BehaviorsRegression.CleanMachine();
            AT.Install();
            CheckProcessAndServicesStarted();

            Console.WriteLine("UnInstall AT with /x key");
            RunMsiExecQuiteMode("x", "q");
            CheckProcessAndServicesAbsent();

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void InstallerRepairKeyF()
        {
            BehaviorsRegression.CleanMachine();
            AT.Install();
            CheckProcessAndServicesStarted();

            Console.WriteLine("UnInstall AT with /x key");
            RunMsiExecQuiteMode("f", "qn");
            var myKey = Registry.LocalMachine.OpenSubKey(NunitSettings.LocationInReg, true);
            myKey.SetValue("SystemSettingsPath", SystemSettings.FilePath, RegistryValueKind.String);
            Service.ReStart(NunitSettings.ServiceWdName, 20000);
            Thread.Sleep(50000);
            CheckProcessAndServicesStarted();
            
            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }


        /// <summary>
        /// Private methods
        /// </summary>
        public static void CheckProcessAndServicesStarted()
        {
            Assert.IsTrue(AT.WatchdogExists(), "Watch Dog service isn't started.");
            Assert.IsTrue(AT.DttExists(), "DTT service isn't started.");
            Assert.IsTrue(AT.OsrExists(), "OSR process isn't started.");
        }


        private static void RunMsiExecQuiteMode(string key1, string key2)
        {
            if (_environmentVariable.Contains("64"))
            {
                var command64 = "msiexec /" + key1 + " " + Path.Combine(NunitSettings.InstallFileLocation, NameInstaller64) + " /" + key2 + "" + " /log " + Path.Combine(LogStore, "AT.log");
                Program.ExecuteCommandCmd(command64);
            }
            else
            {
                var command32 = "msiexec /" + key1 + " " + Path.Combine(NunitSettings.InstallFileLocation, NameInstaller32) + " /" + key2 + " /log " + Path.Combine(LogStore, "AT.log");
                Program.ExecuteCommandCmd(command32);
            }
            Thread.Sleep(DelayInstall);
        }

        private static void MsiExecUnInstallWithKey(string key1, string key2 )
        {
            RunMsiExecQuiteMode(key1, key2);
            Thread.Sleep(DelayInstall);

            Console.WriteLine("Check services");
            CheckProcessAndServicesAbsent();
        }


        private static void MsiExeInstallWithKey(string key)
        {
            if (_environmentVariable.Contains("64"))
            {
                var command64 = "msiexec /i " + Path.Combine(NunitSettings.InstallFileLocation, NameInstaller64) + " SYSTEMSETTINGSPATH=" + SystemSettings.FilePath
                    + " /" + key + " /log " + Path.Combine(LogStore, "AT.log");
                Program.ExecuteCommandCmd(command64);
            }
            else
            {
                var command32 = "msiexec /i " + Path.Combine(NunitSettings.InstallFileLocation, NameInstaller32) + " SYSTEMSETTINGSPATH=" + SystemSettings.FilePath
                    + " /" + key + " /log " + Path.Combine(LogStore, "AT.log");
                Program.ExecuteCommandCmd(command32);
            }
            Thread.Sleep(DelayInstall);
            CheckMsiLogAndServices();
        }


        private static void CheckMsiLogAndServices()
        {
            CheckLogMsi();

            Console.WriteLine("Check services");
            CheckProcessAndServicesStarted();
        }

        private static void CheckLogMsi()
        {
            Console.WriteLine("Check log during installation");
            Assert.IsTrue(CountWordInFile(Path.Combine(LogStore, "AT.log"), "ERROR") == 0, "Error appears in silent mode!");
            Assert.IsTrue(CountWordInFile(Path.Combine(LogStore, "AT.log"), "FATAL") == 0, "Fatal appears in silent mode!");
            Assert.IsTrue(CountWordInFile(Path.Combine(LogStore, "AT.log"), "Fail") == 0, "Fail appears in silent mode!");
        }

        private static void CheckProcessAndServicesAbsent()
        {
            Assert.IsFalse(AT.WatchdogExists(), "Watch Dog service isn't removed.");
            Assert.IsFalse(AT.DttExists(), "DTT service isn't removed.");
            Assert.IsFalse(AT.OsrExists(), "OSR process isn't removed.");
        }

        private static int CountWordInFile(string pathToFile, string nameWord)
        {
            var text = File.ReadAllText(pathToFile, Encoding.Unicode);
            var listWords = text.Split(new[] { ' ' });
            var count = 0;
            foreach (var word in listWords)
            {
                if (word.Contains(nameWord))
                    count = 1 + count;
            }
            return count;
        }

        private static void SetPacketDurationAT(int sec)
        {
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.packetDuration = sec;
            }
        }
    }

}
