using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;
using Rhino.Mocks;

using FizzBuzz.Core;
using System.Text.RegularExpressions;

namespace FizzBuzz.Core.Test
{
    [TestFixture]
    public class PredicatedTransformerFixture
    {
        private MockRepository mocks;

        private ICanTestInts mockPredicateProvider;
        private ITransformer mockFailTransformer;
        private ITransformer mockPassTransformer;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            mockPassTransformer = mocks.CreateMock<ITransformer>();
            mockFailTransformer = mocks.CreateMock<ITransformer>();
            mockPredicateProvider = mocks.CreateMock<ICanTestInts>();
        }

        [Test]
        [ExpectedArgumentException]
        public void Constructor_GivenNullPredicate_ThrowsException()
        {
            Predicate<Int32> nullTest = null;
            
            mocks.ReplayAll();

            PredicatedTransformer transformer = new PredicatedTransformer(nullTest, mockPassTransformer, mockFailTransformer);

            mocks.VerifyAll();
        }

        [Test]
        [ExpectedArgumentException]
        public void Constructor_GivenNullPassTransformer_ThrowsException()
        {
            mocks.ReplayAll();

            ITransformer nullTransformer = null;

            PredicatedTransformer transformer = new PredicatedTransformer(mockPredicateProvider.Test, nullTransformer, mockFailTransformer);

            mocks.VerifyAll();
        }

        [Test]
        [ExpectedArgumentException]
        public void Constructor_GivenNullFailTransformer_ThrowsException()
        {
            mocks.ReplayAll();

            ITransformer nullTransformer = null;

            PredicatedTransformer transformer = new PredicatedTransformer(mockPredicateProvider.Test,
                                                    mockPassTransformer,
                                                    nullTransformer);

            mocks.VerifyAll();
        }

        [Test]
        public void Transform_TestPasses_CallsPassingTransform()
        {
            int dummyNumber = 5;
            Expect.Call(mockPredicateProvider.Test(dummyNumber)).Return(true);
            string expectedResult = "Passes";

            Expect.Call(mockPassTransformer.Transform(dummyNumber)).Return(expectedResult);

            mocks.ReplayAll();

            PredicatedTransformer transformer = new PredicatedTransformer(mockPredicateProvider.Test,
                                                    mockPassTransformer,
                                                    mockFailTransformer);
            string actualResult = transformer.Transform(dummyNumber);

            Assert.AreEqual(expectedResult, actualResult);
            
            mocks.VerifyAll();
        }

        [Test]
        public void Transform_TestFails_CallsFailingTransform()
        {
            int dummyNumber = 5;
            Expect.Call(mockPredicateProvider.Test(dummyNumber)).Return(false);
            string expectedResult = "Fails";

            Expect.Call(mockFailTransformer.Transform(dummyNumber)).Return(expectedResult);

            mocks.ReplayAll();

            PredicatedTransformer transformer = new PredicatedTransformer(mockPredicateProvider.Test,
                                                    mockPassTransformer,
                                                    mockFailTransformer);
            string actualResult = transformer.Transform(dummyNumber);

            Assert.AreEqual(expectedResult, actualResult);

            mocks.VerifyAll();
        }
        
        public interface ICanTestInts
        {
            bool Test(Int32 number);
        }

    }
}
