using Clipt.Keyboards;

namespace Clipt.Apis
{
    public class Clipboard
    {
        public static string GetText() => System.Windows.Clipboard.GetText();
        public static void SetText(string text) => System.Windows.Clipboard.SetText(text);

        public static void Paste(string text)
        {
            SetText(text);
            Paste();
        }

        public static void Paste()
        {
            KeySender.SendKeyDown(KeyCode.Control);
            KeySender.SendKeyPress(KeyCode.V);
            KeySender.SendKeyUp(KeyCode.Control);
        }
    }
}