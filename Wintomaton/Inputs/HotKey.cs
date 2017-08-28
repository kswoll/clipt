using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Wintomaton.WinApis;
using Wintomaton.Windows;

namespace Wintomaton.Inputs
{
    public class HotKey : IMessageReceiver
    {
        public static HotKey Instance { get; } = new HotKey();

        private readonly Dictionary<int, Func<bool>> handlers = new Dictionary<int, Func<bool>>();

        private IntPtr hwnd;
        private int nextHotKeyId = 9000;

        public HotKey()
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

        public void AddHotKey(KeyCode key, ModifierKeys modifiers, Func<bool> handler)
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

        public void AddHotKey(KeyCode key, ModifierKeys modifiers, Action handler)
        {
            AddHotKey(key, modifiers, () =>
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