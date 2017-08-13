namespace Wintomaton.Inputs
{
    public static class KeyCodes
    {
        public static bool IsMouseKey(this KeyCode key)
        {
            switch (key)
            {
                case KeyCode.LeftButton:
                case KeyCode.RightButton:
                case KeyCode.MiddleButton:
                case KeyCode.ExtraButton1:
                case KeyCode.ExtraButton2:
                    return true;
                default:
                    return false;
            }
        }
    }
}