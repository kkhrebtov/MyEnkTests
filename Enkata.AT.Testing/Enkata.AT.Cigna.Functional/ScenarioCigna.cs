using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using System.IO;
using White.Core.WindowsAPI;
using White.Core.InputDevices;
using White.Core;
using White.Core.UIItems.Finders;
using White.Core.UIItems.TabItems;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems.WindowStripControls;
using System.Windows;
using Enkata.ActivityTracker.Core;
using System.Reflection;
using System.Diagnostics;


namespace ScriptForCignaAutomation
{

    [SetUpFixture]
    public class PrepareTest {
        
        [SetUp]
        public void CleanBackupFolderBeforeRunningCases()
        {
            Console.WriteLine("Run SetUpFixture");
            Directory.Delete(NunitSettings.BackupFolder, true);
            Directory.CreateDirectory(NunitSettings.BackupFolder);
        }
    }

    [TestFixture]
    public class SmokeScenarios
    {
        //int count = 0;

        [SetUp]
        public void Init()
        {
            OpenSpanEvents.SetAllHotkeys();
            OpenSpanEvents.SetAllMacros();
            
            //AT.CleanMachine();

            //Directory.CreateDirectory(NunitSettings.DttPath);
            Directory.CreateDirectory(NunitSettings.TempFolder);

            //Stop services
            Service.Start(NunitSettings.ServiceWdName);
            Service.Start(NunitSettings.ServiceDttName);

            OpenSpanWindows.WaitOpenSpan(60000);

            //OpenSpanEvents.SetAllHotkeys();
            ViPrClass.RunVipr();

            ViPrClass.LogOnToSystem(NunitSettings.OperatorId, NunitSettings.PasswordId, NunitSettings.PaymentId, NunitSettings.PasswordPay, NunitSettings.ResearchId, NunitSettings.PasswordRes);


        }

        [TearDown]
        public void Cleanup()
        {
            //ViPrClass.ExitVipr();
            //Stop services
            Console.WriteLine("Stopping AT Services");
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);
            //if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), TestHelper.getTestOutpotFolder() + ".xml", true);
            //if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), "C:\\Backup\\Smoke_DecryptedPacket_" + count.ToString() + ".xml", true);
        }


        [Test]
        public void Scenario1()
        {
            
            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(10000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            //Set PendReason to Maternity      
            UserInputs.PressKey((int)VirtualKeys.M);
            Thread.Sleep(2000);
            
            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);
                        
            Console.WriteLine("Before Exit ViPr");
            ViPrClass.ExitVipr();
                        
            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);
                                  
            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            //ExpApplications.Add("Inquiry:Inquiry - Session 3");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Create Expected data for Close event.
            List<OpenSpanClose> ExpectedCloseEventList = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            ExpectedCloseEvent.trigger               = "z030_Proclaim_WU_Close.WorkUnitClose";
            ExpectedCloseEvent.application_id        = "Proclaim";
            ExpectedCloseEvent.document_id           = "0431202304001";
            ExpectedCloseEvent.work_unit_status_code = "O";
            
            ExpectedCloseEventList.Add(ExpectedCloseEvent);

            // Check if expected CloseEvent correspond to Actual.
            CheckResults.CheckWorkUnitClose(ExpectedCloseEventList);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);
           
        }

        [Test]
        public void Scenario3()
        {
                     
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);

            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            Console.WriteLine("SwitchToSession3");
            ViPrClass.SwitchToSession3();
            

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
           

            Console.WriteLine("SetDocNumberAndSN");
            UserInputs.PressKey(6);
            Thread.Sleep(100);
            ViPrClass.EnterNumberInSession(NunitSettings.DocumentNumber);

            //ViPrClass.SetDocNumberAndSN("0431202304001", "9");
            
            UserInputs.PressEnter();
            Thread.Sleep(5000);
            UserInputs.PressEnter();
            Thread.Sleep(5000);
            UserInputs.PressKey((int)VirtualKeys.M);
            Thread.Sleep(100);
            UserInputs.PressKey((int)VirtualKeys.H);
            Thread.Sleep(100);
            UserInputs.PressKey((int)VirtualKeys.I);
            Thread.Sleep(100);
            
            UserInputs.PressEnter();
            Thread.Sleep(5000);
            
            Console.WriteLine("Return from SESSION3 to ViPr");
            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(5000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);
            //SetPendReason "ALLI-ALLIANCE"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);
            Thread.Sleep(20000);

            ViPrClass.ExitVipr();
            
            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\E!PC\\EXTRA.exe\" -Embedding");
            ExpApplications.Add("Inquiry:Screen 3 - Medical History");
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);
           

        }
    }
        
    [TestFixture]
    public class MainframeApplicationUsage
    {

        [SetUp]
        public void Init()
        {
            Console.WriteLine("Set hotkeys");
            OpenSpanEvents.SetAllHotkeys();
            OpenSpanEvents.SetAllMacros();

            Console.WriteLine("Clean folders");
            AT.CleanMachine();

            Console.WriteLine("Create folders");
            Directory.CreateDirectory(NunitSettings.TempFolder);

            //Start services
            Console.WriteLine("Starting services");

            Service.Start(NunitSettings.ServiceWdName);
            Service.Start(NunitSettings.ServiceDttName);

            Console.WriteLine("Services started");
            OpenSpanWindows.WaitOpenSpan(60000);

            ViPrClass.RunVipr();
            ViPrClass.LogOnToSystem(NunitSettings.OperatorId, NunitSettings.PasswordId, NunitSettings.PaymentId, NunitSettings.PasswordPay, NunitSettings.ResearchId, NunitSettings.PasswordRes);

        }

        [TearDown]
        public void Cleanup()
        {
            ViPrClass.ExitVipr();
            //Stop services
            Console.WriteLine("Stopping AT Services");
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            //String TestName = TestContext.CurrentContext.Test.FullName;
            //if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), TestHelper.getTestOutpotFolder() + ".xml", true);
            //if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), "C:\\Backup\\Macro_DecryptedPacket_" + TestName + ".xml", true);
        
        }

        [Test]
        public void CED()
        {
            Console.WriteLine("CED");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            ViPrClass.SwitchToSession2();
            Thread.Sleep(3000);

            Window SESSION2 = ManageWindows.GetWindow("SESSION2");

            MenuBar menuBar = SESSION2.MenuBar;
            White.Core.UIItems.MenuItems.Menu OpenSessionMenu = menuBar.MenuItem("File", "Open Session...");
            OpenSessionMenu.Click();
            Thread.Sleep(2000);

            SESSION2 = ManageWindows.GetWindow("SESSION2");
            
            var OpenSessionWindow = SESSION2.ModalWindows()[0];
            Thread.Sleep(2000);
            
            White.Core.UIItems.TextBox FileNameTextBox = OpenSessionWindow.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("1152"));
            FileNameTextBox.Click();
            UserInputs.Enter("Session6.edp");
            Thread.Sleep(2000);
            OpenSessionWindow.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Open")).Click();
            ViPrClass.SwitchToSession6();
            
            Thread.Sleep(5000);
            UserInputs.Enter("EBDAA08");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);
            //Enter opertorId
            UserInputs.Enter(NunitSettings.OperatorId);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            UserInputs.Enter(NunitSettings.PasswordId);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);
            UserInputs.Enter("GOMM");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(5000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.F12);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);

            ViPrClass.SwitchToViPr();

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\E!PC\\EXTRA.exe\" C:\\Program Files\\E!PC\\Sessions\\Session6.edp");
            ExpApplications.Add("AppUse:CED - Session 6");
            
            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

        }
        
        [Test]
        public void CPF()
        {
            Console.WriteLine("CPF");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            ViPrClass.SwitchToSession2();
            Thread.Sleep(3000);

            Window SESSION2 = ManageWindows.GetWindow("SESSION2");

            MenuBar menuBar = SESSION2.MenuBar;
            White.Core.UIItems.MenuItems.Menu OpenSessionMenu = menuBar.MenuItem("File", "Open Session...");
            OpenSessionMenu.Click();
            Thread.Sleep(2000);

            SESSION2 = ManageWindows.GetWindow("SESSION2");

            var OpenSessionWindow = SESSION2.ModalWindows()[0];
            Thread.Sleep(2000);

            White.Core.UIItems.TextBox FileNameTextBox = OpenSessionWindow.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("1152"));
            FileNameTextBox.Click();
            UserInputs.Enter("Session5.edp");
            Thread.Sleep(2000);
            OpenSessionWindow.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Open")).Click();
            ViPrClass.SwitchToSession5();

            Thread.Sleep(5000);
            UserInputs.Enter("EBDAA08");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);
            //Enter opertorId
            UserInputs.Enter(NunitSettings.OperatorId);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            UserInputs.Enter(NunitSettings.PasswordId);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);
            UserInputs.Enter("GDMI");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(5000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.F12);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);

            ViPrClass.SwitchToViPr();

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\E!PC\\EXTRA.exe\" C:\\Program Files\\E!PC\\Sessions\\Session5.edp");
            ExpApplications.Add("AppUse:CPF - Session 5");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));
            
        }

        [Test]
        public void CHIRPS()
        {
            Console.WriteLine("CHIRPS");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            ViPrClass.SwitchToSession2();
            Thread.Sleep(3000);

            Window SESSION2 = ManageWindows.GetWindow("SESSION2");

            MenuBar menuBar = SESSION2.MenuBar;
            White.Core.UIItems.MenuItems.Menu OpenSessionMenu = menuBar.MenuItem("File", "Open Session...");
            OpenSessionMenu.Click();
            Thread.Sleep(2000);

            SESSION2 = ManageWindows.GetWindow("SESSION2");

            var OpenSessionWindow = SESSION2.ModalWindows()[0];
            Thread.Sleep(2000);

            White.Core.UIItems.TextBox FileNameTextBox = OpenSessionWindow.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("1152"));
            FileNameTextBox.Click();
            UserInputs.Enter("Session1.edp");
            Thread.Sleep(2000);
            OpenSessionWindow.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Open")).Click();
            ViPrClass.SwitchToSession1();

            Thread.Sleep(5000);
            UserInputs.Enter("EBDAA08");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);
            //Enter opertorId
            UserInputs.Enter(NunitSettings.OperatorId);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            UserInputs.Enter(NunitSettings.PasswordId);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);
            UserInputs.Enter("GHMM");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(5000);
           
            ViPrClass.SwitchToViPr();

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("AppUse:CHIRPS - Session 1");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

        }

        [Test]
        public void GROUP()
        {
            Console.WriteLine("GROUP");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            ViPrClass.SwitchToSession2();
            Thread.Sleep(3000);

            Window SESSION2 = ManageWindows.GetWindow("SESSION2");

            MenuBar menuBar = SESSION2.MenuBar;
            White.Core.UIItems.MenuItems.Menu OpenSessionMenu = menuBar.MenuItem("File", "Open Session...");
            OpenSessionMenu.Click();
            Thread.Sleep(2000);

            SESSION2 = ManageWindows.GetWindow("SESSION2");

            var OpenSessionWindow = SESSION2.ModalWindows()[0];
            Thread.Sleep(2000);

            White.Core.UIItems.TextBox FileNameTextBox = OpenSessionWindow.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("1152"));
            FileNameTextBox.Click();
            UserInputs.Enter("Session4.edp");
            Thread.Sleep(2000);
            OpenSessionWindow.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Open")).Click();
            ViPrClass.SwitchToSession4();

            Thread.Sleep(5000);
            UserInputs.Enter("HSIMSA");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);
            
            //Enter opertorId
            UserInputs.Enter(NunitSettings.OperatorId);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            UserInputs.Enter(NunitSettings.PasswordId);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);
            UserInputs.Enter("GROUP");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(5000);
           
            ViPrClass.SwitchToViPr();

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("https://cigna.esecurecare.net/app/home");
            ExpApplications.Add("https://cigna.esecurecare.net/app/answers/list/search/1/kw/RNC/answers.c$is_healthcare_reform/~any~");
            ExpApplications.Add("C-KIT:Reimbursement RNC 501 Processing Reference Guide");
            ExpApplications.Add("\"C:\\Program Files\\E!PC\\EXTRA.exe\" C:\\Program Files\\E!PC\\Sessions\\Session4.edp");
            ExpApplications.Add("AppUse:Group - Session 4");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

        }

    }

    
    [TestFixture]
    public class CKITUsage
    {
        [SetUp]
        public void Init()
        {
            Console.WriteLine("Set hotkeys");
            OpenSpanEvents.SetAllHotkeys();
            OpenSpanEvents.SetAllMacros();

            Console.WriteLine("Clean folders");
            AT.CleanMachine();

            Console.WriteLine("Create folders");
            Directory.CreateDirectory(NunitSettings.TempFolder);

            //Start services
            Console.WriteLine("Starting services");

            Service.Start(NunitSettings.ServiceWdName);
            Service.Start(NunitSettings.ServiceDttName);

            Console.WriteLine("Services started");
            OpenSpanWindows.WaitOpenSpan(60000);

            ViPrClass.RunVipr();
            ViPrClass.LogOnToSystem(NunitSettings.OperatorId, NunitSettings.PasswordId, NunitSettings.PaymentId, NunitSettings.PasswordPay, NunitSettings.ResearchId, NunitSettings.PasswordRes);
                       
        }

        [TearDown]
        public void Cleanup()
        {
            //Stop services
            Console.WriteLine("Stopping AT Services");
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);
                       
        }

        [Test]
        public void CKitLaunchAndSOPSearch() 
        {
            //Run Internet Explorer
            White.Core.Application IE = White.Core.Application.Launch("C:\\Program Files\\Internet Explorer\\iexplore.exe");
            Thread.Sleep(15000);

            Window IEWindow = IE.GetWindow("Pages - Your Cigna Life - Microsoft Internet Explorer provided by CIGNA-Link", White.Core.Factory.InitializeOption.NoCache);
            IEWindow.Get<White.Core.UIItems.TextBox>("Address").SetValue("https://cigna.esecurecare.net/");
            Thread.Sleep(6000);

            IEWindow.Get<White.Core.UIItems.TextBox>("Address").Click();
            UserInputs.PressEnter();
            Thread.Sleep(5000);

            Window IEModalWindow = IEWindow.ModalWindows()[0];

            IEModalWindow.Get<White.Core.UIItems.TextBox>("User name:").SetValue(NunitSettings.InternalId.Substring(9));
            IEModalWindow.Get<White.Core.UIItems.TextBox>("Password:").SetValue(NunitSettings.InternalPwd);
            IEModalWindow.Get<White.Core.UIItems.Button>("OK").Click();
            Thread.Sleep(5000);

            IEWindow.Focus(DisplayState.Restored);

            Console.WriteLine("Looking for Search Button");
            White.Core.UIItems.Button SearchButton = IEWindow.Get<White.Core.UIItems.Button>("Search");
            Console.WriteLine("Search button found. Get Clickable Point");

            Point SearchClickablePoint = SearchButton.Bounds.TopLeft;

            SearchClickablePoint.Offset(-100, 10);

            Mouse.Instance.Click(SearchClickablePoint);

            Console.WriteLine("Enter RNC");
            UserInputs.Enter("RNC");
            UserInputs.PressEnter();
            Thread.Sleep(3000);
            
            White.Core.UIItems.Hyperlink RNCLink = IEWindow.Get<White.Core.UIItems.Hyperlink>("Reimbursement RNC 501 Processing Reference Guide");
            RNCLink.Click();
            Thread.Sleep(2000);

            Console.WriteLine("Looking for Search Button on RNC page"); 
            SearchButton = IEWindow.Get<White.Core.UIItems.Button>("Search");

            SearchClickablePoint = SearchButton.Bounds.TopLeft;
            SearchClickablePoint.Offset(-100, 10);

            Mouse.Instance.Click(SearchClickablePoint);

            Console.WriteLine("Enter document directory");
            UserInputs.Enter("document directory");
            UserInputs.PressEnter();
            Thread.Sleep(3000);

            White.Core.UIItems.Hyperlink DocumentDirectory = IEWindow.Get<White.Core.UIItems.Hyperlink>("C KIT Document Directory");
            DocumentDirectory.RightClick();
            Thread.Sleep(2000);

            IEWindow.Popup.ItemBy(SearchCriteria.ByText("Open in New Tab")).Click();
            Thread.Sleep(5000);
                        
            UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.Tab);
            Thread.Sleep(1000);

            White.Core.UIItems.Hyperlink DirectoryReport = IEWindow.Get<White.Core.UIItems.Hyperlink>("Directory Report.xls");
            DirectoryReport.Click();
            Thread.Sleep(2000);
            
            Window IEDirectoryReportWindow = IE.GetWindow("https://cigna.esecurecare.net/ci/fattach/get/93710/ - Microsoft Internet Explorer provided by CIGNA-Link");
            Window ModalDirectoryReportWindow = IEDirectoryReportWindow.ModalWindow("File Download");
            Thread.Sleep(2000);
            
            Console.WriteLine("File Download window found");
            
            Console.WriteLine("Press ALT+O");
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "O");
            Thread.Sleep(9000);
            
            Console.WriteLine("Kill IE");
            IE.Kill();

            Console.WriteLine("Close Excel");
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("Microsoft Excel"))
                {
                    element.Focus(DisplayState.Restored);
                    Thread.Sleep(5000);
                    element.Close();
                }
            }
                        
            Thread.Sleep(1000);

            ViPrClass.ExitVipr();
            
            //Stop services
            Console.WriteLine("Stop Services");
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("https://cigna.esecurecare.net/app/home");
            ExpApplications.Add("https://cigna.esecurecare.net/app/answers/list/search/1/kw/RNC/answers.c$is_healthcare_reform/~any~");
            ExpApplications.Add("C-KIT:Reimbursement RNC 501 Processing Reference Guide");
            ExpApplications.Add("C-KIT:C KIT Document Directory");
            ExpApplications.Add("\"C:\\Program Files\\Microsoft Office XP Standard\\OFFICE11\\EXCEL.EXE\"  /e");
                                
            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));
          
                    
        }
        
    }
    
    [TestFixture]
    public class LocalDebug
    {
        public static void Main()
        {
            var test = new LocalDebug();
            test.ShortScenario();
        }

        /*[SetUp]
        public void Init()
        {
            Console.WriteLine("Set hotkeys");
            OpenSpanEvents.SetAllHotkeys();
            OpenSpanEvents.SetAllMacros();

            Console.WriteLine("Clean folders");
            AT.CleanMachine();

            Console.WriteLine("Create folders");
            Directory.CreateDirectory(NunitSettings.TempFolder);
            
            Service.Start(NunitSettings.ServiceWdName);
            Service.Start(NunitSettings.ServiceDttName);

            Console.WriteLine("Services started");
            OpenSpanWindows.WaitOpenSpan(60000);
        }*/

        [Test]
        public void ShortScenario()
        {
            //WriteAssemblyInfo(Assembly.GetCallingAssembly(), "Calling");
            //WriteAssemblyInfo(Assembly.GetEntryAssembly(), "Entry");
            //WriteAssemblyInfo(Assembly.GetExecutingAssembly(), "Executing");

            //Console.WriteLine("Stopping AT Services");
            //Service.Stop(NunitSettings.ServiceWdName);
            //Service.Stop(NunitSettings.ServiceDttName);

            //Check all FocusIns
            //List<string> ExpApplications = new List<string>();
            //ExpApplications.Add("\"C:\\Program Files\\Internet Explorer\\iexplore.exe\" SCODEF:6096 CREDAT:79873\"");
            //ExpApplications.Add("https://centralhub.cigna.com/team/IMPVMT/Calculators/Forms/AllItems.aspx");
            //CheckResults.CheckAllFocusIn(ExpApplications);
            //string pathToCurrentDll = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Substring(6);
            //var commandDecrypt = "type \"" + pathToCurrentDll + "\\pass.txt\" | \"" + pathToCurrentDll + "\\gpg.exe\" --passphrase-fd 0 --output \"" + "D:\\Temp\\DecryptedPacket.xml" + "\" --secret-keyring \"" + pathToCurrentDll + "\\secring.gpg\" --keyring \"" + pathToCurrentDll + "\\pubring.gpg\" -d " + "D:\\Temp\\26941b29-cd09-47c0-9ace-0229792c8b16.packet";
            //Console.WriteLine("commandDecrypt : " + commandDecrypt.ToString());

            //Program.ExecuteCommandCmd(commandDecrypt);

            //Thread.Sleep(3000);

            //SystemSettings.GetContentOfXml(@"D:\Temp\DecryptedPacket.xml");

            Console.WriteLine(TestHelper.getTestOutpotFolder());
            
        }

        void WriteAssemblyInfo(Assembly asm, string info)
        {
            if (asm == null)
            {
                Console.WriteLine(info + "is null");
                return;
            }
            Console.WriteLine(info + ": " + asm.FullName);
            Console.WriteLine("codebase:" + Path.GetDirectoryName(asm.CodeBase).Substring(6) );
            

            //Console.WriteLine("location:" + asm.Location);
        }

       
    }


    


       
}
