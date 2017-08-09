using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Clipt.WinApis;

namespace Clipt.Keyboards
{
    public class KeySender
    {
        public static void SendString(string s)
        {
            for (var i = 0; i < s.Length; i++)
            {
                if (char.IsSurrogatePair(s, i))
                {
                    var low = s[i];
                    var high = s[i + 1];
                    SendKeyPress(KeyCode.Packet, low);
                    SendKeyPress(KeyCode.Packet, high);
                    i++;
                }
                else
                {
                    SendKeyPress(KeyCode.Packet, s[i]);
                }
            }
        }

        public static void SendKeyPress(KeyCode keyCode, ushort scanCode = 0)
        {
            SendInputs(
                CreateInput(keyCode, scanCode, true, null),
                CreateInput(keyCode, scanCode, false, null));
        }

        private static Input CreateInput(KeyCode keyCode, ushort scanCode, bool isKeyDown, MousePoint? point)
        {
            if (point != null)
                return CreateMouseInput(keyCode, isKeyDown, point);
            else
                return CreateKeyboardInput(keyCode, scanCode, isKeyDown);
        }

        private static Input CreateKeyboardInput(KeyCode keyCode, ushort scanCode, bool isKeyDown)
        {
            var input = new Input
            {
                Type = InputType.Keyboard
            };
            input.Data.Keyboard = new KeyboardInput
            {
                Vk = keyCode == KeyCode.Packet ? (ushort)0 : (ushort)keyCode,
                Scan = scanCode,
                Flags = (isKeyDown ? KeyEvent.KEYDOWN : KeyEvent.KEYUP) | (scanCode == 0 ? 0 : KeyEvent.UNICODE),
                Time = 0,
                ExtraInfo = IntPtr.Zero
            };
            return input;
        }

        private static Input CreateMouseInput(KeyCode keyCode, bool isKeyDown, MousePoint? point)
        {
            var input = new Input
            {
                Type = InputType.Mouse
            };
            var mouseInput = new MouseInput
            {
                X = point.Value.X,
                Y = point.Value.Y,
            };

            switch (keyCode)
            {
                case KeyCode.LeftButton:
                    mouseInput.Flags = isKeyDown ? MouseEvent.MOUSEEVENTF_LEFTDOWN : MouseEvent.MOUSEEVENTF_LEFTUP;
                    break;
                case KeyCode.RightButton:
                    mouseInput.Flags = isKeyDown ? MouseEvent.MOUSEEVENTF_RIGHTDOWN : MouseEvent.MOUSEEVENTF_RIGHTUP;
                    break;
                case KeyCode.MiddleButton:
                    mouseInput.Flags = isKeyDown ? MouseEvent.MOUSEEVENTF_MIDDLEDOWN : MouseEvent.MOUSEEVENTF_MIDDLEUP;
                    break;
                case KeyCode.ExtraButton1:
                    mouseInput.Flags = isKeyDown ? MouseEvent.MOUSEEVENTF_XDOWN : MouseEvent.MOUSEEVENTF_XUP;
                    mouseInput.MouseData = 1;
                    break;
                case KeyCode.ExtraButton2:
                    mouseInput.Flags = isKeyDown ? MouseEvent.MOUSEEVENTF_XDOWN : MouseEvent.MOUSEEVENTF_XUP;
                    mouseInput.MouseData = 2;
                    break;
            }
            input.Data.Mouse = mouseInput;

            return input;
        }

        private static void SendInputs(params Input[] inputs)
        {
            if (WinApi.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input))) == 0)
                throw new Exception();
        }

        public static void SendKeyDown(KeyCode keyCode, ushort scanCode = 0, MousePoint? point = null)
        {
            SendInputs(CreateInput(keyCode, scanCode, true, point));
        }

        public static void SendKeyUp(KeyCode keyCode, ushort scanCode = 0, MousePoint? point = null)
        {
            SendInputs(CreateInput(keyCode, scanCode, false, point));
        }
    }
}