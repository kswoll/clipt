using System;
using System.Windows;
using System.Windows.Controls;
using Clipt.Inputs;

namespace Clipt.Windows
{
    public class KeyRecorder : Window, IDisposable
    {
        private readonly TextBox recording;

        public KeyRecorder()
        {
            Icon = App.AppIcon;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 600;
            Height = 400;

            recording = new TextBox();
            recording.IsReadOnly = true;

            var content = new DockPanel();
            content.Children.Add(recording);

            Content = content;

            InputHook.KeyDown += HandleKeyDown;
        }

        private void HandleKeyDown(KeyCode key, uint scanCode, bool isExtended, bool isInjected)
        {
            recording.AppendText($"key: {key}, scanCode: {scanCode}, isExtended: {isExtended}, isInjected: {isInjected}\r\n");
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Dispose();
        }

        public void Dispose()
        {
            InputHook.KeyDown -= HandleKeyDown;
        }
    }
}