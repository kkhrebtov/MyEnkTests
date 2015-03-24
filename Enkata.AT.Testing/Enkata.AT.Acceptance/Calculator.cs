using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace Enkata.ActivityTracker.Acceptance
{
    public class Calculator
    {

        private readonly System.Diagnostics.Process _proc;
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MouseClickDelay = 1000;
        public const uint GW_CHILD = 5;
        public const uint GW_HWNDNEXT = 2;

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "GetWindow")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);


        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, int dwData, int dwExtraInf);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);
        
        public struct Rect
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }


        public Calculator(string path)
        {

            _proc = new System.Diagnostics.Process();
            _proc.StartInfo.FileName = path;
            //var minor = Environment.OSVersion.Version.Minor;
            // if (minor == 5)
            //{
            _proc.Start();
            // }
            // else
            // {
            //var calcWindow = FindWindowByCaption(IntPtr.Zero, "Start");
            //ClickOnButton(calcWindow);
            //Thread.Sleep(MouseClickDelay);
            //SendKeys.SendWait("calc");
            //Thread.Sleep(MouseClickDelay);
            //UserInputs.PressEnter();
            // }

        }

        private static Point CenterPoint(Rect rect)
        {
            return new Point((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        public void ClickOnButton(string nameButton)
        {

            //var minor = Environment.OSVersion.Version.Minor;
            //if (minor == 5)
            //{
            var calcWindow = FindWindowByCaption(IntPtr.Zero, "Calculator");
            var buttonWindow = FindWindowEx(calcWindow, IntPtr.Zero, "Button", nameButton);
            Rect tempRect;
            GetWindowRect(buttonWindow, out tempRect);
            Cursor.Position = CenterPoint(tempRect);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//make left button down 
            Thread.Sleep(MouseClickDelay);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//make left button up
            Thread.Sleep(MouseClickDelay);
            //}
            //else
            //{
            //    ClickOnButtonWin7(nameButton);
            //}


        }

        private void ClickOnButtonWin7(string nameButton)
        {
            var calcWindow = FindWindowByCaption(IntPtr.Zero, "Calculator");
            var frameWindow = FindWindowEx(calcWindow, IntPtr.Zero, "CalcFrame", "");
            var f = GetWindow(frameWindow, GW_CHILD);
            var f1 = GetWindow(f, GW_HWNDNEXT);
            var f2 = GetWindow(f1, GW_HWNDNEXT);
            var buttonWindow = FindWindowEx(f2, IntPtr.Zero, "Button", "");
            var windowNext = buttonWindow;
            var number = 0;
            if (nameButton == "1")
            {
                number = 4;
            }
            else if (nameButton == "+")
            {
                number = 22;
            }
            else if (nameButton == "-")
            {
                number = 21;
            }

            else if (nameButton == "2")
            {
                number = 10;
            }

            else if (nameButton == "3")
            {
                number = 15;
            }

            for (var i = 0; i < number; i++)
            {
                windowNext = GetWindow(windowNext, GW_HWNDNEXT);
            }

            ClickOnButton(windowNext);
        }

        private void ClickOnButton(IntPtr buttonWindow)
        {
            Rect tempRect;
            GetWindowRect(buttonWindow, out tempRect);
            Cursor.Position = CenterPoint(tempRect);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//make left button down 
            Thread.Sleep(MouseClickDelay);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//make left button up
            Thread.Sleep(MouseClickDelay + 2000);
        }

        //
        public static void ClickOnButtonByName(string nameWindow, string nameButtonInWindow)
        {
            var window = FindWindowByCaption(IntPtr.Zero, nameWindow);
            var buttonWindow = FindWindowEx(window, IntPtr.Zero, "Button", nameButtonInWindow);
            Calculator.Rect tempRect;
            GetWindowRect(buttonWindow, out tempRect);
            Cursor.Position = CenterPoint(tempRect);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);//make left button down 
            Thread.Sleep(MouseClickDelay);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);//make left button up
            Thread.Sleep(MouseClickDelay + 2000);
        }

       

        public void Close()
        {
            _proc.CloseMainWindow();
        }
    }
}
