using Clipt.Keyboards;
using Clipt.Utils;

namespace Clipt
{
    public class TestScript
    {
        public static void Run()
        {
            KeyboardUtils.AddShortcut(ModifierKeys.Ctrl | ModifierKeys.Alt, KeyCode.V, () =>
            {
                var text = System.Windows.Clipboard.GetText();
                text = TextUtils.TrimIndent(text);
                ClipboardUtils.Paste(text);
            });

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