namespace Wintomaton.Inputs
{
    public struct KeyData
    {
        public KeyCode Key { get; }
        public bool IsShiftDown { get; }

        public KeyData(KeyCode key, bool isShiftDown)
        {
            Key = key;
            IsShiftDown = isShiftDown;
        }

        public static implicit operator KeyData(KeyCode keyCode)
        {
            return new KeyData(keyCode, false);
        }

        public static implicit operator KeyData((KeyCode keyCode, bool isShiftDown) trigger)
        {
            return new KeyData(trigger.keyCode, trigger.isShiftDown);
        }

        public override string ToString()
        {
            return $"{Key}, IsShiftDown: {IsShiftDown}";
        }

        public bool Equals(KeyData other)
        {
            return Key == other.Key && IsShiftDown == other.IsShiftDown;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is KeyData && Equals((KeyData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Key * 397) ^ IsShiftDown.GetHashCode();
            }
        }
    }
}