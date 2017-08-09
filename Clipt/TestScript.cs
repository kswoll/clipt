using System.Diagnostics;
using Clipt.Keyboards;

namespace Clipt
{
    public class TestScript : Script
    {
        public override void Run()
        {
            Keyboard.AddShortcut(ModifierKeys.Ctrl | ModifierKeys.Alt, KeyCode.V, () =>
            {
                var text = System.Windows.Clipboard.GetText();
                text = Text.TrimIndent(text);
                Clipboard.Paste(text);
            });

            Keyboard.RegisterStroke(new KeyStroke(KeyCode.LeftControl, KeyCode.LeftMenu, KeyCode.R), stroke => Debug.WriteLine("Stroked!"));

            Keyboard.ReplaceKey(KeyCode.N1, KeyCode.N2);
            Keyboard.ReplaceKey(KeyCode.N2, KeyCode.N1);

//            var layout = WinApi.GetKeyboardLayout(0);
//            var result = WinApi.VkKeyScanEx('D', layout);
//            var lowOrderByte = BitUtils.GetLowOrderByte(result);
//            var highOrderByte = BitUtils.GetHighOrderByte(result);

            KeySequence.FromString("tEst").Substitute("🏗️");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Substitute("hello");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Substitute("🏗️");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Register(keys => Debug.WriteLine("Success"));
        }

    }
}