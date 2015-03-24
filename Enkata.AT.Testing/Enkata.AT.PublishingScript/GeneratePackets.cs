using System.IO;

namespace Enkata.ActivityTracker.Testing.PublishingScript
{

    public static class GeneratePackets
    {
        public static void CreateFile(int quantityOfFiles, string folder)
        {
            Directory.CreateDirectory(folder);
            for (var i = 0; i < quantityOfFiles; i++)
            {
                File.WriteAllText(
                    Path.Combine(PublishingScriptSettings.ShareFolder,
                                 RandomStr() + PublishingScriptSettings.ExtensionPacket), Content.ContentXml);
                File.WriteAllText(
                    Path.Combine(PublishingScriptSettings.ShareFolder, RandomStr() + PublishingScriptSettings.ExtensionLog),
                    Content.ContentLog);
            }
        }



        private static string RandomStr()
        {
            string rStr1 = Path.GetRandomFileName();
            rStr1 = rStr1.Replace(".", ""); // For Removing the .

            string rStr2 = Path.GetRandomFileName();
            rStr2 = rStr2.Replace(".", ""); // For Removing the .

            string rStr3 = Path.GetRandomFileName();
            rStr3 = rStr3.Replace(".", ""); // For Removing the .
        
            return rStr1 + rStr2 + rStr3;
        }
    }

}
