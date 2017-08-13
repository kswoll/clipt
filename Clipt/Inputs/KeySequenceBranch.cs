using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Clipt.Inputs
{
    public class KeySequenceBranch : IKeySequenceNode
    {
        private readonly Dictionary<KeyData, IKeySequenceNode> nodes = new Dictionary<KeyData, IKeySequenceNode>();
        private readonly ImmutableList<KeyData> prelude;

        public KeySequenceBranch(ImmutableList<KeyData> prelude)
        {
            this.prelude = prelude;
        }

        public KeySequenceBranchResult Process(KeyData key, out KeySequenceBranch next)
        {
            if (nodes.TryGetValue(key, out var node))
            {
                switch (node)
                {
                    case KeySequenceHandlerNode handler:
                        handler.Fire(handler.Sequence);
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
                    nodes[span.Trigger] = new KeySequenceHandlerNode(span.Sequence, handler);
                    return;
                }
                else
                {
                    branch = new KeySequenceBranch(span.Prefix.ToImmutableList());
                    nodes[span.Trigger] = branch;
                }
            }

            branch.RegisterSpan(span.Next, handler);
        }
    }
}