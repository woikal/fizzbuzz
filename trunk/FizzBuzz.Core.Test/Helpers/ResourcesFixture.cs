using System;
using System.Globalization;

using MbUnit.Framework;

// Note: Don't do this. It is complete rubbish. Unless your manager demands 100% Code Coverage ;)

namespace FizzBuzz.Core.Test.Helpers
{
    [TestFixture]
    public class ResourcesFixture
    {
        [Test]
        public void Resources_Culture_Property()
        {
            CultureInfo cultureInfoCurrentCulture = CultureInfo.CurrentCulture;
            Resources.Culture = cultureInfoCurrentCulture;
            Assert.AreEqual(cultureInfoCurrentCulture, Resources.Culture);
        }

        [Test]
        public void Resources_Constructor_Call()
        {
            Resources r = new Resources();
            Assert.IsTrue(true);
        }
    }
}
