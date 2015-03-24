using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using Enkata.ActivityTracker.Core;

namespace Enkata.ActivityTracker.Acceptance
{
    [TestFixture]
    public class Stability
    {
        public static int DelayHotKey = 2000;
        public static int DelayRunCalc = 5000;
        public static int DelayRecording = 65000;
        public static Calculator Calc;
        //private static string _pathToBeReplace = "c:\\";


        [Test]
        public void Stable()
        {
            Preparation();
            InstallAt();
           
                CheckDtt();
                Assert.IsTrue(AT.WatchdogExists());
                Assert.IsTrue(AT.DttExists());
                Assert.IsTrue(AT.OsrExists());
                 for (var i = 0; i < 2000; i++)
                {
                    Calc = new Calculator(NunitSettings.CalcLocation);
                    Thread.Sleep(DelayRunCalc);
                    UserInputs.PressF1();
                    Thread.Sleep(DelayHotKey);
                    ////Call search in help window
                    UserInputs.PressAltS();
                    Thread.Sleep(DelayHotKey);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.A);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.C);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.F);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.G);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.N);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.O);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.P);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.S);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.V);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.X);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.Z);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.Shift);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.Escape);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.F6);
                    UserInputs.PressHotKey((int)VirtualKeys.LaunchApplication1, (int)VirtualKeys.F6);
                    UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.F2);
                    UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.F1);
                    UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.D);
                    UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.Space);
                    UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.R);
                    UserInputs.PressHotKey((int)VirtualKeys.LeftWindows, (int)VirtualKeys.U);
                    UserInputs.PressHotKey((int)VirtualKeys.Shift, (int)VirtualKeys.Tab);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.Tab);
                    UserInputs.PressHotKey((int)VirtualKeys.Control, (int)VirtualKeys.H);
                    UserInputs.PressHotKey((int)VirtualKeys.Shift, (int)VirtualKeys.I);

                  }


        }
        private void Preparation()
        {
            RestoreSystemSettings();
            //Check calc
            Assert.IsTrue(File.Exists(NunitSettings.CalcLocation), "Couldn't find calculator for Xp OS.");
            //Check install file
            Assert.IsTrue(File.Exists(Path.Combine(NunitSettings.InstallFileLocation, "ActivityTracker.msi")), "Couldn't find installation file.");
            //Check decrypt files
            Assert.IsTrue(File.Exists(Path.Combine(NunitSettings.InstallFileLocation, "pass.txt")), "Couldn't find decrypt file: pass.txt");
            Assert.IsTrue(File.Exists(Path.Combine(NunitSettings.InstallFileLocation, "gpg.exe")), "Couldn't find decrypt file: gpg.exe");
            Assert.IsTrue(File.Exists(Path.Combine(NunitSettings.InstallFileLocation, "pubring.gpg")), "Couldn't find decrypt file: pubring.gpg");
            Assert.IsTrue(File.Exists(Path.Combine(NunitSettings.InstallFileLocation, "secring.gpg")), "Couldn't find decrypt file: secring.gpg");
            Assert.IsTrue(File.Exists(Path.Combine(NunitSettings.InstallFileLocation, "iconv.dll")), "Couldn't find decrypt file: iconv.dll");
        }

        private void RestoreSystemSettings()
        {
            using (var systemSettings = new SystemSettings())
            {
                systemSettings.setTransferItemParameter("packets", "dest", Path.Combine(NunitSettings.ShareFolder, @"DataOutput\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.CREATION.DATE%.%FILE.CREATION.TIME%.%FILE%"));
                systemSettings.setTransferItemParameter("OS log information", "dest", Path.Combine(NunitSettings.ShareFolder, @"%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.MODIFICATION.DATE%.%FILE.MODIFICATION.TIME%.os_runtime.log"));
                systemSettings.setTransferItemParameter("log information", "dest", Path.Combine(NunitSettings.ShareFolder, @"DataOutput\%FILE.CREATION.DATE%\LOGS\%HASHED_ID_SHA2%.%SESSION_ID%.%FILE.MODIFICATION.DATE%.%FILE.MODIFICATION.TIME%.%FILE%"));
                //Set project path
                systemSettings.projectPath = NunitSettings.ProjectPath;
                //Set value packet-duration = 20
                systemSettings.packetDuration = 1800;
                //data-transfer-settings poll-period = ‘20’
                systemSettings.dttPollPeriod = 20;
            }
        }

        private static void InstallAt()
        {
            AT.Install();
            Assert.IsTrue(AT.WatchdogExists());
            Assert.IsTrue(AT.DttExists());
            Assert.IsTrue(AT.OsrExists());
        }

        private static void CheckDtt()
        {
            Calc = new Calculator(NunitSettings.CalcLocation);
            Thread.Sleep(DelayRunCalc);
            Calc.ClickOnButton("1");
            Thread.Sleep(DelayHotKey);
            Calc.ClickOnButton("-");
            //Call help window
            UserInputs.PressF1();
            Thread.Sleep(DelayHotKey);
            //Call search in help window
            UserInputs.PressAltS();
            Thread.Sleep(DelayHotKey);
            //Close Help window
            UserInputs.PressAltF4();
            Thread.Sleep(DelayRecording);
            Calc.ClickOnButton("2");
            Calc.ClickOnButton("3");
            Calc.ClickOnButton("+");

            Thread.Sleep(DelayRecording);
            try
            {
                Calc.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Thread.Sleep(DelayRunCalc);
        }



    }
}
