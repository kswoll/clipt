using System.Collections.Generic;
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
        }
    }
}