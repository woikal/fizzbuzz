using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;

using FizzBuzz.Core;
using FizzBuzz.BusinessLogic;

namespace FizzBuzz.BusinessLogic.Tests
{
    [TestFixture]
    public class FizzBuzzLogicFixture
    {
        private FizzBuzzLogic logic;

        [SetUp]
        public void SetUp()
        {
            logic = new FizzBuzzLogic();
        }

        [Test]
        public void CreateRange_WhenCalled_ReturnsValidARangeObject()
        {
            Range rangeFromLogicComponent = logic.CreateRange();

            Assert.IsNotNull(rangeFromLogicComponent);
        }

        [Row(1, "1")]
        [Row(2, "2")]
        [Row(3, "Fizz")]
        [Row(4, "4")]
        [Row(5, "Buzz")]
        [Row(6, "Fizz")]
        [Row(7, "7")]
        [Row(8, "8")]
        [Row(9, "Fizz")]
        [Row(10, "Buzz")]
        [Row(11, "11")]
        [Row(12, "Fizz")]
        [Row(13, "13")] 
        [Row(14, "14")]
        [Row(15, "FizzBuzz")]
        [RowTest]
        public void CreateTransformer_WhenCalled_ReturnsATransformerWhichAdheresToFizzBuzzBusinessRules(int number, string expected)
        {
            ITransformer transformer = logic.CreateTransformer();
            string resultFromTransformer = transformer.Transform(number);

            Assert.AreEqual(expected, resultFromTransformer);           
        }
    }
}
