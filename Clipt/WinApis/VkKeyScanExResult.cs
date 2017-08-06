using System.Runtime.InteropServices;
using Clipt.Apis;

namespace Clipt.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VkKeyScanExResult
    {
        public KeyCode Key;
        public VkKeyScanModifierKeys Modifiers;
    }
}