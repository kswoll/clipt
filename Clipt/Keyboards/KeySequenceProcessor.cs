using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Clipt.Keyboards
{
    public class KeySequenceProcessor
    {
        public static KeySequenceProcessor Instance { get; } = new KeySequenceProcessor();

        private readonly KeySequenceBranch root = new KeySequenceBranch(ImmutableList<KeyTrigger>.Empty);
        private readonly List<KeySequenceBranch> activeBranches = new List<KeySequenceBranch>();

        public void ProcessKey(KeyCode key, bool isShiftDown)
        {
            Debug.WriteLine($"key: {key}, isShiftDown: {isShiftDown}");
            var trigger = new KeyTrigger(key, isShiftDown);
            foreach (var branch in activeBranches.Concat(new[] { root }).ToArray())
            {
                switch (branch.Process(trigger, out var newBranch))
                {
                    case KeySequenceBranchResult.Unhandled:
                        activeBranches.Remove(branch);
                        break;
                    case KeySequenceBranchResult.Branched:
                        activeBranches.Add(newBranch);
                        break;
                    case KeySequenceBranchResult.Handled:
                        activeBranches.Clear();
                        break;
                }
            }
        }

        public void RegisterSequence(KeySequence sequence, KeySequenceHandler handler)
        {
            root.RegisterSequence(sequence, handler);
        }
    }
}