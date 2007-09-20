using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;
using FizzBuzz.Core;

namespace FizzBuzz.Core.Test
{
    [TestFixture]
    public class StaticMessageTransformerFixture
    {
        private const string Message = "message";
        private StaticMessageTransformer transformer;

        [SetUp]
        public void SetUp()
        {
            transformer = new StaticMessageTransformer(Message);
        }

        [Test]
        public void ConstructorSetsMessageProperty()
        {
            Assert.AreEqual(Message, transformer.Message);
        }

        [Row(1)]
        [Row(0)]
        [Row(-1)]
        [Row(Int32.MaxValue)]
        [Row(Int32.MinValue)]
        [Row(default(Int32))]
        [RowTest]
        public void TestTransform(int number)
        {
            Assert.AreEqual( Message, transformer.Transform(number) );
        }

        [Test]
        [ExpectedArgumentException]
        public void TestConstructorWithBadMessage()
        {
            transformer = new StaticMessageTransformer(null);
        }
    }
}
