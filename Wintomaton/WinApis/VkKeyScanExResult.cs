using System.Runtime.InteropServices;
using Wintomaton.Inputs;

namespace Wintomaton.WinApis
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VkKeyScanExResult
    {
        public KeyCode Key;
        public VkKeyScanModifierKeys Modifiers;
    }
}