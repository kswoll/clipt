using System.Collections.Generic;
using System.Collections.Immutable;

namespace Clipt.Inputs
{
    public class HotKeyProcessor
    {
        public static HotKeyProcessor Instance { get; } = new HotKeyProcessor();

        private readonly Dictionary<KeyCode, List<HotKey>> hotKeysByActivator = new Dictionary<KeyCode, List<HotKey>>();
        private readonly Dictionary<HotKey, HotKeyHandler> handlersByHotKey = new Dictionary<HotKey, HotKeyHandler>();

        private ImmutableHashSet<KeyCode> ignoredKeyUps = ImmutableHashSet<KeyCode>.Empty;

        public void Register(HotKey hotKey, HotKeyHandler handler)
        {
            if (!hotKeysByActivator.TryGetValue(hotKey.Activator, out var hotKeys))
            {
                hotKeys = new List<HotKey>();
                hotKeysByActivator[hotKey.Activator] = hotKeys;
            }
            hotKeys.Add(hotKey);
            handlersByHotKey[hotKey] = handler;
        }

        public void Register(HotKey hotKey, KeyStroke replacement)
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
            if (hotKeysByActivator.TryGetValue(keyCode, out var hotKeys))
            {
                foreach (var hotKey in hotKeys)
                {
                    if (hotKey.Process(keyCode))
                    {
                        ignoredKeyUps = new[] { keyCode }.ToImmutableHashSet();
                        var handler = handlersByHotKey[hotKey];
                        handler(hotKey);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}