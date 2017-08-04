using Clipt.WinApi;

namespace Clipt.KeyboardHooks
{
    public struct KeyData
    {
        public KeyCode Key { get; }
        public uint ScanCode { get; }
        public bool IsExtended { get; }
        public bool IsInjected { get; }

        public KeyData(KeyCode key, uint scanCode, bool isExtended, bool isInjected)
        {
            Key = key;
            ScanCode = scanCode;
            IsExtended = isExtended;
            IsInjected = isInjected;
        }
    }
}