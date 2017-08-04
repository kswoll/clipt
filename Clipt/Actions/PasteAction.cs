using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using Clipt.KeyboardHooks;
using Clipt.WinApi;

namespace Clipt.Actions
{
    public class PasteAction
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public void OnPaste()
        {
            var text = Clipboard.GetText();

            var lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Find the minimum indent
            var minimumIndent = lines.Where(x => x.Trim().Length > 0).Select(x => x.Length - x.TrimStart().Length).Min();
            lines = lines.Select(x => x.Substring(Math.Min(minimumIndent, x.Length))).ToArray();

            text = string.Join(Environment.NewLine, lines);

            Clipboard.SetText(text);

            KeySender.SendKeyDown(KeyCode.Control);
            KeySender.SendKeyPress(KeyCode.V);
            KeySender.SendKeyUp(KeyCode.Control);
        }
    }
}