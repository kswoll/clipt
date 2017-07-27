using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Clipt.WinApi;

namespace Clipt.KeyboardHooks
{
    public class KeyboardHook
    {
        private static readonly LowLevelKeyboardProc proc = HookCallback;

        private static IntPtr hookId;

        public static void Hook()
        {
            hookId = SetHook(proc);
        }

        public static void Unhook()
        {
            UnhookWindowsHookEx(hookId);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx((int)HookType.WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// Return 1 to eat the keystroke.
        /// </summary>
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var message = (WindowMessage)wParam;
            if (nCode >= 0/* && wParam == (IntPtr)WindowMessage.WM_KEYDOWN*/)
            {
                var kbd = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                var vkCode = (KeyCode)kbd.vkCode;
//                var vkCode = (KeyCode)Marshal.ReadInt32(lParam);

                if (vkCode == KeyCode.A)
                {
                    SendKey.SendKeyPress(KeyCode.D);
                    return new IntPtr(1);
                }
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public KBDLLHOOKSTRUCTFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [Flags]
        private enum KBDLLHOOKSTRUCTFlags : uint
        {
            LLKHF_EXTENDED = 0x01,

            /// <summary>
            /// If this is present, it was not a keystroke that naturally occurred -- something else injected it.  If you're
            /// doing something like remapping keys, you should probably ignore events when this flag is present.
            /// </summary>
            LLKHF_INJECTED = 0x10,

            LLKHF_ALTDOWN = 0x20,
            LLKHF_UP = 0x80,
        }
    }
}