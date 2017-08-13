using System.Collections.Immutable;

namespace Clipt.Inputs
{
    public class HotMouse
    {
        public ImmutableList<KeyCode> Modifiers { get; }
        public MouseEvent Event { get; }

        public HotMouse(MouseEvent @event, params KeyCode[] modifiers)
        {
            Event = @event;
            Modifiers = modifiers.ToImmutableList();
        }

        public bool Process(MouseEvent mouseEvent)
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