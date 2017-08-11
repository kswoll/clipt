using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Clipt.Keyboards;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Clipt
{
    public class App : Application
    {
        private static TaskbarIcon notifyIcon;
        private static Script script;

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThread]
        [DebuggerNonUserCode]
        public static void Main()
        {
            var app = new App();

            try
            {
                app.Run();
            }
            finally
            {
                ((IDisposable)script)?.Dispose();
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

//            script = new TestScript();
//            script.Run();
        }
    }
}