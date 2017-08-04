using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Clipt.KeyboardHooks;
using Clipt.WinApi;

namespace Clipt.MessageHooks
{
    public class MessageHook
    {
        private static readonly GetMessageProc proc = HookCallback;

        private static IntPtr hookId;

        public static void Hook()
        {
            hookId = SetHook(proc);
        }

        public static void Unhook()
        {
            UnhookWindowsHookEx(hookId);
        }

        private static IntPtr SetHook(GetMessageProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx((int)HookType.WH_GETMESSAGE, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// Return 1 to eat the keystroke.
        /// </summary>
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var message = (WindowMessage)wParam;
            Debug.WriteLine(message);
/*
            if (nCode >= 0 && message == WindowMessage.WM_KEYDOWN)
            {
                var keyboardData = (KeyboardHook.KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KeyboardHook.KBDLLHOOKSTRUCT));
                var vkCode = keyboardData.vkCode;
                var isExtended = (keyboardData.flags & KeyboardHook.KBDLLHOOKSTRUCTFlags.LLKHF_EXTENDED) != 0;
                var isInjected = (keyboardData.flags & KeyboardHook.KBDLLHOOKSTRUCTFlags.LLKHF_INJECTED) != 0;
                var keyData = new KeyData(vkCode, keyboardData.scanCode, isExtended, isInjected);
                //                KeySequenceProcessor.Process(keyData);
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
*/

            return CallNextHookEx(hookId, nCode, wParam, lParam);
        }

        private delegate IntPtr GetMessageProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, GetMessageProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}