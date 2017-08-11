using System;
using System.Windows;
using Clipt.Keyboards;
using Clipt.WinApis;
using Clipt.Windows;

namespace Clipt.Utils
{
    public class ClipboardUtils : IMessageReceiver
    {
        public string GetText() => Clipboard.GetText();
        public void SetText(string text) => Clipboard.SetText(text);

        public event Action Changed;

        private IntPtr nextClipboardViewer;

        public ClipboardUtils()
        {
            MessageReceiverWindow.Instance.RegisterReceiver(this);
        }

        public void Attach(IntPtr hwnd)
        {
            nextClipboardViewer = WinApi.SetClipboardViewer(hwnd);
        }

        public void Detach(IntPtr hwnd)
        {
            WinApi.ChangeClipboardChain(hwnd, nextClipboardViewer);
        }

        public void HandleMessage(IntPtr hwnd, WindowMessage message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (message)
            {
                case WindowMessage.WM_DRAWCLIPBOARD:
                    WinApi.SendMessage(nextClipboardViewer, message, wParam, lParam);
                    try
                    {
                        Changed?.Invoke();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    break;
                case WindowMessage.WM_CHANGECBCHAIN:
                    if (wParam == nextClipboardViewer)
                        nextClipboardViewer = lParam;
                    else
                        WinApi.SendMessage(nextClipboardViewer, message, wParam, lParam);
                    break;
            }
        }

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