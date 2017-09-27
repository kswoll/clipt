using System.Threading;
using System.Windows;

namespace Wintomaton.Windows
{
    public class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        internal static void Initialize()
        {
            var thread = new Thread(StartMainWindow);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        private static void StartMainWindow()
        {
            Instance = new MainWindow();
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}