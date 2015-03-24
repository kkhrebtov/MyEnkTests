using System;
using System.Globalization;
using System.ServiceProcess;
using System.Threading;
using NUnit.Framework;
//using System.Windows.Forms;

namespace Enkata.ActivityTracker.Core
{
    public static class Service
    {

        public static bool IsStarted(string serviceName)
        {
            if (!Exists(serviceName)) Assert.Fail("Couldn't find service:" + serviceName);
            return GetService(serviceName).Status == ServiceControllerStatus.Running;
        }

        public static bool Exists(string nameService)
        {

            //MessageBox.Show(nameService);
            return GetService(nameService).DisplayName != null;
        }

        private static ServiceController GetService(string serviceName)
        {
            foreach (var service in ServiceController.GetServices())
            {
                
                if (service.ServiceName.Contains(serviceName.ToString(CultureInfo.InvariantCulture))) return service;
            }
            
            return null;
        }

        public static void Start(string serviceName)
        {
            var serv = GetService(serviceName);

          // Console.WriteLine("Service " + serviceName + " isStarted: " + Service.IsStarted(serviceName).ToString());
            if (Service.IsStarted(serviceName) == false)
            {
                serv.Refresh();
                serv.Start();
                WaitServiceStop(serviceName, ServiceControllerStatus.Running);
                Console.WriteLine("Service was started: " + serviceName); 
            }
            else 
            {
                serv.Stop();
                WaitServiceStop(serviceName, ServiceControllerStatus.Stopped);
                serv.Refresh();
                serv.Start();
                WaitServiceStop(serviceName, ServiceControllerStatus.Running);
            };
        }

        public static void Stop(string serviceName)
        {
            var serv = GetService(serviceName);
            TimeSpan timeout = TimeSpan.FromMilliseconds(60000);

            if (Service.IsStarted(serviceName))
            {
                serv.Stop();
                serv.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                Console.WriteLine("Service was stopped: " + serviceName);
            }
        }


        public static void ReStart(string serviceName, int timeoutMilliseconds)
        {
            var service = new ServiceController(serviceName);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch
            {
                // ...
            }
        }


        private static void WaitServiceStop(string serviceName, ServiceControllerStatus state)
        {
            for (var i = 0; i < 100; i++)
            {
                Thread.Sleep(2000);
                if (GetService(serviceName).Status.Equals(state))
                {
                    break;
                }
            }

        }
    }
}
