using System;
using System.Runtime.InteropServices;

namespace Clipt.KeyboardHooks
{
    public class KeySender
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);

        public static void SendKeyPress(KeyCode keyCode, ushort scanCode = 0)
        {
            SendInputs(
                CreateInput(keyCode, scanCode, 0),
                CreateInput(keyCode, scanCode, KEYEVENTF.KEYUP));
        }

        private static INPUT CreateInput(KeyCode keyCode, ushort scanCode, KEYEVENTF keyEvent)
        {
            var input = new INPUT
            {
                Type = 1
            };
            input.Data.Keyboard = new KEYBDINPUT
            {
                Vk = keyCode == KeyCode.Packet ? (ushort)0 : (ushort)keyCode,
                Scan = scanCode,
                Flags = keyEvent | (scanCode == 0 ? 0 : KEYEVENTF.UNICODE),
                Time = 0,
                ExtraInfo = IntPtr.Zero
            };
            return input;
        }

        private static void SendInputs(params INPUT[] inputs)
        {
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
                throw new Exception();
        }

        public static void SendKeyDown(KeyCode keyCode, ushort scanCode = 0)
        {
            SendInputs(CreateInput(keyCode, scanCode, 0));
        }

        public static void SendKeyUp(KeyCode keyCode, ushort scanCode = 0)
        {
            SendInputs(CreateInput(keyCode, scanCode, KEYEVENTF.KEYUP));
        }

        [Flags]
        internal enum KEYEVENTF : uint
        {
            KEYDOWN = 0x0000,
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,

            /// <summary>
            /// Set the KEYEVENTF_SCANCODE flag to define keyboard input in terms of the scan code. This is useful to
            /// simulate a physical keystroke regardless of which keyboard is currently being used. The virtual key
            /// value of a key may alter depending on the current keyboard layout or what other keys were pressed,
            /// but the scan code will always be the same.
            /// </summary>
            SCANCODE = 0x0008,

            /// <summary>
            /// If KEYEVENTF_UNICODE is specified, SendInput sends a WM_KEYDOWN or WM_KEYUP message to the foreground
            /// thread's message queue with wParam equal to VK_PACKET. Once GetMessage or PeekMessage obtains this
            /// message, passing the message to TranslateMessage posts a WM_CHAR message with the Unicode character
            /// originally specified by wScan. This Unicode character will automatically be converted to the appropriate
            /// ANSI value if it is posted to an ANSI window.
            /// </summary>
            UNICODE = 0x0004
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)] public HARDWAREINPUT Hardware;
            [FieldOffset(0)] public KEYBDINPUT Keyboard;
            [FieldOffset(0)] public MOUSEINPUT Mouse;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public uint Msg;
            public ushort ParamL;
            public ushort ParamH;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort Vk;
            public ushort Scan;
            public KEYEVENTF Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }
    }
}