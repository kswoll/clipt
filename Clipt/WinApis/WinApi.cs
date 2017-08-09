using System;
using System.Runtime.InteropServices;
using Clipt.Keyboards;

namespace Clipt.WinApis
{
    public class WinApi
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern KeyStateShort GetKeyState(KeyCode key);

        [DllImport("user32.dll")]
        internal static extern bool GetKeyboardState(KeyStateByte[] keyState);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool RegisterHotKey([In] IntPtr hWnd, [In] int id, [In] ModifierKeys fsModifiers, [In] uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnregisterHotKey([In] IntPtr hWnd, [In] int id);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SendInput(uint numberOfInputs, Input[] inputs, int sizeOfInputStructure);

        [DllImport("user32.dll")]
        internal static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelHookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern VkKeyScanExResult VkKeyScan(char ch);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern VkKeyScanExResult VkKeyScanEx(char ch, IntPtr dwhkl);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetKeyboardLayout(int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MapVirtualKey(uint key, MapVirtualKeyType mapType);
    }
}