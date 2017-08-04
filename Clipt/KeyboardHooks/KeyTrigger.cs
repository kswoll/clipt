using Clipt.WinApi;

namespace Clipt.KeyboardHooks
{
    public struct KeyTrigger
    {
        public KeyCode? Key { get; }
        public uint? ScanCode { get; }
        public bool? IsExtended { get; }
        public bool? IsInjected { get; }

        public KeyTrigger(KeyCode? key, uint? scanCode, bool? isExtended, bool? isInjected)
        {
            Key = key;
            ScanCode = scanCode;
            IsExtended = isExtended;
            IsInjected = isInjected;
        }

        public static implicit operator KeyTrigger(KeyCode key)
        {
            return new KeyTrigger(key, null, null, null);
        }
    }
}