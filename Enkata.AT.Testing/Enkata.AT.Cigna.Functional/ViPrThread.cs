using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Automation;
using White.Core;
using White.Core.InputDevices;
using White.Core.UIItems.Finders;
using White.Core.WindowsAPI;
using White.Core.UIItems.MenuItems;
using White.Core.UIItems.WindowStripControls;
using White.Core.UIItems.WindowItems;
using White.Core.UIItems.ListBoxItems;
using Enkata.ActivityTracker.Core;

namespace ScriptForCignaAutomation
{
   public class ViPrThread
   {
       public void DoNothing()
       {
           Console.WriteLine("Exit ViPr - called from multithread");
       }

       public static void DoAtStart(string text) 
       {
            Console.WriteLine("Enter in RunViPr Thread");
            ViPrThread.RunVipr();
            ViPrThread.LogOnToSystem(NunitSettings.OperatorId, NunitSettings.PasswordId, NunitSettings.PaymentId, NunitSettings.PasswordPay, NunitSettings.ResearchId, NunitSettings.PasswordRes);
       }

       public void DoAtStart()
       {
           Console.WriteLine("Enter in RunViPr Thread");
           ViPrThread.RunVipr();
           ViPrThread.LogOnToSystem(NunitSettings.OperatorId, NunitSettings.PasswordId, NunitSettings.PaymentId, NunitSettings.PasswordPay, NunitSettings.ResearchId, NunitSettings.PasswordRes);
       }

       public void DoExitVipr()
       {
           Console.WriteLine("Enter in DoExitVipr)");
           var allItem = Desktop.Instance.Windows();

           foreach (var element in allItem)
           {
               if (element.Name.Contains("ViPr - ["))
               {
                   Console.WriteLine("Click on Exit button of ViPr - called from DoExitViPr)");
                   MenuBar menuBar = element.Get<MenuBar>(SearchCriteria.ByText("Application"));
                   Thread.Sleep(1000);
                   menuBar.MenuItem("Exit").Click();

               }
           }

           Thread.Sleep(45000);
           ViPrClass.CheckViPrLogoffError();
       }

       public void DoEmergencyExit() 
       {
           Console.WriteLine("ViPr Emergency Close");
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
       
       private static void RunVipr()
       {
           Console.WriteLine("Execute - RunVipr() ");
           ExecuteCommandCmd("\"c:\\Program Files\\ViPr\\ViPr.exe\"");
           Thread.Sleep(30000);
       }

       private static string ExecuteCommandCmd(string command)
       {
           var cmd = new System.Diagnostics.Process();
           cmd.StartInfo.FileName = "cmd.exe";
           cmd.StartInfo.RedirectStandardInput =
               true;
           cmd.StartInfo.RedirectStandardOutput =
               true;
           cmd.StartInfo.LoadUserProfile = true;
           cmd.StartInfo.UseShellExecute = false;
           cmd.Start();

           /* execute "dir" */
           cmd.StandardInput.WriteLine(command);
           cmd.StandardInput.WriteLine("Y");
           cmd.StandardInput.Flush();
           cmd.StandardInput.Close();

           //var result = cmd.StandardOutput.ReadToEnd();
           cmd.Close();
           return "";
       }
       
       private static void LogOnToSystem(string operatorId, string password, string paymentId, string passwordPay, string researchId, string passwordRes)
        {
            Console.WriteLine("Execute LogOnToSystem");
            var allItem = Desktop.Instance.Windows();
            foreach (var element in allItem)
            {
                if (element.Name == "ViPr - [Login Screen]")
                {
                    var operatorPanel = element.Get<White.Core.UIItems.Panel>(SearchCriteria.ByText("Workspace"));
                    Thread.Sleep(5000);
                    var content = operatorPanel.GetMultiple(SearchCriteria.ByControlType(ControlType.Edit));

                    foreach (var i in content) {
                        Console.WriteLine("Name: " + i.Name + " ========== Type: " + i.GetType().ToString());
                    }
                    
                    var Operator_id = operatorPanel.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("17"));
                    var ProcSystem  = operatorPanel.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("8"));
                    
                    Thread.Sleep(5000);
                    //var cbox = operatorPanel.GetMultiple(SearchCriteria.All);
                    //foreach (var item in cbox)
                    //{
                    //    if (item.Name != "Ok")
                    //    {
                    //        item.Click();
                    //        break;
                    //    }
                    //}
                    //Thread.Sleep(3000);
                    Operator_id.SetValue(operatorId);
                    Thread.Sleep(3000);
                    Console.WriteLine("Operator Id: " + Operator_id.Text);

                    var Password = operatorPanel.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("16"));
                    Password.SetValue(password);
                    Thread.Sleep(3000);
                    Console.WriteLine("Pwd: " + Password.Text);
                    
                    //ProcSystem.SetValue("Acceptance Testing");
                    operatorPanel.Get(SearchCriteria.ByText("Acceptance Testing")).Click();
                    Thread.Sleep(3000);

                    var Payment_id = operatorPanel.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("13"));
                    Payment_id.SetValue(paymentId);
                    Thread.Sleep(3000);
                    Console.WriteLine("Payment Id: " + Payment_id.Text);

                    var PaymentPwd = operatorPanel.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("14"));
                    PaymentPwd.SetValue(passwordPay);
                    Thread.Sleep(3000);
                    Console.WriteLine("Payment Pwd: " + PaymentPwd.Text);

                    var Research_id = operatorPanel.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("12"));
                    Research_id.SetValue(researchId);
                    Thread.Sleep(3000);
                    Console.WriteLine("Research Id");

                    var ResearchPwd = operatorPanel.Get<White.Core.UIItems.TextBox>(SearchCriteria.ByAutomationId("11"));                    
                    ResearchPwd.SetValue(passwordRes);
                    Thread.Sleep(3000);
                    Console.WriteLine("Research Pwd: " + ResearchPwd.Text);
                    Thread.Sleep(3000);
                    
                    //operatorPanel.Get<White.Core.UIItems.Button>(SearchCriteria.ByText("Ok")).Click();
                    Thread.Sleep(25000);
                }
            }
       }

       public void CheckViPrRunning()
       {
           Console.WriteLine("Enter in CheckViPrRunning thread");
           bool MainMenuFound = false;

           for (var i = 0; i < 200; i++)
           {

               Console.WriteLine("Try block inside cycle " + i.ToString());
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
                  break;
               }
               else Thread.Sleep(2000);
           }

           if (!MainMenuFound)
           {
               Console.WriteLine("ViPr Main Menu NOT Found");
               DoEmergencyExit();
           }
       }

   }
}
