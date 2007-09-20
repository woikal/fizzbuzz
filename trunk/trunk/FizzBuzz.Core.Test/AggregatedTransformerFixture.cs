using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;

using FizzBuzz.Core;

namespace FizzBuzz.Core.Test
{
    public abstract class AggregatedTransformerFixture
    {
        public abstract AggregatedTransformer GetTransformer(params ITransformer[] children);


        [Test]
        public void Transform_PassingNoChildren_ReturnsEmptyString()
        {
            int dummyNumber = 65;
            AggregatedTransformer transformer = GetTransformer();
            string actualResult = transformer.Transform(dummyNumber);

            Assert.IsEmpty(actualResult);      
        }

        [Test]
        public void Constructor_PassNull_CreatesAnEmptyCollection()
        {
            int dummyNumber = 65;
            AggregatedTransformer transformer = GetTransformer(null);
            string actualResult = transformer.Transform(dummyNumber);

            Assert.IsEmpty(actualResult);     
        }
    }
}
