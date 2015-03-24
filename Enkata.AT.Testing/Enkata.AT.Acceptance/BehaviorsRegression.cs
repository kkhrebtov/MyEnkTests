using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Enkata.ActivityTracker.Core;

namespace Enkata.ActivityTracker.Acceptance
{
    [TestFixture]
    public class BehaviorsRegression
    {
        private int delay = 5000;
        private string url1 = "http://google.com";
        private string url2 = "http://www.microsoft.com";
        private string ie = "iexplore.exe";
        private string ieXp = "IEXPLORE.EXE";
        private string longUrl = "http://windows.microsoft.com/!@#!@+_9!@$@$%%#&&(*(+)";
        private string urlWithSpecialSymbols = "http://windows.microsoft.com/en-US/messenger/Messenger-on-the-web";

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
        public void FocusInBrowserDesktop()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();
            
            Console.WriteLine("Open calculator");
            OpenAndCloseCalculator();

            Console.WriteLine("Open browser go to any url.");
            OpenAndKillDefaultBrowser();


            Console.WriteLine("Decrypt packets.");
            ExtensionRegression.StopAllServices();
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());

            Console.WriteLine("Check Focus In.");
            Assert.IsTrue(focusInStore.Count >= 4, "Check focus in Behavior in packet." + focusInStore.Count);
            var countAppId = 0;
            foreach (var focusIn in focusInStore)
            {

                if (focusIn.ApplicationId.Contains("calc.exe") || focusIn.ApplicationId.Contains(url1) || focusIn.ApplicationId.Contains(ie) || focusIn.ApplicationId.Contains(url2) || focusIn.ApplicationId.Contains(ieXp))
                    countAppId = countAppId + 1;
            }
            Assert.IsTrue(countAppId >= 4, "Check applicationId in foucIn Behavior." + countAppId);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }



        [Test]
        public void FocusInDesktopApp()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Open calculator and emulate hot key.");
            OpenCalculator();
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.M);
            OpenCalculator();
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.D);
            CloseApplication("calc");
            ExtensionRegression.StopAllServices();

            Console.WriteLine("Decrypt packets and Check Focus In.");
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            Assert.IsTrue(focusInStore.Count >= 4, "Check focus in Behavior in packet.");
            var countAppId = 0;
            foreach (var focusIn in focusInStore)
            {

                if (focusIn.ApplicationId.Contains("calc.exe") || focusIn.ApplicationId.Contains("Explorer.EXE"))
                    countAppId = countAppId + 1;
            }
            Assert.IsTrue(countAppId >= 4, "Check applicationId in foucIn Behavior.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void FocusInDifferentNameOfApp()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();
            OpenCalculator();
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.M);
            OpenCalculator();
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.D);
            OpenAndKillDefaultBrowser();
            CloseApplication("calc");
            CloseApplication("iexplorer");
            ExtensionRegression.StopAllServices();

            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            Assert.IsTrue(focusInStore.Count >= 6, "Check focus in Behavior in packet.:" + focusInStore.Count);
            var countAppId = 0;
            foreach (var focusIn in focusInStore)
            {

                if (focusIn.ApplicationId.Contains("calc.exe") || focusIn.ApplicationId.Contains("Explorer.EXE") || focusIn.ApplicationId.Contains(url1) || focusIn.ApplicationId.Contains(ie) || focusIn.ApplicationId.Contains(url2))
                    countAppId = countAppId + 1;
            }
            Assert.IsTrue(countAppId >= 6, "Check applicationId in foucIn Behavior.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void FocusInInBrowserLongUrl()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Open link with special symbols.");
            Thread.Sleep(delay);
            OpenCalculator();
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.M);
            OpenLink(urlWithSpecialSymbols, longUrl);
            Thread.Sleep(delay * 5);
            StopAllServices();
            CloseApplication("iexplorer");
            CloseApplication("calc");

            Console.WriteLine("Decrypt packet.");
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            Assert.IsTrue(focusInStore.Count >= 4, "Check focus in Behavior in packet.:" + focusInStore.Count);
            var countAppId = 0;
            foreach (var focusIn in focusInStore)
            {

                if (focusIn.ApplicationId.Contains(urlWithSpecialSymbols) || focusIn.ApplicationId.Contains(ie) || focusIn.ApplicationId.Contains("calc") || focusIn.ApplicationId.Contains("Explorer.EXE"))
                    countAppId = countAppId + 1;
            }
            Assert.IsTrue(countAppId >= 4, "Check applicationId in foucIn Behavior.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void FocusInInBrowser()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();
            Thread.Sleep(delay);

            Console.WriteLine("Open several links in browser.");
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.M);
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.D);
            OpenAndKillDefaultBrowser();
            Thread.Sleep(delay * 5);
            StopAllServices();
            CloseApplication("iexplorer");
            CloseApplication("calc");

            Console.WriteLine("Decrypt packet.");
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            Assert.IsTrue(focusInStore.Count >= 3, "Check focus in Behavior in packet.:" + focusInStore.Count);
            var countAppId = 0;
            foreach (var focusIn in focusInStore)
            {

                if (focusIn.ApplicationId.Contains(url1) || focusIn.ApplicationId.Contains(ie) || focusIn.ApplicationId.Contains(url2) || focusIn.ApplicationId.Contains("Explorer.EXE"))
                    countAppId = countAppId + 1;
            }
            Assert.IsTrue(countAppId >= 3, "Check applicationId in foucIn Behavior.");

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void FocusInInWinAppParametersInCmdl()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();
            Thread.Sleep(delay);

            Console.WriteLine("Open Notepad with params.");
            Program.ExecuteCommandCmd("notepad test2.txt");
            Thread.Sleep(delay);
            UserInputs.PressEnter();
            UserInputs.PressEnter();
            RunCalcMsiExec();
            StopAllServices();
            CloseApplication("notepad");


            Console.WriteLine("Decrypt packet.");
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            Assert.IsTrue(focusInStore.Count >= 7, "Check focus in Behavior in packet.:" + focusInStore.Count);
            var countAppId = 0;
            foreach (var focusIn in focusInStore)
            {

                if (focusIn.ApplicationId.Contains("notepad") || focusIn.ApplicationId.Contains("calc") || focusIn.ApplicationId.Contains("msiexec"))
                    countAppId = countAppId + 1;
            }
            Assert.IsTrue(countAppId >= 6, "Check applicationId in foucIn Behavior." + countAppId);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void FocusInOffMode()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.enableAppFocusChangeBehavior = false;
            }

            Console.WriteLine("Install AT.");
            InstallATCheckService();
            Thread.Sleep(delay);

            Console.WriteLine("Open Notepad and calc.");
            RunCalcMsiExec();
            StopAllServices();

            Console.WriteLine("Decrypt packet.");
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            Assert.IsTrue(focusInStore.Count == 0, "Check focus in Behavior in packet.:" + focusInStore.Count);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void FocusInOnMode()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.enableAppFocusChangeBehavior = true;
            }

            Console.WriteLine("Install AT.");
            InstallATCheckService();
            Thread.Sleep(delay);

            Console.WriteLine("Open Notepad and calc.");
            RunCalcMsiExec();
            StopAllServices();

            Console.WriteLine("Decrypt packet.");
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            Assert.IsTrue(focusInStore.Count >= 5, "Check focus in Behavior in packet.:" + focusInStore.Count);
            var countAppId = 0;
            foreach (var focusIn in focusInStore)
            {
                if (focusIn.ApplicationId.Contains("calc") || focusIn.ApplicationId.Contains("msiexec"))
                    countAppId = countAppId + 1;
            }
            Assert.IsTrue(countAppId >= 2, "Check applicationId in foucIn Behavior." + countAppId);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void FocusInQuickSwitching()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Run calc and switch between them.");
            Program.ExecuteCommandCmd("calc.exe");
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            Program.ExecuteCommandCmd("msiexec");
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            StopAllServices();
            CloseApplication("msiexec");
            CloseApplication("calc");

            Console.WriteLine("Decrypt packet.");
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            Assert.IsTrue(focusInStore.Count >= 5, "Check focus in Behavior in packet.:" + focusInStore.Count);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void FocusInXmlStructureInWindowsApp()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Run calc and switch between them.");
            Program.ExecuteCommandCmd("calc.exe");
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            Program.ExecuteCommandCmd("msiexec");
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            StopAllServices();
            CloseApplication("msiexec");
            CloseApplication("calc");

            Console.WriteLine("Decrypt packet.");
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            var countAppId = 0;
            foreach (var focusIn in focusInStore)
            {
                if (focusIn.ApplicationId.Contains("calc.exe") || focusIn.ApplicationId.Contains("msiexec") || focusIn.ApplicationId.Contains(@"C:\Windows\Explorer.EXE"))
                    countAppId = countAppId + 1;
            }
            Assert.IsTrue(countAppId >= 3, "Check applicationId in foucIn Behavior." + countAppId);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void FocusInXmlStructureInBrowser()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();
            Thread.Sleep(delay * 3);

            Console.WriteLine("Open link in browser.");
            OpenAndKillDefaultBrowser();
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            OpenAndKillDefaultBrowser();
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            CloseApplication("iexplore");
            StopAllServices();

            Console.WriteLine("Decrypt packet.");
            var focusInStore = PacketParser.GetAllFocusInBehaviour(DecryptPacket());
            var countAppId = 0;
            foreach (var focusIn in focusInStore)
            {
                if (focusIn.ApplicationId.Contains(url1) || focusIn.ApplicationId.Contains(url2))
                    countAppId = countAppId + 1;
            }
            Assert.IsTrue(countAppId >= 2, "Check applicationId in foucIn Behavior." + countAppId);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void HotkeyOffMode()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.enableHotkeysBehavior = false;
            }

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Run calc and switch between them.");
            Program.ExecuteCommandCmd("calc.exe");
            Thread.Sleep(delay);
            UserInputs.PressF1();
            UserInputs.PressAltF4();
            Thread.Sleep(delay);
            StopAllServices();
            CloseApplication("calc");

            Console.WriteLine("Decrypt packet and check FocusIn.");
            var focusInStore = PacketParser.GetAllHotKey(DecryptPacket());
            Assert.IsTrue(focusInStore.Count == 0);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void HotkeySeveralCombination()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Emulate hot keys");
            EmulateHotKey();
            Thread.Sleep(delay);
            StopAllServices();

            Console.WriteLine("Decrypt packet and check FocusIn.");
            var focusInStore = PacketParser.GetAllHotKey(DecryptPacket());
            Assert.IsTrue(focusInStore.Count >= 27, "Check quantity of hotkeys in packet!");
            CheckAllFocusIN(focusInStore);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void IdleTimeMouseActions()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Emulate idle time.");
            Thread.Sleep(delay * 5);
            UserInputs.MoveMouse(2000, 300);
            Thread.Sleep(delay * 5);
            UserInputs.MoveMouse(2000, 300);
            Thread.Sleep(delay * 5);
            StopAllServices();

            Console.WriteLine("Decrypt packet and check Idle time.");
            var focusInStore = PacketParser.GetAllIdleTime(DecryptPacket());
            Assert.IsTrue(focusInStore.Count == 4);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void IdleTimeTurnOffAction()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.enableIdleTimeBehavior = false;
            }

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Emulate idle time.");
            Thread.Sleep(delay * 5);
            UserInputs.MoveMouse(2000, 300);
            Thread.Sleep(delay * 5);
            UserInputs.MoveMouse(2000, 300);
            Thread.Sleep(delay * 5);
            StopAllServices();

            Console.WriteLine("Decrypt packet and check Idle time.");
            var focusInStore = PacketParser.GetAllIdleTime(DecryptPacket());
            Assert.IsTrue(focusInStore.Count == 0);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void IdleTimeinterruptByHotKey()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Emulate idle time.");
            Program.ExecuteCommandCmd("calc.exe");
            Thread.Sleep(delay * 5);
            UserInputs.PressF1();
            Thread.Sleep(delay * 5);
            UserInputs.PressAltS();
            Thread.Sleep(delay * 5);
            UserInputs.PressAltF4();
            Thread.Sleep(delay * 5);
            UserInputs.PressAltF4();
            StopAllServices();

            Console.WriteLine("Decrypt packet and check Idle time.");
            var focusInStore = PacketParser.GetAllIdleTime(DecryptPacket());
            Assert.IsTrue(focusInStore.Count == 8, "Check idele time in decrypted packet.:" + focusInStore.Count);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }

        [Test]
        public void IdleTimeinterruptByFocusIn()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();

            Console.WriteLine("Emulate idle time.");
            Thread.Sleep(delay * 25);
            Program.ExecuteCommandCmd("calc.exe");
            Thread.Sleep(60000);
            UserInputs.PressF1();
            Thread.Sleep(60000);            
            UserInputs.PressAltF4();
            Thread.Sleep(60000);
            UserInputs.PressAltF4();
            StopAllServices();            

            Console.WriteLine("Decrypt packet and check Idle time.");
            var focusInStore = PacketParser.GetAllIdleTime(DecryptPacket());
            Assert.IsTrue(focusInStore.Count >= 6, "Check idele time in decrypted packet.:" + focusInStore.Count);

            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();
        }


        [Test]
        public void HotKeyNotePadApp() { 
        
        
        
        
        
        }




        [Test]
        public void ReproduceBug()
        {
            Console.WriteLine("Clean machine.");
            BehaviorsRegression.CleanMachine();

            Console.WriteLine("Install AT.");
            InstallATCheckService();
            for (var i = 0; i < 10; i++ )
            {
                System.Diagnostics.ProcessStartInfo startInfo1 = new System.Diagnostics.ProcessStartInfo("IExplore.exe", longUrl);
                var proc1 = System.Diagnostics.Process.Start(startInfo1);
                CloseApplication("iexplore");
                UserInputs.PressHotKeyWithoutDelay((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            }
            for (var i = 0; i < 10; i++)
            {
                System.Diagnostics.ProcessStartInfo startInfo1 = new System.Diagnostics.ProcessStartInfo("firefox.exe", longUrl);
                var proc1 = System.Diagnostics.Process.Start(startInfo1);
                CloseApplication("iexplore");
                UserInputs.PressHotKeyWithoutDelay((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            }
            for (var k = 0; k < 10; k++ )
            {
                UserInputs.PressHotKeyWithoutDelay((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
                Thread.Sleep(200);
                UserInputs.PressHotKeyWithoutDelay((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
                Thread.Sleep(200);
                UserInputs.PressHotKeyWithoutDelay((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
                Thread.Sleep(200);

                UserInputs.PressHotKeyWithoutDelay((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
                Thread.Sleep(100);
                UserInputs.PressHotKeyWithoutDelay((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
                Thread.Sleep(100);
                CloseApplication("iexplore");
                UserInputs.PressHotKeyWithoutDelay((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            }
          
            StopAllServices();
            MessageBox.Show("End tests");
        }

        /// <summary>
        /// Private methods
        /// </summary>
        private void RunCalcMsiExec()
        {
            Program.ExecuteCommandCmd("calc.exe");
            Thread.Sleep(delay);
            UserInputs.PressAltF4();
            Program.ExecuteCommandCmd("msiexec");
            Thread.Sleep(delay);
            UserInputs.PressEnter();
        }

        private void CheckAllFocusIN(List<OpenSpan> listHotKey)
        {
            int countHotKey = 0;
            foreach (var itemHotKey in listHotKey)
            {
                if (itemHotKey.Value1 == "F1" | itemHotKey.Value1 == "F6")
                {
                    Assert.IsTrue(itemHotKey.TimeStamp.Contains(AT.GetDate()));
                    countHotKey = countHotKey + 1;
                }
                if (itemHotKey.Value1 == "Alt")
                {
                    if (itemHotKey.Value2 == "S" | itemHotKey.Value2 == "F4")
                    {
                        Assert.IsTrue(itemHotKey.TimeStamp.Contains(AT.GetDate()));
                        countHotKey = countHotKey + 1;
                    }
                }

                if (itemHotKey.Value1 == "Control")
                {
                    if (itemHotKey.Value2 == "A" | itemHotKey.Value2 == "C" | itemHotKey.Value2 == "F" | itemHotKey.Value2 == "G" | itemHotKey.Value2 == "N" | itemHotKey.Value2 == "O"
                        | itemHotKey.Value2 == "P" | itemHotKey.Value2 == "S" | itemHotKey.Value2 == "V" | itemHotKey.Value2 == "X" | itemHotKey.Value2 == "Z" | itemHotKey.Value2 == "F6"
                        | itemHotKey.Value2 == "Tab" | itemHotKey.Value2 == "H")
                    {
                        Assert.IsTrue(itemHotKey.TimeStamp.Contains(AT.GetDate()));
                        countHotKey = countHotKey + 1;
                    }
                }

                if (itemHotKey.Value1 == "Win")
                {
                    if (itemHotKey.Value2 == "F2")
                    {
                        Assert.IsTrue(itemHotKey.TimeStamp.Contains(AT.GetDate()));
                        countHotKey = countHotKey + 1;
                    }
                }

                if (itemHotKey.Value1 == "Shift")
                {
                    if (itemHotKey.Value2 == "Tab")
                    {
                        Assert.IsTrue(itemHotKey.TimeStamp.Contains(AT.GetDate()));
                        countHotKey = countHotKey + 1;
                    }
                }
            }
            Assert.IsTrue(countHotKey >= 27, "Check hotkeys in packet! :" + countHotKey);
        }



        private void StopAllServices()
        {
            Service.Stop(NunitSettings.ServiceWdName);
            Thread.Sleep(delay);
            Service.Stop(NunitSettings.ServiceDttName);
        }



        private void InstallATCheckService()
        {
            AT.Install();
            InstallerRegression.CheckProcessAndServicesStarted();
        }


        //TODO: This DecryptPacket must be redesigned. Only one packet (not necessary first) is used 
        private string DecryptPacket()
        {
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            return PacketParser.DecryptPacket(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NunitSettings.InstallFileLocation);
        }

        private void OpenAndKillDefaultBrowser()
        {

            OpenLink(url2, url1);
            var processList = Process.GetAllProcess();
            foreach (var proces in processList)
            {
                if (proces.ProcessName.Contains("firefox"))
                {
                    proces.Kill();
                }
                if (proces.ProcessName.Contains("iexplore"))
                {
                    proces.Kill();
                }
            }

        }

        private void CloseApplication(string nameAppInProcessList)
        {

            var processList = Process.GetAllProcess();
            foreach (var proces in processList)
            {
                if (proces.ProcessName.Contains(nameAppInProcessList))
                {
                    proces.Kill();
                }
            }

        }

        private void OpenAndCloseCalculator()
        {
            Program.ExecuteCommandCmd("calc");
            ////Call help window
            UserInputs.PressF1();
            Thread.Sleep(delay);
            ////Call search in help window
            UserInputs.PressAltS();
            Thread.Sleep(delay);
            ////Close Help window
            UserInputs.PressAltF4();
            Thread.Sleep(delay);
            UserInputs.PressAltF4();
        }


        private void OpenCalculator()
        {
            var calc = new Calculator(NunitSettings.CalcLocation);
            Thread.Sleep(delay);
        }


        private void OpenLink(string sUrl1, string sUrl2)
        {
            System.Diagnostics.ProcessStartInfo startInfo1 = new System.Diagnostics.ProcessStartInfo("IExplore.exe", sUrl1);
            var proc1 = System.Diagnostics.Process.Start(startInfo1);
            Thread.Sleep(delay * 12);
            System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo("IExplore.exe", sUrl2);
            var proc2 = System.Diagnostics.Process.Start(startInfo2);
            Thread.Sleep(delay * 12);
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);
            UserInputs.PressHotKey((int)VirtualKeys.Menu, (int)VirtualKeys.Tab);   
            startInfo1 = null;
            startInfo2 = null;
        }

        private void EmulateHotKey()
        {
            Program.ExecuteCommandCmd("calc");
            Thread.Sleep(delay);
            UserInputs.PressF1();
            Thread.Sleep(delay);
            ////Call search in help window
            UserInputs.PressAltS();
            Thread.Sleep(delay);
            UserInputs.PressAltF4();
            UserInputs.PressAltF4();
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.A);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.C);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.F);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.G);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.N);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.O);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.P);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.S);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.V);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.X);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.Z);
            UserInputs.PressAltF4();
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.Shift);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.Escape);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.F6);
            UserInputs.PressAltF4();
            UserInputs.PressHotKey((int)VirtualKeys.LaunchApplication1, (int)VirtualKeys.F6);
            UserInputs.PressAltF4();
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.F2);
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.F1);
            UserInputs.PressAltF4();
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.D);
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.Space);
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.R);
            UserInputs.PressAltF4();
            UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.U);
            UserInputs.PressAltF4();
            UserInputs.PressHotKey((int)VirtualKeys.Shift, (int)VirtualKeys.Tab);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.Tab);
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.H);
            UserInputs.PressHotKey((int)VirtualKeys.Shift, (int)VirtualKeys.I);
        }
        
        public static void CleanMachine()
        {
            AT.CleanMachine();
            AT.UpdateSystemSettings();
        }
    }
}
