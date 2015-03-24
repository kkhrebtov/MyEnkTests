using System.Threading;
using NUnit.Framework;
using Enkata.ActivityTracker.Core;

namespace Enkata.ActivityTracker.Acceptance.StressTesting
{
    [TestFixture]
    public class StressScenario
    {

        [Test]
        public void DataPrepare()
        {
            var test1 = new ThreadTest();
            test1.CreateData();
        }

        [Test]
        public void RunUsersWithDelay()
        {
            Thread.Sleep(10000);
            var test1 = new ThreadTest();
            test1.CopyToShareFolder();
        }

        [Test]
        public void RunCopyInTheSameTime()
        {
            for (int i = 1; i < NunitSettings.Users(); i++)
            {
                var myJob = new ThreadTest();
                myJob.counter = i;
                Thread thread = new Thread(new ThreadStart(myJob.CopyOneFile));
                thread.Name = "first";
                thread.Start();

            }
            Thread.Sleep(10000000);
        }

    }
}
