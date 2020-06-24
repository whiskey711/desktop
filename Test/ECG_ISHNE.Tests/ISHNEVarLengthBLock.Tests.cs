using System;
using NUnit.Framework;

namespace ECG_ISHNE.Tests
{
    [TestFixture]
    public class ISHNEVarLengthBlockTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SerializeTest()
        {
            ISHNEVarLengthBlock obj = new ISHNEVarLengthBlock();
            obj.Content = "message";
            byte[] res = obj.Serialize();
            Assert.AreEqual(obj.Length, 7);
            Assert.AreEqual(res, new byte[7] { 109, 101, 115, 115, 97, 103, 101});

            obj = new ISHNEVarLengthBlock();
            obj.Content = " ";
            res = obj.Serialize();
            Assert.AreEqual(obj.Length, 1);
            Assert.AreEqual(res, new byte[1] { 32 });

            obj = new ISHNEVarLengthBlock();
            obj.Content = "";
            Assert.Throws(typeof(ArgumentNullException),
               () => obj.Serialize());

            obj = new ISHNEVarLengthBlock();
            obj.Content = null;
            Assert.Throws(typeof(ArgumentNullException),
               () => obj.Serialize());

        }
    }
}
