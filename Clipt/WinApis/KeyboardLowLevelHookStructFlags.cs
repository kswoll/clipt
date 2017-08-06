using System;

namespace Clipt.WinApis
{
    [Flags]
    public enum KeyboardLowLevelHookStructFlags : uint
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