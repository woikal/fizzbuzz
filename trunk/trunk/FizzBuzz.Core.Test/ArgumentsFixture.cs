using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;

using FizzBuzz.Core.Helpers;

namespace FizzBuzz.Core.Test
{
    [TestFixture]
    public class ArgumentsFixture
    {
        [Test]
        [ExpectedArgumentException]
        public void Test_PassedTrue_ThrowsException()
        {
            Arguments.Test(true, "", "");
        }

        [Test]
        public void Test_PassedFalse_JustReturns()
        {
            Arguments.Test(false, "", "");
        }

        [Test]
        public void Test_PassedTrue_ThrowsExceptionWithCorrectDetails()
        {
            ArgumentException caughtFromTestCall = null;

            string paramName = "parameter name";
            string dummyMessage = "test message";
            try
            {
                Arguments.Test(true, dummyMessage, paramName);
            }
            catch (ArgumentException ex)
            {
                caughtFromTestCall = ex;
            }

            string formattedMessage = String.Format("{0}\r\nParameter name: {1}", dummyMessage, paramName);

            Assert.IsNotNull(caughtFromTestCall);
            Assert.AreEqual(paramName, caughtFromTestCall.ParamName);
            Assert.AreEqual(formattedMessage, caughtFromTestCall.Message);
        }

        [Test]
        public void NotNull_PassedNonNull_JustReturns()
        {
            object dummyObject = new object();

            Arguments.NotNull(dummyObject, "");
        }

        [Test]
        [ExpectedArgumentException]
        public void NotNull_PassedNull_ThrowsException()
        {
            Arguments.NotNull(null, "");
        }
    }
}
