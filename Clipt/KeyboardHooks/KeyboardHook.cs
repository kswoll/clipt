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
            if (nCode >= 0 && message == WindowMessage.WM_KEYDOWN)
            {
                var keyboardData = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                var vkCode = keyboardData.vkCode;
                var isExtended = (keyboardData.flags & KBDLLHOOKSTRUCTFlags.LLKHF_EXTENDED) != 0;
                var isInjected = (keyboardData.flags & KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED) != 0;
                var keyData = new KeyData(vkCode, keyboardData.scanCode, isExtended, isInjected);
                KeySequenceTable.Process(keyData);
//                var vkCode = (KeyCode)Marshal.ReadInt32(lParam);

//                if (vkCode == KeyCode.Packet)
                {
                    Debug.WriteLine($"vkCode: {vkCode}, scanCode: {keyboardData.scanCode}, extendedKey: {isExtended}, injected: {isInjected}");
//                    KeySender.SendKeyPress(KeyCode.D);
//                    return new IntPtr(1);
                }
                if (vkCode == KeyCode.A)
                {
                    KeySender.SendKeyPress(KeyCode.Packet, 55356);
                    KeySender.SendKeyPress(KeyCode.Packet, 57303);
                    KeySender.SendKeyPress(KeyCode.Packet, 65039);
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
            public KeyCode vkCode;
            public uint scanCode;
            public KBDLLHOOKSTRUCTFlags flags;
            public uint time;
            public UIntPtr dwExtraInfo;
        }

        [Flags]
        private enum KBDLLHOOKSTRUCTFlags : uint
        {
            /// <summary>
            /// This will be set in the following situations:
            /// * A key on the numpad is pressed, which is the same as the equivalent number along the top
            ///   of your keyboard -- in this case the scan code and VK code will be the same, but this bit
            ///   will be set for the key on the numpad.
            /// * Use of a FN key applied to another key. This is unpredictable as it depends on the particular
            ///   keyboard and driver.
            /// </summary>
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