using Clipt.Keyboards;
using Clipt.Utils;

namespace Clipt
{
    public abstract class Script
    {
        public abstract void Run();

        public ClipboardUtils Clipboard { get; } = new ClipboardUtils();
        public TextUtils Text { get; } = new TextUtils();
        public KeyboardUtils Keyboard { get; } = new KeyboardUtils();
    }
}