using System.Collections.Immutable;
using Clipt.WinApis;

namespace Clipt.Keyboards
{
    public class KeyReplacementProcessor
    {
        public static KeyReplacementProcessor Instance { get; } = new KeyReplacementProcessor();

        private ImmutableDictionary<KeyCode, KeyCode> replacements = ImmutableDictionary<KeyCode, KeyCode>.Empty;

        public void Register(KeyCode key, KeyCode replacement)
        {
            replacements = replacements.SetItem(key, replacement);
        }

        public bool ProcessKeyDown(KeyCode key, MousePoint? point)
        {
            if (replacements.TryGetValue(key, out var replacement))
            {
                KeySender.SendKeyDown(replacement, point: replacement.IsMouseKey() ? (point ?? new MousePoint()) : (MousePoint?)null);
                return true;
            }
            return false;
        }

        public bool ProcessKeyUp(KeyCode key, MousePoint? point)
        {
            if (replacements.TryGetValue(key, out var replacement))
            {
                KeySender.SendKeyUp(replacement, point: replacement.IsMouseKey() ? (point ?? new MousePoint()) : (MousePoint?)null);
                return true;
            }
            return false;
        }
    }
}