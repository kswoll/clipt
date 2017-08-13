using System.Collections.Generic;
using System.Windows;

namespace Wintomaton.Inputs
{
    public class HotMouseProcessor
    {
        public static HotMouseProcessor Instance { get; } = new HotMouseProcessor();

        private readonly Dictionary<MouseEvent, List<HotMouse>> hotMiceByEvent = new Dictionary<MouseEvent, List<HotMouse>>();
        private readonly Dictionary<HotMouse, HotMouseHandler> handlersByHotMouse = new Dictionary<HotMouse, HotMouseHandler>();

        public void Register(HotMouse hotMouse, HotMouseHandler handler)
        {
            if (!hotMiceByEvent.TryGetValue(hotMouse.Event, out var list))
            {
                list = new List<HotMouse>();
                hotMiceByEvent[hotMouse.Event] = list;
            }
            list.Add(hotMouse);
            handlersByHotMouse[hotMouse] = handler;
        }

        public bool ProcessMouse(MouseEvent @event, Point point, uint delta)
        {
            if (hotMiceByEvent.TryGetValue(@event, out var list))
            {
                foreach (var hotMouse in list)
                {
                    if (hotMouse.Process(@event))
                    {
//                        ignoredKeyUps = new[] { keyCode }.ToImmutableHashSet();
                        var handler = handlersByHotMouse[hotMouse];
                        handler(@event, point, delta);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}