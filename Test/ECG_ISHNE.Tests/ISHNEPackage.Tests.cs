using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;

namespace ECG_ISHNE.Tests
{
    [TestFixture]
    public class ISHNEPackageTest
    {

        ISHNEPackage obj;
        List<string> paths;
        static byte[] a = new byte[] { 2, 1, 4, 3 };
        static byte[] b = new byte[] { 6, 5, 8, 7 };
        static byte[] c = null;
        byte[][] temp = new byte[][] { a, b, c };

        [SetUp]
        public void Setup()
        {
            byte[] one = new byte[] { 0, 1, 2, 3, 4 };
            byte[] two = new byte[] { 0, 5, 6, 7, 8, 0, 9, 10 };
            byte[] three = new byte[] { 0 };

            FileStream fs;
            try
            {
                fs = File.Create(@"rawData\testIn1");
            }
            catch (Exception e)
            {
                return;
            }
            try
            {
                fs.Write(one, 0, one.Length);
                fs.Close();
            }
            catch (Exception e) {
                return;
            }

            fs = File.Create(@"rawData\testIn2");
            fs.Write(two, 0, two.Length);
            fs.Close();

            fs = File.Create(@"rawData\testIn3");
            fs.Write(three, 0, three.Length);
            fs.Close();

            paths = new List<string>();
            paths.Add(@"rawData\testIn1");
            paths.Add(@"rawData\testIn2");
            paths.Add(@"rawData\testIn3");
        }

        [Test]
        public void ReadDataFromFilesTest()
        {
            obj = new ISHNEPackage();
            obj.ReadDataFromFiles(paths);
            Assert.IsTrue(Enumerable.SequenceEqual(obj.Data[0], temp[0]));
            Assert.IsTrue(Enumerable.SequenceEqual(obj.Data[1], temp[1]));

            Assert.AreEqual(null, obj.Data[2]);
            Assert.AreEqual(null, temp[2]);
        }

        [Test]
        public void SerializeTest()
        {
            obj = new ISHNEPackage();
            obj.ReadDataFromFiles(paths);
            byte[] res = obj.Serialize();

            byte[] mc = new byte[] { 73, 83, 72, 78, 69, 49, 46, 48, 53, 67 }; // 53, 67
            byte[] h = obj.Header.Serialize();
            byte[] target = new byte[mc.Length + h.Length + a.Length + b.Length];
            Array.Copy(mc, 0, target, 0, mc.Length);
            Array.Copy(h, 0, target, mc.Length, h.Length);
            Utility.Copy(temp, target, mc.Length + h.Length);
            Assert.IsTrue(Enumerable.SequenceEqual(res, target));
        }

        [Test]
        public void WriteToFileTest()
        {
            string pa = @"rawData\testOut";
            obj = new ISHNEPackage();
            obj.ReadDataFromFiles(paths);
            obj.WriteToFile(pa);

            using (SHA256 mySHA256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(pa))
                {
                    var hash0 = mySHA256.ComputeHash(stream);
                    var hash1 = mySHA256.ComputeHash(obj.Serialize());
                    Assert.IsTrue(Enumerable.SequenceEqual(hash0, hash1));
                    stream.Close();
                    File.Delete(pa);
                }
            }
        }      
    }
}
