namespace Clipt.KeyboardHooks
{
    public struct ScanCode
    {
        public ushort Code { get; }
        public bool IsExtended { get; }

        public ScanCode(ushort code, bool isExtended)
        {
            Code = code;
            IsExtended = isExtended;
        }
    }
}