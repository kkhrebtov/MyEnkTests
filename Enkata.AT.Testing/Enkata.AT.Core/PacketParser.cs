using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using NUnit.Framework;
using System.Reflection;
using System.Threading;

namespace Enkata.ActivityTracker.Core
{
    public class PacketParser
    {

        private const string NameOutPutFile = "DecryptedPacket.xml";
        private static List<OpenSpan> _idleTimeStore;

        public static List<OpenSpanClose> GetAllCloseEvents(string pathToDecryptedPacket)
        {
            var CloseEventsStore = new List<OpenSpanClose>();
            const string nameTag = "event-dump";
            const string eventCategory = "WorkUnitStateChange:Close";

            var content = SystemSettings.GetContentOfXml(pathToDecryptedPacket);

            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == eventCategory)
                {
                    var tempClose = new OpenSpanClose();
                    tempClose.trigger = tag.GetAttribute("trigger");
                    if (tag.ChildNodes[0] != null)
                    {
                        //TODO: Looks suspicious, XPath would be better solution
                        tempClose.application_id = tag.ChildNodes[0].Attributes[1].Value;
                    }

                    CloseEventsStore.Add(tempClose);
                }

             }

            return CloseEventsStore;
        }

        public static List<string> GetAllOpenEvents(string pathToDecryptedPacket)
        {
            var OpenEventStore = new List<string>();
            const string nameTag = "event-dump";
            string AppId;
            const string nameBehaviour = "WorkUnitStateChange:Open";
            var content = SystemSettings.GetContentOfXml(pathToDecryptedPacket);
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == nameBehaviour)
                {
                    if (tag.FirstChild.Attributes != null)
                    {
                        AppId = tag.FirstChild.Attributes[1].Value;
                        OpenEventStore.Add(AppId);
                    }

                }

            }
            return OpenEventStore;
        }
        
        public static bool GetAllScreen_5_DestroyEvents(string pathToDecryptedPacket)
        {
            var AllScreen_5_DestoyEvents = new List<string>();
            bool result = false;
            const string nameTag = "event_dump";
            
            const string Screen_5_Destroy = "screen_5_destroying";
                       
            var content = SystemSettings.GetContentOfXml(pathToDecryptedPacket);
            var listTags = content.GetElementsByTagName(nameTag);

            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == Screen_5_Destroy)
                {
                    result = true;
                }
            }
            return result;
        }
        
        public static bool GetAllScreen_6_DestroyEvents(string pathToDecryptedPacket)
        {
            var AllScreen_6_DestoyEvents = new List<string>();
            bool result = false;
            const string nameTag = "event_dump";

            const string Screen_6_Destroy = "screen_6_destroying";

            var content = SystemSettings.GetContentOfXml(pathToDecryptedPacket);
            var listTags = content.GetElementsByTagName(nameTag);

            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == Screen_6_Destroy)
                {
                    result = true;
                }
            }
            return result;
        }

        public static List<OpenSpanClose> GetAllScreen_7_DestroyEvents(string pathToDecryptedPacket)
        {
            Console.WriteLine("Execute GetAllScreen_7_DestroyEvents");
            var CloseEventsStore = new List<OpenSpanClose>();
            const string nameTag = "event-dump";
            const string eventCategory = "screen_7_destroying";

            var content = SystemSettings.GetContentOfXml(pathToDecryptedPacket);

            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == eventCategory)
                {
                    var tempClose = new OpenSpanClose();
                    tempClose.trigger = tag.GetAttribute("trigger");
                    if (tag.ChildNodes[0] != null)
                    {
                        //TODO: Looks suspicious, XPath would be better solution
                        tempClose.document_id = tag.ChildNodes[0].Attributes[1].Value;
                        tempClose.pend_reason_code = tag.ChildNodes[3].Attributes[1].Value;
                        tempClose.work_unit_status_code = tag.ChildNodes[1].Attributes[1].Value;
                    }

                    CloseEventsStore.Add(tempClose);
                }

            }

            return CloseEventsStore;
        }
               
        public static List<FocusInStruct> GetAllFocusInBehaviour(string pathToDecryptedPacket)
        {
            var focusInStore = new List<FocusInStruct>();
            const string nameTag = "event-dump";
            const string nameBehaviour = "Behavior:FocusIn";
            var content = SystemSettings.GetContentOfXml(pathToDecryptedPacket);
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                var tempStore = new FocusInStruct();
                if (tag.GetAttribute("category") == nameBehaviour || tag.GetAttribute("category") == "CustomFocusIn")
                {
                    if (tag.FirstChild.Attributes != null)
                    {
                        var appId = tag.FirstChild.Attributes[1].Value;
                        tempStore.ApplicationId = appId;
                    }
                    focusInStore.Add(tempStore);
                }

            }
            return focusInStore;
        }

        public static List<OpenSpan> GetAllHotKey(string pathToDecryptedPacket)
        {
            Console.WriteLine("CallGetAllHotkey");

            var hotKeyStore = new List<OpenSpan>();
            const string nameTag = "event-dump";
            const string nameBehaviour = "Behavior:hotkey";
            var content = SystemSettings.GetContentOfXml(pathToDecryptedPacket);
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
                    if (tag.ChildNodes[2] != null)
                    {
                        tempStore.Name3 = tag.ChildNodes[2].Attributes[0].Value;
                        tempStore.Value3 = tag.ChildNodes[2].Attributes[1].Value;
                    }
                   hotKeyStore.Add(tempStore);
                }
            }
                       
            return hotKeyStore;
        }
        
        public static List<OpenSpan> GetAllMacros(string pathToDecryptedPacket)
        {
            var macroStore = new List<OpenSpan>();
            const string nameTag = "event-dump";
            const string nameBehaviour = "Behavior:Macro";
            var content = SystemSettings.GetContentOfXml(pathToDecryptedPacket);
            var listTags = content.GetElementsByTagName(nameTag);
            foreach (XmlElement tag in listTags)
            {
                if (tag.GetAttribute("category") == nameBehaviour)
                {
                    var tempStore = new OpenSpan();
                    
                    if (tag.ChildNodes[0] != null)
                    {
                        //TODO: Looks suspicious, XPath would be better solution
                        tempStore.Name1 = tag.ChildNodes[0].Attributes[0].Value;
                        tempStore.Value1 = tag.ChildNodes[0].Attributes[1].Value;
                    }
                   

                    macroStore.Add(tempStore);
                }
            }

            foreach (OpenSpan macro in macroStore)
            {
                Console.WriteLine("Found macro in packet: " + macro.Name1 + " = " + macro.Value1);
            }

            return macroStore;
        }

        public static List<OpenSpan> GetAllIdleTime(string pathToDecryptedPacket)
        {
            _idleTimeStore = new List<OpenSpan>();
            const string nameTag = "event-dump";
            const string nameBehaviour1 = "Behavior:idle-start";
            const string nameBehaviour2 = "Behavior:idle-stop";
            var content = SystemSettings.GetContentOfXml(pathToDecryptedPacket);
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
            //Check that event Idle stop contains idle start
            //var countIdelTime = 0;
            //foreach (var eventHotKey in _idleTimeStore)
            //{
            //    Assert.IsTrue(eventHotKey.TimeStamp.Contains(AT.GetDate()));
            //    if (eventHotKey.Name1 == "idle_start") countIdelTime = countIdelTime + 1;
            //}
            return _idleTimeStore;
        }
       
        public static List<FileInfo> GetPackets(string dttPath, string tempFolder)
        {
            Directory.CreateDirectory(tempFolder);
            //Copy packets to temp folder from dttoutput.
            var packetsFolderPath = Path.Combine(Path.Combine(dttPath, AT.GetDate()), @"PACKETS");
            return AT.GetContentOfFolder(packetsFolderPath, "*.packet");

        }

        //TODO: One of the two following functions must be removed. At least one of them should use another
        public static string DecryptPacket(string dttPath, string namePacket, string tempFolder, string pathToDecriptFile)
        {
            var dttSource = Path.Combine(Path.Combine(Path.Combine(dttPath, AT.GetDate()), @"PACKETS"), namePacket);
            var tempDest = Path.Combine(tempFolder, namePacket);
            File.Copy(dttSource, tempDest, true);
            string packetBackupFolder = Path.Combine(tempFolder, @"PacketsBackup");
            if (!Directory.Exists(packetBackupFolder))
            {
                Directory.CreateDirectory(packetBackupFolder);
            }
            File.Copy(dttSource, Path.Combine(packetBackupFolder, namePacket), true);
            //Decript packets
            var outPutFile = Path.Combine(tempFolder, NameOutPutFile);
            var fileTobeDecrypt = Path.Combine(tempFolder, namePacket);
            var key = "";
            if (File.Exists(outPutFile)) key = "--i";
            var commandDecrypt = "type \"" + pathToDecriptFile + "\\pass.txt\" | \"" + pathToDecriptFile + "\\gpg.exe\"  " + key + " --passphrase-fd 0 --output \"" + outPutFile + "\" --secret-keyring \"" + pathToDecriptFile + "\\secring.gpg\" --keyring \"" + pathToDecriptFile + "\\pubring.gpg\" -d " + fileTobeDecrypt;
            Program.ExecuteCommandCmd(commandDecrypt);

            return outPutFile;
        }

        public static string DecryptPacketToXmlFile(string dttPath, string namePacket, string tempFolder, string nameOutPutFile)
        {
            var dttSource = Path.Combine(Path.Combine(Path.Combine(dttPath, AT.GetDate()), @"PACKETS"), namePacket);
            
            //var tempDest = Path.Combine(tempFolder, namePacket);
            //Console.WriteLine("tempDest : " + tempDest.ToString());

            string pathToCurrentDll = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Substring(6);
            
            Console.WriteLine("Path To Current DLL: " + pathToCurrentDll);
            
            //File.Copy(dttSource, tempDest, true);
            
            //Decript packets
            var outPutFile = Path.Combine(tempFolder, nameOutPutFile);
            Console.WriteLine("outPutFile : " + outPutFile.ToString());
            var fileTobeDecrypt = Path.Combine(tempFolder, namePacket);
            Console.WriteLine("fileTobeDecrypt : " + fileTobeDecrypt.ToString());
            var key = "";
            if (File.Exists(outPutFile)) key = "--i";
            var commandDecrypt = "type \"" + pathToCurrentDll + "\\pass.txt\" | \"" + pathToCurrentDll + "\\gpg.exe\"  " + key + " --passphrase-fd 0 --output \"" + outPutFile + "\" --secret-keyring \"" + pathToCurrentDll + "\\secring.gpg\" --keyring \"" + pathToCurrentDll + "\\pubring.gpg\" -d " + dttSource;
            Console.WriteLine("commandDecrypt : " + commandDecrypt.ToString());
            
            Program.ExecuteCommandCmd(commandDecrypt);
            //TODO: Sleep must be removed. gpg application must be called synchronously
            Thread.Sleep(3000);                     
            return outPutFile;
        }

        public static void CheckWdLogFileNoError(string pathToWdLogFile)
        {
            var pathToLog = Path.Combine(Path.Combine(pathToWdLogFile, AT.GetDate()), "LOGS");
            var listFiles = AT.GetContentOfFolder(pathToLog, "*.log");
            var logfile = "";
            foreach (var listFile in listFiles)
            {
                if (listFile.Name.Contains("Watchdog"))
                    logfile = listFile.Name;
            }
            var text = File.ReadAllText(Path.Combine(pathToLog, logfile));
            Assert.IsTrue(!text.Contains("terminate"), "'terminate' appears in the WatchDog.log");
            Assert.IsTrue(!text.Contains("Fatal"), "'Fatal' appears in the WatchDog.log");
            Assert.IsTrue(!text.Contains("ERROR"), "ERROR appears in the WatchDog.log");
        }

        public static void CheckOsRunTimeLogFileExistCorrectInformation(string pathToWdLogFile)
        {
            var pathToLog = Path.Combine(Path.Combine(pathToWdLogFile, AT.GetDate()), "LOGS");
            var listFiles = AT.GetContentOfFolder(pathToLog, "*.log");
            var logfile = "";
            foreach (var listFile in listFiles)
            {
                if (listFile.Name.Contains("os_runtime"))
                    logfile = listFile.Name;
            }
            var text = File.ReadAllText(Path.Combine(pathToLog, logfile));
            Assert.IsTrue(!text.Contains("terminate"), "'terminate' appears in the OSRunTime.log");
            Assert.IsTrue(!text.Contains("Fatal"), "'Fatal' appears in the OSRunTime.log");
            Assert.IsTrue(!text.Contains("ERROR"), "ERROR appears in the OSRunTime.log");

        }
        
    }
       
}
