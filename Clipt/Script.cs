using System.Diagnostics;
using Clipt.Apis;

namespace Clipt
{
    public static class Script
    {
        public static void Run()
        {
            Keyboard.AddShortcut(ModifierKeys.Ctrl | ModifierKeys.Alt, KeyCode.V, () =>
            {
                var text = System.Windows.Clipboard.GetText();
                text = TextTransform.TrimIndent(text);
                Clipboard.Paste(text);
            });

            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Register(keys => Debug.WriteLine("Success"));
        }
    }
}