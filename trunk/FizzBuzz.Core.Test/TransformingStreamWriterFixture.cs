using System;
using System.Collections.Generic;
using System.Text;

using MbUnit.Framework;
using Rhino.Mocks;

using FizzBuzz.Core;
using System.IO;

namespace FizzBuzz.Core.Test
{
    [TestFixture]
    public class TransformingStreamWriterFixture
    {
        private MockRepository mocks;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
        }

        
        [Test]
        [ExpectedArgumentException]
        public void Constructor_CalledWithNullTextWriter_ThrowsException()
        {
            ITransformer mockTransformer = mocks.CreateMock<ITransformer>();
            mocks.ReplayAll();

            TransformingTextWriter writer = new TransformingTextWriter(null, mockTransformer);

            mocks.VerifyAll();
        }


        [Test]
        [ExpectedArgumentException]
        public void Constructor_CalledWithNullTransformer_ThrowsException()
        {
            TextWriter mockTextWriter = mocks.CreateMock<TextWriter>();
            mocks.ReplayAll();

            TransformingTextWriter writer = new TransformingTextWriter(mockTextWriter, null);

            mocks.VerifyAll();
        }

        
        [Test]
        public void Write_WhenCalled_CallsUnderlyingTransformAndTextWriter()
        {
            TextWriter mockTextWriter = mocks.CreateMock<TextWriter>();
            ITransformer mockTransformer = mocks.CreateMock<ITransformer>();

            int dummyNumber = 12;
            string transformedNumber = "Transformed";

            Expect.Call(mockTransformer.Transform(dummyNumber)).Return(transformedNumber);

            mockTextWriter.WriteLine(transformedNumber);

            mocks.ReplayAll();

            TransformingTextWriter writer = new TransformingTextWriter(mockTextWriter, mockTransformer);
            writer.Write(dummyNumber);

            mocks.VerifyAll();
        }

    }
}
