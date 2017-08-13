using System;
using Wintomaton.Inputs;
using Wintomaton.Utils;

namespace Wintomaton
{
    public abstract class Script : IDisposable
    {
        public abstract void Run();

        public ClipboardUtils Clipboard { get; } = new ClipboardUtils();
        public TextUtils Text { get; } = new TextUtils();
        public KeyboardUtils Keyboard { get; } = new KeyboardUtils();
        public MouseUtils Mouse { get; } = new MouseUtils();
        public WindowUtils Windows { get; }
        public ProcessUtils Processes { get; } = new ProcessUtils();
        public TrayUtils Tray { get; } = new TrayUtils();

        private bool isKeyboardHookEnabled;
        private bool isMouseHookEnabled;

        protected Script()
        {
            Windows = new WindowUtils(this);
        }

        protected void EnableKeyboardHook()
        {
            if (isKeyboardHookEnabled)
                throw new Exception("Keyboard hook already enabled");

            isKeyboardHookEnabled = true;
            InputHook.HookKeyboard();
        }

        protected void EnableMouseHook()
        {
            if (isMouseHookEnabled)
                throw new Exception("Mouse hook already enabled");

            isMouseHookEnabled = true;
            InputHook.HookMouse();
        }

        void IDisposable.Dispose()
        {
            if (isKeyboardHookEnabled)
                InputHook.UnhookKeyboard();
            if (isMouseHookEnabled)
                InputHook.UnhookMouse();
        }
    }
}