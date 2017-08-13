using System.Collections.Immutable;
using Wintomaton.WinApis;

namespace Wintomaton.Inputs
{
    public class KeySequenceProcessor
    {
        public static KeySequenceProcessor Instance { get; } = new KeySequenceProcessor();

        private readonly KeySequenceBranch root = new KeySequenceBranch(ImmutableList<KeyData>.Empty);

        private ImmutableList<KeySequenceBranch> activeBranches = ImmutableList<KeySequenceBranch>.Empty;

        public bool ProcessKey(KeyCode key, bool isShiftDown)
        {
            var character = WinApi.MapVirtualKey((uint)key, MapVirtualKeyType.VirtualKeyToChar);
            if (character == 0)
            {
                return false;
            }

            var trigger = new KeyData(key, isShiftDown);
            foreach (var branch in activeBranches.Add(root))
            {
                switch (branch.Process(trigger, out var newBranch))
                {
                    case KeySequenceBranchResult.Unhandled:
                        activeBranches = activeBranches.Remove(branch);
                        break;
                    case KeySequenceBranchResult.Branched:
                        activeBranches = activeBranches.Add(newBranch);
                        break;
                    case KeySequenceBranchResult.Handled:
                        activeBranches = ImmutableList<KeySequenceBranch>.Empty;
                        return true;
                }
            }
            return false;
        }

        public void RegisterSequence(KeySequence sequence, KeySequenceHandler handler)
        {
            root.RegisterSequence(sequence, handler);
        }
    }
}