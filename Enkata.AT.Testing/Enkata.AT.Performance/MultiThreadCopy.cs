using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace Enkata.ActivityTracker.Testing.Performance
{
    public static class MultiThreadCopy
    {
        public static void CopyToShareFolder()
        {
            for (var i = 1; i < PerformanceSettings.QuantityCopyFilesInOneThread; i++)
            {
                Thread.Sleep(PerformanceSettings.TimeOut);
                try
                {
                    var namePacket = RandomStr() + "-" + RandomStr() + "-" + RandomStr() + ".packet";
                    var nameFile = GeneratePackets.CreateFile(namePacket, Content.ContentXML, PerformanceSettings.From);
                    if (File.Exists(nameFile))
                    {
                        File.Copy(nameFile, Path.Combine(PerformanceSettings.ShareFolder, namePacket), true);
                        File.Delete(nameFile);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        private static string RandomStr()
        {
            string rStr = Path.GetRandomFileName();
            rStr = rStr.Replace(".", ""); // For Removing the .
            return rStr;
        }

    }
}
