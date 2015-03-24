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
    public class PrepareMacro
    {

        [SetUp]
        public void CleanBackupFolderBeforeRunningCases()
        {
            Console.WriteLine("Run SetUpFixture for Macro");
            Directory.Delete(NunitSettings.BackupFolder, true);
            Directory.CreateDirectory(NunitSettings.BackupFolder);
        }
    }


    [TestFixture]
    public class MacroScenarios
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
            //Thread.Sleep(15000);

            ViPrClass.RunVipr();
            //ViPrClass.GetViPrWindow().Focus();
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
            // if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), "C:\\Backup\\Macro_DecryptedPacket_" + TestName + ".xml", true);
            //if (Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml") != null) File.Copy(Path.Combine(NunitSettings.TempFolder.ToString(), "DecryptedPacket.xml"), "C:\\Backup\\Macro_DecryptedPacket_" + count.ToString() + ".xml", true);
        }

        [Test]
        public void CodeDefinitionMacro1()
        {

            Console.WriteLine("CodeDefinitionMacro1");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to General Tab
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            Console.WriteLine("Run Macro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);
            Thread.Sleep(5000);

            Console.WriteLine("Switch to ViPr");
            ViPrClass.SwitchToViPr();

            Console.WriteLine("Processing Claim");
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);
            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Console.WriteLine("Stop Services");
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);


            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void CodeDefinitionMacro2()
        {

            Console.WriteLine("-------CodeDefinitionMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Item Name: Label2 Item Type: White.Core.UIItems.TabItems.Tab
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("General");
            Thread.Sleep(3000);

            Console.WriteLine("Run Macro - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);



            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);
            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_General);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));


            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ClaimDemographicsMacro1()
        {
            Console.WriteLine("-------ClaimDemographicsMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);
            //Switch back to tab Claim Demo
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "C");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run Macro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);


            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltC_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_ClaimDemo);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void ClaimDemographicsMacro2()
        {
            Console.WriteLine("-------ClaimDemographicsMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            //Switch back to tab Claim Demo
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Claim Demo");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run Macro - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);


            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(3000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_ClaimDemo);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);


        }

        [Test]
        public void ClaimImageMacro1()
        {
            Console.WriteLine("-------ClaimImageMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            Console.WriteLine("Corkboard Window Name: " + CorkBoardWindow.Name);

            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);
            //Switch back to tab Claim Demo
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "C");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Macro - F2");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);

            //Close corkboard ModalWindow
            CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.ModalWindow("Logon Info - iView").Close();
            Thread.Sleep(3000);
            CorkBoardWindow.ModalWindow("ProclaimMacros").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            Console.WriteLine("Exit ViPr From Scenario");

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltC_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_ClaimDemo);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ClaimImageMacro2()
        {
            Console.WriteLine("-------ClaimImageMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            //Switch back to tab Claim Demo
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Claim Demo");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Macro - F2");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F2")).Click();
            Thread.Sleep(2000);

            //Close corkboard ModalWindow
            CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.ModalWindow("Logon Info - iView").Close();
            Thread.Sleep(3000);
            CorkBoardWindow.ModalWindow("ProclaimMacros").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_ClaimDemo);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void TotalChargesMacro1()
        {
            Console.WriteLine("-------TotalChargesMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Macro - F2");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void TotalChargesMacro2()
        {
            Console.WriteLine("-------TotalChargesMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("General");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Macro - F2");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F2")).Click();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void LPIDateCopyMacro1()
        {
            Console.WriteLine("-------LPIDateCopyMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();
            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            //Press F3 Macro
            Console.WriteLine("Run Macro - F3");
            UserInputs.PressKey((int)VirtualKeys.F3);
            Thread.Sleep(1000);

            //Close corkboard ModalWindow
            CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            Console.WriteLine("Close modal window");
            CorkBoardWindow.ModalWindow("Macro LPIDateCopy Not Available").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F3_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F3_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void LPIDateCopyMacro2()
        {
            Console.WriteLine("-------LPIDateCopyMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("General");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Macro - F3");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F3")).Click();
            Thread.Sleep(1000);

            //Close corkboard ModalWindow
            CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            Console.WriteLine("Close modal window");
            CorkBoardWindow.ModalWindow("Macro LPIDateCopy Not Available").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F3_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void TLCChangerMacro1()
        {
            Console.WriteLine("-------TLCChangerMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            Console.WriteLine("Switch to Corkboard");
            CorkBoardWindow.Click();
            Thread.Sleep(2000);

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            //Press F4 Macro
            Console.WriteLine("Run Macro - F4");
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(1000);

            //Close corkboard ModalWindow
            Console.WriteLine("Close modal window");
            CorkBoard.GetCorkboardWindow().ModalWindow("TLC Changer").Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Cancel")).Click();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F4_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void TLCChangerMacro2()
        {
            Console.WriteLine("-------TLCChangerMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            while (CorkBoardWindow.DisplayState != DisplayState.Restored)
            {
                Console.WriteLine("Activate CorkBoard window");
                CorkBoardWindow.Click();
                Thread.Sleep(2000);
            }


            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("General");
            Thread.Sleep(5000);

            //Press F4 Macro
            Console.WriteLine("Run Macro - F4");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F4")).Click();
            Thread.Sleep(1000);

            //Close corkboard ModalWindow
            Console.WriteLine("Close modal window");
            CorkBoard.GetCorkboardWindow().ModalWindow("TLC Changer").Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Cancel")).Click();
            Thread.Sleep(3000);


            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F4_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void MultipleModifierMacro1()
        {
            Console.WriteLine("-------MultipleModifierMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(3000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            CorkBoardWindow.Focus(DisplayState.Restored);
            while (CorkBoardWindow.DisplayState != DisplayState.Restored)
            {
                Console.WriteLine("Activate CorkBoard window");
                CorkBoardWindow.Click();
                Thread.Sleep(4000);
            }

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            //Press F5 Macro
            Console.WriteLine("Run Macro - F5");
            UserInputs.PressKey((int)VirtualKeys.F5);
            Thread.Sleep(5000);

            //Close Modal Window of CorkBoard
            List<Window> ModalWindows = CorkBoardWindow.ModalWindows();

            foreach (Window ModalWin in ModalWindows)
            {
                Console.WriteLine("Modal Window Name: " + ModalWin.Name);
            }

            CorkBoardWindow.ModalWindow("Invalid Screen Encountered").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1500);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F5_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F5_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void MultipleModifierMacro2()
        {
            Console.WriteLine("-------MultipleModifierMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            while (CorkBoardWindow.DisplayState != DisplayState.Restored)
            {
                Console.WriteLine("Activate CorkBoard window");
                CorkBoardWindow.Click();
                Thread.Sleep(2000);
            }

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("General");
            Thread.Sleep(2000);

            //Press F5 Macro
            Console.WriteLine("Run Macro - F5");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F5")).Click();
            Thread.Sleep(5000);

            //Close Modal Window of CorkBoard
            CorkBoardWindow.ModalWindow("Invalid Screen Encountered").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F5_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void ProclaimLetterReceivedMacro1()
        {
            Console.WriteLine("-------ProclaimLetterReceivedMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            while (CorkBoardWindow.DisplayState != DisplayState.Restored)
            {
                Console.WriteLine("Activate CorkBoard window");
                Thread.Sleep(2000);
                CorkBoardWindow.Click();
            }

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            //Press F6 Macro
            Console.WriteLine("Run Macro - F6");
            UserInputs.PressKey((int)VirtualKeys.F6);
            Thread.Sleep(3000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("Invalid Proclaim Screen").Close();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F6_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F6_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ProclaimLetterReceivedMacro2()
        {
            Console.WriteLine("-------ProclaimLetterReceivedMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            while (CorkBoardWindow.DisplayState != DisplayState.Restored)
            {
                Console.WriteLine("Activate CorkBoard window");
                Thread.Sleep(2000);
                CorkBoardWindow.Click();
            }


            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("General");
            Thread.Sleep(5000);

            //Press F6 Macro
            Console.WriteLine("Run Macro - F6");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F6")).Click();
            Thread.Sleep(3000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("Invalid Proclaim Screen").Close();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F6_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void AdjustmentReasonMacro1()
        {
            Console.WriteLine("-------AdjustmentReasonMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);

            while (CorkBoardWindow.DisplayState != DisplayState.Restored)
            {
                Console.WriteLine("Activate CorkBoard window");
                Thread.Sleep(2000);
                CorkBoardWindow.Click();
            }

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            //Press F7 Macro
            Console.WriteLine("Run Macro - F7");
            UserInputs.PressKey((int)VirtualKeys.F7);
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(1000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F7_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F7_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


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
        public void AdjustmentReasonMacro2()
        {
            Console.WriteLine("-------AdjustmentReasonMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            while (CorkBoardWindow.DisplayState != DisplayState.Restored)
            {
                Console.WriteLine("Activate CorkBoard window");
                Thread.Sleep(2000);
                CorkBoardWindow.Click();
            }

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("General");
            Thread.Sleep(3000);

            //Press F7 Macro
            Console.WriteLine("Run Macro - F7");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F7")).Click();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F7_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void PrivateRoomMacro1()
        {
            Console.WriteLine("-------PrivateRoomMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            while (CorkBoardWindow.DisplayState != DisplayState.Restored)
            {
                Console.WriteLine("Activate CorkBoard window");
                Thread.Sleep(2000);
                CorkBoardWindow.Click();
            }

            //Switch to Tab General
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "G");
            Thread.Sleep(5000);

            //Press F8 Macro
            Console.WriteLine("Run Macro - F8");
            UserInputs.PressKey((int)VirtualKeys.F8);
            Thread.Sleep(5000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("Document Does Not Exist").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltG_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F8_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F8_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void PrivateRoomMacro2()
        {
            Console.WriteLine("-------PrivateRoomMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            while (CorkBoardWindow.DisplayState != DisplayState.Restored)
            {
                Console.WriteLine("Activate CorkBoard window");
                Thread.Sleep(2000);
                CorkBoardWindow.Click();
            }

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("General");
            Thread.Sleep(5000);

            //Press F8 Macro
            Console.WriteLine("Run Macro - F8");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F8")).Click();
            Thread.Sleep(5000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("Document Does Not Exist").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F8_macro_General);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void HistorySearchMacro1()
        {
            Console.WriteLine("-------HistorySearchMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab History
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "H");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run Macro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);
            Thread.Sleep(52000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("HistorySearchForm").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltH_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void HistorySearchMacro2()
        {
            Console.WriteLine("-------HistorySearchMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);


            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("History");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run History Search Macro - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(5000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("HistorySearchForm").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void PreExistingMacro1()
        {
            Console.WriteLine("-------PreExistingMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();
            Thread.Sleep(2000);

            //Switch to Tab History
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "H");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Macro - F2");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltH_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void PreExistingMacro2()
        {
            Console.WriteLine("-------PreExistingMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("History");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Pre-Existing Macro - F2");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F2")).Click();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void DuplicateCheckMacro1()
        {

            Console.WriteLine("-------DuplicateCheckMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();
            Thread.Sleep(2000);

            //Switch to Tab History
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "H");
            Thread.Sleep(5000);

            //Press F3 Macro
            Console.WriteLine("Run Macro - F3");
            UserInputs.PressKey((int)VirtualKeys.F3);
            Thread.Sleep(3000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("ProclaimMacros").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltH_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F3_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F3_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void DuplicateCheckMacro2()
        {
            Console.WriteLine("-------DuplicateCheckMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("History");
            Thread.Sleep(5000);

            //Press F3 Macro
            Console.WriteLine("Run Duplicate Check Macro - F3");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F3")).Click();
            Thread.Sleep(2000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("ProclaimMacros").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F3_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ServiceRequestMacro1()
        {
            Console.WriteLine("-------ServiceRequestMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab History
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "H");
            Thread.Sleep(5000);

            //Press F4 Macro
            Console.WriteLine("Run Macro - F4");
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(2000);

            //Close corkboard ModalWindow
            Console.WriteLine("Close modal window");
            CorkBoard.GetCorkboardWindow().ModalWindow("Create SR").Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Cancel")).Click();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltH_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F4_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ServiceRequestMacro2()
        {
            Console.WriteLine("-------ServiceRequestMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("History");
            Thread.Sleep(5000);

            //Press F4 Macro
            Console.WriteLine("Run Service Request Macro - F4");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F4")).Click();
            Thread.Sleep(2000);

            //Close corkboard ModalWindow
            Console.WriteLine("Close modal window");
            CorkBoard.GetCorkboardWindow().ModalWindow("Create SR").Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Cancel")).Click();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F4_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void GlobalMaternityMacro1()
        {

            Console.WriteLine("-------GlobalMaternityMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab History
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "H");
            Thread.Sleep(5000);

            //Press F5 Macro
            Console.WriteLine("Run Macro - F5");
            UserInputs.PressKey((int)VirtualKeys.F5);
            Thread.Sleep(5000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("No Partial Delivery Code").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltH_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F5_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F5_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void GlobalMaternityMacro2()
        {
            Console.WriteLine("-------GlobalMaternityMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("History");
            Thread.Sleep(5000);

            //Press F5 Macro
            Console.WriteLine("Run Global Maternity Macro - F5");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F5")).Click();
            Thread.Sleep(5000);

            //Close CorkBoard Modal Window
            CorkBoardWindow.ModalWindow("No Partial Delivery Code").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F5_macro_History);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ProclaimOPPDMMacro1()
        {
            Console.WriteLine("-------ProclaimOPPDMMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab Reimbursement
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "R");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run Macro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);
            Thread.Sleep(30000);

            //Close CorkBoard Modal Windows
            CorkBoardWindow.ModalWindow("Logon Info - ACF2").Close();
            Thread.Sleep(5000);
            CorkBoardWindow.ModalWindow("Logon Info - Group").Close();
            Thread.Sleep(5000);
            ManageWindows.CloseWindow("OP PDM");
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltR_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Reimbursement);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


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
        public void ProclaimOPPDMMacro2()
        {
            Console.WriteLine("-------ProclaimOPPDMMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Reimbursement");
            Thread.Sleep(20000);

            //Press F1 Macro
            Console.WriteLine("Run Proclaim OP PDM Macro - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(30000);

            //Close CorkBoard Modal Windows
            CorkBoardWindow.ModalWindow("Logon Info - ACF2").Close();
            Thread.Sleep(5000);
            CorkBoardWindow.ModalWindow("Logon Info - Group").Close();
            Thread.Sleep(5000);
            ManageWindows.CloseWindow("OP PDM");
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Reimbursement);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void AnesthesiaCalculatorMacro1()
        {
            Console.WriteLine("-------AnesthesiaCalculatorMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab Reimbursement
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "R");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Macro - F2");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltR_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_Reimbursement);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void AnesthesiaCalculatorMacro2()
        {
            Console.WriteLine("-------AnesthesiaCalculatorMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Reimbursement");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Anesthesia Calculator Macro - F2");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F2")).Click();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_Reimbursement);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void AssistantSurgeonMacro1()
        {
            Console.WriteLine("-------AssistantSurgeonMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab Reimbursement
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "R");
            Thread.Sleep(5000);

            //Press F3 Macro
            Console.WriteLine("Run Macro - F3");
            UserInputs.PressKey((int)VirtualKeys.F3);
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltR_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F3_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F3_macro_Reimbursement);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void AssistantSurgeonMacro2()
        {

            Console.WriteLine("-------AssistantSurgeonMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Reimbursement");
            Thread.Sleep(5000);

            //Press F3 Macro
            Console.WriteLine("Run Assistant Surgeon Macro - F3");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F3")).Click();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F3_macro_Reimbursement);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ScheduleSearchMacro1()
        {
            Console.WriteLine("-------ScheduleSearchMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab Reimbursement
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "R");
            Thread.Sleep(5000);

            //Press F4 Macro
            Console.WriteLine("Run Macro - F4");
            UserInputs.PressKey((int)VirtualKeys.F4);
            Thread.Sleep(8000);

            //Close CorkBoard Modal Windows
            ManageWindows.CloseWindow("CPF");
            Thread.Sleep(15000);

            ManageWindows.CloseWindow("ACF2");
            Thread.Sleep(3000);

            ManageWindows.CloseWindow("Group");
            Thread.Sleep(5000);

            ManageWindows.CloseWindow("Macro Schedule");
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltR_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F4_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F4_macro_Reimbursement);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


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
        public void ScheduleSearchMacro2()
        {
            Console.WriteLine("-------ScheduleSearchMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Reimbursement");
            Thread.Sleep(5000);

            //Press F4 Macro
            Console.WriteLine("Run Schedule Search Macro - F4");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F4")).Click();
            Thread.Sleep(8000);

            //Close CorkBoard Modal Windows
            ManageWindows.CloseWindow("CPF");
            Thread.Sleep(15000);

            ManageWindows.CloseWindow("ACF2");
            Thread.Sleep(3000);

            ManageWindows.CloseWindow("Group");
            Thread.Sleep(5000);

            ManageWindows.CloseWindow("Macro Schedule");
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();
            Thread.Sleep(10000);
            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F4_macro_Reimbursement);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void OtherInsuranceInfoMacro1()
        {

            Console.WriteLine("-------OtherInsuranceInfoMacro1-------");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab COB
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "O");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run Macro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);
            Thread.Sleep(6000);

            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("Logon Info - CED"))
                {
                    Console.WriteLine("Close : " + element.Name);
                    element.Focus(DisplayState.Restored);
                    element.Close();
                    Thread.Sleep(4000);
                    break;
                }
            }

            allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("Macro Other Insurance Information"))
                {
                    Console.WriteLine("Close : " + element.Name);
                    element.Focus(DisplayState.Restored);
                    element.Close();
                    Thread.Sleep(4000);
                    break;
                }
            }


            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltO_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_COB);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void OtherInsuranceInfoMacro2()
        {
            Console.WriteLine("-------OtherInsuranceInfoMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("COB");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run Other Insurance Info Macro - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(6000);

            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("Logon Info - CED"))
                {
                    Console.WriteLine("Close : " + element.Name);
                    element.Focus(DisplayState.Restored);
                    element.Close();
                    Thread.Sleep(4000);
                    break;
                }
            }

            allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("Macro Other Insurance Information"))
                {
                    Console.WriteLine("Close : " + element.Name);
                    element.Focus(DisplayState.Restored);
                    element.Close();
                    Thread.Sleep(4000);
                    break;
                }
            }

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_COB);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void COBCalculatorMacro1()
        {
            Console.WriteLine("-------COBCalculatorMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab COB
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "O");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Macro - F2");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltO_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_COB);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void COBCalculatorMacro2()
        {
            Console.WriteLine("-------COBCalculatorMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("COB");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run COB calculator Macro - F2");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F2")).Click();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_COB);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void MedicareSupplementMacro1()
        {
            Console.WriteLine("-------MedicareSupplementMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab COB
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "O");
            Thread.Sleep(5000);

            //Press F3 Macro
            Console.WriteLine("Run Macro - F3");
            UserInputs.PressKey((int)VirtualKeys.F3);
            Thread.Sleep(2000);

            //Close CorkBoard Modal Windows
            CorkBoardWindow.ModalWindow("Invalid Screen Encountered").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltO_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F3_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F3_macro_COB);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void MedicareSupplementMacro2()
        {
            Console.WriteLine("-------MedicareSupplementMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("COB");
            Thread.Sleep(5000);

            //Press F3 Macro
            Console.WriteLine("Run Medicare Supplement Macro - F3");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F3")).Click();
            Thread.Sleep(2000);

            //Close CorkBoard Modal Windows
            CorkBoardWindow.ModalWindow("Invalid Screen Encountered").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F3_macro_COB);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ProviderMatchMacro1()
        {
            Console.WriteLine("-------ProviderMatchMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab COB
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "P");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run Macro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);
            Thread.Sleep(2000);

            //Close Modal Window
            CorkBoardWindow.ModalWindow("ProviderMatchAddressDialog").Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Cancel")).Click();
            Thread.Sleep(3000);


            //CorkBoardWindow.ModalWindow("Logon Info - ACF2").Close();
            //Thread.Sleep(3000);
            //CorkBoardWindow.ModalWindow("Logon Info - Proclaim Research").Close();
            //Thread.Sleep(3000);
            //CorkBoardWindow.ModalWindow("Macro Provider Match Not Available").Close();
            //Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltP_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Provider);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ProviderMatchMacro2()
        {
            Console.WriteLine("-------ProviderMatchMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Provider");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run Provider Match Macro - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(2000);

            //Close Modal Window
            CorkBoardWindow.ModalWindow("ProviderMatchAddressDialog").Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Cancel")).Click();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Provider);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void AlfaSearchMacro1()
        {
            Console.WriteLine("-------AlfaSearchMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            //Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            //CorkBoardWindow.Focus(DisplayState.Restored);
            //Thread.Sleep(2000);
            //CorkBoardWindow.Click();

            Thread.Sleep(2000);

            //Switch to Tab COB
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "P");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run AlfaSearchMacro - F2");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);

            //Close Modal Window
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltP_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_Provider);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


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
        public void AlfaSearchMacro2()
        {
            Console.WriteLine("-------AlfaSearchMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab General
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Provider");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run AlfaSearch Macro - F2");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F2")).Click();
            Thread.Sleep(5000);

            //Close Modal Window
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_Provider);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void ServiceProcedureCodeGuideMacro1()
        {
            Console.WriteLine("-------ServiceProcedureCodeGuideMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab COB
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run ServiceProcedureCodeGuide Macro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_PreCertification);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ServiceProcedureCodeGuideMacro2()
        {
            Console.WriteLine("-------ServiceProcedureCodeGuideMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab Pre-Certification
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Pre-Certification");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run ServiceProcedureCodeGuide Macro - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(2000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_PreCertification);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void PreCertificationSearchMacro1()
        {
            Console.WriteLine("-------PreCertificationSearchMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab COB
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Pre-Certification Search Macro - F2");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);

            //Close Modal Window
            CorkBoardWindow.ModalWindow("Logon Info - ICMS").Close();
            Thread.Sleep(5000);
            CorkBoardWindow.ModalWindow("Macro Pre-Certification Search Not Available").Close();
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_PreCertification);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


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
        public void PreCertificationSearchMacro2()
        {
            Console.WriteLine("-------PreCertificationSearchMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab Pre-Certification
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Pre-Certification");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Pre-Certification Search Macro - F2");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F2")).Click();
            Thread.Sleep(5000);

            //Close Modal Window
            CorkBoardWindow.ModalWindow("Logon Info - ICMS").Close();
            Thread.Sleep(5000);
            CorkBoardWindow.ModalWindow("Macro Pre-Certification Search Not Available").Close();
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_PreCertification);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void ProviderNumberMacro1()
        {
            Console.WriteLine("-------ProviderNumberMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Medicaid COB
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "M");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run  ProviderNumber Macro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);
            Thread.Sleep(4000);

            //Close Modal Window
            UserInputs.Enter("177");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            UserInputs.Enter("1");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Medicaid);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);


            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");


            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ProviderNumberMacro2()
        {
            Console.WriteLine("-------ProviderNumberMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            Window ViPr = ViPrClass.ViPr.GetWindow("ViPr - [Benefit Determination - Detail Entry]");
            Window ViPrModal = ViPr.ModalWindows()[0];
            ViPrModal.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Yes")).Click();
            Thread.Sleep(3000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab Pre-Certification
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Medicaid");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run Provider Number Macro - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(4000);

            //Close Modal Window
            UserInputs.Enter("177");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.TAB);
            UserInputs.Enter("1");
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Medicaid);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void ProviderNumberMacro3()
        {

            Console.WriteLine("-------This is fake test created to avoid additional modal windows in upcoming tests. It should be run only after ProviderNumberMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            Window ViPr = ViPrClass.ViPr.GetWindow("ViPr - [Benefit Determination - Detail Entry]");
            Window ViPrModal = ViPr.ModalWindows()[0];
            ViPrModal.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Yes")).Click();
            Thread.Sleep(3000);

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

        }

        [Test]
        public void MedicateAllowanceMacro1()
        {
            Console.WriteLine("-------MedicateAllowanceMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Medicaid COB
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "M");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Medicate Allowance Macro - F2");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);

            //Close Modal Window
            CorkBoardWindow.ModalWindow("ProclaimMacros").Close();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_Medicaid);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void MedicateAllowanceMacro2()
        {
            Console.WriteLine("-------MedicateAllowanceMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab Pre-Certification
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Medicaid");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Medicate Allowance Macro - F2");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F2")).Click();
            Thread.Sleep(4000);

            //Close Modal Window
            CorkBoardWindow.ModalWindow("ProclaimMacros").Close();
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(2000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_Medicaid);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void EORPriceImageMacro1()
        {
            Console.WriteLine("-------EORPriceImageMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Pricing tab
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "I");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run EOR Price Image Macro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);
            Thread.Sleep(5000);

            //Close Modal Window
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltI_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Pricing);

            //Check if Expected Macros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void EORPriceImageMacro2()
        {
            Console.WriteLine("-------EORPriceImageMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab Pricing
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Pricing");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run EOR Price Image Macro - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(5000);

            //Close Modal Window
            UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
            Thread.Sleep(5000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Pricing);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void LOCDiscountOffMacro1()
        {
            Console.WriteLine("-------LOCDiscountOffMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to facility Tools Tab 
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Facilty Tools");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("LOCDiscountOffMacro - F1");
            UserInputs.PressKey((int)VirtualKeys.F1);
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(8000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F1_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Facilty);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void LOCDiscountOffMacro2()
        {
            Console.WriteLine("-------LOCDiscountOffMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to facility Tools Tab 
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Facilty Tools");
            Thread.Sleep(5000);

            //Press F1 Macro
            Console.WriteLine("Run LOC Discount Off - F1");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F1")).Click();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

            UserInputs.PressKey((int)VirtualKeys.O);
            Thread.Sleep(3000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(3000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(8000);

            //Enter Pend Reason "AAMC"
            UserInputs.PressKey((int)VirtualKeys.A);
            Thread.Sleep(2000);

            UserInputs.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

            ViPrClass.ExitVipr();
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F1_macro_Facilty);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

            Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            // Check screen destroying.
            List<OpenSpanClose> ExpScreen7Destroy = new List<OpenSpanClose>();
            OpenSpanClose ExpectedCloseEvent = new OpenSpanClose();

            //ExpectedCloseEvent.trigger = "GlobalContainer.screen_7_destroying";
            //ExpectedCloseEvent.application_id = "Proclaim";
            ExpectedCloseEvent.document_id = NunitSettings.DocumentNumber;
            //ExpectedCloseEvent.oploc = "4055299040054";
            ExpectedCloseEvent.pend_reason_code = "AAMC";   //this shoud be confirmed.
            ExpectedCloseEvent.work_unit_status_code = "O";

            ExpScreen7Destroy.Add(ExpectedCloseEvent);
            //Program.CheckScreen_7_DestroyEvent(ExpScreen7Destroy);

            //Here we check if number of Close an Open events are the same
            Assert.AreEqual(PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count, PacketParser.GetAllOpenEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"))).Count);

        }

        [Test]
        public void SplitYearMacro1()
        {
            Console.WriteLine("-------SplitYearMacro1-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();
            CorkBoardWindow.Focus(DisplayState.Restored);
            Thread.Sleep(2000);
            CorkBoardWindow.Click();

            //Switch to Tab Pricing
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Facilty Tools");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Split Year Macro- F2");
            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(2000);

            //Close Modal Window
            CorkBoardWindow.ModalWindow("Split Year").Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Cancel")).Click();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

            //Stop services
            Service.Stop(NunitSettings.ServiceWdName);
            Service.Stop(NunitSettings.ServiceDttName);

            // Create List of Expected hotkeys
            List<OpenSpan> ExpHotkeys = new List<OpenSpan>();
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.F2_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.AltA_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);
            ExpHotkeys.Add(OpenSpanEvents.CtrlM_hotkey);

            //Check if Expected hotkeys correspond to Actual
            CheckResults.CheckAllHotkeys(ExpHotkeys);

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_Facilty);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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
        public void SplitYearMacro2()
        {
            Console.WriteLine("-------SplitYearMacro2-------");

            UserInputs.PressKey((int)VirtualKeys.F2);
            Thread.Sleep(5000);
            ViPrClass.SetDocNumberAndSN(NunitSettings.DocumentNumber, NunitSettings.SN);

            //Hot keys
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);
            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(8000);

            CorkBoard.RunNew();

            //Switch to CorkBoard;
            Window CorkBoardWindow = CorkBoard.GetCorkboardWindow();

            //Switch to Tab Pricing
            var Label2 = CorkBoardWindow.Get<Tab>(SearchCriteria.ByText("Label2"));
            Label2.SelectTabPage("Facilty Tools");
            Thread.Sleep(5000);

            //Press F2 Macro
            Console.WriteLine("Run Split Year Macro - F2");
            var ToolBar = CorkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByText("ToolStrip1"));
            ToolBar.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("F2")).Click();
            Thread.Sleep(2000);

            //Close Modal Window
            CorkBoardWindow.ModalWindow("Split Year").Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Cancel")).Click();
            Thread.Sleep(3000);

            ViPrClass.SwitchToViPr();

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
            CorkBoard.Close();

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

            // Create List of Expected Macros
            List<OpenSpan> ExpMacros = new List<OpenSpan>();
            ExpMacros.Add(OpenSpanEvents.F2_macro_Facilty);

            //Check if Expected MAcros correspond to Actual
            CheckResults.CheckAllMacros(ExpMacros);

            // Check all FocusIns
            List<string> ExpApplications = new List<string>();
            ExpApplications.Add("\"C:\\Program Files\\ViPr\\ViPr.exe\"");
            ExpApplications.Add("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\" ");

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

