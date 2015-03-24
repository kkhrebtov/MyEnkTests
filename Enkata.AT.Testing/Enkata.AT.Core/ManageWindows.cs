using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using White.Core;
using White.Core.UIItems.WindowItems;

namespace Enkata.ActivityTracker.Core
{
    public class ManageWindows
    {

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "GetWindow")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        public static void CloseWindow(String WindowName)
        {
            Console.WriteLine("Call CloseWindow: " + WindowName);
            List<Window> allItem = Desktop.Instance.Windows();
            foreach (Window element in allItem)
            {
                if (element.Name.Contains(WindowName))
                {
                    Console.WriteLine("Window " + WindowName + " found. Closing");
                    element.Close();
                    break;
                }
            }
        }

        public static Window GetWindow(String name)
        {

            List<Window> allItem = Desktop.Instance.Windows();
            foreach (Window element in allItem)
            {
                Console.WriteLine("Window Name: " + element.Name);
                if (element.Name.Contains(name))
                {
                    return element;
                }
            }

            return null;
        }

        public static bool WindowExists(string nameWindow, string elementInWindow)
        {
            var enkataWindow = FindWindowByCaption(IntPtr.Zero, nameWindow);
            if (FindWindowEx(enkataWindow, IntPtr.Zero, "Static", elementInWindow) != IntPtr.Zero)
                return true;

            if (Environment.OSVersion.Version.Major != 5)
            {
                UserInputs.PressEnter();
                return enkataWindow != IntPtr.Zero;
            }

            return false;
        }




    }
}
