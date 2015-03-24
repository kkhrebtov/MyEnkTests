using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Enkata.ActivityTracker.Core;

namespace Enkata.ActivityTracker.Acceptance
{
    [TestFixture]
    public class ExtensionRegression
    {
        private const int timeToWait = 60000;
        private string errorMessage = "System.NullReferenceException: Object reference not set to an instance of an object." +
    "at PacketBuilder.PacketBuilder.Write(Byte[] data, Int32 offset, Int32 length)" +
    "at Enkata.Analytics.OSInterop.Components.EnkataConnector.LogEvent(IEventData eventData)" +
    "at Enkata.Analytics.OSInterop.Components.HLEvents.Processor.OnEventOccured(Object sender, EventDataArgs args";
        private string errorInial = "Error initializing Packet Builder:Unknown symmetric encryption algorithm:invalid";
        private string nameOSRunTimeLog = "runtime";
        private string errorMessageEmptyPathToKey = "System.UnauthorizedAccessException: Access to the path 'incorrect path' is denied.";
        private string errorMessageEmptyPathToKeyXP = "System.IO.FileNotFoundException: Could not find file 'incorrect path'.";


        [Test]
        public void ExtensionsIncorrectEncryptionPackets()
        {
            Console.WriteLine("Change systemsettings.xml");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.encryptionAlgorithm = "invalid";
            }

            Console.WriteLine("Install AT");
            InstallATCheckServiceStopServices();

            //DEV-7005 - issue
            Console.WriteLine("Check os runtime log file");
            errorMessage = errorMessage.Replace(" ", "").Replace("\r", "").Replace("\n", "");
            if (GetContentOfLog(nameOSRunTimeLog) == null) Assert.Fail("Coudn't find osruntime log file.");
            Assert.IsTrue(GetContentOfLog(nameOSRunTimeLog).Contains(errorMessage), "Incorrect error in osruntime log file.");
            Assert.IsTrue(GetContentOfLog(nameOSRunTimeLog).Contains(errorInial.Replace(" ", "")), "Error initializing Packet Builder:Unknown symmetric encryption algorithm:invalid");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
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


        [Test]
        public void ExtensionsCorruptedSystemSettings()
        {
            //TODO: to be refactored - file copy has to be in try-finaly clause
            Console.WriteLine("Change systemsettings.xml");
            BehaviorsRegression.CleanMachine();
            var xmlFile = File.ReadAllText(SystemSettings.FilePath);
            File.Copy(SystemSettings.FilePath, Path.Combine(NunitSettings.InstallFileLocation, NunitSettings.SystemSettingsNameFile.Replace(".xml", "1.xml")), true);
            string[] lines = { xmlFile, "<First line", "<Second line", "//>Third line" };
            System.IO.File.WriteAllLines(SystemSettings.FilePath, lines);

            Console.WriteLine("Install AT");
            AT.Install();
            Thread.Sleep(timeToWait);
            Assert.IsFalse(AT.OsrExists(), "OSR process is started without project path.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            File.Copy(Path.Combine(NunitSettings.InstallFileLocation, NunitSettings.SystemSettingsNameFile.Replace(".xml", "1.xml")), SystemSettings.FilePath, true);
        }


        [Test]
        public void ExtensionsIncorrectPathToKey()
        {
            Console.WriteLine("Change systemsettings.xml");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.keyRingPath = "incorrect path";
            }

            Console.WriteLine("Install AT");
            AT.Install();
            Thread.Sleep(timeToWait);
            Assert.IsFalse(AT.OsrExists(), "OSR process is started without project path.");
            StopAllServices();

            Console.WriteLine("Check os runtime log file");
            Assert.IsTrue(GetContentOfLog(nameOSRunTimeLog).Contains(errorMessageEmptyPathToKey.Replace(" ", "")) || GetContentOfLog(nameOSRunTimeLog).Contains(errorMessageEmptyPathToKeyXP.Replace(" ", "")), "Incorrect error in osruntime log file. Path to pubring key.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void ExtensionsInvalidRawDataStorageForPackets()
        {
            Console.WriteLine("Change systemsettings.xml");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.rawDataStoragePackets = "incorrect path";
            }

            Console.WriteLine("Install AT");
            InstallATCheckServiceStopServices();

            Console.WriteLine("Check os runtime log file");
            Assert.IsTrue(GetContentOfLog(nameOSRunTimeLog).Contains(errorMessageEmptyPathToKey.Replace(" ", "")), "DEV-7444");
            Assert.IsTrue(GetContentOfLog(nameOSRunTimeLog).Contains("System.NullReferenceException"), "DEV-7444");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }



        [Test]
        public void ExtensionsKeyringPath()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT");
            InstallATCheckServiceStopServices();

            Console.WriteLine("Decrypt packet using secring.gpg");
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            //TODO: bad idea to use first file in the folder. all files need to be analyzed
            var pathToDecryptedPacket = PacketParser.DecryptPacket(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NunitSettings.InstallFileLocation);

            Console.WriteLine("Check content of packet.");
            var content = File.ReadAllText(pathToDecryptedPacket);
            content = content.Replace(" ", "");
            Assert.IsTrue(content.Contains("OpenSpan:start"), "Couldn't find OpenSpan:start in packet.");
            Assert.IsTrue(content.Contains("OpenSpan:stop"), "Coildn't find OpenSpan:stop in packet.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void ExtensionsLogBrokenRuntimeConfig()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT");
            InstallATCheckServiceStopServices();

            Console.WriteLine("Delete run time config");
            File.Delete(Path.Combine(DttRegression.PathToRuntime, "RuntimeConfig.xml"));
            File.Create(Path.Combine(DttRegression.PathToRuntime, "RuntimeConfig.xml"));
            StartAllServices();

            Console.WriteLine("Check os runtime log file.");
            string logerror;
            if (File.Exists(Path.Combine(DttRegression.PathToRuntime, "OpenSpan.Runtime32.exe.Exception.Log")))
            {
                logerror = File.ReadAllText(Path.Combine(DttRegression.PathToRuntime, "OpenSpan.Runtime32.exe.Exception.Log"));
            }
            else
            {
                logerror = File.ReadAllText(Path.Combine(DttRegression.PathToRuntime, "OpenSpan.Runtime.exe.Exception.Log"));
            }
            Assert.IsTrue(logerror.Contains("OpenSpan.Diagnostics.DiagnosticException"), "Couldn't find OpenSpan.Diagnostics.DiagnosticException in os run time log.");
            Assert.IsTrue(logerror.Contains("System.NullReferenceException"), "Couldn't find OpenSpan.Diagnostics.DiagnosticException in os run time log.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void ExtensionsLogLevels()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT");
            InstallATCheckServiceStopServices();

            Console.WriteLine("Check osruntime config.");
            var osrunTimeConfig = File.ReadAllText(Path.Combine(DttRegression.PathToRuntime, "RuntimeConfig.xml"));
            osrunTimeConfig = osrunTimeConfig.Replace(" ", "");
            Assert.IsTrue(osrunTimeConfig.Contains("<publisher mode=\"on\" trace_mode=\"on\" exception_mode=\"on\" assembly=\"Enkata.Analytics.OSRuntime\" type=\"Enkata.Analytics.OSRuntime.EnkataFilePublisher\" fileName=\"RuntimeLog.txt\" filesToKeep=\"10\" initMode=\"ClearLog\" />".Replace(" ", "")), "Couldn't find OpenSpan.Diagnostics.DiagnosticException in os run time log.");
            Assert.IsTrue(osrunTimeConfig.Contains("<Category name=\"Runtime\" logLevel=\"3\"/>".Replace(" ", "")), "Run time log level is incorrect.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void ExtensionsPacketCounter()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.packetDuration = 10;
            }

            Console.WriteLine("Install AT");
            InstallATCheckServiceStopServices();
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            int[] packetNumber = { 0, 1, 2, 3, 4, 5, 6 };

			// TODO: ACHTUNG !!!
            for (var i = 0; i < packets.Count; i++)
            {
                var pathToDecryptedPacket = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[i].Name, NunitSettings.TempFolder, NunitSettings.InstallFileLocation);
                for (var j = 0; j < packetNumber.Length; j++)
                {
                    var content = File.ReadAllText(pathToDecryptedPacket);
                    if (content.Replace(" ", "").Contains("packet-number=\"" + j + "\""))
                    {
                        packetNumber[j] = 10;
                        break;
                    }
                }
            }

            foreach (var item in packetNumber)
            {
                Assert.IsTrue(item == 10, "Incorrect value for packet number!");
            }

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void ExtensionsPacketsComputerName()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT");
            InstallATCheckServiceStopServices();

            Console.WriteLine("Decrypt packet.");
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            //TODO: bad idea to use first file in the folder. all files need to be analyzed
            var pathToDecryptedPacket = PacketParser.DecryptPacket(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NunitSettings.InstallFileLocation);

            Console.WriteLine("Check decrypted packet. Packet should contain computer name!");
            var content = File.ReadAllText(pathToDecryptedPacket);
            content = content.Replace(" ", "");
            Assert.IsTrue(content.Contains("computer=\"" + Environment.MachineName + "\""), "Couldn't find computer name in packet.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void ExtensionsPacketsDurationInvalid()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.packetDuration = -10;
            }

            Console.WriteLine("Install AT and check services.");
            AT.Install();
            Thread.Sleep(timeToWait);
            Assert.IsFalse(AT.OsrExists(), "OSR process started with incorrect packet duration.");
            StopAllServices();

            Console.WriteLine("Check osrun time log file.");
            var errorNegativeDuration = "System.ArgumentException: Invalid maximum packet duration parameter.";
            var errorPacketBuilder = "Error initializing Packet Builder";
            Assert.IsTrue(GetContentOfLog(nameOSRunTimeLog).Contains(errorNegativeDuration.Replace(" ", "")), "Incorrect error in osruntime log file. Negative value in  packet duration.");
            Assert.IsTrue(GetContentOfLog(nameOSRunTimeLog).Contains(errorPacketBuilder.Replace(" ", "")), "Incorrect error in osruntime log file. Packet builder.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void ExtensionsPacketsDuration()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.packetDuration = 120;
                systemSettings.packetSize = 2000000;
                systemSettings.depotSize = 50;
            }

            Console.WriteLine("Install AT and wait 2.5 min.");
            AT.Install();
            Thread.Sleep(150000);
            Service.Stop(NunitSettings.ServiceWdName);
            Thread.Sleep(timeToWait);
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Check quantity of generated packets.");
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            Assert.IsTrue(packets.Count == 2, "Quantity of packets incorrect:" + packets.Count);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void ExtensionsPacketsSessionId()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT and check services.");
            InstallATCheckServiceStopServices();

            Console.WriteLine("Check session id in decrypted packet.");
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            //TODO: bad idea to use first file in the folder. all files need to be analyzed
            var pathToDecryptedPacket = PacketParser.DecryptPacket(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NunitSettings.InstallFileLocation);
            var session1 = GetSessionId(pathToDecryptedPacket);
            Assert.IsTrue(session1 != "", "Session id1 is absent in decrypted packet.");

            Console.WriteLine("Get another session id.");
            Directory.Delete(NunitSettings.TempFolder, true);
            Directory.Delete(NunitSettings.DttPath, true);
            StartAllServices();
            StopAllServices();
            var packetsSessio2 = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            //TODO: bad idea to use first file in the folder. all files need to be analyzed
            var pathToDecryptedPacketSession2 = PacketParser.DecryptPacket(NunitSettings.DttPath, packetsSessio2[0].Name, NunitSettings.TempFolder, NunitSettings.InstallFileLocation);
            var session2 = GetSessionId(pathToDecryptedPacketSession2);
            Assert.IsTrue(session2 != "", "Session id2 is absent in decrypted packet.");
            Assert.IsTrue(session1 != session2, "Session id has incorrect values.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void ExtensionsPacketsSizeInvalid()
        {
            Console.WriteLine("Clean machine and edit SystemSettings.xml.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.packetSize = -10;
            }

            Console.WriteLine("Install AT and check services.");
            AT.Install();
            Thread.Sleep(timeToWait);
            Assert.IsFalse(AT.OsrExists(), "OSR process started with incorrect packet duration.");
            StopAllServices();

            Console.WriteLine("Check osrun time log file.");
            var errorNegativePacketSize = "System.ArgumentException: Invalid desired packet size parameter.";
            Assert.IsTrue(GetContentOfLog(nameOSRunTimeLog).Contains(errorNegativePacketSize.Replace(" ", "")), "Incorrect error in osruntime log file. Negative value in  packet size.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }



        [Test]
        public void ExtensionsPacketsUserInfo()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.packetSize = 100;
            }

            Console.WriteLine("Install AT and check services.");
            InstallATCheckServiceStopServices();

            Console.WriteLine("Check session id in decrypted packet.");
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            var pathToDecryptedPacket = PacketParser.DecryptPacket(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NunitSettings.InstallFileLocation);
            var userName = GetUserName(pathToDecryptedPacket);
            Assert.IsTrue(userName == Environment.UserName, "Incorrect username in packet:" + userName + ".");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void ExtensionsPacketsStoreNetwork()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.rawDataStoragePackets = NunitSettings.ShareFolder;
                systemSettings.packetDuration = 5;
            }

            Console.WriteLine("Install AT");
            AT.Install();
            Thread.Sleep(30000);

            Console.WriteLine("Check number of packets in custom folder.");
            var packets = AT.GetContentOfFolder(NunitSettings.ShareFolder, "*.packet");
            Assert.IsTrue(packets.Count >= 9, "Incorrect quantity of packets:" + packets.Count);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            foreach (var packet in packets)
            {
                File.Delete(Path.Combine(NunitSettings.ShareFolder, packet.Name));
            }
        }

        [Test]
        public void ExtensionsPacketsStore()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.rawDataStoragePackets = NunitSettings.TempFolder;
                systemSettings.packetDuration = 5;
            }

            Console.WriteLine("Install AT");
            AT.Install();
            Thread.Sleep(30000);

            Console.WriteLine("Check number of packets in custom folder.");
            var packets = AT.GetContentOfFolder(NunitSettings.TempFolder, "*.packet");
            Assert.IsTrue(packets.Count >= 8, "Incorrect quantity of packets:" + packets.Count);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }



        /// <summary>
        /// Private methoda
        /// </summary>
        /// <returns></returns>
        private string GetContentOfLog(string nameLog)
        {
            //change 
            var listFiles = AT.GetContentOfFolder(Path.Combine(Path.Combine(NunitSettings.DttPath, AT.GetDate()), "LOGS"), "*" + nameLog + ".log");
            foreach (var file in listFiles)
            {
                var text = File.ReadAllText(Path.Combine(Path.Combine(Path.Combine(NunitSettings.DttPath, AT.GetDate()), "LOGS"), file.Name));
                return text.Replace(" ", "").Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
            }
            return null;
        }


        private void InstallATCheckServiceStopServices()
        {
            AT.Install();
            InstallerRegression.CheckProcessAndServicesStarted();
            StopAllServices();
        }


        private string GetSessionId(string pathToFile)
        {
            var xmlDoc = SystemSettings.GetContentOfXml(pathToFile);
            var listTags = xmlDoc.GetElementsByTagName("events");
            Assert.IsTrue(listTags.Count >= 1);
            //TODO: Use Xpath instead of indexes
            return listTags[0].Attributes["session-id"].Value;
        }

        private string GetUserName(string pathToFile)
        {
            var xmlDoc = SystemSettings.GetContentOfXml(pathToFile);
            var listTags = xmlDoc.GetElementsByTagName("events");
            Assert.IsTrue(listTags.Count >= 1);
            //TODO: Use Xpath instead of indexes
            return listTags[0].Attributes["user"].Value;
        }


        public static void StopAllServices()
        {
            Thread.Sleep(timeToWait);
            Service.Stop(NunitSettings.ServiceWdName);
            Thread.Sleep(timeToWait);
            Service.Stop(NunitSettings.ServiceDttName);
        }

        private void StartAllServices()
        {
            Thread.Sleep(timeToWait);
            Service.Start(NunitSettings.ServiceWdName);
            Thread.Sleep(timeToWait);
            Service.Start(NunitSettings.ServiceDttName);
        }
    }
}
