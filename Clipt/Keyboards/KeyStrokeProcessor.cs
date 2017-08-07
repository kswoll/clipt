using System.Collections.Generic;
using System.Collections.Immutable;
using Clipt.Apis;

namespace Clipt.Keyboards
{
    public class KeyStrokeProcessor
    {
        public static KeyStrokeProcessor Instance { get; } = new KeyStrokeProcessor();

        private Dictionary<KeyCode, ImmutableList<KeyStroke>> keyStrokesByKey = new Dictionary<KeyCode, ImmutableList<KeyStroke>>();

        private ImmutableHashSet<KeyStroke> activeKeyStrokes = ImmutableHashSet<KeyStroke>.Empty;

        public bool ProcessKey(KeyCode key)
        {
            if (keyStrokesByKey.TryGetValue(key, out var keyStrokes))
            {
                foreach (var keyStroke in keyStrokes)
                {
                    switch (keyStroke.ProcessKey(key))
                    {
                        case KeyStrokeResult.Failed:
                            activeKeyStrokes = activeKeyStrokes.Remove(keyStroke);
                            break;
                        case KeyStrokeResult.Activated:
                            activeKeyStrokes = ImmutableHashSet<KeyStroke>.Empty;
                            break;
                        case KeyStrokeResult.Consumed:
                            activeKeyStrokes = activeKeyStrokes.Add(keyStroke);
                            break;
                    }
                }
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