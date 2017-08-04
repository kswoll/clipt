namespace Clipt.Utils
{
    public static class BitUtils
    {
        public const byte HighOrderBitMaskByte = 0x80;
        public const ushort HighOrderBitMaskUShort = 0x8000;

        public static bool IsHighOrderBitSet(byte value)
        {
            return (value & HighOrderBitMaskByte) != 0;
        }

        public static bool IsHighOrderBitSet(short value)
        {
            return (value & HighOrderBitMaskUShort) != 0;
        }
    }
}