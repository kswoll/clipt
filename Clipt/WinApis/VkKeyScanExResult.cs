using System.Runtime.InteropServices;
using Clipt.Inputs;

namespace Clipt.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VkKeyScanExResult
    {
        public KeyCode Key;
        public VkKeyScanModifierKeys Modifiers;
    }
}