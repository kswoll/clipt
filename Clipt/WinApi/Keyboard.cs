using System.Runtime.InteropServices;

namespace Clipt.WinApi
{
    public static class Keyboard
    {
        [DllImport("user32.dll")]
        public static extern KeyStateShort GetKeyState(KeyCode key);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] keyState);
    }
}