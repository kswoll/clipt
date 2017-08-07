using System.Collections.Generic;
using System.Collections.Immutable;

namespace Clipt.Keyboards
{
    public class KeyStrokeProcessor
    {
        public static KeyStrokeProcessor Instance { get; } = new KeyStrokeProcessor();

        private readonly Dictionary<KeyCode, ImmutableList<KeyStroke>> keyStrokesByKey = new Dictionary<KeyCode, ImmutableList<KeyStroke>>();

        private ImmutableHashSet<KeyCode> activeKeys = ImmutableHashSet<KeyCode>.Empty;
        private Dictionary<KeyStroke, >

        public bool ProcessKey(KeyCode key)
        {
            if (keyStrokesByKey.TryGetValue(key, out var keyStrokes))
            {
                activeKeys.Add(key);
                foreach (var keyStroke in keyStrokes)
                {
                    if (keyStroke.ProcessKey(key, activeKeys))
                    {
                        keyStroke.Activate();
                    }
                }
            }
            else
            {
                activeKeys = ImmutableHashSet<KeyCode>.Empty;
            }
            return false;
        }

        public void Register(KeyStroke keyStroke)
        {
            foreach (var key in keyStroke)
            {
                if (!keyStrokesByKey.TryGetValue(key, out var list))
                {
                    list = ImmutableList<KeyStroke>.Empty;
                    keyStrokesByKey[key] = list;
                }

                list.Add(keyStroke);
            }
        }
    }
}