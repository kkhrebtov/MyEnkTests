using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using NUnit.Framework;

namespace Enkata.ActivityTracker.Testing.Performance
{
    class PerformanceSettings
    {

        private const string NameConfigurationFile = "PerofmanceSettings.xml";

        public static int QuantityOfFiles
        {
            get { return int.Parse(GetParameter("QuantityOfFiles")); }
        }

        public static int TimeOut
        {
            get { return int.Parse(GetParameter("TimeOut")); }
        }

        public static string ShareFolder
        {
            get { return GetParameter("ShareFolder"); }
        }

        public static string From
        {
            get { return GetParameter("From"); }
        }

        public static int QuantityConnection
        {
            get { return int.Parse(GetParameter("QuanityConnection")); }
        }

        public static int QuantityCopyFilesInOneThread
        {
            get { return int.Parse(GetParameter("QuantityCopyFilesInOneThread")); }
        }

        public static int DelayBetweenThread
        {
            get { return int.Parse(GetParameter("DelayBetweenThread")); }
        }

        public static string NameFileForMachin
        {
            get
            {
                if (Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").Contains("64"))
                {
                    return GetParameter("NameFileForMachinWin764");
                }
                else if (!Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").Contains("64") && Environment.OSVersion.Version.Major != 5)
                {
                    return GetParameter("NameFileForMachinWin732");
                }
                else
                {
                    return GetParameter("NameFileForMachinXP");
                }               
            }
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
