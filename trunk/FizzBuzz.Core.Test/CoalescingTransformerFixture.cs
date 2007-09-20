using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;

using Rhino.Mocks;

namespace FizzBuzz.Core.Test
{
    [TestFixture]
    public class CoalescingTransformerFixture : AggregatedTransformerFixture
    {
        private string DummyValue = "Some Value";
        private MockRepository mocks;
        private ITransformer mockTransformerWhichReturnsValue;
        private ITransformer mockTransformerWhichReturnsEmptyString;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            mockTransformerWhichReturnsEmptyString = mocks.CreateMock<ITransformer>();
            Expect.Call(mockTransformerWhichReturnsEmptyString.Transform(1)).IgnoreArguments().Return("").Repeat.Any();

            mockTransformerWhichReturnsValue = mocks.CreateMock<ITransformer>();
            Expect.Call(mockTransformerWhichReturnsValue.Transform(1)).IgnoreArguments().Return(DummyValue).Repeat.Any();

            mocks.ReplayAll();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        public override AggregatedTransformer GetTransformer(params ITransformer[] children)
        {
            return new CoalescingTransformer(children);
        }

        [Test]
        public void Transform_FirstChildReturnsValue_ReturnValueOfFirstChild()
        {
            CoalescingTransformer transform = new CoalescingTransformer(mockTransformerWhichReturnsValue,
                mockTransformerWhichReturnsEmptyString
                );
            int dummyNumber = 5;
            string actualReturn = transform.Transform(dummyNumber);

            Assert.AreEqual(DummyValue, actualReturn);
        }

        [Test]
        public void Transform_FirstChildReturnsEmptyString_ReturnValueOfSecondChild()
        {
            CoalescingTransformer transform = new CoalescingTransformer(mockTransformerWhichReturnsEmptyString, mockTransformerWhichReturnsValue);
            int dummyNumber = 5;
            string actualReturn = transform.Transform(dummyNumber);

            Assert.AreEqual(DummyValue, actualReturn);
        }
    }
}
