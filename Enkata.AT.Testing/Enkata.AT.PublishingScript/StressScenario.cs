using System;
using System.Globalization;
using System.IO;
using System.Text;


namespace Enkata.ActivityTracker.Testing.PublishingScript
{
    public class StressScenario
    {
        private static void Main(string[] args)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < 11; i++ )
            {
                Console.WriteLine("Generate data.");
                GeneratePackets.CreateFile(PublishingScriptSettings.QuantityFiles, PublishingScriptSettings.ShareFolder);

                Console.WriteLine("Run publishing script with parameters.");
                int start = Environment.TickCount;
                ExecuteCommandCmd("powershell " + PublishingScriptSettings.LocationScript + "\\publishing.ps1 " +
                                  PublishingScriptSettings.KeysForScript);
                float duration = Environment.TickCount - start;
                float sec = duration / 1000;
                float minutes = sec / 60;

                Console.WriteLine("Rite time to file.");
                sb.AppendLine("Publishing script duration_" + i + ": " + minutes.ToString(CultureInfo.InvariantCulture) + "min.");
            }
            using (var outfile =
                   new StreamWriter(@"c:\Set\timer.txt"))
            {
                outfile.Write(sb.ToString());
            }

            Console.WriteLine("End Script.");
        }



        public static string ExecuteCommandCmd(string command)
        {
            var cmd = new System.Diagnostics.Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput =
                true;
            cmd.StartInfo.RedirectStandardOutput =
                true;
            cmd.StartInfo.LoadUserProfile = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            /* execute "dir" */
            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.WriteLine("Y");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();

            var result = cmd.StandardOutput.ReadToEnd();
            cmd.Close();
            return result;
        }
    }
}
