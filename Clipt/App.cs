using System;
using System.Diagnostics;
using System.Windows;
using Clipt.Actions;
using Clipt.KeyboardHooks;

namespace Clipt
{
    public class App : Application
    {
        private static KeyboardHook keyboardHook;

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread]
        [DebuggerNonUserCode]
        public static void Main()
        {
            var app = new App();
            app.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            keyboardHook = new KeyboardHook();
            keyboardHook.AddShortcut(ModifierKeys.Ctrl | ModifierKeys.Alt, KeyCode.V, () => new PasteAction().OnPaste());

/*
            GlobalStyles.RegisterStyles(Resources);

            var mainWindow = new MainWindow
            {
                Model = new MainWindowModel()
            };
            mainWindow.Show();
*/
        }
    }
}