using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Hardcodet.Wpf.TaskbarNotification;

namespace Clipt
{
    public class App : Application
    {
        public static App Instance { get; private set; }

        public TaskbarIcon TrayIcon { get; private set; }

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread]
        [DebuggerNonUserCode]
        public static void Main()
        {
            Instance = new App();
            Instance.Run();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("pack://application:,,,/Clipt;component/Assets/Wintomaton.ico");
            logo.EndInit();

            var contextMenu = new ContextMenu();
            var exitMenuItem = new MenuItem
            {
                Header = "Exit"
            };
            exitMenuItem.Click += (sender, args) => Shutdown();
            contextMenu.Items.Add(exitMenuItem);

            TrayIcon = new TaskbarIcon
            {
                IconSource = logo,
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

            using (var script = new TestScript())
            {
                script.Run();
            }
        }
    }
}