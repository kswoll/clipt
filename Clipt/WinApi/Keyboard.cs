using System;
using System.Runtime.InteropServices;
using Clipt.Keyboards;

namespace Clipt.WinApi
{
    public static class Keyboard
    {
        [DllImport("user32.dll")]
        public static extern KeyStateShort GetKeyState(KeyCode key);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(KeyStateByte[] keyState);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey([In] IntPtr hWnd, [In] int id, [In] ModifierKeys fsModifiers, [In] uint vk);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey([In] IntPtr hWnd, [In] int id);
    }
}