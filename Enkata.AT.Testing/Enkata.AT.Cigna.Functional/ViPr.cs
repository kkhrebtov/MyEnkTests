using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Automation;
using White.Core;
using White.Core.WindowsAPI;
using White.Core.InputDevices;
using White.Core.UIItems.Finders;
using White.Core.UIItems.WindowStripControls;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems.ListBoxItems;
using Enkata.ActivityTracker.Core;
//using CignaAutomation;

namespace ScriptForCignaAutomation
{
    public static class ViPrClass
    {

        public static White.Core.Application ViPr;

        public static void SetDocNumberAndSN(string documentNumber, string sn)
        {
            Console.WriteLine("Run Login Procedure");
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name == "ViPr - [Document Selection]")
                {
                    var allTxtField = element.GetMultiple(SearchCriteria.ByControlType(ControlType.Edit));
                    allTxtField[1].Click();
                    allTxtField[2].Click();
                    Thread.Sleep(1000);
                    Keyboard.Instance.Enter(documentNumber);
                    Thread.Sleep(1000);
                    Thread.Sleep(1000);
                    Keyboard.Instance.Enter(sn);
                    Thread.Sleep(1000);
                    allTxtField[0].Click();
                }
            }
        }

        public static void LogOnToSystem(string operatorId, string password, string paymentId, string passwordPay, string researchId, string passwordRes)
        {
            Console.WriteLine("Run LogOn to ViPr");

            Window ViPrLoginWindow = ViPr.GetWindow("ViPr - [Login Screen]");

            var operatorPanel = ViPrLoginWindow.Get<White.Core.UIItems.Panel>(SearchCriteria.ByText("Workspace"));
            var elements = operatorPanel.GetMultiple(SearchCriteria.All);

            operatorPanel.Get(SearchCriteria.ByText("Acceptance Testing")).Click();
            Thread.Sleep(2000);

            foreach (var el in elements)
            {
                switch (el.Id)
                {
                    case "17":
                        el.Focus();
                        Thread.Sleep(1000);
                        Keyboard.Instance.Enter(operatorId);
                        Thread.Sleep(1000);
                        break;
                    case "16":
                        el.Focus();
                        Thread.Sleep(1000);
                        Keyboard.Instance.Enter(password);
                        Thread.Sleep(1000);
                        break;
                    case "13":
                        el.Focus();
                        Thread.Sleep(1000);
                        Keyboard.Instance.Enter(paymentId);
                        Thread.Sleep(1000);
                        break;
                    case "14":
                        el.Focus();
                        Thread.Sleep(1000);
                        Keyboard.Instance.Enter(passwordPay);
                        Thread.Sleep(1000);
                        break;
                    case "12":
                        el.Focus();
                        Thread.Sleep(1000);
                        Keyboard.Instance.Enter(researchId);
                        Thread.Sleep(1000);
                        break;
                    case "11":
                        el.Focus();
                        Thread.Sleep(1000);
                        Keyboard.Instance.Enter(passwordRes);
                        Thread.Sleep(1000);
                        break;
                    case "8":
                        el.Focus();
                        Thread.Sleep(1000);
                        Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.DOWN);
                        Thread.Sleep(1000);
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("Click Ok Button");
            operatorPanel.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Ok")).Click();
            Thread.Sleep(10000);

            bool MainMenuFound = false;

            for (int i = 1; i < 10; i++)
            {
                List<Window> ViPrWindows = ViPr.GetWindows();
                if (ViPrWindows[0].Name != "ViPr - [Main Menu]")
                {
                    Thread.Sleep(2000);
                }
                else
                {
                    MainMenuFound = true;
                    break;
                }
            }

            if (!MainMenuFound)
            {
                ViPrClass.ViPrEmergencyClose();
            }
        }

        public static void EnterNumberInSession(string Number)
        {
            var NumberArray = Number.ToCharArray();
            int input;
            foreach (char Key in NumberArray)
            {
                input = Convert.ToInt32(Key);
                UserInputs.PressKey((byte)input);
                Console.WriteLine("Pressed key: " + input);
                Thread.Sleep(50);
            }

        }


        public static void ViPrEmergencyClose()
        {
            var allItem = Desktop.Instance.Windows();

            foreach (var element in allItem)
            {
                if (element.Name.Contains("ViPr - ["))
                {
                    Console.WriteLine("Click on Close button of ViPr");
                    White.Core.UIItems.Button CloseButton = element.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Close"));
                    CloseButton.Click();
                }
            }

            Thread.Sleep(45000);
            ViPrClass.CheckViPrLogoffError();

        }

        public static void RunVipr()
        {
            Console.WriteLine("Launch ViPr");
            ViPr = White.Core.Application.Launch("c:\\Program Files\\ViPr\\ViPr.exe");
            //ViPr.WaitWhileBusy();
            Thread.Sleep(25000);

        }

        public static void CheckVipr()
        {

            ViPrThread CheckViPrRunning = new ViPrThread();
            Thread CheckViPrRunningThread = new Thread(CheckViPrRunning.CheckViPrRunning);
            CheckViPrRunningThread.Start();

            CheckViPrRunningThread.Join(60000);

            bool MainMenuFound = false;

            for (var i = 0; i < 100; i++)
            {

                Console.WriteLine("Try block inside cycle");
                var allItems = Desktop.Instance.GetMultiple(SearchCriteria.All);

                foreach (var item in allItems)
                {
                    if (item.Name.Contains("ViPr - [Main Menu]"))
                    {
                        Console.WriteLine("Item Name: " + item.Name);
                        Console.WriteLine("ViPr Main Menu Found");
                        MainMenuFound = true;
                        break;
                    }
                }
                if (MainMenuFound)
                {
                    //ViPrObjectThread.Abort();
                    break;
                }
                else Thread.Sleep(2000);
            }

            if (!MainMenuFound)
            {
                Console.WriteLine("ViPr Main Menu NOT Found");
                CheckViPrRunning.DoEmergencyExit();
                CheckViPrRunningThread.Join();
            }

            //ExecuteCommandCmd("\"c:\\Program Files\\ViPr\\ViPr.exe\"");
            //Thread.Sleep(30000);
        }

        public static void ExitVipr()
        {
            List<Window> ViPers = ViPr.GetWindows();

            foreach (Window ViPrWindow in ViPers)
            {
                if (ViPrWindow.Name.Contains("ViPr - ["))
                {
                    MenuBar menuBar = ViPrWindow.Get<MenuBar>(SearchCriteria.ByText("Application"));
                    Thread.Sleep(1000);
                    menuBar.MenuItem("Exit").Click();
                }
            }

            Thread.Sleep(15000);
            //Thread.Sleep(45000);
            //ViPrClass.CheckViPrLogoffError();

        }

        public static bool CheckViPrLogoffError()
        {
            var all = Desktop.Instance.GetMultiple(SearchCriteria.All);

            foreach (var element in all)
            {
                if (element.Name.Contains("There has been an irregularity with the logoff procedure."))
                {
                    Console.WriteLine("Logoff Error detected");
                    element.Click();
                    Thread.Sleep(2000);
                    UserInputs.PressKey((int)VirtualKeys.Return);
                    Thread.Sleep(30000);
                    return true;
                }
            }
            return false;
        }

        public static bool CheckViPrHostWaitError()
        {
            var all = Desktop.Instance.GetMultiple(SearchCriteria.All);

            foreach (var element in all)
            {
                var parent = TreeWalker.RawViewWalker.GetParent(element.AutomationElement);
                var parentName = parent.GetType().ToString();
                Console.WriteLine("Try to get host error type = " + parentName);
                if (element.Name.Contains("Cancel") && element.GetType().ToString() == "button")
                {
                    Console.WriteLine("Host Wait Error detected");
                    element.Click();
                    Thread.Sleep(2000);

                    return true;
                }
            }
            return false;
        }

        public static void SetPendReason(string PendReason)
        {
            var ViPr = ViPrClass.GetViPrWindow();

            Window ClaimFinalization = ViPr.Get<Window>(SearchCriteria.ByText("Claim finalization"));
            var PendReasonBox = ClaimFinalization.Get<Win32ComboBox>(SearchCriteria.ByAutomationId("3"));

            PendReasonBox.Click();
            PendReasonBox.SetValue(PendReason);

        }

        public static void SwitchToSession1()
        {
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("SESSION1 - EXTRA!"))
                {
                    element.Focus(DisplayState.Restored);
                    element.Click();
                    while (!element.IsCurrentlyActive)
                    {
                        Thread.Sleep(2000);
                    }
                    break;
                }
            }

        }

        public static void SwitchToSession2()
        {
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("SESSION2 - EXTRA!"))
                {
                    element.Focus(DisplayState.Restored);
                    element.Click();
                    while (!element.IsCurrentlyActive)
                    {
                        Thread.Sleep(2000);
                    }
                    break;
                }
            }

        }

        public static void SwitchToSession3()
        {
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("SESSION3 - EXTRA!"))
                {
                    element.Focus(DisplayState.Restored);
                    element.Click();
                    while (!element.IsCurrentlyActive)
                    {
                        Thread.Sleep(2000);
                    }
                    break;
                }
            }
        }

        public static void SwitchToSession4()
        {
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("SESSION4 - EXTRA!"))
                {
                    element.Focus(DisplayState.Restored);
                    element.Click();
                    while (!element.IsCurrentlyActive)
                    {
                        Thread.Sleep(2000);
                    }
                    break;
                }
            }
        }

        public static void SwitchToSession5()
        {
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("SESSION5 - EXTRA!"))
                {
                    element.Focus(DisplayState.Restored);
                    element.Click();
                    while (!element.IsCurrentlyActive)
                    {
                        Thread.Sleep(2000);
                    }
                    break;
                }
            }
        }

        public static void SwitchToSession6()
        {
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name.Contains("SESSION6 - EXTRA!"))
                {
                    element.Focus(DisplayState.Restored);
                    element.Click();
                    while (!element.IsCurrentlyActive)
                    {
                        Thread.Sleep(2000);
                    }
                    break;
                }
            }
        }

        public static void SwitchToViPr()
        {
            List<Window> ViPrWindows = ViPrClass.ViPr.GetWindows();
            foreach (Window ViPrWindow in ViPrWindows)
            {
                if (ViPrWindow.Name.Contains("ViPr - ["))
                {
                    ViPrWindow.Focus(DisplayState.Restored);
                }
            }
            Thread.Sleep(3000);

        }

        public static Window GetViPrWindow()
        {
            Console.WriteLine("Call GetViPrWindow()");
            List<Window> allItem = Desktop.Instance.Windows();
            foreach (Window element in allItem)
            {
                if (element.Name.Contains("ViPr - ["))
                {
                    return element;
                }
            }
            return null;
        }

    }
}
