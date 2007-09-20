using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;


namespace FizzBuzz.Core.Test.Helpers
{
    static class TestCollections
    {

        // This beauty comes from http://www.4wednesdays.com/Bill/2006/09/17/unit-testing-part-1/
        // There's more too and I added to it to test the IEnumerator<T> separately (for Reset testing)

        public static void TestIEnumerable<T>(IEnumerable<T> enumerable, T[] expected, bool tryReset)
        {
            IEnumerator<T> enumerator = enumerable.GetEnumerator();

            TestIEnumerator<T>(enumerator, expected, tryReset);
        }

        public static void TestIEnumerator<T>(IEnumerator<T> enumerator, T[] expected, bool tryReset)
        {

            // check that iteration is equivalent to expected

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsTrue(enumerator.Current.Equals(expected[i]));
            }

            // check that iterator is done
            Assert.IsFalse(enumerator.MoveNext());

            // If we are testing the resetability of the Enumerator run again.
            if (tryReset)
            {
                enumerator.Reset();
                TestIEnumerator<T>(enumerator, expected, false);
            }
        }

        // Note that this is really just a non-generic rehash of the above

        public static void TestIEnumerable(IEnumerable enumerable, object[] expected, bool tryReset)
        {
            IEnumerator enumerator = enumerable.GetEnumerator();
            TestIEnumerator(enumerator, expected, tryReset);
        }

        public static void TestIEnumerator(IEnumerator enumerator, object[] expected, bool tryReset)
        {

            // check that iteration is equivalent to expected

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(enumerator.MoveNext());
                Assert.IsTrue(enumerator.Current.Equals(expected[i]));
            }

            // check that the iterator is done
            Assert.IsFalse(enumerator.MoveNext());

            // If we are testing the resetability of the Enumerator run again.
            if (tryReset)
            {
                enumerator.Reset();
                TestIEnumerator(enumerator, expected, false);
            }

        }

    }
}
