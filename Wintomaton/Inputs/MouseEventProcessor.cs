using System.Collections.Generic;
using System.Windows;

namespace Wintomaton.Inputs
{
    public class MouseEventProcessor
    {
        public static MouseEventProcessor Instance { get; } = new MouseEventProcessor();

        private readonly Dictionary<MouseEventType, List<MouseEvent>> mouseEventsByType = new Dictionary<MouseEventType, List<MouseEvent>>();
        private readonly Dictionary<MouseEvent, MouseEventHandler> handlersByMouseEvent = new Dictionary<MouseEvent, MouseEventHandler>();

        public void Register(MouseEvent mouseEvent, MouseEventHandler handler)
        {
            if (!mouseEventsByType.TryGetValue(mouseEvent.Event, out var list))
            {
                list = new List<MouseEvent>();
                mouseEventsByType[mouseEvent.Event] = list;
            }
            list.Add(mouseEvent);
            handlersByMouseEvent[mouseEvent] = handler;
        }

        public bool ProcessMouse(MouseEventType @event, Point point, uint delta)
        {
            if (mouseEventsByType.TryGetValue(@event, out var list))
            {
                foreach (var mouseEvent in list)
                {
                    if (mouseEvent.Process())
                    {
                        var handler = handlersByMouseEvent[mouseEvent];
                        handler(@event, point, delta);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}