using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using White.Core;
using White.Core.UIItems.Finders;
using System.Windows.Automation;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems.Custom;
using White.Core.UIItems.WindowStripControls;
using White.Core.WindowsAPI;
using White.Core.UIItems;
using System.Diagnostics;
using White.Core.Factory;
using Enkata.ActivityTracker.Core;

namespace ScriptForCignaAutomation
{
    
    public static class CorkBoard
    {
        public static White.Core.Application CorkBoardApp;

        public static void RunNew()
        {

            Console.WriteLine("Launch Corkboard");
            CorkBoardApp = Application.Launch("C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe");
            Console.WriteLine("Corkboard - Start Wait while busy: " + System.DateTime.UtcNow);
            CorkBoardApp.WaitWhileBusy();
            Console.WriteLine("Corkboard - Stop Wait while busy: " + System.DateTime.UtcNow);
            Thread.Sleep(20000);
          
            CorkBoardApp.GetWindow("CorkBoard").Close();
            Thread.Sleep(2000);

        }

        public static void Run()
        {

            Console.WriteLine("Launch Corkboard");
            Program.ExecuteCommandCmd("\"C:\\Program Files\\SastMacros\\CORKBOARD_PRD\\CorkBoard.exe\"");
            Thread.Sleep(25000);

            var items = Desktop.Instance.GetMultiple(SearchCriteria.All);

            foreach (var item in items)
            {   
                if (item.Name.Contains("Corkboard is unable"))
                {   
                    Console.WriteLine("Close Corkboard modal Window");
                    item.Click();
                    Thread.Sleep(2000);
                    UserInputs.PressKey((int)KeyboardInput.SpecialKeys.RETURN);
                    Thread.Sleep(1000);
                    return;
                }
                
            }

            //Desktop.Instance.Get<Button>(SearchCriteria.ByText("OK")).Click();
            //Window dialog = getWindow("CorkBoard");
            //dialog.Get<Button>(SearchCriteria.ByText("OK")).Click();
            Thread.Sleep(10000);

        }

        public static void Close() {

            Window corkBoardWindow = CorkBoardApp.GetWindow("ViPr Macros - CorkBoard - v. 12.7.2.0");
            corkBoardWindow.Close();

            Thread.Sleep(10000);
        }

        public static void Login(string operatorId, string password, string paymentId, string passwordPay, string researchId, string passwordRes) {

            Window corkBoardWindow = getCorkBoardWindow();

            // Choose vipr in drop down list

            ToolStrip toolBar = corkBoardWindow.Get<White.Core.UIItems.WindowStripControls.ToolStrip>(SearchCriteria.ByControlType(ControlType.ToolBar));
            White.Core.UIItems.ListBoxItems.ComboBox comboBox = toolBar.Get<White.Core.UIItems.ListBoxItems.ComboBox>(SearchCriteria.ByControlType(ControlType.ComboBox));
            
            comboBox.Select("ViPr");

            // Logon Info - ACF2
            //Login(corkBoardWindow, "CredentialsDialog", "q054", "#happy07");
            Login(corkBoardWindow, "Logon Info - ACF2", "q054", "#happy07");

            // Logon Info - Proclaim Payment
            //Login(corkBoardWindow, "CredentialsDialog", "tstplb", "#happy05");
            Login(corkBoardWindow, "Logon Info - Proclaim Payment", "tstplb", "#happy05");

            // Logon Info - Proclaim Research
            //Login(corkBoardWindow, "CredentialsDialog", "tr9plb", "#happy05");
            Login(corkBoardWindow, "Logon Info - Proclaim Research", "tr9plb", "#happy05");

            // Wait for ViPr
            Thread.Sleep(30000);

            ViPrHack();
        }

        // Workaround for ViPr. 
        // 1. Click ok button in the Invalid Proclaim Payment Password.
        // 2. Choose Acceptance Testing.
        // 3. Login.
        private static void ViPrHack() {

            // Close invalid password dialog
            Window ViPr = getViprWindow();

            ViPr.Get<Button>(SearchCriteria.ByText("OK")).Click();
            Thread.Sleep(20000);

            Panel operatorPanel = ViPr.Get<Panel>(SearchCriteria.ByText("Workspace"));
            
            operatorPanel.Get(SearchCriteria.ByAutomationId("DropDown")).Click();
            Thread.Sleep(3000);
            operatorPanel.Get(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Acceptance Testing")).Click();
            Thread.Sleep(3000);

            operatorPanel.Get<Button>(SearchCriteria.ByText("Ok")).Click();
            Thread.Sleep(30000);
        }

        private static void Login(Window parentWindow, String windowTitle, String login, String password) {
            
            //Window logonInfo = getChildWindowByAutomationId(parentWindow, windowTitle);
            Window logonInfo = getModalWindowByName(parentWindow, windowTitle);

            TextBox userIdEditBox = logonInfo.Get<TextBox>(SearchCriteria.ByAutomationId("uxLoginID"));
            userIdEditBox.SetValue(login);

            TextBox passwordEditBox = logonInfo.Get<TextBox>(SearchCriteria.ByAutomationId("uxPassword"));
            passwordEditBox.SetValue(password);

            // Workaround : sometimes ViPr freezes and test failed
            // Window didn't respond, after waiting for 5000 ms. Timeout occured, after waiting for 5000 ms 
            try {
                Button okButton = logonInfo.Get<Button>(SearchCriteria.ByAutomationId("OK_Button"));
                okButton.Click();
            } catch {
                
            }

            Thread.Sleep(5000);
        }

        public static Window GetCorkboardWindow(string WindowName)
        {
            return CorkBoard.CorkBoardApp.GetWindow(WindowName);
        }

        public static Window GetCorkboardWindow()
        {
            return CorkBoard.CorkBoardApp.GetWindow("ViPr Macros - CorkBoard - v. 12.7.2.0");
        }

        private static Window getCorkBoardWindow() {
        
            return getWindow("ViPr Macros - CorkBoard");
        }

        public static Window getViprWindow() {
                              
            return getWindow("ViPr - [Login Screen]");
        }

        private static Window getWindow(String name) {

            List<Window> allItem = Desktop.Instance.Windows();
            foreach (Window element in allItem)
            {
                Console.WriteLine("Window Name: " + element.Name);
                if (element.Name.Contains(name))
                {
                    return element;
                }
            }

            return null;
        }

        private static Window getModalWindowByAutomationId(Window window, String automationId) {

            return window.ModalWindow(SearchCriteria.ByAutomationId(automationId));

        }

        private static Window getModalWindowByName(Window window, String name) {

            return window.ModalWindow(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, name));

        }

        private static UIItem getChildByAutomationId(Window window, String automationId) {

            return window.Get<UIItem>(SearchCriteria.ByAutomationId(automationId));

        }

        private static UIItem getChildByName(Window window, String name) {

            return window.Get<UIItem>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, name));

        }
    }
}
