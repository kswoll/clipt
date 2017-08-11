using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using Clipt.WinApis;

namespace Clipt.Windows
{
    /// <summary>
    /// Represents the window that is used internally to get the messages.
    /// </summary>
    public class MessageReceiverWindow : Window
    {
        public static MessageReceiverWindow Instance { get; } = new MessageReceiverWindow();

        private readonly List<IMessageReceiver> receivers = new List<IMessageReceiver>();
        private readonly WindowInteropHelper interop;

        private HwndSource source;

        public MessageReceiverWindow()
        {
            interop = new WindowInteropHelper(this);
            interop.EnsureHandle();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            source = HwndSource.FromHwnd(interop.Handle);
            source.AddHook(HwndHook);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            source.RemoveHook(HwndHook);
            UnregisterReceivers();
        }

        public void RegisterReceiver(IMessageReceiver receiver)
        {
            receivers.Add(receiver);
            receiver.Attach(interop.Handle);
        }

        private void UnregisterReceivers()
        {
            foreach (var receiver in receivers)
            {
                receiver.Detach(interop.Handle);
            }
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            foreach (var receiver in receivers)
            {
                receiver.HandleMessage(hwnd, (WindowMessage)msg, wParam, lParam, ref handled);
                if (handled)
                    break;
            }

            return IntPtr.Zero;
        }
    }
}