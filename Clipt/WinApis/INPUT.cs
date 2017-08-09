﻿using System.Runtime.InteropServices;

namespace Clipt.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Input
    {
        public InputType Type;
        public MouseKeyboardHardwareInput Data;
    }
}