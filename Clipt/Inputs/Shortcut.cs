using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Clipt.WinApis;
using Clipt.Windows;

namespace Clipt.Inputs
{
    public class Shortcut : IMessageReceiver
    {
        public static Shortcut Instance { get; } = new Shortcut();

        private readonly Dictionary<int, Func<bool>> handlers = new Dictionary<int, Func<bool>>();

        private IntPtr hwnd;
        private int nextHotKeyId = 9000;

        public Shortcut()
        {
            MessageReceiverWindow.Instance.RegisterReceiver(this);
        }

        public void Attach(IntPtr hwnd)
        {
            this.hwnd = hwnd;
        }

        public void Detach(IntPtr hwnd)
        {
            this.hwnd = IntPtr.Zero;

            foreach (var id in handlers.Keys)
            {
                WinApi.UnregisterHotKey(hwnd, id);
            }
        }

        public void AddShortcut(ModifierKeys modifiers, KeyCode key, Func<bool> handler)
        {
            if (!WinApi.RegisterHotKey(hwnd, nextHotKeyId, modifiers, (uint)key))
            {
                var error = Marshal.GetLastWin32Error();
                throw new Exception($"Unable to register hotkey: {error}");
            }
            else
            {
                handlers[nextHotKeyId] = handler;
                nextHotKeyId++;
            }
        }

        public void AddShortcut(ModifierKeys modifiers, KeyCode key, Action handler)
        {
            AddShortcut(modifiers, key, () =>
            {
                handler();
                return true;
            });
        }

        public void HandleMessage(IntPtr hwnd, WindowMessage message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (message)
            {
                case WindowMessage.WM_HOTKEY:
                    var handler = handlers[wParam.ToInt32()];
                    handled = handler();
                    break;
            }
        }
    }
}