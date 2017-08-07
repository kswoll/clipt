using System;

namespace Clipt.Keyboards
{
    public class KeyboardUtils
    {
        public static void AddShortcut(ModifierKeys modifiers, KeyCode key, Func<bool> handler) => HotKey.AddShortcut(modifiers, key, handler);
        public static void AddShortcut(ModifierKeys modifiers, KeyCode key, Action handler) => HotKey.AddShortcut(modifiers, key, handler);

    }
}