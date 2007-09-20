using System;
using System.Text;
using System.Collections.Generic;
using MbUnit;

using FizzBuzz.Core;

using Rhino.Mocks;

using MbUnit.Framework;
using System.Collections;

namespace FizzBuzz.Core.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class RangeFixture
    {

        public RangeFixture() { }

        private MockRepository mocks;
        protected Range range;

        // Use TestInitialize to run code before running each test 
        [SetUp]
        public void MyTestInitialize()
        {
            range = new Range(1, 100);
            mocks = new MockRepository();
        }


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [Test]
        public void BoundsSetByConstructor()
        {
            Assert.AreEqual(1, range.LowerBound);
            Assert.AreEqual(100, range.UpperBound);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void LowerBoundGreaterThanHigherBoundResultsInException()
        {
            Range r = new Range(2, 1);
        }

        [Test]
        public void ReturnsNonNullEnumerator()
        {
            IEnumerator<Int32> RangeEnumerator = range.GetEnumerator();

            Assert.IsNotNull(RangeEnumerator);
        }

        [Test]
        // Doesn't check the Reset() call because Iterators in C# throw a not supported exception
        public void FulfillsIEnumerableGenericContract()
        {
            Int32[] oneToOneHundred = GetOneToOneHundred();

            Helpers.TestCollections.TestIEnumerable<Int32>(range, oneToOneHundred, false);
        }

        [Test]
        // Doesn't check the Reset() call because Iterators in C# throw a not supported exception
        public void FulfillsIEnumerableContract()
        {
            Int32[] oneToOneHundred = GetOneToOneHundred();
            object[] oneToOneHundredAsObjects = new object[oneToOneHundred.Length];
            oneToOneHundred.CopyTo(oneToOneHundredAsObjects, 0);

            Helpers.TestCollections.TestIEnumerable(range as IEnumerable, 
                oneToOneHundredAsObjects, 
                false);
        }

        [Test]
        public void CallingEachWithNullActionDoesntFail()
        {
            range.Each(null);
        }

        [Test]
        public void EachCallsActionAtLeastOnce()
        {
            ICanDoSomething<Int32> mockActioner = mocks.CreateMock<ICanDoSomething<Int32>>();

            mockActioner.PerformAction(default(Int32));
            LastCall.IgnoreArguments().Repeat.AtLeastOnce();

            mocks.ReplayAll();

            range.Each(mockActioner.PerformAction);

            mocks.VerifyAll();
        }

        [Test]
        public void EachCallsActionExactlyNTimes()
        {
            ICanDoSomething<Int32> mockActioner = mocks.CreateMock<ICanDoSomething<Int32>>();
            Int32 dummy = 1;
            mockActioner.PerformAction(dummy);
            LastCall.IgnoreArguments().Repeat.Times(100);

            mocks.ReplayAll();

            range.Each(mockActioner.PerformAction);

            mocks.VerifyAll();
        }

        [Test]
        public void EachCallsActionWithCorrectArguments()
        {
            ICanDoSomething<Int32> mockActioner = mocks.CreateMock<ICanDoSomething<Int32>>();

            foreach (Int32 i in GetOneToOneHundred())
            {
                mockActioner.PerformAction(i);
            }

            mocks.ReplayAll();

            range.Each(mockActioner.PerformAction);

            mocks.VerifyAll();
        }

        [Test]
        public void EachCallsActionInCorrectOrder()
        {
            ICanDoSomething<Int32> mockActioner = mocks.CreateMock<ICanDoSomething<Int32>>();

            using (mocks.Ordered())
            {
                foreach (Int32 i in GetOneToOneHundred())
                {
                    mockActioner.PerformAction(i);
                }
            }

            mocks.ReplayAll();

            range.Each(mockActioner.PerformAction);

            mocks.VerifyAll();
        }

        private static Int32[] GetOneToOneHundred()
        {
            Int32[] oneToOneHundred = new Int32[100];

            for (int i = 0; i < 100; i++)
                oneToOneHundred[i] = i + 1;
            return oneToOneHundred;
        }

        public interface ICanDoSomething<T>
        {
            void PerformAction(T item);
        }
    }
}
