using System.Runtime.InteropServices;

namespace Clipt.WinApis
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct MouseKeyboardHardwareInput
    {
        [FieldOffset(0)] public HardwareInput Hardware;
        [FieldOffset(0)] public KeyboardInput Keyboard;
        [FieldOffset(0)] public MouseInput Mouse;
    }
}