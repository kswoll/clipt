using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using Wintomaton.WinApis;

namespace Wintomaton.Inputs
{
    public class InputHook
    {
        public static event KeyEventHandler KeyDown;
        public static event KeyEventHandler KeyUp;

        private static readonly LowLevelHookProc keyboardHookCallback = KeyboardHookCallback;
        private static readonly LowLevelHookProc mouseHookCallback = MouseHookCallback;
        private static readonly KeyStateByte[] keyPressedState = new KeyStateByte[256];

        private static IntPtr keyboardHookId;
        private static IntPtr mouseHookId;

        public static void HookKeyboard()
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                keyboardHookId = WinApi.SetWindowsHookEx((int)HookType.WH_KEYBOARD_LL, keyboardHookCallback, WinApi.GetModuleHandle(curModule.ModuleName), 0);
            }
            WinApi.GetKeyboardState(keyPressedState);
        }

        public static void UnhookKeyboard()
        {
            WinApi.UnhookWindowsHookEx(keyboardHookId);
        }

        public static void HookMouse()
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                mouseHookId = WinApi.SetWindowsHookEx((int)HookType.WH_MOUSE_LL, mouseHookCallback, WinApi.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public static void UnhookMouse()
        {
            WinApi.UnhookWindowsHookEx(mouseHookId);
        }

        public static bool IsKeyPressed(KeyCode key)
        {
            return keyPressedState[(byte)key] == KeyStateByte.Pressed;
        }

        public static bool IsKeyToggled(KeyCode key)
        {
            return keyPressedState[(byte)key] == KeyStateByte.Toggled;
        }

        private static bool ProcessKey(KeyCode keyCode, uint scanCode, bool isKeyDown, bool isKeyUp, bool isExtended, bool isInjected, MousePoint? point)
        {
            if (isKeyDown)
            {
                KeyDown?.Invoke(keyCode, scanCode, isExtended, isInjected);
            }
            else
            {
                KeyUp?.Invoke(keyCode, scanCode, isExtended, isInjected);
            }

            if ((isKeyDown || isKeyUp) && !isInjected)
                if (isKeyDown)
                {
                    if (KeyReplacementProcessor.Instance.ProcessKeyDown(keyCode, point))
                        return true;
                }
                else
                {
                    if (KeyReplacementProcessor.Instance.ProcessKeyUp(keyCode, point))
                        return true;
                }

            if (KeyStrokeProcessor.Instance.ProcessKey(keyCode, isKeyDown))
                return true;

            if (ShortcutProcessor.Instance.ProcessKey(keyCode, isKeyDown))
                return true;

            return false;
        }

        private static bool ProcessWheel(MousePoint point, int delta)
        {
            Point p = new Point(point.X, point.Y);
            return MouseEventProcessor.Instance.ProcessMouse(delta < 0 ? MouseEventType.WheelDown : MouseEventType.WheelUp, p, (uint)Math.Abs(delta));
        }

        /// <summary>
        /// Return 1 to eat the keystroke.
        /// </summary>
        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
                return WinApi.CallNextHookEx(keyboardHookId, nCode, wParam, lParam);

            var message = (WindowMessage)wParam;

            var keyboardData = (KeyboardLowLevelHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardLowLevelHookStruct));
            var keyCode = keyboardData.vkCode;

            // Update keyPressedState
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

            var isShiftDown = IsKeyPressed(KeyCode.LeftShift) || IsKeyPressed(KeyCode.RightShift);
            var isExtended = (keyboardData.flags & KeyboardLowLevelHookStructFlags.LLKHF_EXTENDED) != 0;
            var isInjected = (keyboardData.flags & KeyboardLowLevelHookStructFlags.LLKHF_INJECTED) != 0;
            var isKeyDown = message == WindowMessage.WM_KEYDOWN || message == WindowMessage.WM_SYSKEYDOWN;
            var isKeyUp = message == WindowMessage.WM_KEYUP || message == WindowMessage.WM_SYSKEYUP;

            if (ProcessKey(keyCode, keyboardData.scanCode, isKeyDown, isKeyUp, isExtended, isInjected, null))
                return new IntPtr(1);

            if (message == WindowMessage.WM_KEYDOWN)
                if (KeySequenceProcessor.Instance.ProcessKey(keyCode, isShiftDown))
                    return new IntPtr(1);

            return WinApi.CallNextHookEx(keyboardHookId, nCode, wParam, lParam);
        }

        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
                return WinApi.CallNextHookEx(keyboardHookId, nCode, wParam, lParam);

            var message = (WindowMessage)wParam;

            var mouseData = (MouseLowLevelHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLowLevelHookStruct));

            switch (message)
            {
                case WindowMessage.WM_MOUSEMOVE:
                    break;
                case WindowMessage.WM_MOUSEWHEEL:
                    var delta = mouseData.MouseData.Data;
                    if (ProcessWheel(mouseData.Point, delta))
                        return new IntPtr(1);
                    break;
                default:
                    var isInjected = (mouseData.Flags & MouseLowLevelHookStructFlags.LLMHF_INJECTED) != 0;
                    var (keyCode, isKeyDown, isKeyUp, _) = DecodeMouseData(message, mouseData.MouseData.Data);

                    if (ProcessKey(keyCode, 0, isKeyDown, isKeyUp, false, isInjected, mouseData.Point))
                        return new IntPtr(1);

                    break;
            }

            return WinApi.CallNextHookEx(mouseHookId, nCode, wParam, lParam);
        }

        private static (KeyCode keyCode, bool isKeyDown, bool isKeyUp, bool isDoubleClick) DecodeMouseData(WindowMessage message, short mouseData)
        {
            (KeyCode keyCode, bool isKeyDown, bool isKeyUp, bool isDoubleClick) result = (0, false, false, false);

            switch (message)
            {
                case WindowMessage.WM_LBUTTONDOWN:
                    result.keyCode = KeyCode.LeftButton;
                    result.isKeyDown = true;
                    break;
                case WindowMessage.WM_RBUTTONDOWN:
                    result.keyCode = KeyCode.RightButton;
                    result.isKeyDown = true;
                    break;
                case WindowMessage.WM_MBUTTONDOWN:
                    result.keyCode = KeyCode.MiddleButton;
                    result.isKeyDown = true;
                    break;
                case WindowMessage.WM_XBUTTONDOWN:
                    result.keyCode = mouseData == 1 ? KeyCode.ExtraButton1 : KeyCode.ExtraButton2;
                    result.isKeyDown = true;
                    break;
                case WindowMessage.WM_LBUTTONUP:
                    result.keyCode = KeyCode.LeftButton;
                    result.isKeyUp = true;
                    break;
                case WindowMessage.WM_RBUTTONUP:
                    result.keyCode = KeyCode.RightButton;
                    result.isKeyUp = true;
                    break;
                case WindowMessage.WM_MBUTTONUP:
                    result.keyCode = KeyCode.MiddleButton;
                    result.isKeyUp = true;
                    break;
                case WindowMessage.WM_XBUTTONUP:
                    result.keyCode = mouseData == 1 ? KeyCode.ExtraButton1 : KeyCode.ExtraButton2;
                    result.isKeyUp = true;
                    break;
                case WindowMessage.WM_LBUTTONDBLCLK:
                    result.keyCode = KeyCode.LeftButton;
                    result.isDoubleClick = true;
                    break;
                case WindowMessage.WM_RBUTTONDBLCLK:
                    result.keyCode = KeyCode.RightButton;
                    result.isDoubleClick = true;
                    break;
                case WindowMessage.WM_MBUTTONDBLCLK:
                    result.keyCode = KeyCode.MiddleButton;
                    result.isDoubleClick = true;
                    break;
                case WindowMessage.WM_XBUTTONDBLCLK:
                    result.keyCode = mouseData == 1 ? KeyCode.ExtraButton1 : KeyCode.ExtraButton2;
                    result.isDoubleClick = true;
                    break;
            }

            return result;
        }
    }
}