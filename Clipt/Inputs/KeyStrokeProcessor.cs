using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Windows;

namespace Clipt.Inputs
{
    public class KeyStrokeProcessor
    {
        public static KeyStrokeProcessor Instance { get; } = new KeyStrokeProcessor();

        private readonly Dictionary<KeyCode, ImmutableList<KeyStroke>> keyStrokesByKey = new Dictionary<KeyCode, ImmutableList<KeyStroke>>();

        private ImmutableHashSet<KeyCode> activeKeys = ImmutableHashSet<KeyCode>.Empty;
        private ImmutableHashSet<KeyCode> ignoredKeyUps = ImmutableHashSet<KeyCode>.Empty;
        private ImmutableDictionary<KeyStroke, KeyStrokeHandler> handlersByKeyStroke = ImmutableDictionary<KeyStroke, KeyStrokeHandler>.Empty;

        public bool ProcessKey(KeyCode key, bool isKeyDown)
        {
            if (isKeyDown)
                return ProcessKeyDown(key);
            else
                return ProcessKeyUp(key);
        }

        public bool ProcessKeyUp(KeyCode key)
        {
            if (ignoredKeyUps.Contains(key))
            {
                ignoredKeyUps = ignoredKeyUps.Remove(key);
                return true;
            }
            return false;
        }

        public bool ProcessKeyDown(KeyCode key)
        {
            if (keyStrokesByKey.TryGetValue(key, out var keyStrokes))
            {
                activeKeys = activeKeys.Add(key);
                foreach (var keyStroke in keyStrokes)
                {
                    if (keyStroke.ProcessKey(activeKeys))
                    {
                        var handler = handlersByKeyStroke[keyStroke];

                        ignoredKeyUps = activeKeys;
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

        public void Register(KeyStroke keyStroke, KeyStroke replacement)
        {
            Register(keyStroke, _ =>
            {
                for (var i = 0; i < replacement.Count; i++)
                {
                    KeySender.SendKeyDown(replacement[i]);
                }
                for (var i = replacement.Count - 1; i >= 0; i--)
                {
                    KeySender.SendKeyUp(replacement[i]);
                }
            });
        }
    }
}