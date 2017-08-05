﻿using System;
using System.Runtime.InteropServices;

namespace Clipt.WinApi
{
    public static class Kernel
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string moduleName);
    }
}