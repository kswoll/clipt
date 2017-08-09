using System;

namespace Clipt.WinApis
{
    public delegate IntPtr LowLevelHookProc(int nCode, IntPtr wParam, IntPtr lParam);
}