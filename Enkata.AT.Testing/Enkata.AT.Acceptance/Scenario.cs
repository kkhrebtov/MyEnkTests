using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using Enkata.ActivityTracker.Core;

namespace Enkata.ActivityTracker.Acceptance
{
    
    [TestFixture]
    public class Scenario
    {
        private const int DelayHotKey = 2000;
        private const int DelayRunCalc = 5000;
        private static string _baseDir;
        private static string _locationCalc;
        private static string _dttDestinationDir;
        private static string _tempFolder;
        private static string _pathToDecriptFile;
        private static string _nameSystemSettings;
        private static string _nameServiceWd;
        private static string _nameServiceDtt;
        private static string _nameOsRunTime;
        public static Calculator Calc;

        [TestFixtureSetUp]
        public void ScenarioSetup()
        {
            _baseDir = NunitSettings.InstallFileLocation;
            _locationCalc = NunitSettings.CalcLocation;
            _dttDestinationDir = NunitSettings.DttPath;
            _tempFolder = NunitSettings.TempFolder;
            _pathToDecriptFile = _baseDir;
            _nameSystemSettings = NunitSettings.SystemSettingsNameFile;
            _nameServiceWd = NunitSettings.ServiceWdName;
            _nameServiceDtt = NunitSettings.ServiceDttName;
            _nameOsRunTime = NunitSettings.OsRunTimeName;
            SystemSettings.StoreSystemSettings();
        }

        [TestFixtureTearDown]
        public void TestFixtureCleanUp()
        {
            SystemSettings.RestoreSystemSetingsFile();

            Console.WriteLine("Unistall AT");
            AT.UninstallAt(_baseDir);
        }

        [Test]
        public void AcceptanceScenarioTest()
        {
            CheckPrerequisites();

            SetDefaultSystemSettings();
            InstallAt();

            PerformScenario();

            CheckDtt();
            CheckPacketFocusIn();
            CheckPacketHotKey();
            CheckOpenStartStop();
            CheckPacketIdleTime();
            CheckPacketEnkataEvent1();

            TestPacketDuration();
            CheckWatchlogs();
        }

        private void CheckPrerequisites()
        {
            Console.WriteLine("Check scenarion prerequisites");
            if (Directory.Exists(_dttDestinationDir))
            {
                Directory.Delete(_dttDestinationDir, true);
            }

            if (Directory.Exists(_tempFolder))
            {
                Directory.Delete(_tempFolder, true);
            }

            CheckScenarioPrerequisites();
        }

        private void CheckDtt()
        {
            Console.WriteLine("Check DTT");
            AT.CheckOutPutFolder(_dttDestinationDir);
        }

        private void CheckWatchlogs()
        {
            Console.WriteLine("Check Watch Dog logs");

            PacketParser.CheckWdLogFileNoError(_dttDestinationDir);
            PacketParser.CheckOsRunTimeLogFileExistCorrectInformation(_dttDestinationDir);
        }

        private void TestPacketDuration()
        {
            Console.WriteLine("Testing packet duration...");

            //Restore
            var packetsBefore = PacketParser.GetPackets(_dttDestinationDir, _tempFolder);
            int packetDurationInSec = 20;
            using (var systemSettings = new SystemSettings())
            {
                //Set value packet-duration = 20
                systemSettings.packetDuration = packetDurationInSec;
                //data-transfer-settings poll-period = ‘20’
                systemSettings.dttPollPeriod = packetDurationInSec;
            }

            Service.Start(_nameServiceWd);
            Service.Start(_nameServiceDtt);

            Calc = new Calculator(_locationCalc);
            Thread.Sleep(DelayRunCalc);

            int attempts = 3;
            for (int i = 0; i < attempts; ++i)
            {
                Calc.ClickOnButton("1");
                Console.WriteLine("Wait for packet to be generated.");
                Thread.Sleep(packetDurationInSec * 1000);
            }
            
            Service.Stop(_nameServiceWd);
            Service.Stop(_nameServiceDtt);
            var packets = PacketParser.GetPackets(_dttDestinationDir, _tempFolder);
            Assert.AreEqual(packets.Count - packetsBefore.Count, attempts + 1, "Packets=" + (packets.Count - packetsBefore.Count));
            Calc.Close();
        }

        private static void SetDefaultSystemSettings()
        {
            Console.WriteLine("Set default System Settings");
            using (var systemSettings = new SystemSettings())
            {
                //Set pro
                systemSettings.projectPath = NunitSettings.ProjectPath;
                //Set value packet-duration = 20
                systemSettings.packetDuration = 1800;
                //data-transfer-settings poll-period = ‘20’
                systemSettings.dttPollPeriod = 20;
            }
        }


        private void CheckScenarioPrerequisites()
        {
            Console.WriteLine("Check calc");
            Assert.IsTrue(File.Exists(_locationCalc), "Couldn't find calculator for Xp OS.");

            Console.WriteLine("Check install file");
            Assert.IsTrue(File.Exists(Path.Combine(_baseDir, NunitSettings.InstallFileName)), "Couldn't find installation file.");

            Console.WriteLine("Check decrypt files");
            Assert.IsTrue(File.Exists(Path.Combine(_baseDir, "pass.txt")), "Please put decrypt file to the base directory: pass.txt");
            Assert.IsTrue(File.Exists(Path.Combine(_baseDir, "gpg.exe")), "Please put decrypt file to the base directory: gpg.exe");
            Assert.IsTrue(File.Exists(Path.Combine(_baseDir, "pubring.gpg")), "Please put decrypt file to the base directory: pubring.gpg");
            Assert.IsTrue(File.Exists(Path.Combine(_baseDir, "secring.gpg")), "Please put decrypt file to the base directory: secring.gpg");
            Assert.IsTrue(File.Exists(Path.Combine(_baseDir, "iconv.dll")), "Please put decrypt file to the base directory: iconv.dll");
        }

        private static void InstallAt()
        {
            Console.WriteLine("Install AT");

            AT.Install();
            Assert.IsTrue(AT.WatchdogExists(), "Watch Dog service isn't started.");
            Assert.IsTrue(AT.DttExists(), "DTT service isn't started.");
            Assert.IsTrue(AT.OsrExists(), "OSR process isn't started.");
        }

        private static void PerformScenario()
        {
            Console.WriteLine("Performing scenario...");

            Calc = new Calculator(_locationCalc);
            Thread.Sleep(DelayRunCalc);
            ////Call help window
            UserInputs.PressF1();
            Thread.Sleep(DelayHotKey);
            ////Call search in help window
            UserInputs.PressAltS();
            Thread.Sleep(DelayHotKey);
            ////Close Help window
            UserInputs.PressAltF4();
            Thread.Sleep(DelayHotKey);
            //Identify win7 or winXp
            var major = Environment.OSVersion.Version.Major;
            if (major != 5)
            {
                Calc = new Calculator(_locationCalc);
                Thread.Sleep(DelayRunCalc);
            }
            Calc.ClickOnButton("3");
            Thread.Sleep(DelayHotKey);
            Calc.ClickOnButton("1");
            Thread.Sleep(DelayHotKey);
            Calc.ClickOnButton("3");
            Thread.Sleep(DelayHotKey);

            // sleep to generate second idle time
            Thread.Sleep(20000);

            Service.Stop(_nameServiceWd);
            Service.Stop(_nameServiceDtt);
            ////Close calc and uninstall AT
            Calc.Close();
        }

        private static void CheckPacketFocusIn()
        {
            Console.WriteLine("Check FocusIn");

            var packets = PacketParser.GetPackets(_dttDestinationDir, _tempFolder);
            Assert.IsTrue(packets.Count >= 1);
            //TODO: bad idea to use first file in the folder. all files need to be analyzed
            var pathToDecryptedPacket = PacketParser.DecryptPacket(_dttDestinationDir, packets[0].Name, _tempFolder, _pathToDecriptFile);
            Program.CheckFocusInBehaviourAndTime(pathToDecryptedPacket, _tempFolder);
        }

        private static void CheckPacketHotKey()
        {
            Console.WriteLine("Check HotKey");

            var packets = PacketParser.GetPackets(_dttDestinationDir, _tempFolder);
            //TODO: bad idea to use first file in the folder. all files need to be analyzed
            var pathToDecryptedPacket = PacketParser.DecryptPacket(_dttDestinationDir, packets[0].Name, _tempFolder, _pathToDecriptFile);
            Program.CheckHotKeyBehaviourAndTime(pathToDecryptedPacket, _tempFolder);
        }

        private static void CheckPacketIdleTime()
        {
            Console.WriteLine("Check packet IdleTime");

            var packets = PacketParser.GetPackets(_dttDestinationDir, _tempFolder);
            //TODO: bad idea to use first file in the folder. all files need to be analyzed
            var pathToDecryptedPacket = PacketParser.DecryptPacket(_dttDestinationDir, packets[0].Name, _tempFolder, _pathToDecriptFile);
            Program.CheckIdleTimeAndTime(pathToDecryptedPacket, _tempFolder);
        }

        private static void CheckPacketEnkataEvent1()
        {
            Console.WriteLine("Check Packet EnkataEvent1");

            var packets = PacketParser.GetPackets(_dttDestinationDir, _tempFolder);
            //TODO: bad idea to use first file in the folder. all files need to be analyzed
            var pathToDecryptedPacket = PacketParser.DecryptPacket(_dttDestinationDir, packets[0].Name, _tempFolder, _pathToDecriptFile);
            Program.CheckPacketEnkataEvent1(pathToDecryptedPacket, _tempFolder, "Automation1.enkataEvent1");
        }

        private void CheckOpenStartStop()
        {
            Console.WriteLine("Check OpenStartStop");

            var packets = PacketParser.GetPackets(_dttDestinationDir, _tempFolder);
            //TODO: bad idea to use first file in the folder. all files need to be analyzed
            var pathToDecryptedPacket = PacketParser.DecryptPacket(_dttDestinationDir, packets[0].Name, _tempFolder, _pathToDecriptFile);
            Program.CheckOpenSpanStopStart(pathToDecryptedPacket, _tempFolder);

        }
    }

}
