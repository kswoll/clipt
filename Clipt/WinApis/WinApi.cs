using System;
using System.Runtime.InteropServices;
using Clipt.Apis;

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
        internal static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);
    }
}