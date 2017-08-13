using Wintomaton.Inputs;

namespace Wintomaton.Utils
{
    public class MouseUtils
    {
        public void AddHotMouse(HotMouse hotMouse, HotMouseHandler handler) => HotMouseProcessor.Instance.Register(hotMouse, handler);
    }
}