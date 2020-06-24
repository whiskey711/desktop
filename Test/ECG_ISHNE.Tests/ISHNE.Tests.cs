using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using Uvic_Ecg_Model;

namespace ECG_ISHNE.Tests
{
    [TestFixture]
    public class ISHNEUtilityTest
    {
        ISHNEPackage obj;
        List<string> paths;
        PatientInfo patientInfo;
        EcgTest ecgTest;
        string outputPath;
        string varLengthBlockMessage;

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
            catch (Exception e)
            {
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

            patientInfo = new PatientInfo(123456789,
                           "LastName",
                           "MidName",
                           "FirstName",
                           "01/02/2020",
                           "address1",
                           "addres2",
                           "provin",
                           "City",
                           "mailaddress",
                           "phnum",
                           "phoneNum",
                           "workNum",
                           "homeNum",
                           "Man",
                           "poscode",
                           true,
                           00000,
                           "1",
                           "superPhysi",
                           "Weight",
                           "Height",
                           "Indications",
                           "Medications",
                           "referPhysi",
                           "Remark",
                           "Age");
            ecgTest = new EcgTest(new DateTime(2011, 03, 16, 06, 45, 13),
                           new DateTime(),
                           new DateTime(),
                           patientInfo.PatientId,
                           0000,
                           0000,
                           "commenti",
                           0000);

            outputPath = @"\rawData\TestOut2";
            varLengthBlockMessage = @"It is var length block content.";

        }

        [Test]
        public void ConvertToISHNETest()
        {
            obj = new ISHNEPackage();
            obj.ReadDataFromFiles(paths);
            obj.PatientInfo = patientInfo;
            obj.EcgTest = ecgTest;

            ISHNEUtility.ConvertToISHNE(paths, patientInfo, ecgTest, outputPath);
            using (SHA256 mySHA256 = SHA256.Create())
            {

                using (var stream = File.OpenRead(outputPath))
                {
                    var hash0 = mySHA256.ComputeHash(stream);
                    var hash1 = mySHA256.ComputeHash(obj.Serialize());
                    Assert.IsTrue(Enumerable.SequenceEqual(hash0, hash1));
                    stream.Close();
                    File.Delete(outputPath);
                }
            }

        }

        [Test]
        public void ConvertToISHNEWithMessTest()
        {
            obj = new ISHNEPackage();
            obj.ReadDataFromFiles(paths);
            obj.PatientInfo = patientInfo;
            obj.EcgTest = ecgTest;
            obj.VarLengthBlockContent = varLengthBlockMessage;

            ISHNEUtility.ConvertToISHNE(paths, patientInfo, ecgTest, outputPath, varLengthBlockMessage);

            using (SHA256 mySHA256 = SHA256.Create())
            {

                using (var stream = File.OpenRead(outputPath))
                {
                    var hash0 = mySHA256.ComputeHash(stream);
                    var hash1 = mySHA256.ComputeHash(obj.Serialize());
                    Assert.IsTrue(Enumerable.SequenceEqual(hash0, hash1));
                    stream.Close();
                    File.Delete(outputPath);
                }
            }
        }
    }
}