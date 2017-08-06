using System.Collections.Generic;
using Clipt.Apis;
using NUnit.Framework;
using Clipt.Keyboards;

namespace Wintomaton.Tests.Keyboards
{
    [TestFixture]
    public class KeySequenceProcessorTests
    {
        [Test]
        public void OneKeyHandler()
        {
            var processor = new KeySequenceProcessor();
            IReadOnlyList<KeyTrigger> handledKeys = null;
            processor.RegisterSequence(new KeySequence(KeyCode.A), keys => handledKeys = keys);
            processor.ProcessKey(KeyCode.A, false);

            Assert.IsNotNull(handledKeys);
            Assert.AreEqual(KeyCode.A, handledKeys[0].Key);
            Assert.IsFalse(handledKeys[0].IsShiftDown);
        }

        [Test]
        public void OneKeyHandlerCapitalLetter()
        {
            var processor = new KeySequenceProcessor();
            IReadOnlyList<KeyTrigger> handledKeys = null;
            processor.RegisterSequence(new KeySequence((KeyCode.A, true)), keys => handledKeys = keys);
            processor.ProcessKey(KeyCode.A, true);

            Assert.IsNotNull(handledKeys);
            Assert.AreEqual(KeyCode.A, handledKeys[0].Key);
            Assert.IsTrue(handledKeys[0].IsShiftDown);
        }

        [Test]
        public void OnKeyHandlerTwoCharacters()
        {
            var processor = new KeySequenceProcessor();
            IReadOnlyList<KeyTrigger> handledKeys = null;
            processor.RegisterSequence(new KeySequence(KeyCode.A, KeyCode.S), keys => handledKeys = keys);
            processor.ProcessKey(KeyCode.A, false);
            processor.ProcessKey(KeyCode.S, false);

            Assert.IsNotNull(handledKeys);
            Assert.AreEqual(KeyCode.A, handledKeys[0].Key);
            Assert.AreEqual(KeyCode.S, handledKeys[1].Key);
        }
    }
}