using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;
using Enkata.ActivityTracker.Core;


namespace Enkata.ActivityTracker.Acceptance.StressTesting
{
    internal class ThreadTest
    {
        public void CreateData()
        {
            for (var i = 1; i < NunitSettings.Users(); i++)
            {
                Thread.Sleep(NunitSettings.Delay());
                File.Copy(NunitSettings.From + "ActivityTracker.msi", NunitSettings.To + i + ".msi", true);
            }
        }

        public void CopyToShareFolder()
        {
            for (var i = 1; i < NunitSettings.Users(); i++)
            {
                Thread.Sleep(NunitSettings.Delay());
                File.Copy(NunitSettings.From + "ActivityTracker" + i + ".msi", NunitSettings.To + i + ".xml", true);
            }
        }

        public int counter = 0;

        public void CopyOneFile()
        {
            File.Copy(NunitSettings.From + "ActivityTracker" + counter + ".msi", NunitSettings.To + counter + ".xml",
                      true);
        }


        public void InstallMsi64()
        {
            AT.InstallMsiFile(NunitSettings.InstallFileLocation, "ActivityTracker.msi");
        }

        public void InstallMsi32()
        {
            Program.ExecuteCommandCmd(Path.Combine(NunitSettings.InstallFileLocation,"ActivityTracker64.msi"));
        }



    }
}
