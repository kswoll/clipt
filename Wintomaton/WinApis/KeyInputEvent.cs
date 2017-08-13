using System;

namespace Wintomaton.WinApis
{
    [Flags]
    internal enum KeyInputEvent : uint
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
}