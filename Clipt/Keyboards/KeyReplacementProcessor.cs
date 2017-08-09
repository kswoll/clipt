using System.Collections.Immutable;

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

        public bool ProcessKeyDown(KeyCode key)
        {
            if (replacements.TryGetValue(key, out var replacement))
            {
                KeySender.SendKeyDown(replacement);
                return true;
            }
            return false;
        }

        public bool ProcessKeyUp(KeyCode key)
        {
            if (replacements.TryGetValue(key, out var replacement))
            {
                KeySender.SendKeyUp(replacement);
                return true;
            }
            return false;
        }
    }
}