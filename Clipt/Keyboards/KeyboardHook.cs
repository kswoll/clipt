using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Clipt.WinApis;

namespace Clipt.Keyboards
{
    public class KeyboardHook
    {
        private static readonly LowLevelKeyboardProc proc = HookCallback;
        private static readonly KeyStateByte[] keyPressedState = new KeyStateByte[256];

        private static IntPtr hookId;

        public static void Hook()
        {
            hookId = SetHook(proc);
            if (hookId == null)
            {
                WinApi.GetKeyboardState(keyPressedState);
            }
        }

        public static bool IsKeyPressed(KeyCode key)
        {
            return keyPressedState[(byte)key] == KeyStateByte.Pressed;
        }

        public static bool IsKeyToggled(KeyCode key)
        {
            return keyPressedState[(byte)key] == KeyStateByte.Toggled;
        }

        public static void Unhook()
        {
            WinApi.UnhookWindowsHookEx(hookId);
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return WinApi.SetWindowsHookEx((int)HookType.WH_KEYBOARD_LL, proc, WinApi.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// Return 1 to eat the keystroke.
        /// </summary>
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            var message = (WindowMessage)wParam;

            // Update keyPressedState
            var keyboardData = (KeyboardLowLevelHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardLowLevelHookStruct));
            var keyCode = keyboardData.vkCode;
            switch (message)
            {
                case WindowMessage.WM_KEYDOWN:
                case WindowMessage.WM_SYSKEYDOWN:
                case WindowMessage.WM_IME_KEYDOWN:
                    keyPressedState[(byte)keyCode] = KeyStateByte.Pressed;
                    break;
                case WindowMessage.WM_KEYUP:
                case WindowMessage.WM_IME_KEYUP:
                case WindowMessage.WM_SYSKEYUP:
                    keyPressedState[(byte)keyCode] = KeyStateByte.None;
                    break;
            }

            bool isShiftDown = IsKeyPressed(KeyCode.LeftShift) || IsKeyPressed(KeyCode.RightShift);
            var isExtended = (keyboardData.flags & KeyboardLowLevelHookStructFlags.LLKHF_EXTENDED) != 0;
            var isInjected = (keyboardData.flags & KeyboardLowLevelHookStructFlags.LLKHF_INJECTED) != 0;
            var isKeyDown = message == WindowMessage.WM_KEYDOWN || message == WindowMessage.WM_SYSKEYDOWN;
            var isKeyUp = message == WindowMessage.WM_KEYUP || message == WindowMessage.WM_SYSKEYUP;

            if (nCode >= 0 && (isKeyDown || isKeyUp) && !isInjected)
            {
                if (isKeyDown)
                {
                    if (KeyReplacementProcessor.Instance.ProcessKeyDown(keyCode))
                    {
                        return new IntPtr(1);
                    }
                }
                else
                {
                    if (KeyReplacementProcessor.Instance.ProcessKeyUp(keyCode))
                    {
                        return new IntPtr(1);
                    }
                }
            }

            if (nCode >= 0 && isKeyDown)
            {
                if (KeyStrokeProcessor.Instance.ProcessKey(keyCode))
                {
                    return new IntPtr(1);
                }
            }

            if (nCode >= 0 && message == WindowMessage.WM_KEYDOWN)
            {
                if (KeySequenceProcessor.Instance.ProcessKey(keyCode, isShiftDown))
                {
                    return new IntPtr(1);
                }
            }

            if (nCode >= 0 && (message == WindowMessage.WM_KEYDOWN || message == WindowMessage.WM_SYSKEYDOWN || (message == WindowMessage.WM_IME_KEYDOWN)))
            {
//                var keyData = new KeyData(keyCode, keyboardData.scanCode, isExtended, isInjected);
//                var vkCode = (KeyCode)Marshal.ReadInt32(lParam);

//                if (vkCode == KeyCode.Packet)
                {
                    Debug.WriteLine($"vkCode: {keyCode}, scanCode: {keyboardData.scanCode}, extendedKey: {isExtended}, injected: {isInjected}");
//                    KeySender.SendKeyPress(KeyCode.D);
//                    return new IntPtr(1);
                }
/*
                if (keyCode == KeyCode.A)
                {
                    KeySender.SendKeyPress(KeyCode.Packet, 55356);
                    KeySender.SendKeyPress(KeyCode.Packet, 57303);
                    KeySender.SendKeyPress(KeyCode.Packet, 65039);
                    return new IntPtr(1);
                }
*/
            }

            return WinApi.CallNextHookEx(hookId, nCode, wParam, lParam);
        }
    }
}