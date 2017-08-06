using System;
using Clipt.Keyboards;

namespace Clipt.Apis
{
    public class Keyboard
    {
        public static void AddShortcut(ModifierKeys modifiers, KeyCode key, Func<bool> handler) => HotKey.AddShortcut(modifiers, key, handler);
        public static void AddShortcut(ModifierKeys modifiers, KeyCode key, Action handler) => HotKey.AddShortcut(modifiers, key, handler);

    }
}