using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Enkata.ActivityTracker.Testing.Performance
{
    public static class GeneratePackets
    {

        public static string CreateFile(string name, string content, string folder)
        {
            Directory.CreateDirectory(folder);
            var fileName = Path.Combine(folder, name);
            File.WriteAllText(fileName, content);
            return fileName;
        }
    }
}
