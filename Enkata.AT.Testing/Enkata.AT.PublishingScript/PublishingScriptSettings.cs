using System.IO;
using System.Xml;
using NUnit.Framework;

namespace Enkata.ActivityTracker.Testing.PublishingScript
{
    class PublishingScriptSettings
    {
        private const string NameConfigurationFile = "PublishingScriptSettings.xml";

        public static string ShareFolder
        {
            get { return GetParameter("ShareFolder"); }
        }

        public static string DmzFolder
        {
            get { return GetParameter("DMZFolder"); }
        }

        public static string LocationScript
        {
            get { return GetParameter("LocationScript"); }
        }

        public static string KeysForScript
        {
            get { return GetParameter("KeysForScript"); }
        }


        public static string ExtensionPacket
        {
            get { return GetParameter("ExtensionPacket"); }
        }


        public static string ExtensionLog
        {
            get { return GetParameter("ExtensionLog"); }
        }


        public static string ExtensionVideo
        {
            get { return GetParameter("ExtensionVideo"); }
        }

        public static int QuantityFiles
        {
            get { return int.Parse(GetParameter("QuantityFiles")); }
        }

        public static string GetParameter(string nameParamenter)
        {
            try
            {
                const string nameTag = "parameter";
                var content = GetContentOfXml(Path.Combine(@"C:\Set", NameConfigurationFile));
                var listTags = content.GetElementsByTagName(nameTag);
                foreach (XmlElement tag in listTags)
                {
                    if (tag.GetAttribute("name").ToLower() == nameParamenter.ToLower())
                    {
                        return tag.GetAttribute("value");
                    }
                }

            }
            catch (IOException)
            {

            }
            return "";
        }

        public static XmlDocument GetContentOfXml(string pathToXml)
        {
            //Check that xml file exist
            var xmlDoc = new XmlDocument();
            if (!File.Exists(pathToXml))
            {
                Assert.Fail("The file " + pathToXml + " was not found");
            }
            xmlDoc.Load(pathToXml);
            return xmlDoc;
        }

    }
}
