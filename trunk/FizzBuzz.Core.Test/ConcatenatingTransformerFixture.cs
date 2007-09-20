using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;

using Rhino.Mocks;

namespace FizzBuzz.Core.Test
{
    [TestFixture]
    public class ConcatenatingTransformerFixture : AggregatedTransformerFixture
    {
        private string DummyValueA = "A";
        private string DummyValueB = "B";
        private MockRepository mocks;
        private ITransformer mockTransformerWhichReturnsValueA;
        private ITransformer mockTransformerWhichReturnsValueB;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();

            mockTransformerWhichReturnsValueA = mocks.CreateMock<ITransformer>();
            Expect.Call(mockTransformerWhichReturnsValueA.Transform(1)).IgnoreArguments().Return(DummyValueA).Repeat.Any();

            mockTransformerWhichReturnsValueB = mocks.CreateMock<ITransformer>();
            Expect.Call(mockTransformerWhichReturnsValueB.Transform(1)).IgnoreArguments().Return(DummyValueB).Repeat.Any();

            mocks.ReplayAll();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        public override AggregatedTransformer GetTransformer(params ITransformer[] children)
        {
            return new ConcatenatingTransformer(children);
        }

        [Test]
        public void Transform_TwoChildTransforms_ReturnsConcatenatedResults()
        {
            ConcatenatingTransformer transform = new ConcatenatingTransformer(mockTransformerWhichReturnsValueA, mockTransformerWhichReturnsValueB);

            int dummyNumber = 5;

            string actualResult = transform.Transform(dummyNumber);

            string expectedResult = String.Format("{0}{1}", DummyValueA, DummyValueB);

            Assert.AreEqual(expectedResult, actualResult);
        }


    }
}
