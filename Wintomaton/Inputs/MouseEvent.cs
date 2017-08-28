using System.Collections.Immutable;

namespace Wintomaton.Inputs
{
    public class MouseEvent
    {
        public ImmutableList<KeyCode> Modifiers { get; }
        public MouseEventType Event { get; }

        public MouseEvent(MouseEventType @event, params KeyCode[] modifiers)
        {
            Event = @event;
            Modifiers = modifiers.ToImmutableList();
        }

        public bool Process()
        {
            foreach (var modifier in Modifiers)
            {
                if (!InputHook.IsKeyPressed(modifier))
                    return false;
            }
            return true;
        }
    }
}