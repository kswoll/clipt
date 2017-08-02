using System.Collections.Generic;

namespace Clipt.KeyboardHooks
{
    public class KeySequenceBranch : IKeySequenceNode
    {
        private readonly Dictionary<KeyTrigger, IKeySequenceNode> nodes = new Dictionary<KeyTrigger, IKeySequenceNode>();

        public void RegisterSequence(KeySequence sequence, KeySequenceHandler handler)
        {
            RegisterSpan(sequence.Start, handler);
        }

        private void RegisterSpan(KeySequenceSpan span, KeySequenceHandler handler)
        {
            KeySequenceBranch branch;
            if (nodes.TryGetValue(span.Trigger, out var node))
            {
                if (span.IsTerminal)
                {
                    throw new SequenceAlreadyRegisteredException(span);
                }
                else
                {
                    branch = (KeySequenceBranch)node;
                }
            }
            else
            {
                if (span.IsTerminal)
                {
                    nodes[span.Trigger] = new KeySequenceHandlerNode(handler);
                    return;
                }
                else
                {
                    branch = new KeySequenceBranch();
                    nodes[span.Trigger] = branch;
                }
            }

            branch.RegisterSpan(span.Next, handler);
        }
    }
}