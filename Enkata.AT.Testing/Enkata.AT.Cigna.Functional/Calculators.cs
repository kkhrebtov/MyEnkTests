using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using White.Core;
using White.Core.UIItems.Finders;
using White.Core.WindowsAPI;
using White.Core.UIItems.WindowItems;
using Enkata.ActivityTracker.Core;
using System.IO;

namespace ScriptForCignaAutomation
{
    public static class Calculators
    {

        [TestFixture]
        public class SharepointCalculators
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
            public void TestCalculators()
            {
                Console.WriteLine("SharepointCalculators");

                //Run Internet Explorer
                White.Core.Application IE = White.Core.Application.Launch("C:\\Program Files\\Internet Explorer\\iexplore.exe");
                Thread.Sleep(30000);

                Window IEWindow = IE.GetWindow("Pages - Your Cigna Life - Microsoft Internet Explorer provided by CIGNA-Link", White.Core.Factory.InitializeOption.NoCache);
                IEWindow.Get<White.Core.UIItems.TextBox>("Address").SetValue("https://centralhub.cigna.com/team/IMPVMT/Calculators/Forms/AllItems.aspx");
                Thread.Sleep(4000);

                IEWindow.Get<White.Core.UIItems.TextBox>("Address").Click();
                UserInputs.PressEnter();
                Thread.Sleep(3000);

                Window IEModalWindow = IEWindow.ModalWindows()[0];

                IEModalWindow.Get<White.Core.UIItems.TextBox>("User name:").SetValue(NunitSettings.InternalId);
                IEModalWindow.Get<White.Core.UIItems.TextBox>("Password:").SetValue(NunitSettings.InternalPwd);
                IEModalWindow.Get<White.Core.UIItems.Button>("OK").Click();
                Thread.Sleep(5000);

                IEWindow.Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Advanced Surgical Calculator", "Advanced Surgical Calculator.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Anesthesia Calculator INN and OON", "Anesthesia Calculator INN and OON.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Anesthesia Calculator INN and OON", "Anesthesia Calculator INN and OON.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Dialysis-Epogen Calculator", "Dialysis-Epogen Calculator.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Facility Tool", "Facility Tool.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Julian Date Converter", "Julian Date Converter.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "LifeSource Split Calculator", "LifeSource Split Calculator.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "LPI Calculator", "LPI Calculator.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Medicare Primacy Tool", "Medicare Primacy Tool.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Other Insurance COB Calculator", "Other Insurance COB Calculator.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Other Insurance Primacy Tool", "Other Insurance Primacy Tool.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "PCL Calculator", "PCL Calculator.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Rework (2nd touch)", "Out of Pocket Calculator.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Rework (2nd touch)", "Overpayment and Customer Responsibility Calculator.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Rework (2nd touch)", "Timely Adjustment Date Calculator.xls").Focus(DisplayState.Maximized);
                Calculators.RunCalcFromSharepoint(IEWindow, "Timely Filing Calculator", "Timely Filing Calculator.xls").Close();


                Console.WriteLine("Stopping AT Services");
                Service.Stop(NunitSettings.ServiceWdName);
                Service.Stop(NunitSettings.ServiceDttName);

                //Check all FocusIns
                List<string> ExpApplications = new List<string>();
                //ExpApplications.Add("\"C:\\Program Files\\Internet Explorer\\iexplore.exe\" SCODEF:6096 CREDAT:79873\"");
                ExpApplications.Add("https://centralhub.cigna.com/team/IMPVMT/Calculators/Forms/AllItems.aspx");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Advanced Surgical Calculator ");
                ExpApplications.Add("Calculator:Advanced Surgical Calculator.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Anesthesia Calculator INN and OON ");
                ExpApplications.Add("Calculator:Anesthesia Calculator INN and OON.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Dialysis-Epogen Calculator ");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Facility Tool ");
                ExpApplications.Add("Calculator:Facility Tool.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Julian Date Converter ");
                ExpApplications.Add("Calculator:Julian Date Converter.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > LifeSource Split Calculator ");
                ExpApplications.Add("Calculator:LifeSource Split Calculator.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > LPI Calculator ");
                ExpApplications.Add("Calculator:LPI Calculator.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Medicare Primacy Tool ");
                ExpApplications.Add("Calculator:Medicare Primacy Tool.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Other Insurance COB Calculator ");
                ExpApplications.Add("Calculator:Other Insurance COB Calculator.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Other Insurance Primacy Tool ");
                ExpApplications.Add("Calculator:Other Insurance Primacy Tool.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > PCL Calculator ");
                ExpApplications.Add("Calculator:PCL Calculator.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Rework (2nd touch) ");
                ExpApplications.Add("Calculator:Out of Pocket Calculator.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Rework (2nd touch) ");
                ExpApplications.Add("Calculator:Overpayment and Customer Responsibility Calculator.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Rework (2nd touch) ");
                ExpApplications.Add("Calculator:Timely Adjustment Date Calculator.xls");
                ExpApplications.Add("SharepointPath:Improvement Team > Calculators > Timely Filing Calculator ");
                ExpApplications.Add("Calculator:Timely Filing Calculator.xls");
                Assert.IsTrue(CheckResults.CheckAllFocusIn(ExpApplications));

            }

        }







        public static Window RunCalcFromSharepoint(Window IEWindow, string CalcName, string CalcFile)
        {
            Console.WriteLine("-------------- " + CalcName + " --------------");

            White.Core.UIItems.Panel CalculatorsPanel = IEWindow.Get<White.Core.UIItems.Panel>("Calculators");
            var links = CalculatorsPanel.GetMultiple(SearchCriteria.All);

            //Launch calculator
            foreach (var link in links)
            {
                if (link.Name.Contains(CalcName))
                {
                    link.Click();
                    break;
                }
            }

            Thread.Sleep(2000);

            CalculatorsPanel = IEWindow.Get<White.Core.UIItems.Panel>("Calculators");
            links = CalculatorsPanel.GetMultiple(SearchCriteria.All);

            foreach (var link in links)
            {
                if (link.Name.Contains(CalcFile))
                {
                    link.Click();
                    break;
                }
            }

            Thread.Sleep(2000);
            Window IEModalWindow = IEWindow.ModalWindows()[0];
            IEModalWindow.Get<White.Core.UIItems.Button>("OK").Click();
            Thread.Sleep(10000);

            List<Window> AllWindows = Desktop.Instance.Windows();

            foreach (Window win in AllWindows)
            {
                if (win.Name.Contains("Microsoft Excel"))
                {
                    List<Window> ExcelModalWindows = win.ModalWindows();
                    foreach (Window ModalWin in ExcelModalWindows)
                    {
                        if (ModalWin.Name.Contains("Connect to centralhub.cigna.com"))
                        {
                            ModalWin.Get<White.Core.UIItems.TextBox>("User name:").SetValue(NunitSettings.InternalId);
                            ModalWin.Get<White.Core.UIItems.TextBox>("Password:").SetValue(NunitSettings.InternalPwd);
                            ModalWin.Get<White.Core.UIItems.Button>("OK").Click();
                        }

                        break;
                    }
                    break;
                }

            }

            Thread.Sleep(6000);
            AllWindows = Desktop.Instance.Windows();

            foreach (Window win in AllWindows)
            {
                if (win.Name.Contains("Microsoft Excel"))
                {
                    List<Window> ExcelModalWindows = win.ModalWindows();
                    foreach (Window ModalWin in ExcelModalWindows)
                    {
                        if (ModalWin.Name.Contains("Security Warning"))
                        {
                            ModalWin.Get<White.Core.UIItems.Button>("Enable Macros").Click();
                            break;
                        }
                    }
                    
                    Thread.Sleep(2000);
                    win.Close();
                    Thread.Sleep(2000);

                    //if (CalcName == "Medicare Primacy Tool" || CalcName == "Other Insurance Primacy Tool" || CalcName == "Timely Filing Calculator" )
                    //{
                        if (win.ModalWindows().Count > 0)
                        {
                            UserInputs.HotKey(KeyboardInput.SpecialKeys.ALT, "N");
                            
                        }
                    //}
                    //break;
                }
            }
            Thread.Sleep(3000);
            //Return to IE
            AllWindows = Desktop.Instance.Windows();

            foreach (Window win in AllWindows)
            {
                if (win.Name.Contains("Microsoft Internet Explorer"))
                {
                    IEWindow = win;
                    break;
                }
            }

            White.Core.UIItems.WindowStripControls.ToolStrip toolStrip = IEWindow.GetToolStrip("");
            toolStrip.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Back")).Click();
            Thread.Sleep(3000);

            return IEWindow;

        }
        
    }
}
