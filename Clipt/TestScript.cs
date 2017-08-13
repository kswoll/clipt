using System.Diagnostics;
using System.Windows;
using Clipt.Inputs;
using Clipt.Utils;
using Clipt.WinApis;
using Hardcodet.Wpf.TaskbarNotification;

namespace Clipt
{
    public class TestScript : Script
    {
        public override void Run()
        {
            EnableKeyboardHook();
            EnableMouseHook();

//            Clipboard.Changed += () => MessageBox.Show(Clipboard.GetText());
            Clipboard.Changed += () =>
            {
                var text = Clipboard.GetText();
                Debug.WriteLine(text);
                Tray.Icon.ShowBalloonTip("Copied!", text, BalloonIcon.Info);
            };

            Mouse.AddHotMouse(new HotMouse(MouseEvent.WheelUp, KeyCode.LeftControl), Actions.Nothing);
            Mouse.AddHotMouse(new HotMouse(MouseEvent.WheelDown, KeyCode.LeftControl), Actions.Nothing);

            Keyboard.AddShortcut(ModifierKeys.Ctrl | ModifierKeys.Windows, KeyCode.V, () =>
            {
                var text = System.Windows.Clipboard.GetText();
                text = Text.TrimIndent(text);
                Clipboard.Paste(text);
            });

            Keyboard.AddShortcut(
                ModifierKeys.Alt | ModifierKeys.Ctrl | ModifierKeys.Shift | ModifierKeys.Windows,
                KeyCode.Escape,
                () => Application.Current.Shutdown());

//            Keyboard.RegisterStroke(new KeyStroke(KeyCode.LeftControl, KeyCode.LeftMenu, KeyCode.R), stroke => Debug.WriteLine("Stroked!"));

            Keyboard.RegisterStroke(new KeyStroke(KeyCode.ExtraButton2), _ =>
            {
                Keyboard.SendKeyDown(KeyCode.Control);
                Keyboard.SendKeyPress(KeyCode.Home);
                Keyboard.SendKeyUp(KeyCode.Control);
            });
            Keyboard.RegisterStroke(new KeyStroke(KeyCode.ExtraButton1), _ =>
            {
                Keyboard.SendKeyDown(KeyCode.Control);
                Keyboard.SendKeyPress(KeyCode.End);
                Keyboard.SendKeyUp(KeyCode.Control);
            });

            Keyboard.AddHotKey(new HotKey(KeyCode.OEM3, KeyCode.LeftMenu), hotKey =>
            {
                var activeWindow = WinApi.GetForegroundWindow();
                var windows = Windows.GetVisibleWindowsWithSameProcess(activeWindow);
                Windows.ActivateNextWindow(windows, activeWindow);
            });

            Keyboard.AddHotKey(new HotKey(KeyCode.Left, KeyCode.F24), new KeyStroke(KeyCode.Home));
            Keyboard.AddHotKey(new HotKey(KeyCode.Right, KeyCode.F24), new KeyStroke(KeyCode.End));
            Keyboard.AddHotKey(new HotKey(KeyCode.Up, KeyCode.F24), new KeyStroke(KeyCode.Prior));
            Keyboard.AddHotKey(new HotKey(KeyCode.Down, KeyCode.F24), new KeyStroke(KeyCode.Next));

//            Keyboard.ReplaceKey(KeyCode.N1, KeyCode.N2);
//            Keyboard.ReplaceKey(KeyCode.N2, KeyCode.RightButton);
//            Keyboard.ReplaceKey(KeyCode.LeftButton, KeyCode.RightButton);
//            Keyboard.ReplaceKey(KeyCode.RightButton, KeyCode.G);

//            var layout = WinApi.GetKeyboardLayout(0);
//            var result = WinApi.VkKeyScanEx('D', layout);
//            var lowOrderByte = BitUtils.GetLowOrderByte(result);
//            var highOrderByte = BitUtils.GetHighOrderByte(result);

            KeySequence.FromString("tEst").Substitute("🏗️");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Substitute("hello");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Substitute("🏗️");
//            new KeySequence(KeyCode.T, KeyCode.E, KeyCode.S, KeyCode.T).Register(keys => Debug.WriteLine("Success"));
        }
    }
}