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

        public void RegisterSequence(KeySequence sequence, KeySequenceHandler handler) => KeySequenceProcessor.Instance.RegisterSequence(sequence, handler);
        public void RegisterSequence(KeySequence sequence, string substitution) => sequence.Substitute(substitution);

        public void RegisterStroke(KeyStroke keyStroke, KeyStrokeHandler handler) => KeyStrokeProcessor.Instance.Register(keyStroke, handler);

        public void ReplaceKey(KeyCode key, KeyCode replacement) => KeyReplacementProcessor.Instance.Register(key, replacement);
    }
}