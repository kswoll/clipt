using System;

namespace Clipt.Keyboards
{
    public class KeyboardUtils
    {
        public void AddShortcut(ModifierKeys modifiers, KeyCode key, Func<bool> handler) => HotKey.AddShortcut(modifiers, key, handler);
        public void AddShortcut(ModifierKeys modifiers, KeyCode key, Action handler) => HotKey.AddShortcut(modifiers, key, handler);

        public void SendString(string s) => KeySender.SendString(s);
        public void SendKeyPress(KeyCode keyCode, ushort scanCode = 0) => KeySender.SendKeyPress(keyCode, scanCode);
        public void SendKeyDown(KeyCode keyCode, ushort scanCode = 0) => KeySender.SendKeyDown(keyCode, scanCode);
        public void SendKeyUp(KeyCode keyCode, ushort scanCode = 0) => KeySender.SendKeyUp(keyCode, scanCode);

        public void RegisterKeySequence
        public void RegisterKeyStroke
    }
}