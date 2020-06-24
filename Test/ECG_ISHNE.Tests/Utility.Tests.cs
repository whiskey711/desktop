using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Uvic_Ecg_Model;

namespace ECG_ISHNE.Tests
{
    [TestFixture]
    public class UtilityTest
    {
        object[] parameter1;


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CopyByteArrayTest()
        {
            byte[] src = new byte[] { 1, 2 };
            byte[] dest = new byte[4];
            int res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(2, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 1, 2, 0, 0 }));

            src = new byte[] { 0, 1, 2, 3 };
            dest = new byte[2];
            res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(2, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 0, 1 }));

            src = new byte[] { 1, 4, 2, 3 };
            dest = new byte[2];
            res = Utility.Copy(src, dest, 1);
            Assert.AreEqual(2, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 0, 1 }));
        }

        [Test]
        public void CopyByteArrayExceptionTest()
        {
            Assert.Throws(typeof(ArgumentNullException),
                () => Utility.Copy((byte[])null, new byte[2], 1));

            Assert.Throws(typeof(ArgumentNullException),
                () => Utility.Copy(new byte[2], null, 0));

            Assert.Throws(typeof(ArgumentOutOfRangeException),
                () => Utility.Copy(new byte[2], new byte[2], -2));

            Assert.Throws(typeof(ArgumentOutOfRangeException),
                () => Utility.Copy(new byte[2], new byte[2], 4));
        }

        [Test]
        public void CopyCharArrayTest()
        {
            char[] src = new char[] { 'a', 'b' };
            byte[] dest = new byte[4];
            int res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(2, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 97, 98, 0, 0 }));
        }

        [Test]
        public void CopyShortTest()
        {
            short src = 20;
            byte[] dest = new byte[4];
            int res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(2, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 20, 0, 0, 0 }));
        }

        [Test]
        public void CopyShortArrayTest()
        {
            short[] src = new short[] { -1, 0, 0 }; // 11111111 11111111
            byte[] dest = new byte[2];
            int res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(2, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 255, 255 }));

            src = new short[]{ -24, 0, 0 }; //11111111 11101000
            dest = new byte[4];
            res = Utility.Copy(src, dest, 2);
            Assert.AreEqual(4, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 0, 0, 232, 255}));
        }

        [Test]
        public void CopyUintTest()
        {
            uint src = 16779216; //00000001 00000000 00000111 11010000
            byte[] dest = new byte[4];
            int res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(4, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 208, 7 , 0, 1}));
        }

        [Test]
        public void CopyStringTest()
        {
            string src = "";
            byte[] dest = new byte[4];
            int res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(0, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 0, 0, 0, 0 }));

            src = "123456";
            dest = new byte[4];
            res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(4, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 49, 50, 51, 52 }));

            Assert.Throws(typeof(ArgumentNullException),
               () => Utility.Copy((string)null, new byte[2], 1));
        }

        [Test]
        public void Copy2DByteArrayTest()
        {
            byte[] src1 = new byte[] { 4 };
            byte[] src2 = new byte[] { 1, 3 };
            byte[][] src = new byte[][] { src1, src2 };
            byte[] dest = new byte[4];
            int res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(3, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 4, 1, 3, 0 }));

            src1 = new byte[0];
            src2 = new byte[] { 4 };
            src = new byte[][] { src1, src2 };
            dest = new byte[4];
            res = Utility.Copy(src, dest, 0);
            Assert.AreEqual(1, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 4, 0, 0, 0 }));

            src1 = new byte[] { 9, 8, 7 };
            src2 = new byte[] { 4 };
            src = new byte[][] { src1, src2 };
            dest = new byte[4];
            res = Utility.Copy(src, dest, 1);
            Assert.AreEqual(4, res);
            Assert.IsTrue(Enumerable.SequenceEqual(dest, new byte[] { 0, 9, 8, 7 }));

            src1 = new byte[] { 4 };
            src2 = null;
            src = new byte[][] { src1, src2 };
            dest = new byte[4];
            Assert.Throws(typeof(ArgumentNullException),
               () => Utility.Copy(src, dest, 0));

            src1 = null;
            src2 = new byte[] { 4 };
            src = new byte[][] { src1, src2 };
            dest = new byte[4];
            Assert.Throws(typeof(ArgumentNullException),
              () => Utility.Copy(src, dest, 0));
        }

        [Test]
        public void ConvertToByteArrayTest()
        {
            short[] src = new short[] { 1, 2 }; // 
            byte[] res = Utility.ConvertToByteArray(src);
            Assert.IsTrue(Enumerable.SequenceEqual(res, new byte[] { 1, 0, 2, 0 }));


            Assert.Throws(typeof(ArgumentNullException),
               () => Utility.ConvertToByteArray((short[])null));
        }
    }
}