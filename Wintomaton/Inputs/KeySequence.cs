using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintomaton.WinApis;

namespace Wintomaton.Inputs
{
    public class KeySequence : IEnumerable<KeyData>
    {
        public int Count => keys.Count;
        public KeyData this[int index] => keys[index];
        public KeySequenceSpan Start => new KeySequenceSpan(this, 0, Count);
        public void Register(KeySequenceHandler handler) => KeySequenceProcessor.Instance.RegisterSequence(this, handler);

        private readonly IReadOnlyList<KeyData> keys;

        public KeySequence(params KeyData[] keys) : this((IEnumerable<KeyData>)keys)
        {
        }

        public KeySequence(IEnumerable<KeyData> keys)
        {
            this.keys = keys.ToArray();
            if (this.keys.Count == 0)
                throw new ArgumentException(nameof(keys));
        }

        public static KeySequence FromString(string input)
        {
            return new KeySequence(input.Select(x => WinApi.VkKeyScan(x)).Select(x => new KeyData(x.Key, x.Modifiers.HasFlag(VkKeyScanModifierKeys.Shift))));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyData> GetEnumerator()
        {
            return keys.GetEnumerator();
        }

        public void Substitute(string replacement)
        {
            Register(keys =>
            {
                foreach (var _ in keys.Take(keys.Count - 1))
                {
                    KeySender.SendKeyPress(KeyCode.Back);
                }
                KeySender.SendString(replacement);
            });
        }
    }
}