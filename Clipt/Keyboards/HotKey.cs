// <copyright file="KeyboardHook.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2017 PlanGrid, Inc. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Clipt.Apis;
using Clipt.WinApis;

namespace Clipt.Keyboards
{
    public static class HotKey
    {
        private static readonly KeyboardWindow window = new KeyboardWindow();

        public static void AddShortcut(ModifierKeys modifiers, KeyCode key, Func<bool> handler)
        {
            window.RegisterHotKey(modifiers, key, handler);
        }

        public static void AddShortcut(ModifierKeys modifiers, KeyCode key, Action handler)
        {
            window.RegisterHotKey(modifiers, key, () =>
            {
                handler();
                return true;
            });
        }

        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class KeyboardWindow : Window
        {
            private readonly Dictionary<int, Func<bool>> handlers = new Dictionary<int, Func<bool>>();
            private readonly WindowInteropHelper interop;

            private HwndSource source;
            private int nextHotKeyId = 9000;

            public KeyboardWindow()
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
                UnregisterHotKeys();
            }

            public void RegisterHotKey(ModifierKeys modifiers, KeyCode key, Func<bool> handler)
            {
                if (!WinApi.RegisterHotKey(interop.Handle, nextHotKeyId, modifiers, (uint)key))
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

            private void UnregisterHotKeys()
            {
                var helper = new WindowInteropHelper(this);
                foreach (var id in handlers.Keys)
                {
                    WinApi.UnregisterHotKey(helper.Handle, id);
                }
            }

            private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                const int wmHotkey = 0x0312;

                switch (msg)
                {
                    case wmHotkey:
                        var handler = handlers[wParam.ToInt32()];
                        handled = handler();
                        break;
                }
                return IntPtr.Zero;
            }
        }
    }
}