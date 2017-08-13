using NUnit.Framework;
using Wintomaton.Inputs;

namespace Wintomaton.Tests.Keyboards
{
    [TestFixture]
    public class KeySequenceProcessorTests
    {
        [Test]
        public void OneKeyHandler()
        {
            var processor = new KeySequenceProcessor();
            KeySequence sequence = null;
            processor.RegisterSequence(new KeySequence(KeyCode.A), keys => sequence = keys);
            processor.ProcessKey(KeyCode.A, false);

            Assert.IsNotNull(sequence);
            Assert.AreEqual(KeyCode.A, sequence[0].Key);
            Assert.IsFalse(sequence[0].IsShiftDown);
        }

        [Test]
        public void OneKeyHandlerCapitalLetter()
        {
            var processor = new KeySequenceProcessor();
            KeySequence sequence = null;
            processor.RegisterSequence(new KeySequence((KeyCode.A, true)), keys => sequence = keys);
            processor.ProcessKey(KeyCode.A, true);

            Assert.IsNotNull(sequence);
            Assert.AreEqual(KeyCode.A, sequence[0].Key);
            Assert.IsTrue(sequence[0].IsShiftDown);
        }

        [Test]
        public void OnKeyHandlerTwoCharacters()
        {
            var processor = new KeySequenceProcessor();
            KeySequence sequence = null;
            processor.RegisterSequence(new KeySequence(KeyCode.A, KeyCode.S), keys => sequence = keys);
            processor.ProcessKey(KeyCode.A, false);
            processor.ProcessKey(KeyCode.S, false);

            Assert.IsNotNull(sequence);
            Assert.AreEqual(KeyCode.A, sequence[0].Key);
            Assert.AreEqual(KeyCode.S, sequence[1].Key);
        }
    }
}