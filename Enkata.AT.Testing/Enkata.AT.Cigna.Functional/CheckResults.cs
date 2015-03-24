using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using NUnit.Framework;
using White.NUnit;
using System.Diagnostics;
using System.Reflection;
using Enkata.ActivityTracker.Core;

namespace ScriptForCignaAutomation
{
    class CheckResults
    {
        public static void CheckAllHotkeys(List<OpenSpan> ExpHotkeys)
        {
            Console.WriteLine("Call Check all Hotkeys");

            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"));
            var allHotkeys = PacketParser.GetAllHotKey(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml")));

            Console.WriteLine("Full List of actual hotkeys");

            foreach (OpenSpan hotkey in allHotkeys)
            {
                Console.WriteLine("Actual hotkey: " + hotkey.Name1 + " = " + hotkey.Value1 + "  " + hotkey.Name2 + " = " + hotkey.Value2 + "  " + hotkey.Name3 + " = " + hotkey.Value3);
            }
            Console.WriteLine("========================================");

            var index = 0;

            if (ExpHotkeys.Count == allHotkeys.Count)
            {

                foreach (OpenSpan hotkey in ExpHotkeys)
                {
                    Console.WriteLine("Expected hotkey: " + hotkey.Name1 + " = " + hotkey.Value1 + " " + hotkey.Name2 + " = " + hotkey.Value2);
                    Console.WriteLine("Actual hotkey: " + allHotkeys[index].Name1 + " = " + allHotkeys[index].Value1 + " " + allHotkeys[index].Name2 + " = " + allHotkeys[index].Value2);
                    Console.WriteLine("========================================");

                    Assert.AreEqual(hotkey.Name1, allHotkeys[index].Name1);
                    Assert.AreEqual(hotkey.Value1, allHotkeys[index].Value1);
                    Assert.AreEqual(hotkey.Name2, allHotkeys[index].Name2);
                    Assert.AreEqual(hotkey.Value2, allHotkeys[index].Value2);
                    Assert.AreEqual(hotkey.Name3, allHotkeys[index].Name3);
                    Assert.AreEqual(hotkey.Value3, allHotkeys[index].Value3);

                    index++;
                }
            }
            else
            {
                Console.WriteLine("Expected hotkeys: " + ExpHotkeys.Count + "    Actual hotkeys: " + allHotkeys.Count);
                Assert.AreEqual(ExpHotkeys.Count, allHotkeys.Count);
            }
        }

        public static void CheckAllMacros(List<OpenSpan> ExpMacros)
        {
            Console.WriteLine("Call Check all Macros");
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"));
            var allMacros = PacketParser.GetAllMacros(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml")));

            var index = 0;
            foreach (OpenSpan macro in ExpMacros)
            {
                Console.WriteLine("Expected macro: " + macro.Name1 + " = " + macro.Value1);
                Console.WriteLine("Actual macro: " + allMacros[index].Name1 + " = " + allMacros[index].Value1);
                Console.WriteLine("------------------------------------");

                Assert.AreEqual(allMacros[index].Name1, macro.Name1);
                Assert.AreEqual(allMacros[index].Value1, macro.Value1);
                index++;
            }
        }

        //This function verifies if any aplication  from the list in the ExpectedApplication is met in the packet at least once.
        // Return value: true  - if all apllications from ExpectedApplications are met in the packet
        //               false - otherwise   
        public static bool CheckAllFocusIn(List<string> ExpectedApplications)
        {
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"));
            var allFocusIn = PacketParser.GetAllFocusInBehaviour(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml")));

            int i = 0;

            //Create list of comparison results 
            List<bool> results = new List<bool>();

            List<string> Applications = new List<string>();
            foreach (FocusInStruct ActApplication in allFocusIn)
            {
                Applications.Insert(i, ActApplication.ApplicationId);
                i++;
            }

            i = 0;

            //Compare Expected and Actual
            foreach (string ExpFocusIn in ExpectedApplications)
            {
                bool AppFound = false;
                Console.WriteLine("Expected FocusIn: " + ExpFocusIn);

                foreach (string ActFocusIn in Applications)
                {
                    Console.WriteLine("  Actual FocusIn: " + ActFocusIn);
                    if (ExpFocusIn.ToLower().Trim() == ActFocusIn.ToLower().Trim())
                    {
                        Console.WriteLine("Expected Application found");
                        AppFound = true;
                        ActFocusIn.Remove(0);
                        break;
                    }
                }

                //Add comparison result in the list
                if (AppFound == true)
                    results.Add(true);
                else
                    results.Add(false);

                Console.WriteLine("Index = " + i);
                i++;
            }

            i = 0;
            foreach (bool res in results)
            {
                Console.WriteLine("Expected FocusIn " + ExpectedApplications[i] + " found: " + res);
                //Console.WriteLine("Result = " + res);
                i++;
            }

            List<bool> trueResults = new List<bool>();

            //Evaluate overall result of comparing Expected focusIns and Actual FocusIns
            trueResults = results.FindAll(
                   delegate(bool res)
                   {
                       return res;
                   }
                   );

            Console.WriteLine("Totally FocusIns expected: " + ExpectedApplications.Count);
            Console.WriteLine("TrueResultCount:           " + trueResults.Count);

            if (trueResults.Count == ExpectedApplications.Count)
                return true;
            else
                return false;
        }

        public static void CheckWorkUnitClose(List<OpenSpanClose> ExpectedCloseEventList)
        {

            Console.WriteLine("Execute CheckWorkUnitClose");
            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"));
            var content = PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml")));

            var index = 0;
            foreach (OpenSpanClose ActualCloseEvent in content)
            {
                Verify.AreEqual(ActualCloseEvent.trigger, ExpectedCloseEventList[index].trigger);
                Verify.AreEqual(ActualCloseEvent.application_id, ExpectedCloseEventList[index].application_id);
                Verify.AreEqual(ActualCloseEvent.document_id, ExpectedCloseEventList[index].document_id);
                Verify.AreEqual(ActualCloseEvent.oploc, ExpectedCloseEventList[index].oploc);
                Verify.AreEqual(ActualCloseEvent.pend_reason_code, ExpectedCloseEventList[index].pend_reason_code);
                Verify.AreEqual(ActualCloseEvent.work_unit_status_code, ExpectedCloseEventList[index].work_unit_status_code);
                index++;
            }
        }

        public static void CheckScreen_7_DestroyEvent(List<OpenSpanClose> ExpectedDestroyEventList)
        {
            Console.WriteLine("Execute CheckScreen_7_DestroyEvent");

            var packets = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml"));
            var content = PacketParser.GetAllScreen_7_DestroyEvents(Path.Combine(NunitSettings.BackupFolder, String.Concat(TestHelper.getTestName(), ".xml")));

            Console.WriteLine("Full List of CheckScreen_7_DestroyEvent");
            foreach (OpenSpanClose Screen7Destroy in content)
            {
                Console.WriteLine("Screen7Destroy.document_id = " + Screen7Destroy.document_id + " Screen7Destroy.pend_reason_code = " + Screen7Destroy.pend_reason_code + " Screen7Destroy.work_unit_status_code = " + Screen7Destroy.work_unit_status_code);
            }

            Console.WriteLine("===========================");

            int index = 0;
            foreach (OpenSpanClose Screen7Destroy in content)
            {
                //Verify.AreEqual(ActualCloseEvent.trigger, ExpectedDestroyEventList[index].trigger);
                //Verify.AreEqual(ActualCloseEvent.application_id, ExpectedDestroyEventList[index].application_id);
                Verify.AreEqual(Screen7Destroy.document_id, ExpectedDestroyEventList[index].document_id);
                //Verify.AreEqual(ActualCloseEvent.oploc, ExpectedDestroyEventList[index].oploc);
                Verify.AreEqual(Screen7Destroy.pend_reason_code, ExpectedDestroyEventList[index].pend_reason_code);
                Verify.AreEqual(Screen7Destroy.work_unit_status_code, ExpectedDestroyEventList[index].work_unit_status_code);
                index++;
            }
        }
        
    }
    
    public class TestHelper
    {
        public static string getTestName()
        {
            StackTrace stackTrace = new StackTrace();
            foreach (var stackFrame in stackTrace.GetFrames())
            {
                MethodInfo methodInfo = stackFrame.GetMethod() as MethodInfo;
                Object[] attributes = methodInfo.GetCustomAttributes(true);
                for (int j = 0; j < attributes.Length; j++)
                {
                    var attr = attributes[j] as Attribute;
                    if (attr.GetType().Name == "TestAttribute")
                        return methodInfo.Name;
                }

            }
            Assert.Fail("Test name cannot be found");
            return "";
        }

        public static string getTestOutpotFolder()
        {
            return Path.Combine("C:\\Backup\\Macro_DecryptedPacket_", getTestName());
        }
    }
}
