using Wintomaton.Inputs;

namespace Wintomaton.Utils
{
    public class MouseUtils
    {
        public void AddHotMouse(MouseEvent mouseEvent, MouseEventHandler handler) => MouseEventProcessor.Instance.Register(mouseEvent, handler);
    }
}