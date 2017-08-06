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

        public static byte GetHighOrderByte(ushort value)
        {
            return (byte)((value & 0xFF00) >> 8);
        }

        public static byte GetLowOrderByte(ushort value)
        {
            return (byte)(value & 0x00FF);
        }
    }
}