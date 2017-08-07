using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Clipt.Keyboards;
using Hardcodet.Wpf.TaskbarNotification;

namespace Clipt
{
    public class App : Application
    {
        private static TaskbarIcon notifyIcon;

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread]
        [DebuggerNonUserCode]
        public static void Main()
        {
            var app = new App();

            KeyboardHook.Hook();
            try
            {
                app.Run();
            }
            finally
            {
                KeyboardHook.Unhook();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("pack://application:,,,/Clipt;component/Assets/Wintomaton.ico");
            logo.EndInit();

            var contextMenu = new ContextMenu();
            contextMenu.Items.Add(new MenuItem
            {
                Header = "Test"
            });

            notifyIcon = new TaskbarIcon
            {
                IconSource = logo,
                ToolTipText = "Wintomaton",
                ContextMenu = contextMenu
            };

//            notifyIcon.ShowBalloonTip("Title", "Content", BalloonIcon.Error);

            Script.Run();
        }
    }
}