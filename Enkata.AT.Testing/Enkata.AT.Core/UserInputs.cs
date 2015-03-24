using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading;
using White.Core.InputDevices;
using White.Core.WindowsAPI;


namespace Enkata.ActivityTracker.Core
{
    public static class UserInputs
    {
        private const int DelayAfterKeyEvent = 1000;
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MouseClickDelay = 2000;

        public const int VkAlt = (int)VirtualKeys.Menu;// 0x12;
        public const int KeyeventfKeyup = 0x2;
        public const int VkWinL = (int)VirtualKeys.LeftWindows;//0x5B;
        public const int VkL = (int)VirtualKeys.L;//0x4C;
        public const int VkF1 = (int)VirtualKeys.F1;
        public const int VkF2 = (int)VirtualKeys.F2;//0x70;
        public const int VkF4 = (int)VirtualKeys.F4;//0x73;
        public const int VkS = (int)VirtualKeys.S;//0x53;
        public const int VkEnter = (int)VirtualKeys.Return;

        const int KEYEVENTF_EXTENDEDKEY = 0x1;
        const int KEYEVENTF_KEYUP = 0x2;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags);
                     
        public static void PressF1()
        {
            // Simulate a key press
            keybd_event(VkF1, 0x45, KEYEVENTF_EXTENDEDKEY | 0, 0);
            // Simulate a key release
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(VkF1, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent + 1000);
        }

        public static void PressF2()
        {
            // Simulate a key press
            keybd_event(VkF2, 0x45, KEYEVENTF_EXTENDEDKEY | 0, 0);
            // Simulate a key release
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(VkF2, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent + 1000);
        }

        public static void PressHotKey(byte key1, byte key2)
        {
            keybd_event(key1, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(key2, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(key1, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(key2, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent);
        }
        
        public static void PressEnter()
        {
            // Simulate a key press
            keybd_event(VkEnter, 0x45, KEYEVENTF_EXTENDEDKEY | 0, 0);
            // Simulate a key release
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(VkEnter, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent + 1000);
        }

        public static void PressKey(byte key1)
        {
            // Simulate a key press
            keybd_event(key1, 0x45, KEYEVENTF_EXTENDEDKEY | 0, 0);
            // Simulate a key release
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(key1, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent + 1000);
        }
        
        public static void PressAltS()
        {
            keybd_event(VkAlt, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(VkS, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(VkAlt, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(VkS, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent);
        }

        public static void PressAltF4()
        {
            keybd_event(VkAlt, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(VkF4, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(VkF4, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent);
            keybd_event(VkAlt, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            Thread.Sleep(DelayAfterKeyEvent);
        }
        
        public static void HotKey(KeyboardInput.SpecialKeys key1, string key2)
        {
            Console.WriteLine("Press Hotkey:" + key1.ToString() + "+" + key2);
            Keyboard.Instance.HoldKey(key1);
            Thread.Sleep(1000);
            Keyboard.Instance.Enter(key2);
            Thread.Sleep(1000);
            Keyboard.Instance.LeaveAllKeys();
            Thread.Sleep(1000);
        }

        public static void HotKey(KeyboardInput.SpecialKeys key1, KeyboardInput.SpecialKeys key2)
        {
            Console.WriteLine("Press Hotkey:" + key1.ToString() + "+" + key2.ToString());
            Keyboard.Instance.HoldKey(key1);
            Thread.Sleep(1000);
            Keyboard.Instance.PressSpecialKey(key2);
            Thread.Sleep(1000);
            Keyboard.Instance.LeaveAllKeys();
            Thread.Sleep(1000);
        }

        public static void PressHotKeyWithoutDelay(byte key1, byte key2)
        {
            keybd_event(key1, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(key2, 0x45, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(key1, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            keybd_event(key2, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }

        public static void Enter(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Keyboard.Instance.Enter(input.Substring(i, 1));
                Thread.Sleep(100);
            }

        }

        [DllImport("user32")]
        private static extern void mouse_event(Int32 dwFlags, Int32 dx, Int32 dy, Int32 cbuttons, Int32 dwExtraInfo);

        private const Int32 MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const Int32 MOUSEEVENTF_MIDDLEDOWN = 0x20;
        private const Int32 MOUSEEVENTF_MIDDLEUP = 0x40;
        private const Int32 MOUSEEVENTF_MOVE = 0x1;
        private const Int32 MOUSEEVENTF_RIGHTDOWN = 0x8;
        private const Int32 MOUSEEVENTF_RIGHTUP = 0x10;

        public static void MoveMouse(Int32 MickyX, Int32 MickyY)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, MickyX, MickyY, 0, 0);
        }
             
        
    }
}
