using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using NUnit.Framework;
using White.NUnit;

namespace Enkata.ActivityTracker.Core
{
    //TODO: Class name is irrelevant
    public static class Program
    {
        private static List<OpenSpan> _idleTimeStore;
        private const string NameOutPutFile = "DecryptedPacket.xml";
        public static string ScreenRecording = "OFF";
                      
        public static void CheckAllHotkeys(List<OpenSpan> ExpHotkeys)
        {
            Console.WriteLine("Call Check all Hotkeys");

            var packets    = PacketParser.GetPackets(NunitSettings.DttPath, NunitSettings.TempFolder);
            //TODO: Looks suspicious, XPath would be better solution
            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NameOutPutFile);
            var allHotkeys = PacketParser.GetAllHotKey(Path.Combine(NunitSettings.TempFolder, NameOutPutFile));
            
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
            //TODO: Looks suspicious, XPath would be better solution

            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NameOutPutFile);
            
            var allMacros = PacketParser.GetAllMacros(Path.Combine(NunitSettings.TempFolder, NameOutPutFile));

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
            //TODO: Looks suspicious, XPath would be better solution
            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NameOutPutFile);

            var allFocusIn = PacketParser.GetAllFocusInBehaviour(Path.Combine(NunitSettings.TempFolder, NameOutPutFile));

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
            //TODO: Looks suspicious, XPath would be better solution
            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NameOutPutFile);
                                    
            var content = PacketParser.GetAllCloseEvents(Path.Combine(NunitSettings.TempFolder, NameOutPutFile));

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
            //TODO: Looks suspicious, XPath would be better solution
            var outPutFile = PacketParser.DecryptPacketToXmlFile(NunitSettings.DttPath, packets[0].Name, NunitSettings.TempFolder, NameOutPutFile);

            var content = PacketParser.GetAllScreen_7_DestroyEvents(Path.Combine(NunitSettings.TempFolder, NameOutPutFile));

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
            
        public static void CheckIdleTimeAndTime( string tempFolder)
        {
            _idleTimeStore = new List<OpenSpan>();
            const string nameTag = "event-dump";
            const string nameBehaviour1 = "Behavior:idle-start";
            const string nameBehaviour2 = "Behavior:idle-stop";
            var content = SystemSettings.GetContentOfXml(Path.Combine(tempFolder, NameOutPutFile));
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == nameBehaviour1 | tag.GetAttribute("category") == nameBehaviour2)
                {
                    var tempStore = new OpenSpan();
                    //TODO: Looks suspicious, XPath would be better solution
                    if (tag.ChildNodes[0] != null)
                    {
                        tempStore.Name1 = tag.ChildNodes[0].Attributes[0].Value;
                        tempStore.Value1 = tag.ChildNodes[0].Attributes[1].Value;
                    }
                    _idleTimeStore.Add(tempStore);
                }
            }
            //Check event idle start and idle stop contains in packet
            Assert.IsTrue(_idleTimeStore.Count >= 2);
            //Check that event Idle stop contains idle start
            var countIdelTime = 0;
            //foreach (var eventHotKey in _idleTimeStore)
            //{
            //    Assert.IsTrue(eventHotKey.TimeStamp.Contains(AT.GetDate()));
            //    if (eventHotKey.Name1 == "idle_start") countIdelTime = countIdelTime + 1;
            //}
            Assert.IsTrue(countIdelTime >= 1);
        }

        public static string ExecuteCommandCmd(string command)
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

            cmd.WaitForExit();
            //var result = cmd.StandardOutput.ReadToEnd();
            cmd.Close();
            return "";
        }
             
        public static void CheckHotKeyBehaviourAndTime(string tempFolder)
        {
            var hotKeyStore = new List<OpenSpan>();
            const string nameTag = "event-dump";
            const string nameBehaviour = "Behavior:hotkey";
            var content = SystemSettings.GetContentOfXml(Path.Combine(tempFolder, NameOutPutFile));
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == nameBehaviour)
                {
                    var tempStore = new OpenSpan();
                    //TODO: Looks suspicious, XPath would be better solution
                    if (tag.ChildNodes[0] != null)
                    {
                        tempStore.Name1 = tag.ChildNodes[0].Attributes[0].Value;
                        tempStore.Value1 = tag.ChildNodes[0].Attributes[1].Value;
                    }
                    if (tag.ChildNodes[1] != null)
                    {
                        tempStore.Name2 = tag.ChildNodes[1].Attributes[0].Value;
                        tempStore.Value2 = tag.ChildNodes[1].Attributes[1].Value;
                    }
                    hotKeyStore.Add(tempStore);
                }

            }

            var hotKey = 0;
          
            Assert.IsTrue(hotKey == 6, "Incorrect number of hot key! - " + hotKey);
        }

        public static void CheckHotKeyBehaviourAndTime(string pathToDecryptedPacket, string tempFolder)
        {
            var hotKeyStore = new List<OpenSpan>();
            const string nameTag = "event-dump";
            const string nameBehaviour = "Behavior:hotkey";
            var content = SystemSettings.GetContentOfXml(Path.Combine(tempFolder, NameOutPutFile));
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == nameBehaviour)
                {
                    var tempStore = new OpenSpan();
                    tempStore.TimeStamp = tag.GetAttribute("time");
                    //TODO: Looks suspicious, XPath would be better solution
                    if (tag.ChildNodes[0] != null)
                    {
                        tempStore.Name1 = tag.ChildNodes[0].Attributes[0].Value;
                        tempStore.Value1 = tag.ChildNodes[0].Attributes[1].Value;
                    }
                    if (tag.ChildNodes[1] != null)
                    {
                        tempStore.Name2 = tag.ChildNodes[1].Attributes[0].Value;
                        tempStore.Value2 = tag.ChildNodes[1].Attributes[1].Value;
                    }
                    hotKeyStore.Add(tempStore);
                }

            }

            var countFocusIn = 0;
            foreach (var eventHotKey in hotKeyStore)
            {
                if (eventHotKey.Value1 == "F1")
                {
                    Assert.IsTrue(eventHotKey.TimeStamp.Contains(AT.GetDate()));
                    countFocusIn = countFocusIn + 1;
                }
                if (eventHotKey.Value1 == "Alt")
                {
                    if (eventHotKey.Value2 == "S" | eventHotKey.Value2 == "F4")
                    {
                        Assert.IsTrue(eventHotKey.TimeStamp.Contains(AT.GetDate()));
                        countFocusIn = countFocusIn + 1;
                    }
                }
            }
            Assert.IsTrue(countFocusIn == 3);
        }

        public static void CheckFocusInBehaviourAndTime(string pathToDecryptedPacket, string tempFolder)
        {
            //Check count FocusIn event
            var focusInStore = new List<FocusInStruct>();
            const string nameTag = "event-dump";
            const string nameBehaviour = "Behavior:FocusIn";
            var content = SystemSettings.GetContentOfXml(Path.Combine(tempFolder, NameOutPutFile));
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == nameBehaviour)
                {
                    var tempStore = new FocusInStruct();
                    tempStore.TimeStamp = tag.GetAttribute("time");
                    if (tag.FirstChild.Attributes != null)
                    {
                        var appId = tag.FirstChild.Attributes[1].Value;
                        tempStore.ApplicationId = appId;
                    }
                    focusInStore.Add(tempStore);
                }

            }

            var countFocusIn = 0;
            foreach (var eventFocusIn in focusInStore)
            {
                if (eventFocusIn.ApplicationId.ToLower().Contains((@"Enkata Technologies Inc\Activity Tracker\OS Runtime\Enkata.Analytics.VideoRecordingHost.exe").ToLower()) | eventFocusIn.ApplicationId.ToLower().Contains((@"Calc.exe").ToLower()))
                {
                    eventFocusIn.TimeStamp.Contains(AT.GetDate());
                    countFocusIn = countFocusIn + 1;
                }

            }
            if (Program.ScreenRecording == "ON")
            {
                Assert.IsTrue(countFocusIn >= 2);
            }
            else
            {
                Assert.IsTrue(countFocusIn >= 1);
            }
        }

        public static void CheckIdleTimeAndTime(string pathToDecryptedPacket, string tempFolder)
        {
            _idleTimeStore = new List<OpenSpan>();
            const string nameTag = "event-dump";
            const string nameBehaviour1 = "Behavior:idle-start";
            const string nameBehaviour2 = "Behavior:idle-stop";
            var content = SystemSettings.GetContentOfXml(Path.Combine(tempFolder, NameOutPutFile));
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == nameBehaviour1 | tag.GetAttribute("category") == nameBehaviour2)
                {
                    var tempStore = new OpenSpan();
                    tempStore.TimeStamp = tag.GetAttribute("time");
                    //TODO: Looks suspicious, XPath would be better solution
                    if (tag.ChildNodes[0] != null)
                    {
                        tempStore.Name1 = tag.ChildNodes[0].Attributes[0].Value;
                        tempStore.Value1 = tag.ChildNodes[0].Attributes[1].Value;
                    }
                    _idleTimeStore.Add(tempStore);
                }
            }
            //Check event idle start and idle stop contains in packet
            Assert.IsTrue(_idleTimeStore.Count >= 2);
            //Check that event Idle stop contains idle start
            var countIdelTime = 0;
            foreach (var eventHotKey in _idleTimeStore)
            {
                Assert.IsTrue(eventHotKey.TimeStamp.Contains(AT.GetDate()));
                if (eventHotKey.Name1 == "idle_start") countIdelTime = countIdelTime + 1;
            }
            Assert.IsTrue(countIdelTime >= 1);
        }

        public static void CheckOpenSpanStopStart(string pathToDecryptedPacket, string tempFolder)
        {
            var openSpanStartStop = new List<OpenSpan>();
            const string nameTag = "event-dump";
            const string nameOpenSpanStart = "OpenSpan:start";
            const string nameOpenSpanStop = "OpenSpan:stop";
            //TODO: assuming single packet? - bad idea
            var content = SystemSettings.GetContentOfXml(Path.Combine(tempFolder, NameOutPutFile));
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == nameOpenSpanStart | tag.GetAttribute("category") == nameOpenSpanStop)
                {
                    var tempStore = new OpenSpan();
                    tempStore.TimeStamp = tag.GetAttribute("time");
                    openSpanStartStop.Add(tempStore);
                }
            }
            //Check open span start open span stop 
            Assert.IsTrue(openSpanStartStop.Count == 2);
            foreach (var eventHotKey in openSpanStartStop)
            {
                Assert.IsTrue(eventHotKey.TimeStamp.Contains(AT.GetDate()));
            }

            var dateOpenSpanStart = DateTime.Parse(openSpanStartStop[0].TimeStamp);
            var dataOpenSpanStop = DateTime.Parse(openSpanStartStop[1].TimeStamp);
            if (DateTime.Compare(dataOpenSpanStop, dateOpenSpanStart) < 0 | DateTime.Compare(dataOpenSpanStop, dateOpenSpanStart) == 0)
            {
                Console.WriteLine("dataOpenSpanStop=" + dataOpenSpanStop + ".dateOpenSpanStart=" + dateOpenSpanStart);
                Assert.Fail("Event Idle stop and Idle start has incorrect time stamp!");
            }

            //Check time stamp idle time and openSpanStart
            CheckIdleTimeAndTime(pathToDecryptedPacket, tempFolder);
            foreach (var openSpan in _idleTimeStore)
            {
                if (!(DateTime.Compare(dateOpenSpanStart, DateTime.Parse(openSpan.TimeStamp)) < 0))
                {
                    Console.WriteLine("dateOpenSpanStart=" + dateOpenSpanStart + ". openSpan.TimeStamp=" + openSpan.TimeStamp);
                    Assert.Fail("Event Idle time is earlier than OpenSpanStart!");
                }

                if (!(DateTime.Compare(dataOpenSpanStop, DateTime.Parse(openSpan.TimeStamp)) > 0))
                {
                    if (DateTime.Compare(DateTime.Parse(openSpan.TimeStamp), DateTime.Parse(_idleTimeStore[_idleTimeStore.Count - 1].TimeStamp)) != 0)
                        Assert.Fail("Event Idle time is later than OpenSpanStop!");
                }

            }
            //TODO: What are these indexes: 0 and 2?
            if (DateTime.Compare(DateTime.Parse(_idleTimeStore[0].TimeStamp), DateTime.Parse(_idleTimeStore[2].TimeStamp)) == 0) Assert.Fail("All Idle time has the same time!");
        }

        public static void CheckPacketEnkataEvent1(string pathToDecryptedPacket, string tempFolder, string nameEvent)
        {
            var eventPacketStore = new List<OpenSpan>();
            const string nameTag = "event-dump";
            string nameBehaviour1 = nameEvent;
            var content = SystemSettings.GetContentOfXml(Path.Combine(tempFolder, NameOutPutFile));
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("trigger") == nameBehaviour1)
                {
                    var tempStore = new OpenSpan();
                    tempStore.TimeStamp = tag.GetAttribute("time");
                    //TODO: Looks suspicious, XPath would be better solution
                    if (tag.ChildNodes[0] != null)
                    {
                        tempStore.Name1 = tag.ChildNodes[0].Attributes[0].Value;
                        tempStore.Value1 = tag.ChildNodes[0].Attributes[1].Value;
                    }
                    eventPacketStore.Add(tempStore);
                }
            }
            //Check EnkataEvent1 contains in packet
            Assert.IsTrue(eventPacketStore.Count >= 1, "Enkata event = " + eventPacketStore.Count);
            //Check that event EnkataEvent1 contains correct value
            var countFocusIn = 0;
            foreach (var eventHotKey in eventPacketStore)
            {
                Assert.IsTrue(eventHotKey.TimeStamp.Contains(AT.GetDate()));
                if (eventHotKey.Value1 == "31." || eventHotKey.Name1 == "Field1") countFocusIn = countFocusIn + 1;
            }
            Assert.IsTrue(countFocusIn == 1);



        }

    }

}
