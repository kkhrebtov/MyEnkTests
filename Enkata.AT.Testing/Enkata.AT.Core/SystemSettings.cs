using System;
using System.Globalization;
using System.IO;
using System.Xml;
using NUnit.Framework;
using White.NUnit;
using System.Threading;
using System.Text.RegularExpressions;

namespace Enkata.ActivityTracker.Core
{
    public class SystemSettings : IDisposable
    {
        private readonly XmlDocument xmlDoc;
        private XmlNamespaceManager xmlNamespaceMgr;
        private string xmlNamespacePrefix = "ns";
        private const string _tempFile = "SystemSettingsRestore.xml";

        //TODO: resore/backup is bad pattern, I think. File should be created in place using this class and removed when it is not needed
        public SystemSettings()
        { 
            xmlDoc = new XmlDocument();
            xmlDoc.Load(SystemSettings.FilePath);
            Console.WriteLine("SS was read:" + SystemSettings.FilePath);

            var rootNode = xmlDoc.DocumentElement;
            xmlNamespaceMgr = new XmlNamespaceManager(xmlDoc.DocumentElement.OwnerDocument.NameTable);
            xmlNamespaceMgr.AddNamespace(xmlNamespacePrefix, rootNode.OwnerDocument.DocumentElement.NamespaceURI);
        }

        public int packetDuration
        {
            set { setValue("//system-settings/os-extension-settings/packet", "duration", value); }
        }

        public int packetSize
        {
            set { setValue("//system-settings/os-extension-settings/packet", "size", value); }
        }

        public int depotSize
        {
            set { setValue("//system-settings/os-extension-settings/packet", "depot-size", value); }
        }

        public string projectPath
        {
            set { setValue("//system-settings/watchdog-settings/runtime-project", "path", value); }
        }

        public int dttPollPeriod
        {
            set { setValue("//system-settings/watchdog-settings/process-management", "performance-poll-period", value); }
        }

        public int timeOut
        {
            set { setValue("//system-settings/watchdog-settings/process-management", "process-termination-timeout", value); }
        }

        public string encryptionAlgorithm
        {
            set { setValue("//system-settings/os-extension-settings/encryption", "symmetric-encryption", value); }
        }

        public int dttPolPeriod
        {
            set { setValue("//system-settings/data-transfer-settings", "poll-period", value); }
        }

        public string keyRingPath
        {
            set { setValue("//system-settings/os-extension-settings/encryption", "keyring-path", value);}
        }

        public string rawDataStoragePackets
        {
            set { setValue("//system-settings/os-extension-settings/raw-data-storage", "path", value);}
        }

        public bool enableAppFocusChangeBehavior
        {
            set {setValue("//system-settings/os-extension-settings/behaviors/app-focus-change", "enabled", value);}
        }

        public bool enableHotkeysBehavior
        {
            set { setValue("//system-settings/os-extension-settings/behaviors/hotkeys", "enabled", value);}
        }

        public bool enableIdleTimeBehavior
        {
            set {setValue("/system-settings/os-extension-settings/behaviors/idle-time", "enabled", value);}
        }

        public string getTransferItemParameter(string itemName, string parameterName)
        {
            return getStringValue(string.Format("//system-settings/data-transfer-settings/item-to-transfer[@name='{0}']", itemName), parameterName);
        }

        public void setTransferItemParameter(string itemName, string parameterName, string value)
        {
            setValue(string.Format("//system-settings/data-transfer-settings/item-to-transfer[@name='{0}']", itemName), parameterName, value);
        }

        private void setValue<T>(string XPath, string attributeName, T value)
        {
            var patchedXpath = applyNamespaceToPath(XPath);
            var listTags = xmlDoc.SelectNodes(patchedXpath, xmlNamespaceMgr);
            Assert.AreEqual(listTags.Count, 1);
            listTags[0].Attributes[attributeName].Value = value.ToString();
        }

        private string getStringValue(string XPath, string attributeName)
        {
            var patchedXpath = applyNamespaceToPath(XPath);
            var listTags = xmlDoc.SelectNodes(patchedXpath, xmlNamespaceMgr);
            Assert.AreEqual(listTags.Count, 1);
            return listTags[0].Attributes[attributeName].Value;
        }

        private string applyNamespaceToPath(string xPath)
        {
            return Regex.Replace(xPath, @"(?<=/|^)(?=\w)(?!\w+:)", xmlNamespacePrefix + ":");
        }

        //TODO: "wait for file" functionality must be removed from this method
        //TODO: this method must be moved outside of this class
        public static XmlDocument GetContentOfXml(string pathToXml)
        {
            Console.WriteLine("Get Content of XML: " + pathToXml);
            
            //Check that xml file exist
            var xmlDoc = new XmlDocument();
            
            for (int count = 1; count < 5; count++)
            {
                if (!File.Exists(pathToXml))
                {
                    Console.WriteLine(pathToXml + " NOT FOUND. Waiting " + count.ToString());
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("Load XML File: " + pathToXml);
                    xmlDoc.Load(pathToXml);
                    return xmlDoc;
                }
            }
            //Assert.Fail("The file " + pathToXml + " was not found");
            Console.WriteLine(pathToXml + " NOT FOUND");
            Verify.Fail("The file " + pathToXml + " was not found");
            return xmlDoc;
        }

        public void Dispose()
        {
            xmlDoc.Save(SystemSettings.FilePath);
        }

        public static void StoreSystemSettings()
        {
            File.Copy(SystemSettings.FilePath, Path.Combine(NunitSettings.InstallFileLocation, _tempFile), true);
        }

        public static void DeleteSystemSettings()
        {
            File.Delete(SystemSettings.FilePath);
        }
        
        public static void RestoreSystemSetingsFile()
        {
            Console.WriteLine("Restore SystemSettings file from backup");
            File.Copy(Path.Combine(NunitSettings.InstallFileLocation, _tempFile), SystemSettings.FilePath, true);
        }

        public static string FilePath
        {
            get { return Path.Combine(NunitSettings.InstallFileLocation, NunitSettings.SystemSettingsNameFile); }
        }

    }
}
