using Clipt.Keyboards;

namespace Clipt.Utils
{
    public class ClipboardUtils
    {
        public string GetText() => System.Windows.Clipboard.GetText();
        public void SetText(string text) => System.Windows.Clipboard.SetText(text);

        public void Paste(string text)
        {
            SetText(text);
            Paste();
        }

        public void Paste()
        {
            KeySender.SendKeyDown(KeyCode.Control);
            KeySender.SendKeyPress(KeyCode.V);
            KeySender.SendKeyUp(KeyCode.Control);
        }
    }
}