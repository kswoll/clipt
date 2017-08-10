using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clipt.WinApis;

namespace Clipt.Utils
{
    public class WindowUtils
    {
        private Script script;

        public WindowUtils(Script script)
        {
            this.script = script;
        }

        public IReadOnlyList<IntPtr> GetVisibleWindows()
        {
            var result = new List<IntPtr>();
            WinApi.EnumWindows(
                (wnd, param) =>
                {
                    if (WinApi.IsWindowVisible(wnd))
                        result.Add(wnd);

                    return true;
                },
                IntPtr.Zero);
            GC.KeepAlive(result);
            return result;
        }

        public string GetWindowProcessImageName(IntPtr hwnd)
        {
            WinApi.GetWindowThreadProcessId(hwnd, out var processId);
            var activeWindowProcessImageFileName = script.Processes.GetProcessImageFileName(processId);
            return activeWindowProcessImageFileName;
        }

        public string GetWindowText(IntPtr hwnd)
        {
            var builder = new StringBuilder(1024);
            WinApi.GetWindowText(hwnd, builder, builder.Capacity);
            return builder.ToString();
        }

        public void ActivateNextWindow(IEnumerable<IntPtr> windows, IntPtr activeWindow)
        {
            var windowsList = windows.OrderBy(x => (uint)x).ToList();
            var activeWindowIndex = windowsList.IndexOf(activeWindow);
            var nextWindowIndex = activeWindowIndex + 1;
            if (nextWindowIndex == windowsList.Count)
                nextWindowIndex = 0;

            var nextWindow = windowsList[nextWindowIndex];
            WinApi.SetForegroundWindow(nextWindow);
        }

        public IEnumerable<IntPtr> GetVisibleWindowsWithSameProcess(IntPtr window)
        {
            string activeWindowProcessImageFileName = GetWindowProcessImageName(window);
            return GetVisibleWindows().Where(x => GetWindowProcessImageName(x) == activeWindowProcessImageFileName).ToList();
        }
    }
}