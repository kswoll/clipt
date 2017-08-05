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
    }
}