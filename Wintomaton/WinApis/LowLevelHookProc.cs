using System;

namespace Wintomaton.WinApis
{
    public delegate IntPtr LowLevelHookProc(int nCode, IntPtr wParam, IntPtr lParam);
}