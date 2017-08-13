using System;
using Wintomaton.WinApis;

namespace Wintomaton.Windows
{
    public interface IMessageReceiver
    {
        void Attach(IntPtr hwnd);
        void Detach(IntPtr hwnd);
        void HandleMessage(IntPtr hwnd, WindowMessage message, IntPtr wParam, IntPtr lParam, ref bool handled);
    }
}