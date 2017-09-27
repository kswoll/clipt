using System;

namespace Wintomaton.Inputs
{
    public class KeyboardUtils
    {
        /// <summary>
        /// Leverages the HotKey mechanism in Windows, allowing you to execute actions based on a KeyCode and modifiers such as control, alt,
        /// etc.  This is the optimal solution if feasible, as it does not require the use of a keyboard hook.  As you might expect, the
        /// specified KeyCode, when pressed, initiates the action, so at that time the required modifiers must be depressed.
        /// </summary>
        /// <param name="key">The key that activates the HotKey.  All the modifiers must be depressed when this key is pressed to invoke the
        /// handler.</param>
        /// <param name="modifiers">The modifier(s) that must be depressed in order for the activation key to trigger the handler.</param>
        /// <param name="handler">The behavior you want to execute when the HotKey is triggered.  If you return true from this function,
        /// the HotKey is considered handled, and no other HotKey registered with the same key and modifiers will trigger.  In contrast,
        /// if you return true, after executing your handler, other HotKeys will be allowed to trigger.</param>
        public void AddHotKey(KeyCode key, ModifierKeys modifiers, Func<bool> handler) => HotKey.Instance.AddHotKey(key, modifiers, handler);

        /// <summary>
        /// Leverages the HotKey mechanism in Windows, allowing you to execute actions based on a KeyCode and modifiers such as control, alt,
        /// etc.  This is the optimal solution if feasible, as it does not require the use of a keyboard hook.  As you might expect, the
        /// specified KeyCode, when pressed, initiates the action, so at that time the required modifiers must be depressed.
        /// </summary>
        /// <param name="key">The key that activates the HotKey.  All the modifiers must be depressed when this key is pressed to invoke the
        /// handler.</param>
        /// <param name="modifiers">The modifier(s) that must be depressed in order for the activation key to trigger the handler.</param>
        /// <param name="handler">The behavior you want to execute when the HotKey is triggered. No other potential HotKey registered with
        /// the same key and modifiers will be allowed to trigger.</param>
        public void AddHotKey(KeyCode key, ModifierKeys modifiers, Action handler) => HotKey.Instance.AddHotKey(key, modifiers, handler);

        /// <summary>
        /// Similar to a HotKey, but more flexible as the modifiers can be any key on the keyboard.  Shortcuts require the use of a keyboard
        /// hook.  As you might expect, the specified KeyCode, when pressed, initiates the action, so at that time the required modifiers
        /// must be depressed.
        /// </summary>
        /// <param name="shortcut">A Shortcut representing an activating KeyCode plus zero or more other keys that must be depressed at the
        /// time of pressing the activating key.</param>
        /// <param name="handler">The behavior you want to execute when the Shortcut is triggered.</param>
        public void AddShortcut(Shortcut shortcut, ShortcutHandler handler) => ShortcutProcessor.Instance.Register(shortcut, handler);

        /// <summary>
        /// Similar to a HotKey, but more flexible as the modifiers can be any key on the keyboard.  Shortcuts require the use of a keyboard
        /// hook.  As you might expect, the specified KeyCode, when pressed, initiates the action, so at that time the required modifiers
        /// must be depressed.  This overload is a special case that makes it easier to simply respond with a set of key presses when the
        /// Shortcut is activated.
        /// </summary>
        /// <param name="shortcut">A Shortcut representing an activating KeyCode plus zero or more other keys that must be depressed at the
        /// time of pressing the activating key.</param>
        /// <param name="replacement">The KeyStroke you want to play back when the Shortcut is triggered.</param>
        public void AddShortcut(Shortcut shortcut, KeyStroke replacement) => ShortcutProcessor.Instance.Register(shortcut, replacement);

        /// <summary>
        /// Allows you to take an action after a specific sequence of keys has been typed. This is different from the above in that the
        /// modifier keys above must be depressed at the same time when pressing the KeyCode that activates the action.  In contrast,
        /// keys in a sequence can be pressed one at a time; the only requirement is that they are pressed in sequence.  This facility
        /// is great for replacing strings, for example by replacing a key sequnce with emoji or textmoji, or any other string.
        /// </summary>
        /// <param name="sequence">Represents a particular sequence of KeyCodes that when satisfied will trigger the handler</param>
        /// <param name="handler">The behavior you want to execute when the sequence is triggered</param>
        public void RegisterSequence(KeySequence sequence, KeySequenceHandler handler) => KeySequenceProcessor.Instance.RegisterSequence(sequence, handler);

        /// <summary>
        /// Allows you to take an action after a specific sequence of keys has been typed. This is different from the above in that the
        /// modifier keys above must be depressed at the same time when pressing the KeyCode that activates the action.  In contrast,
        /// keys in a sequence can be pressed one at a time; the only requirement is that they are pressed in sequence.  This facility
        /// is great for replacing strings, for example by replacing a key sequence with emoji or textmoji, or any other string.
        /// </summary>
        /// <param name="sequence">Represents a particular sequence of KeyCodes that when satisfied will trigger the replacement</param>
        /// <param name="substitution">The string that should be sent instead of the original sequence</param>
        public void RegisterSequence(KeySequence sequence, string substitution) => sequence.Substitute(substitution);

        /// <summary>
        /// Sends the specified string to whatever input control current has focus in the active application's active window.
        /// </summary>
        /// <param name="s">The string to send to the input control</param>
        public void SendString(string s) => KeySender.SendString(s);

        /// <summary>
        /// Sends the specified string to whatever input control current has focus in the active application's active window.
        /// </summary>
        /// <param name="keyCode">The key code to send to the input control</param>
        /// <param name="scanCode">Optionally the hardware specific scan code to send to the control</param>
        public void SendKeyPress(KeyCode keyCode, ushort scanCode = 0) => KeySender.SendKeyPress(keyCode, scanCode);

        /// <summary>
        /// Sends the specified string to whatever input control current has focus in the active application's active window.
        /// </summary>
        /// <param name="keyCode">The key code to send to the input control</param>
        /// <param name="scanCode">Optionally the hardware specific scan code to send to the control</param>
        public void SendKeyDown(KeyCode keyCode, ushort scanCode = 0) => KeySender.SendKeyDown(keyCode, scanCode);

        /// <summary>
        /// Sends the specified string to whatever input control current has focus in the active application's active window.
        /// </summary>
        /// <param name="keyCode">The key code to send to the input control</param>
        /// <param name="scanCode">Optionally the hardware specific scan code to send to the control</param>
        public void SendKeyUp(KeyCode keyCode, ushort scanCode = 0) => KeySender.SendKeyUp(keyCode, scanCode);

        public void RegisterStroke(KeyStroke keyStroke, KeyStrokeHandler handler) => KeyStrokeProcessor.Instance.Register(keyStroke, handler);
        public void RegisterStroke(KeyStroke keyStroke, KeyStroke replacement) => KeyStrokeProcessor.Instance.Register(keyStroke, replacement);

        /// <summary>
        /// Replaces one key with another.  So you could, for example, make it so that when you press P, a Q is sent.  Most likely
        /// only useful for modifier keys.
        /// </summary>
        /// <param name="key">The key that is pressed</param>
        /// <param name="replacement">The key that is sent instead</param>
        public void ReplaceKey(KeyCode key, KeyCode replacement) => KeyReplacementProcessor.Instance.Register(key, replacement);

        /// <summary>
        /// Returns true if the specified key is currently pressed.
        /// </summary>
        /// <param name="key">The key for which you want to determine its pressed state</param>
        public static bool IsKeyPressed(KeyCode key) => InputHook.IsKeyPressed(key);

        /// <summary>
        /// Returns true if the specified key is currently pressed.
        /// </summary>
        /// <param name="key">The key for which you want to determine its pressed state</param>
        public static bool IsKeyToggled(KeyCode key) => InputHook.IsKeyToggled(key);
    }
}