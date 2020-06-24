using System;
using System.Linq;
using NUnit.Framework;

namespace ECG_ISHNE.Tests
{
    [TestFixture]
    public class ISHNEHeaderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SerializeTest()
        {
            ISHNEHeader obj = new ISHNEHeader();
            obj.VarLengthBlockContent = "m";
            byte[] res = obj.Serialize();

            ISHNEFixLengthBlock fixB = new ISHNEFixLengthBlock();
            fixB.VarLengthBlockSize = 1;
            byte[] f = fixB.Serialize();

            ISHNEVarLengthBlock varB = new ISHNEVarLengthBlock();
            varB.Content = "m";
            byte[] v = varB.Serialize();

            byte[] temp = new byte[f.Length + v.Length];
            Array.Copy(f, 0, temp, 0, f.Length);
            Array.Copy(v, 0, temp, f.Length, v.Length);
            Assert.IsTrue(Enumerable.SequenceEqual(temp, res));
        }
    }
}
