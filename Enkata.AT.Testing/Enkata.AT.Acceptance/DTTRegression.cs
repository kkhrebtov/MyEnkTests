using System;
using System.IO;
using System.Security.AccessControl;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;
using Enkata.ActivityTracker.Core;


namespace Enkata.ActivityTracker.Acceptance
{
    [TestFixture]
    public class DttRegression
    {
        private const int Delay = 5000;
        private static string _pathToLog = DttRegression.PathToLog;
        private static string _pathToPacket = DttRegression.PathToPacket;
        private static string _pathToPacketInLog = DttRegression.PathToPacketInLog;
        private const string NamePacket = "duplicatepacket.packet";
        private const string NameVideo = "duplicatevideo.recording";
        private const string NamePacketNewExtension = "newExtensionPacket.png";
        private const string NameVideoNewExtension = "newExtensionRecording.mp4";
        private const int ServiceDelay = 5000;
        private static string ScreenRecording = "OFF";
        private static string _filenameRecording;
        private const int DelayAfterInstall = 60000;
        private const string NameInstaller32 = "ActivityTracker.msi";
        private const string NameInstaller64 = "ActivityTracker64.msi";

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
        public void DttCorruptedSystemSettings()
        {
            Console.WriteLine("Install AT");
            BehaviorsRegression.CleanMachine();
            AT.Install();

            Console.WriteLine("Stop DTT and remove systemsettings");
            Service.Stop(NunitSettings.ServiceDttName);

            SystemSettings.StoreSystemSettings();
            SystemSettings.DeleteSystemSettings();

            Service.Start(NunitSettings.ServiceDttName);
            Thread.Sleep(Delay);
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Check log DTT");
            var contentOfFolder = AT.GetContentOfFolder(_pathToLog, "*.log");
            var countError = 0;
            var contentLog = "";
            foreach (var fileInfo in contentOfFolder)
            {
                if (fileInfo.Name.Contains("Data Transfer Tool"))
                {
                    countError = CountWordInFile(Path.Combine(_pathToLog, fileInfo.Name), "ERROR");
                    contentLog = File.ReadAllText(Path.Combine(_pathToLog, fileInfo.Name));
                }

            }
            Assert.IsTrue(countError == 1);
            Assert.IsTrue(contentLog.Contains("Configuration file is absent or access is denied. Considering session as not intended for AT."));

            Console.WriteLine("Restore systemsettings");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttDuplicatedFilesToTransfer()
        {
            Console.WriteLine("Install AT and generate packets.");
            BehaviorsRegression.CleanMachine();
            SetPacketDurationAT(5);
            AT.Install();
            GeneratePacket();

            Console.WriteLine("Create two duplicate files and put them to target and source.");
            var filenamePacket = GetNameOfPacket(PathToPacketsDTT, "*.packet");

            if (ScreenRecording == "ON")
            {
                //TODO: no more recordings. Test has to be redesigned
                _filenameRecording =
                    GetNameOfPacket(PathToRecordingDTT,
                                    "*.recording");
                File.Copy(PathToVideoAT, Path.Combine(Path.Combine(PathToRecordingDTT,
                                 _filenameRecording),
                    Path.Combine(PathToVideoAT, _filenameRecording)));
            }
            File.Copy(Path.Combine(PathToPacketsDTT, filenamePacket), Path.Combine(PathToPacketsAT, filenamePacket));
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Check dtt log file.");
            var countError = CountWordInFile(Path.Combine(_pathToLog, GetNameOfLogFile()), "ERROR");
            var contentLog = File.ReadAllText(Path.Combine(_pathToLog, GetNameOfLogFile()));
            var file = "Can't move file " + _pathToPacketInLog + "/packets/" + filenamePacket + " Error: The file exists.";


            Assert.IsTrue(contentLog.ToLower().Contains(file.ToLower()));
            if (ScreenRecording == "ON")
            {
                Assert.IsTrue(countError >= 2, "Error:" + countError);
                Assert.IsTrue(contentLog.Contains("Can't move file " + _pathToPacketInLog + "/video/" + _filenameRecording + " Error: The file exists."));
            }
            else
            {
                Assert.IsTrue(countError >= 1, "Error:" + countError);
            }

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttFilesWithVariousExtensionsToTransfer()
        {
            Console.WriteLine("Change SystemSetting.xml and Install AT.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.setTransferItemParameter("packets", "source", @"C:\Application Data\Enkata\Activity Tracker\packets\*.png");
            }

            Console.WriteLine("Install AT.");
            AT.Install();
            CreateUserDirectory();

            Console.WriteLine("Create file with mp4 and png extension.");
            File.WriteAllText(Path.Combine(PathToPacketsAT, NamePacketNewExtension), "save");
            File.WriteAllText(Path.Combine(DttRegression.PathToVideoAT, NameVideoNewExtension), "save");
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Check that transfered files.");
            Assert.IsTrue(File.Exists(Path.Combine(DttRegression.PathToPacketsDTT,
                                     NamePacketNewExtension)), "Couldn't find transfer file:" + NamePacketNewExtension);
            if (ScreenRecording == "ON")
                Assert.IsTrue(File.Exists(Path.Combine(DttRegression.PathToRecordingDTT,
                                         NameVideoNewExtension)), "Couldn't find transfer file:" + NameVideoNewExtension);

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttNegativePollPeriod()
        {
            Console.WriteLine("Install AT.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.dttPollPeriod = -1;
            }
            CreateUserDirectory();
            CreateSamplePackets();
            AT.Install();
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Check Dtt log.");
            Thread.Sleep(ServiceDelay);
            var countError = CountWordInFile(Path.Combine(_pathToLog, GetNameOfLogFile()), "ERROR");
            var contentLog = File.ReadAllText(Path.Combine(_pathToLog, GetNameOfLogFile()));
            Assert.IsTrue(countError == 0, "Error:" + countError);
            //TODO: replace to Assert.IsTrue when bug DEV-6471//
            Assert.IsFalse(contentLog.Contains("Can't move file "));
            Assert.IsFalse(File.Exists(Path.Combine(DttRegression.PathToPacketsDTT,
                                    NamePacket)), "Couldn't find transfered file:" + NamePacket);
            if (ScreenRecording == "ON")
                Assert.IsFalse(File.Exists(Path.Combine(DttRegression.PathToRecordingDTT,
                                         NameVideo)), "Couldn't find transfered file:" + NameVideo);

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttNoReadAccessToSourceDir()
        {
            Console.WriteLine("Change permision of folder.");
            BehaviorsRegression.CleanMachine();
            CreateUserDirectory();
            CreateSamplePackets();
            SetAccessToObject(AccessControlType.Deny, FileSystemRights.Read, Path.Combine(PathToPacketsAT, NamePacket));
            SetAccessToObject(AccessControlType.Deny, FileSystemRights.Read, Path.Combine(DttRegression.PathToVideoAT, NameVideo));

            Console.WriteLine("Install AT");
            AT.Install();
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Check dtt log");
            var countError = CountWordInFile(Path.Combine(_pathToLog, GetNameOfLogFile()), "ERROR");
            var contentLog = File.ReadAllText(Path.Combine(_pathToLog, GetNameOfLogFile()));
            Assert.IsTrue(countError >= 2, "Error:" + countError);
            var file = "Can't move file " + _pathToPacketInLog + "/packets/" + NamePacket + " Error: Access is denied.";
            Assert.IsTrue(contentLog.ToLower().Contains(file.ToLower()));
            if (ScreenRecording == "ON")
                Assert.IsTrue(contentLog.Contains("Can't move file " + _pathToPacketInLog + "/video/" + NameVideo + " Error: Access is denied."));

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttNoWriteAccessToDestinationDir()
        {
            BehaviorsRegression.CleanMachine();
            Console.WriteLine("Creat Folders and files.");
            CreateUserDirectory();
            CreateSamplePackets();
            Directory.CreateDirectory(DttRegression.PathToPacketsDTT);
            Directory.CreateDirectory(DttRegression.PathToRecordingDTT);

            Console.WriteLine("Change permision of folder and Install AT");
            SetAccessToObject(AccessControlType.Deny, FileSystemRights.Write, DttRegression.PathToPacketsDTT);
            SetAccessToObject(AccessControlType.Deny, FileSystemRights.Write, DttRegression.PathToRecordingDTT);

            AT.Install();
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Check dtt log");
            var countError = CountWordInFile(Path.Combine(_pathToLog, GetNameOfLogFile()), "ERROR");
            var contentLog = File.ReadAllText(Path.Combine(_pathToLog, GetNameOfLogFile()));
            Assert.IsTrue(countError >= 2, "Error:" + countError);
            var file = "Can't move file " + _pathToPacketInLog + "/packets/" + NamePacket + " Error: Access is denied.";
            Assert.IsTrue(contentLog.ToLower().Contains(file.ToLower()));
            if (ScreenRecording == "ON")
                Assert.IsTrue(contentLog.Contains("Can't move file " + _pathToPacketInLog + "/video/" + NameVideo + " Error: Access is denied."));

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttNoWriteAccessToSourceDir()
        {
            BehaviorsRegression.CleanMachine();
            Console.WriteLine("Change permision of folder and Install AT");
            CreateUserDirectory();
            CreateSamplePackets();
            FileSystemRights rights = FileSystemRights.Delete | FileSystemRights.Write |
                FileSystemRights.WriteExtendedAttributes | FileSystemRights.WriteData | FileSystemRights.CreateFiles | FileSystemRights.Modify |
                FileSystemRights.WriteAttributes;
            SetAccessToObject(AccessControlType.Deny, rights, PathToPacketsAT);
            SetAccessToObject(AccessControlType.Deny, rights, DttRegression.PathToVideoAT);
            AT.Install();
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Check dtt log");
            var countError = CountWordInFile(Path.Combine(_pathToLog, GetNameOfLogFile()), "ERROR");
            var contentLog = File.ReadAllText(Path.Combine(_pathToLog, GetNameOfLogFile()));
            //TODO new bug
            Assert.IsTrue(countError == 0);
            Assert.IsTrue(!contentLog.Contains("Can't move file"));
            Assert.IsTrue(File.Exists(Path.Combine(DttRegression.PathToPacketsDTT,
                                    NamePacket)), "Couldn't find transfered file:" + NamePacket);
            if (ScreenRecording == "ON")
                Assert.IsTrue(File.Exists(Path.Combine(DttRegression.PathToRecordingDTT,
                                         NameVideo)), "Couldn't find transfered file:" + NameVideo);

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttPollPeriodZero()
        {
            BehaviorsRegression.CleanMachine();
            Console.WriteLine("Set poll period = 0 and Install AT");
            CreateUserDirectory();
            CreateSamplePackets();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.dttPollPeriod = 0;
            }

            Console.WriteLine("Install AT");
            AT.Install();

            Console.WriteLine("Check Dtt is stopped.");
            Assert.IsFalse(Directory.Exists(NunitSettings.DttPath));
            //TODO new BUG
            //Qusetion
            //Assert.IsFalse(Service.Exists(NunitSettings.NameServiceDtt()));

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttUnreachableDestinationDir()
        {
            Console.WriteLine("Set to SystemSettings.xml not existing disk for Dtt dest.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.setTransferItemParameter("packets", "dest", @"L:\DataOutput\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.CREATION.DATE%.%FILE.CREATION.TIME%.%FILE%");
                systemSettings.setTransferItemParameter("OS log information", "dest", @"L:\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.MODIFICATION.DATE%.%FILE.MODIFICATION.TIME%.os_runtime.log");
                systemSettings.setTransferItemParameter("log information", "dest", @"L:\DataOutput\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.MODIFICATION.DATE%.%FILE.MODIFICATION.TIME%.%FILE%");
            }
            CreateUserDirectory();
            CreateSamplePackets();

            Console.WriteLine("Install AT");
            AT.Install();

            Console.WriteLine("Check dtt logs");
            Service.Stop(NunitSettings.ServiceDttName);
            var countError = CountWordInFile(Path.Combine(_pathToLog, GetNameOfLogFile()), "ERROR");
            var contentLog = File.ReadAllText(Path.Combine(_pathToLog, GetNameOfLogFile()));
            Assert.IsTrue(countError >= 2);
            var error = "Can't move file " + _pathToPacketInLog + "/packets/" + NamePacket +
                        " Error: boost::filesystem::create_directory: The system cannot find the path specified";
            Assert.IsTrue(contentLog.ToLower().Contains(error.ToLower()));
            if (ScreenRecording == "ON")
                Assert.IsTrue(contentLog.Contains("Can't move file " + _pathToPacketInLog + "/video/" + NameVideo + " Error: boost::filesystem::create_directory: The system cannot find the path specified"));

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttUnreachableSourceDir()
        {
            Console.WriteLine("Set to SystemSettings.xml not existing disk for Dtt source.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.setTransferItemParameter("packets", "dest", @"L:\DataOutput\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.CREATION.DATE%.%FILE.CREATION.TIME%.%FILE%");
                systemSettings.setTransferItemParameter("OS log information", "dest", @"L:\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.MODIFICATION.DATE%.%FILE.MODIFICATION.TIME%.os_runtime.log");
                systemSettings.setTransferItemParameter("log information", "dest", @"L:\DataOutput\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.MODIFICATION.DATE%.%FILE.MODIFICATION.TIME%.%FILE%");


                systemSettings.setTransferItemParameter("packets", "source", @"L:\Application Data\Enkata\Activity Tracker\packets\*.packet");
                systemSettings.setTransferItemParameter("OS log information", "source", @"L:\%USERPROFILE%\Application Data\OpenSpan Studio for VS 2008\*.txt");
                systemSettings.setTransferItemParameter("log information", "source", @"L:\Application Data\Enkata\Activity Tracker\log\*.log");
            }
            CreateUserDirectory();
            CreateSamplePackets();

            Console.WriteLine("Install AT");
            AT.Install();

            Console.WriteLine("Check dtt logs");
            Service.Stop(NunitSettings.ServiceDttName);
            var countError = CountWordInFile(Path.Combine(_pathToLog, GetNameOfLogFile()), "ERROR");
            Assert.IsTrue(countError == 0);
            Assert.IsFalse(Directory.Exists(NunitSettings.DttPath));

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttUseOfDttVariables()
        {
            Console.WriteLine("Create DTT environment variable.");
            BehaviorsRegression.CleanMachine();
            Environment.SetEnvironmentVariable("DTT", NunitSettings.InstallFileLocation, EnvironmentVariableTarget.Machine);

            Console.WriteLine("Set new Environment variable to dtt in 'dest'");
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.setTransferItemParameter("packets", "dest", @"%DTT%\DataOutput\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.CREATION.DATE%.%FILE.CREATION.TIME%.%FILE%");
                systemSettings.setTransferItemParameter("OS log information", "dest", @"%DTT%\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.MODIFICATION.DATE%.%FILE.MODIFICATION.TIME%.os_runtime.log");
                systemSettings.setTransferItemParameter("log information", "dest", @"%DTT%\DataOutput\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.MODIFICATION.DATE%.%FILE.MODIFICATION.TIME%.%FILE%");
            }

            Console.WriteLine("Install AT");
            AT.Install();

            Console.WriteLine("Create packets and video");
            CreateUserDirectory();
            CreateSamplePackets();
            Service.Stop(NunitSettings.ServiceDttName);

            Console.WriteLine("Check files have been transferred correctly.");
            Assert.IsTrue(File.Exists(Path.Combine(Path.Combine(Path.Combine(NunitSettings.InstallFileLocation + "\\DATAOUTPUT", AT.GetDate()), "PACKETS"),
                                    NamePacket)), "Couldn't find transfered file:" + NamePacket);
            if (ScreenRecording == "ON")
                Assert.IsTrue(File.Exists(Path.Combine(Path.Combine(Path.Combine(NunitSettings.InstallFileLocation + "\\DATAOUTPUT", AT.GetDate()), "RECORDINGS"),
                                         NameVideo)), "Couldn't find transfered file:" + NameVideo);

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void DttTransferringFilesDuringServiceStop()
        {
            Console.WriteLine("Set poll period 200 for dtt.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.dttPollPeriod = 200;
            }

            Console.WriteLine("Install AT.");
            AT.Install();
            CreateUserDirectory();
            CreateSamplePackets();

            Console.WriteLine("Check packets not transferred.");
            Assert.IsTrue(File.Exists(Path.Combine(PathToPacketsAT, NamePacket)));
            Assert.IsTrue(File.Exists(Path.Combine(DttRegression.PathToVideoAT, NameVideo)));

            Console.WriteLine("Check packets is transferred after stop Dtt.");
            Service.Stop(NunitSettings.ServiceDttName);
            Assert.IsFalse(File.Exists(Path.Combine(PathToPacketsAT, NamePacket)));
            if (ScreenRecording == "ON")
                Assert.IsFalse(File.Exists(Path.Combine(DttRegression.PathToVideoAT, NameVideo)));
            Assert.IsTrue(File.Exists(Path.Combine(DttRegression.PathToPacketsDTT,
                                   NamePacket)), "Couldn't find transfered file:" + NamePacket);
            if (ScreenRecording == "ON")
                Assert.IsTrue(File.Exists(Path.Combine(DttRegression.PathToRecordingDTT,
                                         NameVideo)), "Couldn't find transfered file:" + NameVideo);

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void DttLockedFilesToTransfer()
        {
            Console.WriteLine("Create packets.");
            BehaviorsRegression.CleanMachine();
            CreateUserDirectory();
            CreateSamplePackets();

            Console.WriteLine("Open files for edit.");
            var file1 = File.Open(Path.Combine(PathToPacketsAT, NamePacket), FileMode.Open);
            var file2 = File.Open(Path.Combine(DttRegression.PathToVideoAT, NameVideo), FileMode.Open);

            Console.WriteLine("Install AT.");
            AT.Install();
            Service.Stop(NunitSettings.ServiceDttName);
            Assert.IsFalse(File.Exists(Path.Combine(DttRegression.PathToPacketsDTT,
                                  NamePacket)), "Couldn't find transfered file:" + NamePacket);
            if (ScreenRecording == "ON")
                Assert.IsFalse(File.Exists(Path.Combine(DttRegression.PathToRecordingDTT,
                                         NameVideo)), "Couldn't find transfered file:" + NameVideo);
            Service.Start(NunitSettings.ServiceDttName);

            Console.WriteLine("Close files.");
            file1.Close();
            file2.Close();
            Thread.Sleep(Delay);
            Service.Stop(NunitSettings.ServiceDttName);
            Thread.Sleep(Delay);

            Console.WriteLine("Check packets is transferred after stop Dtt.");
            Assert.IsTrue(File.Exists(Path.Combine(DttRegression.PathToPacketsDTT,
                                  NamePacket)), "Couldn't find transfered file:" + NamePacket);
            if (ScreenRecording == "ON")
                Assert.IsTrue(File.Exists(Path.Combine(DttRegression.PathToRecordingDTT,
                                         NameVideo)), "Couldn't find transfered file:" + NameVideo);

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();

        }


        [Test]
        public void DttLoadingSystemSettings()
        {
            Console.WriteLine("Install AT");
            BehaviorsRegression.CleanMachine();
            AT.Install();
            Thread.Sleep(DelayAfterInstall);

            Console.WriteLine("Stop services");
            Service.Stop(NunitSettings.ServiceDttName);
            var dttLog = GetTextDTTLog().Replace(" ", "").Replace("/n", "");
            var systemSettings =
                File.ReadAllText(SystemSettings.FilePath).Replace(" ", "").Replace("/n", "");

            Console.WriteLine("Check that systemsettings.xml present in dtt log.");
            Assert.IsTrue(dttLog.Contains(systemSettings));

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void DttRestartInCaseOfFailure()
        {
            Console.WriteLine("Install AT");
            BehaviorsRegression.CleanMachine();
            AT.Install();
            Thread.Sleep(DelayAfterInstall);

            Console.WriteLine("Kill process");
            //TODO: Kill em all!
            Process.GetProcess("Data Transfer Tool")[0].Kill();
            //TODO: the interval has to be choosen depending on DTT services restart timeout
            Thread.Sleep(DelayAfterInstall);

            Console.WriteLine("Check that dtt process is restored.");
            Assert.IsTrue(Process.GetProcess("Data Transfer Tool").Length != 0);

            Console.WriteLine("Clean machine");
            BehaviorsRegression.CleanMachine();
        }




        /// <summary>
        /// Private methods
        /// </summary>
        private void GeneratePacket()
        {
            var cal = new Calculator(NunitSettings.CalcLocation);
            Thread.Sleep(5000);
            cal.ClickOnButton("2");
            cal.ClickOnButton("3");
            cal.ClickOnButton("1");
            cal.ClickOnButton("1");
            cal.ClickOnButton("3");
            cal.ClickOnButton("1");
            cal.ClickOnButton("1");
            cal.ClickOnButton("+");
            Thread.Sleep(10000);
            cal.ClickOnButton("-");
            Thread.Sleep(5000);
            cal.Close();
        }

        private static int CountWordInFile(string pathToFile, string nameWord)
        {
            var text = File.ReadAllText(pathToFile);
            var listWords = text.Split(new[] { ' ' });
            var count = 0;
            foreach (var word in listWords)
            {
                if (word.Contains(nameWord))
                    count = 1 + count;
            }
            return count;
        }



        private static string GetTextDTTLog()
        {
            var content = AT.GetContentOfFolder(PathToLog, "*.log");

            foreach (var fileInfo in content)
            {
                if (fileInfo.Name.Contains("Data Transfer"))
                {
                    return File.ReadAllText(Path.Combine(PathToLog, fileInfo.Name));
                }
            }
            return null;
        }

        private string GetNameOfLogFile()
        {
            var contentOfFolder = AT.GetContentOfFolder(_pathToLog, "*.log");
            foreach (var fileInfo in contentOfFolder)
            {
                if (fileInfo.Name.Contains("Data Transfer Tool"))
                {
                    return fileInfo.Name;
                }
            }
            return null;
        }

        //TODO: must be redesinged. It is bad idea to use only the first file in the folder
        private string GetNameOfPacket(string path, string mask)
        {
            var contentOfFolder = AT.GetContentOfFolder(path, mask);
            return contentOfFolder[0].Name;
        }




        private static void DeleteFolder(string folder)
        {

            try
            {
                Directory.Delete(folder, true);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public static void DeleteFile(string file)
        {
            try
            {
                File.Delete(file);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public static void CleanFilesAndATUninstall()
        {
            AT.UninstallAt(NunitSettings.InstallFileLocation);
            AT.DeleteFolder(Path.Combine(NunitSettings.InstallFileLocation, "DATAOUTPUT"));
            AT.DeleteFolder(NunitSettings.DttPath);
            AT.DeleteFolder(_pathToLog);
            AT.DeleteFolder(NunitSettings.TempFolder);
            AT.DeleteFolder(DttRegression.PathToRuntime);
        }

        public static void SetAccessToObject(AccessControlType access, FileSystemRights rights, string pathToFolder)
        {
            SafeCall(delegate
            {
                var dSecurity = Directory.GetAccessControl(pathToFolder);
                dSecurity.AddAccessRule(new FileSystemAccessRule(Environment.UserName, rights, access));
                Directory.SetAccessControl(pathToFolder, dSecurity);
            });
        }



        delegate void Action();

        private static void SafeCall(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }
        }


        private void CreateUserDirectory()
        {
            Directory.CreateDirectory(PathToPacketsAT);
            Directory.CreateDirectory(DttRegression.PathToVideoAT);
        }

        private void CreateSamplePackets()
        {
            File.WriteAllText(Path.Combine(PathToPacketsAT, NamePacket), "dtt");
            File.WriteAllText(Path.Combine(DttRegression.PathToVideoAT, NameVideo), "dtt");
        }

        private static void SetPacketDurationAT(int sec)
        {
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.packetDuration = sec;
            }
        }

        /// <summary>
        /// Properties
        /// </summary>
        ///
        public int Major
        {
            get { return Environment.OSVersion.Version.Major; }
        }

        public static string PathToLog
        {
            get
            {
                if (new DttRegression().Major != 5)
                    return @"C:\ProgramData\Enkata\Activity Tracker\log";
                return @"C:\Documents and Settings\All Users\Application Data\Enkata\Activity Tracker\log";
            }
        }

        public static string PathToPacket
        {
            get
            {
                if (new DttRegression().Major != 5)
                    return @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\Enkata\Activity Tracker";
                return @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\Enkata\Activity Tracker";
            }
        }


        public static string PathToRuntime
        {
            get
            {
                if (new DttRegression().Major != 5)
                    return @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\OpenSpan Studio for VS 2008";
                return @"C:\Documents and Settings\" + Environment.UserName + @"\Application Data\OpenSpan Studio for VS 2008";
            }
        }

        public static string PathToPacketInLog
        {
            get
            {
                if (new DttRegression().Major != 5)
                    return "C:/Users/" + Environment.UserName + "/Application Data/Enkata/Activity Tracker";
                return "C:/Documents and Settings/" + Environment.UserName + "/Application Data/Enkata/Activity Tracker";
            }
        }

        public static string PathToRecordingDTT
        {
            get
            {
                return Path.Combine(Path.Combine(NunitSettings.DttPath, AT.GetDate()), "RECORDINGS");
            }
        }


        public static string PathToPacketsDTT
        {
            get
            {
                return Path.Combine(Path.Combine(NunitSettings.DttPath, AT.GetDate()), "PACKETS");
            }
        }

        public static string PathToPacketsAT
        {
            get
            {
                return Path.Combine(_pathToPacket, "PACKETS");
            }
        }

        public static string PathToVideoAT
        {
            get
            {
                return Path.Combine(_pathToPacket, "video");
            }
        }
    }
}
