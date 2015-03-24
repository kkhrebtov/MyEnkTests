using System;
using System.IO;
using System.Xml;
using System.Reflection;
//using System.Windows.Forms;

namespace Enkata.ActivityTracker.Core
{
    public static class NunitSettings
    {
        private const string NameConfigurationFile = "nunitSettings.xml";
        private static XmlDocument xmlDoc = new XmlDocument();

        static NunitSettings()
        {
            string assemblyPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            string path = Path.Combine(Path.GetDirectoryName(assemblyPath), NameConfigurationFile);
            //string path = new Uri(uriPath).LocalPath;
            Console.WriteLine("Read nunit settings from: " + path);
            xmlDoc.Load(path);
        }

        public static string InstallFileLocation
        {
            get { return GetParameter("LocationInstalFile"); }
        }

        public static string LocationInReg
        {
            get
            {
                var environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                if (environmentVariable != null && environmentVariable.Contains("64"))
                    return "SOFTWARE\\Wow6432Node\\Enkata\\Activity Tracker";
                return "SOFTWARE\\Enkata\\Activity Tracker";
            }
        }

        public static string CalcLocation
        {
            get { return GetParameter("LocationCalc"); }
        }

        public static string DttPath
        {
            get { return GetParameter("DttPath"); }
        }

        public static string TempFolder
        {
            get { return GetParameter("TempFolder"); }
        }

        public static string BackupFolder
        {
            get { return GetParameter("BackupFolder"); }
        }

        public static string SystemSettingsNameFile
        {
            get { return GetParameter("NameSystemSettings"); }
        }

        public static string ServiceWdName
        {
            get { return "Activity Tracker Watchdog"; }
        }

        public static string ServiceDttName
        {
            get { return "Activity Tracker Data Transfer Tool"; }
        }

        public static string OsRunTimeName
        {
            get
            {
                var architecture = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                if (architecture != null && architecture.Contains("64"))
                {
                    return "OpenSpan.Runtime32";
                }
                return "OpenSpan.Runtime";
            }
        }

        public static string ProjectPath
        {
            get { return GetParameter("ProjectPath"); }
        }

        public static string InstallFileName
        {
            get
            {
                var environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                if (environmentVariable != null && environmentVariable.Contains("64")) return "ActivityTracker64.msi";
                return "ActivityTracker.msi";
            }
        }

        public static string From
        {
            get { return GetParameter("From"); }
        }

        public static string To
        {
            get { return GetParameter("To"); }
        }

        public static string ShareFolder
        {
            get { return GetParameter("ShareFolder"); }
        }

        public static string OpenSpanService
        {
            get
            {
                var environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                if (environmentVariable != null && environmentVariable.Contains("64")) return "OpenSpan.Services";
                return "OpenSpan.Services";
            }
        }
        public static string OpenSpanDriverService
        {
            get
            {
                var environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");
                if (environmentVariable != null && environmentVariable.Contains("64")) return "OpenSpan.DriverService64";
                return "OpenSpan.DriverService32";
            }
        }

        public static string OperatorId
        {
            get { return GetParameter("OperatorId"); }

        }

        public static string InternalId
        {
            get { return GetParameter("InternalId"); }

        }

        public static string InternalPwd
        {
            get { return GetParameter("InternalPwd"); }

        }


        public static string PasswordId
        {
            get { return GetParameter("PasswordId"); }
        }

        public static string PaymentId
        {
            get { return GetParameter("PaymentId"); }
        }

        public static string PasswordPay
        {
            get { return GetParameter("PasswordPay"); }
        }

        public static string ResearchId
        {
            get { return GetParameter("ResearchId"); }
        }

        public static string PasswordRes
        {
            get { return GetParameter("PasswordRes"); }
        }

        public static string DocumentNumber {
            get { return GetParameter("DocumentNumber"); }
        }

        public static string SN { 
            get { return GetParameter("SN"); } 
        }

        private static string GetParameter(string nameParamenter)
        {
            try
            {
                const string nameTag = "parameter";
                var listTags = xmlDoc.GetElementsByTagName(nameTag);
                foreach (XmlElement tag in listTags)
                {
                    if (tag.GetAttribute("name").ToLower() == nameParamenter.ToLower())
                    {
                        return tag.GetAttribute("value");
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return "";
        }

        public static int Delay()
        {
            return Convert.ToInt32(GetParameter("Delay"));
        }

        public static decimal Users()
        {
            return Convert.ToInt32(GetParameter("Users"));
        }

        public static int QuantityPackets
        {
            get { return Convert.ToInt32(GetParameter("QuantityPackets")); }
        }
    }
}
