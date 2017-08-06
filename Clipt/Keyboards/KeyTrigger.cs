namespace Clipt.Keyboards
{
    public struct KeyTrigger
    {
        public KeyCode Key { get; }
        public bool IsShiftDown { get; }

        public KeyTrigger(KeyCode key, bool isShiftDown)
        {
            Key = key;
            IsShiftDown = isShiftDown;
        }

        public static implicit operator KeyTrigger(KeyCode keyCode)
        {
            return new KeyTrigger(keyCode, false);
        }

        public static implicit operator KeyTrigger((KeyCode keyCode, bool isShiftDown) trigger)
        {
            return new KeyTrigger(trigger.keyCode, trigger.isShiftDown);
        }

        public override string ToString()
        {
            return $"{Key}, IsShiftDown: {IsShiftDown}";
        }

        public bool Equals(KeyTrigger other)
        {
            return Key == other.Key && IsShiftDown == other.IsShiftDown;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is KeyTrigger && Equals((KeyTrigger)obj);
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