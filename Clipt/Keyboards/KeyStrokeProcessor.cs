using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows;

namespace Clipt.Keyboards
{
    public class KeyStrokeProcessor
    {
        public static KeyStrokeProcessor Instance { get; } = new KeyStrokeProcessor();

        private readonly Dictionary<KeyCode, ImmutableList<KeyStroke>> keyStrokesByKey = new Dictionary<KeyCode, ImmutableList<KeyStroke>>();

        private ImmutableHashSet<KeyCode> activeKeys = ImmutableHashSet<KeyCode>.Empty;
        private ImmutableDictionary<KeyStroke, KeyStrokeHandler> handlersByKeyStroke = ImmutableDictionary<KeyStroke, KeyStrokeHandler>.Empty;

        public bool ProcessKey(KeyCode key)
        {
            if (keyStrokesByKey.TryGetValue(key, out var keyStrokes))
            {
                activeKeys = activeKeys.Add(key);
                foreach (var keyStroke in keyStrokes)
                {
                    if (keyStroke.ProcessKey(activeKeys))
                    {
                        var handler = handlersByKeyStroke[keyStroke];

                        try
                        {
                            handler(keyStroke);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString());
                        }
                        finally
                        {
                            activeKeys = ImmutableHashSet<KeyCode>.Empty;
                        }
                        return true;
                    }
                }
            }
            else
            {
                activeKeys = ImmutableHashSet<KeyCode>.Empty;
            }
            return false;
        }

        public void Register(KeyStroke keyStroke, KeyStrokeHandler handler)
        {
            foreach (var key in keyStroke)
            {
                if (!keyStrokesByKey.TryGetValue(key, out var list))
                {
                    list = ImmutableList<KeyStroke>.Empty;
                    list = list.Add(keyStroke);
                    keyStrokesByKey[key] = list;
                }

                list.Add(keyStroke);
            }

            handlersByKeyStroke = handlersByKeyStroke.SetItem(keyStroke, handler);
        }
    }
}