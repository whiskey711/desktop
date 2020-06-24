using System;
using System.Linq;
using NUnit.Framework;

namespace ECG_ISHNE.Tests
{
    [TestFixture]
    public class MyExtensionsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SetStringTest()
        {
            string str = "me";
            char[] ch = new char[3];
            ch.Set(str);
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new char[] { 'm', 'e', '\0' }));

            str = " ";
            ch = new char[2];
            ch.Set(str);
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new char[] { ' ', '\0' }));

            str = "meee";
            ch = new char[2];
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => MyExtensions.Set(ch, str));

            str = "";
            ch = new char[2];
            Assert.Throws(typeof(ArgumentNullException), () => MyExtensions.Set(ch, str));

            str = null;
            ch = new char[2];
            Assert.Throws(typeof(ArgumentNullException), () => MyExtensions.Set(ch, str));
        }

        [Test]
        public void SetIntTest()
        {
            int num = 2;
            char[] ch = new char[3];
            ch.Set(num);
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new char[] { '2', '\0', '\0' }));

            num = 200;
            ch = new char[4];
            ch.Set(num);
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new char[] { '2', '0', '0', '\0' }));

            num = -1;
            ch = new char[] { };
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => MyExtensions.Set(ch, num));

            num = 20;
            ch = new char[1];
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => MyExtensions.Set(ch, num));

        }

        [Test]
        public void SetResolutionsTest()
        {
            string str = "2000 5000";
            short[] ch = new short[2];
            ch.SetResolutions(str);
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new short[] { 2000, 5000}));

        }

        [Test]
        public void SetDefaultTest()
        {
            short[] ch = new short[5];
            ch.SetDefault();
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new short[] { -9, -9, -9, -9, -9 }));

            ch = new short[1];
            ch.SetDefault();
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new short[] { -9 }));


            ch = new short[0];
            Assert.Throws(typeof(ArgumentNullException), () => MyExtensions.SetDefault(ch));

        }

        [Test]
        public void SetDateTest()
        {
            string str = "03/02/2020";  // March 2nd, 2020
            short[] ch = new short[3];
            ch.SetDate(str);
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new short[] { 2, 3, 2020 }));

            str = "03022020";
            ch = new short[3];
            Assert.Throws(typeof(FormatException), () => MyExtensions.SetDate(ch, str));
        }

        [Test]
        public void SetDateTimeTest()
        {
            DateTime date = new DateTime(2020, 3, 15);
            short[] ch = new short[3];
            ch.SetDate(date);
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new short[] { 15, 3, 2020 }));

            //date = new DateTime();
            //ch = new short[3];
            //ch.SetDate(date);
            //Assert.IsTrue(Enumerable.SequenceEqual(ch, new short[] { 0, 0, 0 }));

            ch = new short[3];
            Assert.Throws(typeof(ArgumentNullException), () => MyExtensions.SetDate(ch, null));

        }

        [Test]
        public void SetTimeTest()
        {
            DateTime date = new DateTime(2020, 12, 30, 23, 58, 59, 999);
            short[] ch = new short[3];
            ch.SetTime(date);
            Assert.IsTrue(Enumerable.SequenceEqual(ch, new short[] { 23, 58, 59 }));
        }

        [Test]
        public void SetSexTest()
        {
            string str = "Man";
            short[] ch = new short[1];
            MyExtensions.SetSex(ch, str);
            Assert.AreEqual(ch[0], 1);

            str = "Ma";
            ch = new short[1];
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => MyExtensions.SetSex(ch, str));

            ch = new short[1];
            Assert.Throws(typeof(ArgumentNullException), () => MyExtensions.SetSex(ch, null));
        }

    }
}
