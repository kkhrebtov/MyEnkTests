using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using Enkata.ActivityTracker.Core;

namespace Enkata.ActivityTracker.Testing.Performance
{
    class Scenario
    {
        public static void Main(string[] args) {
            Console.WriteLine("Install AT");
            cleanMachine();
            AT.Install();
            Thread.Sleep(10000);

            Console.WriteLine("RunScript");
            for (int i = 1; i < PerformanceSettings.QuantityConnection; i++)
            {
                Thread.Sleep(PerformanceSettings.DelayBetweenThread);
                Thread thread = new Thread(new ThreadStart(MultiThreadCopy.CopyToShareFolder));
                thread.Name = "first" + i;
                thread.Start();
                Console.WriteLine("Thread " + i + " is started!");
            }
            Console.WriteLine("ALL " + PerformanceSettings.QuantityConnection + "connection is running. Start measure data on machine with share folder!");
            Thread.Sleep(5000000);

            Console.WriteLine("Clean Machine");
            cleanMachine(); 
        }


        private static void cleanMachine() {
            try
            {
                Directory.Delete(PerformanceSettings.From, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }        
        }
    
    
    
    
    }



}
