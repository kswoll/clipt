using System;
using System.Windows;
using Wintomaton.Inputs;
using Wintomaton.WinApis;
using Wintomaton.Windows;

namespace Wintomaton.Utils
{
    public class ClipboardUtils : IMessageReceiver
    {
        public string GetText() => Clipboard.GetText();

        private bool isIgnoringClipboardChanged;

        public void SetText(string text)
        {
            isIgnoringClipboardChanged = true;
            try
            {
                Clipboard.SetText(text, TextDataFormat.Text);
            }
            finally
            {
                isIgnoringClipboardChanged = false;
            }
        }

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
            if (isIgnoringClipboardChanged)
                return;

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