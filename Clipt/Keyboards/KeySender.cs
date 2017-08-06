using System;
using System.Runtime.InteropServices;
using Clipt.Apis;
using Clipt.WinApis;

namespace Clipt.Keyboards
{
    public class KeySender
    {
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
            if (WinApi.SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0)
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
    }
}