using System;
using System.Collections.Generic;

namespace Clipt.Keyboards
{
    public class KeySequenceBranch : IKeySequenceNode
    {
        private readonly Dictionary<KeyTrigger, IKeySequenceNode> nodes = new Dictionary<KeyTrigger, IKeySequenceNode>();

        private List<KeyTrigger> keys = new List<KeyTrigger>();

        public KeySequenceBranchResult Process(KeyTrigger key, out KeySequenceBranch next)
        {
            keys.Add(key);

            if (nodes.TryGetValue(key, out var node))
            {
                switch (node)
                {
                    case KeySequenceHandlerNode handler:
                        var handledKeys = keys.ToArray();
                        keys.Clear();
                        handler.Fire(handledKeys);
                        next = default(KeySequenceBranch);
                        return KeySequenceBranchResult.Handled;
                    case KeySequenceBranch branch:
                        next = branch;
                        return KeySequenceBranchResult.Branched;
                    default:
                        throw new Exception();
                }
            }

            next = default(KeySequenceBranch);
            return KeySequenceBranchResult.Unhandled;
        }

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