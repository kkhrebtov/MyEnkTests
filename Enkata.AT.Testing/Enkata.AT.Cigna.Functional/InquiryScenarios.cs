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

namespace ScriptForCignaAutomation
{
    [SetUpFixture]
    public class PrepareInquiries
    {

        [SetUp]
        public void CleanBackupFolderBeforeRunningCases()
        {
            Console.WriteLine("Run SetUpFixture for Inquiry");
            Directory.Delete(NunitSettings.BackupFolder, true);
            Directory.CreateDirectory(NunitSettings.BackupFolder);
        }
    }

    [TestFixture]
    public class InquiryScenariosWithOpenClaim
    {

        [SetUp]
        public void Init()
        {
            Console.WriteLine("Set hotkeys");
            OpenSpanEvents.SetAllHotkeys();
            OpenSpanEvents.SetAllMacros();

            Console.WriteLine("Clean folders");
            AT.CleanMachine();


            //Directory.CreateDirectory(NunitSettings.DttPath);
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
            //ViPrClass.ExitVipr();
            //Stop services
            Console.WriteLine("Stopping AT Services");
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            //String TestName = TestContext.CurrentContext.Test.FullName;
            //if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), TestHelper.getTestOutpotFolder() + ".xml", true);
            //if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), "C:\\Backup\\Macro_DecryptedPacket_" + TestName + ".xml", true);

        }

        [Test]
        public void ProclaimTestII_ViPr()
        {
            Console.WriteLine("ProclaimTestII_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(3000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Return to ViPr
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);


            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Proclaim Test II ");


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

        [Test]
        public void ProclaimTestII_SESSION3()
        {
            Console.WriteLine("ProclaimTestII_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Inquiry - Session 2");

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

        [Test]
        public void ChargeAndCreditIssueScreen_SESSION3()
        {
            Console.WriteLine("ChargeAndCreditIssueScreen_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F5);
            Thread.Sleep(2000);

            //Enter Doc Number
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(1000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter("is2");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F5_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 4 - Charge and Credit Issue Screen");

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

        [Test]
        public void PaymentStatus_ViPr()
        {
            Console.WriteLine("PaymentStatus_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);


            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 7 - Payment Status");


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

        [Test]
        public void PaymentStatus_SESSION3()
        {
            Console.WriteLine("PaymentStatus_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Screen 7 - Payment Status");

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

        [Test]
        public void ClaimDetails_ViPr()
        {
            Console.WriteLine("ClaimDetails_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);


            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 8 - Claim Details");


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

        [Test]
        public void ClaimDetails_SESSION3()
        {
            Console.WriteLine("ClaimDetails_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Screen 8 - Claim Details");

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

        [Test]
        public void AdditionalDocInfo_ViPr()
        {
            Console.WriteLine("AdditionalDocInfo_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("adi");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Additional Doc Info");

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

        [Test]
        public void AdditionalDocInfo_SESSION3()
        {
            Console.WriteLine("AdditionalDocInfo_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.Enter("adi");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Additional Doc Info");

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

        [Test]
        public void PatientNoteInquiry_ViPr()
        {
            Console.WriteLine("PatientNoteInquiry_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("not");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);


            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 14 - Patient Note Inquiry");

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

        [Test]
        public void PatientNoteInquiry_SESSION3()
        {
            Console.WriteLine("AdditionalDocInfo_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.Enter("not");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(4000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Screen 14 - Patient Note Inquiry");

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

        [Test]
        public void CoverageScreenMedical_ViPr()
        {
            Console.WriteLine("PatientNoteInquiry_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("oc");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 15 - Other Coverage Screen Medical");

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

        [Test]
        public void CoverageScreenMedical_SESSION3()
        {
            Console.WriteLine("CoverageScreenMedical_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(8000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.Enter("oc");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Screen 15 - Other Coverage Screen Medical");

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

        [Test]
        public void MedicalHistory_ViPr()
        {
            Console.WriteLine("MedicalHistory_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("mhi");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 3 - Medical History");

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

        [Test]
        public void MedicalHistory_SESSION3()
        {
            Console.WriteLine("MedicalHistory_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.Enter("mhi");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Screen 3 - Medical History");

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

        [Test]
        public void PendClaims_ViPr()
        {
            Console.WriteLine("PendClaims_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("pnd");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 27 - Pend Claims");

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

        [Test]
        public void PendClaims_SESSION3()
        {
            Console.WriteLine("PendClaims_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.Enter("pnd");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Screen 27 - Pend Claims");

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

        [Test]
        public void PatientNote2_ViPr()
        {
            Console.WriteLine("PatientNote2_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("not");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("pdx");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 14 - Patient Note2");

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

        [Test]
        public void PatientNote2_SESSION3()
        {
            Console.WriteLine("PatientNote2_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.Enter("not");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("pdx");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Screen 14 - Patient Note2");

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

        [Test]
        public void CurrentPatientAccumulators_ViPr()
        {
            Console.WriteLine("CurrentPatientAccumulators_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(5000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("acm");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 1 - Current Patient Accumulators");

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

        [Test]
        public void CurrentPatientAccumulators_SESSION3()
        {
            Console.WriteLine("CurrentPatientAccumulators_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.Enter("acm");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Screen 1 - Current Patient Accumulators");

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

        [Test]
        public void Deductibles_ViPr()
        {
            Console.WriteLine("Deductibles_ViPr");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("ded");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 26 - Deductibles");

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

        [Test]
        public void Deductibles_SESSION3()
        {
            Console.WriteLine("Deductibles_SESSION3");

            //Action
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            //Switch to Session 3
            ViPrClass.SwitchToSession3();
            Thread.Sleep(8000);

            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.Enter("ded");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            //Choose disposition code
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(4000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason AAMC
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

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
            ExpApplications.Add("Inquiry:Screen 26 - Deductibles");

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
    public class InquiryScenariosSession2
    {
        [SetUp]
        public void Init()
        {
            Console.WriteLine("Set hotkeys");
            OpenSpanEvents.SetAllHotkeys();
            OpenSpanEvents.SetAllMacros();

            Console.WriteLine("Clean folders");
            AT.CleanMachine();

            //Directory.CreateDirectory(NunitSettings.DttPath);
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

            //String TestName = TestContext.CurrentContext.Test.FullName;
            //if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), TestHelper.getTestOutpotFolder() + ".xml", true);
            //if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), "C:\\Backup\\Macro_DecryptedPacket_" + TestName + ".xml", true);
        }

        [Test]
        public void ChargeAndCreditIssueScreen_SESSION2()
        {

            Console.WriteLine("ChargeAndCreditIssueScreen_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F5);
            Thread.Sleep(2000);

            //Enter Doc Number
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(2000);

            UserInputs.Enter("is2");
            Thread.Sleep(1000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F5_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 4 - Charge and Credit Issue Screen");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void PaymentStatus_SESSION2()
        {
            Console.WriteLine("PaymentStatus_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(2000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(3000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 7 - Payment Status");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ClaimDetails_SESSION2()
        {
            Console.WriteLine("ClaimDetails_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(5000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(1000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(3000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(4000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 8 - Claim Details");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void AdditionalDocInfo_SESSION2()
        {
            Console.WriteLine("AdditionalDocInfo_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(4000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(2000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(3000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(4000);

            UserInputs.Enter("adi");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(4000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Additional Doc Info");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);


        }

        [Test]
        public void PatientNoteInquiry_SESSION2()
        {
            Console.WriteLine("PatientNoteInquiry_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(4000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(1000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(1000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            UserInputs.Enter("not");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(4000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 14 - Patient Note Inquiry");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void CoverageScreenMedical_SESSION2()
        {
            Console.WriteLine("CoverageScreenMedical_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(4000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(1000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(3000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            UserInputs.Enter("oc");
            Thread.Sleep(1000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(4000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 15 - Other Coverage Screen Medical");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void MedicalHistory_SESSION2()
        {
            Console.WriteLine("MedicalHistory_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(4000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(2000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(3000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            UserInputs.Enter("mhi");
            Thread.Sleep(1000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(4000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 3 - Medical History");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void PendClaims_SESSION2()
        {
            Console.WriteLine("PendClaims_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(4000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(2000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(3000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            UserInputs.Enter("pnd");
            Thread.Sleep(1000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(4000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 27 - Pend Claims");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void PatientNote2_SESSION2()
        {
            Console.WriteLine("PatientNote2_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(4000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(2000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(3000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            UserInputs.Enter("not");
            Thread.Sleep(1000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            UserInputs.Enter("pdx");
            Thread.Sleep(1000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(4000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 14 - Patient Note2");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);
        }

        [Test]
        public void CurrentPatientAccumulators_SESSION2()
        {
            Console.WriteLine("CurrentPatientAccumulators_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(4000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(1000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(3000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            UserInputs.Enter("acm");
            Thread.Sleep(1000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(4000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 1 - Current Patient Accumulators");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void Deductibles_SESSION2()
        {
            Console.WriteLine("CurrentPatientAccumulators_SESSION2");

            //Switch to Session 2
            ViPrClass.SwitchToSession2();

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(4000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            Thread.Sleep(2000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(3000);
            //Enter DocumentNumber
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(3000);

            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            UserInputs.Enter("ded");
            Thread.Sleep(1000);
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(2000);

            //Return to ViPr
            ViPrClass.SwitchToViPr();
            Thread.Sleep(4000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 26 - Deductibles");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }
    }

    [TestFixture]
    public class InquiryScenariosWithoutOpenClaim
    {

        [SetUp]
        public void Init()
        {
            Console.WriteLine("Set hotkeys");
            OpenSpanEvents.SetAllHotkeys();
            OpenSpanEvents.SetAllMacros();

            Console.WriteLine("Clean folders");
            AT.CleanMachine();

            //Directory.CreateDirectory(NunitSettings.DttPath);
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
        public void ProclaimTestII()
        {
            Console.WriteLine("ProclaimTestII_WithoutClaim");

            //Open Inquiry screen
            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Proclaim Test II ");


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

        [Test]
        [Ignore("Ignore a test")]
        public void ChargeAndCreditIssueScreen()
        {
            Console.WriteLine("ChargeAndCreditIssueScreen_WithoutClaim");

            //Open Inquiry
            UserInputs.PressKey((int)VirtualKeys.F5);
            Thread.Sleep(2000);

            //Enter Doc Number
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(1000);
            //Enter SN
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter("is2");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F5_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 4 - Charge and Credit Issue Screen");

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

        [Test]
        public void PaymentStatus()
        {
            Console.WriteLine("PaymentStatus_ViPr_WithoutClaim");


            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 7 - Payment Status");

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

        [Test]
        public void ClaimDetails()
        {
            Console.WriteLine("ClaimDetails_ViPr_WithoutClaim");

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);

            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);

            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 8 - Claim Details");

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

        [Test]
        public void AdditionalDocInfo()
        {
            Console.WriteLine("AdditionalDocInfo_WithoutClaim");

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("adi");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            Console.WriteLine("Call ExitVipr - from AdditionaldocInfo");
            ViPrClass.ExitVipr();

            //Stop services
            Console.WriteLine("Stop Services");
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Additional Doc Info");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            //Check screen destroying.
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

        [Test]
        public void PatientNoteInquiry()
        {
            Console.WriteLine("PatientNoteInquiry_WithoutClaim");

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("not");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 14 - Patient Note Inquiry");

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

        [Test]
        [Ignore("Ignore a test")]
        public void CoverageScreenMedical()
        {
            Console.WriteLine("PatientNoteInquiry_WithoutClaim");

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("oc");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            var ViPr = ViPrClass.GetViPrWindow();

            MenuBar menuBar = ViPr.Get<MenuBar>(SearchCriteria.ByText("Application"));
            menuBar.MenuItem("Return to Claim").Click();
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 15 - Other Coverage Screen Medical");

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

        [Test]
        public void MedicalHistory()
        {
            Console.WriteLine("MedicalHistory_WithoutClaim");

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("mhi");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 3 - Medical History");

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

        [Test]
        public void PendClaims()
        {

            Console.WriteLine("PendClaims_WithoutClaim");

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("pnd");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 27 - Pend Claims");

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

        [Test]
        public void PatientNote2()
        {

            Console.WriteLine("PatientNote2_WithoutClaim");

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("not");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("pdx");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 14 - Patient Note Inquiry");

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

        [Test]
        public void CurrentPatientAccumulators()
        {
            Console.WriteLine("CurrentPatientAccumulators_ViPr");

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("acm");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 1 - Current Patient Accumulators");

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

        [Test]
        public void Deductibles()
        {
            Console.WriteLine("Deductibles_WithoutClaim");

            UserInputs.HotKey(KeyboardInput.SpecialKeys.SHIFT, KeyboardInput.SpecialKeys.F4);
            Thread.Sleep(3000);

            //Enter to Inquiry
            UserInputs.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.SN);
            Thread.Sleep(2000);
            UserInputs.Enter(NunitSettings.DocumentNumber);
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(2000);
            UserInputs.Enter("ded");
            Thread.Sleep(2000);
            UserInputs.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(2000);

            ViPrClass.ExitVipr();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.ShiftF4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("Inquiry:Screen 26 - Deductibles");

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
}
