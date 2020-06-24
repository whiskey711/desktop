using System;
using System.Linq;
using NUnit.Framework;

namespace ECG_ISHNE.Tests
{
    [TestFixture]
    public class ISHNEFixLengthBlockTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SerializeDefaultTest()
        {
            ISHNEFixLengthBlock obj = new ISHNEFixLengthBlock();
            byte[] res = obj.Serialize();
            byte[] des = new byte[4];

            Array.Copy(res, 0, des, 0, 4);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0 }));    // VarLengthBlockSize = 0;

            Array.Copy(res, 4, des, 0, 4);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0 }));    // SampleSizeECG = 0;

            Array.Copy(res, 8, des, 0, 4);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 10, 2, 0, 0 }));   // OffsetVarLengthBlock = 522; 10 00001010

            Array.Copy(res, 12, des, 0, 4);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 10, 2, 0, 0 }));    // OffsetECGBlock = 522: 10 00000000

            des = new byte[2];
            Array.Copy(res, 16, des, 0, 2);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 1, 0 }));      // short FileVersion = 1 

            des = new byte[40];
            Array.Copy(res, 18, des, 0, 40);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));    // FirstName 

            des = new byte[40];
            Array.Copy(res, 58, des, 0, 40);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));    // LastName

            des = new byte[20];
            Array.Copy(res, 98, des, 0, 20);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));    // Id

            des = new byte[2];
            Array.Copy(res, 118, des, 0, 2);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0 }));    // Sex

            des = new byte[2];
            Array.Copy(res, 120, des, 0, 2);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0 }));    // Race

            des = new byte[6];
            Array.Copy(res, 122, des, 0, 6);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0, 0, 0 }));  // BirthDate 0, 0, 0

            Array.Copy(res, 128, des, 0, 6);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0, 0, 0 })); // RecordDate

            Array.Copy(res, 134, des, 0, 6);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0, 0, 0 })); // FileDate

            Array.Copy(res, 140, des, 0, 6);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0, 0, 0 })); // StartTime

            des = new byte[2];
            Array.Copy(res, 146, des, 0, 2);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 2, 0 }));   // NLeads = 2

            des = new byte[24];
            Array.Copy(res, 148, des, 0, 24);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 5, 0, 6, 0, 247, 255, 247, 255, 247,
                255, 247, 255, 247, 255, 247, 255, 247, 255, 247, 255, 247, 255, 247, 255 })); // LeadSpec = -9: ????

            Array.Copy(res, 172, des, 0, 24);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0, 247, 255, 247, 255, 247,
               255, 247, 255, 247, 255, 247, 255, 247, 255, 247, 255, 247, 255, 247, 255 })); // LeadQual = -9:

            Array.Copy(res, 196, des, 0, 24);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 247, 255, 247, 255, 247, 255, 247, 255, 247,
               255, 247, 255, 247, 255, 247, 255, 247, 255, 247, 255, 247, 255, 247, 255 })); // Resolution = -9:

            des = new byte[2];
            Array.Copy(res, 220, des, 0, 2);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0 })); // Pacemaker

            des = new byte[40];
            Array.Copy(res, 222, des, 0, 40);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));

            des = new byte[2];
            Array.Copy(res, 262, des, 0, 2);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 250, 0 })); // 250 ??????

            des = new byte[80];
            Array.Copy(res, 264, des, 0, 80);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));

            des = new byte[80];
            Array.Copy(res, 344, des, 0, 80);
            Assert.IsTrue(Enumerable.SequenceEqual(des, new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0}));
        }
    }
}
