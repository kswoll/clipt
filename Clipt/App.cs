using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Clipt.Windows;
using Hardcodet.Wpf.TaskbarNotification;

namespace Clipt
{
    public class App : Application
    {
        public static App Instance { get; private set; }

        public TaskbarIcon TrayIcon { get; private set; }

        public static BitmapImage AppIcon { get; private set; }

        private static Script script;

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Instance = new App();
            try
            {
                Instance.Run();
            }
            finally
            {
                ((IDisposable)script)?.Dispose();
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppIcon = new BitmapImage();
            AppIcon.BeginInit();
            AppIcon.UriSource = new Uri("pack://application:,,,/Clipt;component/Assets/Wintomaton.ico");
            AppIcon.EndInit();

            var contextMenu = new ContextMenu();

            var keyRecorderMenuItem = new MenuItem
            {
                Header = "Key _Recorder"
            };
            keyRecorderMenuItem.Click += (sender, args) => new KeyRecorder().Show();
            contextMenu.Items.Add(keyRecorderMenuItem);

            var exitMenuItem = new MenuItem
            {
                Header = "E_xit"
            };
            exitMenuItem.Click += (sender, args) => Shutdown();
            contextMenu.Items.Add(exitMenuItem);

            TrayIcon = new TaskbarIcon
            {
                IconSource = AppIcon,
                ToolTipText = "Wintomaton",
                ContextMenu = contextMenu
            };

//            notifyIcon.ShowBalloonTip("Title", "Content", BalloonIcon.Error);

/*
            var testScript = CSharpScript.Create(@"
public class MyScript : Script
{
    public override void Run()
    {
        Clipboard.Changed += () => MessageBox.Show(Clipboard.GetText());
    }
}
new MyScript().Run();
", ScriptOptions.Default.WithImports("Clipt", "System.Windows").AddReferences(typeof(Script).Assembly)).RunAsync();
*/

            script = new TestScript();
            script.Run();
        }
    }
}