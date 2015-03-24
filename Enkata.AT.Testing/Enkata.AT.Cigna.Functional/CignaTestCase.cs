using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptForCignaAutomation.apps;
using NUnit.Framework;
using White.Core.WindowsAPI;
using System.Threading;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems.Finders;
using System.Windows.Automation;
using White.Core.UIItems.ListViewItems;
using White.Core.UIItems;
using White.Core;

namespace ScriptForCignaAutomation {

    [TestFixture]
    class CignaTestCase {

        [Test]
        public void CignaTestCase2() {

            CorkBoard.Run();
            CorkBoard.Login(NunitSettings.OperatorId, NunitSettings.PasswordId, NunitSettings.PaymentId, NunitSettings.PasswordPay, NunitSettings.ResearchId, NunitSettings.PasswordRes);

            viprActions_case02();

            CorkBoard.Close();
            Vipr.close();
        }

        [Test]
        public void CignaTestCase4() {

            Vipr.run();
            Vipr.Login(NunitSettings.OperatorId, NunitSettings.PasswordId, NunitSettings.PaymentId, NunitSettings.PasswordPay, NunitSettings.ResearchId, NunitSettings.PasswordRes);

            viprActions_case04();

            //Vipr.close();
        }

        [Test]
        public void CignaFake1() {

            //Run Extra emulator for Session 6 from the Desktop
            //Login to CED z420/#happy06
            //CED – From Session 2 -> File -> Open Session (Session6)
            Window session2 = CorkBoard.getWindow("SESSION2 - EXTRA!");

            session2.Focus(DisplayState.Restored);
            Thread.Sleep(1000);

            Program.HotKey(KeyboardInput.SpecialKeys.ALT, "f");
            Thread.Sleep(1000);

            AT.PressKey((int)VirtualKeys.O);
            Thread.Sleep(1000);

            session2.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "File name:")).SetValue("Session6.edp");
            Thread.Sleep(1000);

            session2.Get(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Open")).Click();
            Thread.Sleep(1000);

            //Enter application: 'EBDAA08' - > Enter
            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "e");
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "b");
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "d");
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "a");
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "a");
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad0);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad8);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(500);

            //Enter ACF2 ID -> Tab -> password -> Enter

            //q054", "#happy07"

            AT.PressKey((int)VirtualKeys.Q);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad0);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad5);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad4);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "3");
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.H);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.A);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.P);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.P);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Y);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad0);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad7);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.G);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.O);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.M);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.M);

        }
        

        void viprActions_case02() {

            Window viprWindow = Vipr.getViprWindow();

            // Press F2
            AT.PressF2();
            Thread.Sleep(10000);

            // Enter doc. Number: 0431202304001
            // Enter SSN: 9
            Program.SetDocNumberAndSN("0431202304001", "9");

            // Press ‘Alt+A’
            Program.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            // Press ‘Alt+A’
            Program.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            // Press ‘O’ to set disposition code
            AT.PressKey((int)VirtualKeys.O);
            Thread.Sleep(5000);

            // Press ‘Alt+A’
            Program.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            // Enter Pend reason ‘ALLI-ALLIANCE’
            viprWindow.Get(SearchCriteria.ByAutomationId("3")).Click();
            Thread.Sleep(1000);

            AT.PressKey((int)VirtualKeys.A);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.A);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.A);
            Thread.Sleep(500);

            // Press ‘Ctrl+M’
            Program.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            Thread.Sleep(5000);

        }

        void viprActions_case04() {

            Window viprWindow = Vipr.getViprWindow();

            // Press F2
            AT.PressF2();
            Thread.Sleep(10000);

            // Enter doc. Number: 0431202304001
            // Enter SSN: 9
            Program.SetDocNumberAndSN("0431202304001", "9");

            // Press ‘Alt+A’
            Program.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            Thread.Sleep(5000);

            //Run Extra emulator for Session 6 from the Desktop
            //Login to CED z420/#happy06
            //CED – From Session 2 -> File -> Open Session (Session6)
            Window session2 = CorkBoard.getWindow("SESSION2 - EXTRA!");

            session2.Focus(DisplayState.Restored);
            Thread.Sleep(1000);

            Program.HotKey(KeyboardInput.SpecialKeys.ALT, "f");
            Thread.Sleep(1000);

            AT.PressKey((int)VirtualKeys.O);
            Thread.Sleep(1000);

            session2.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "File name:")).SetValue("Session6.edp");
            Thread.Sleep(1000);

            session2.Get(SearchCriteria.ByNativeProperty(AutomationElement.NameProperty, "Open")).Click();
            Thread.Sleep(1000);

            //Enter application: 'EBDAA08' - > Enter
            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "e");
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "b");
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "d");
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "a");
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "a");
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad0);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad8);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(500);

            //Enter ACF2 ID -> Tab -> password -> Enter

            //q054", "#happy07"

            AT.PressKey((int)VirtualKeys.Q);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad0);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad5);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad4);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Tab);
            Thread.Sleep(500);

            Program.HotKey(KeyboardInput.SpecialKeys.SHIFT, "3");
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.H);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.A);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.P);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.P);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Y);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad0);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Numpad7);
            Thread.Sleep(500);

            AT.PressKey((int)VirtualKeys.Return);
            Thread.Sleep(500);

            //Enter gomm -> Enter



            //// Press ‘Alt+A’
            //Program.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            //Thread.Sleep(5000);

            //// Press ‘O’ to set disposition code
            //AT.PressKey((int)VirtualKeys.O);
            //Thread.Sleep(5000);

            //// Press ‘Alt+A’
            //Program.HotKey(KeyboardInput.SpecialKeys.ALT, "A");
            //Thread.Sleep(5000);

            //// Press ‘Ctrl+M’
            //Program.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            //Thread.Sleep(5000);

            //// Press ‘M’ to set disposition code
            //AT.PressKey((int)VirtualKeys.M);
            //Thread.Sleep(5000);

            //// Press ‘Ctrl+M’
            //Program.HotKey(KeyboardInput.SpecialKeys.CONTROL, "M");
            //Thread.Sleep(5000);

            // TBD :
            //Switch  to Session6 -> F12 -> Enter
            //Close Session 6 by pressing X
        }
    }
}
