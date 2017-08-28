using System.Collections.Generic;
using System.Collections.Immutable;

namespace Wintomaton.Inputs
{
    public class ShortcutProcessor
    {
        public static ShortcutProcessor Instance { get; } = new ShortcutProcessor();

        private readonly Dictionary<KeyCode, List<Shortcut>> shortcutsByKeyCode = new Dictionary<KeyCode, List<Shortcut>>();
        private readonly Dictionary<Shortcut, ShortcutHandler> keyCodeHandlersByShortcut = new Dictionary<Shortcut, ShortcutHandler>();

        private ImmutableHashSet<KeyCode> ignoredKeyUps = ImmutableHashSet<KeyCode>.Empty;

        public void Register(Shortcut hotKey, ShortcutHandler handler)
        {
            if (!shortcutsByKeyCode.TryGetValue(hotKey.Activator, out var hotKeys))
            {
                hotKeys = new List<Shortcut>();
                shortcutsByKeyCode[hotKey.Activator] = hotKeys;
            }
            hotKeys.Add(hotKey);
            keyCodeHandlersByShortcut[hotKey] = handler;
        }

        public void Register(Shortcut hotKey, KeyStroke replacement)
        {
            Register(hotKey, _ =>
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

        public bool ProcessKey(KeyCode keyCode, bool isKeyDown)
        {
            if (isKeyDown)
                return ProcessKeyDown(keyCode);
            else
                return ProcessKeyUp(keyCode);
        }

        private bool ProcessKeyUp(KeyCode key)
        {
            if (ignoredKeyUps.Contains(key))
            {
                ignoredKeyUps = ignoredKeyUps.Remove(key);
                return true;
            }
            return false;
        }

        private bool ProcessKeyDown(KeyCode keyCode)
        {
            if (shortcutsByKeyCode.TryGetValue(keyCode, out var hotKeys))
            {
                foreach (var hotKey in hotKeys)
                {
                    if (hotKey.Process())
                    {
                        ignoredKeyUps = new[] { keyCode }.ToImmutableHashSet();
                        var handler = keyCodeHandlersByShortcut[hotKey];
                        handler(hotKey);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}