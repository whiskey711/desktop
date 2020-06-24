using System;
namespace ECG_ISHNE
{
    public static class MyExtensions
    {
        /// <summary>
        /// Convert string to char array
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="value"></param>
        public static void Set(this char[] ch, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }
            else if (value.Length > ch.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = 0; i < value.Length; i++)
            {
                ch[i] = value[i];
            }
        }

        /// <summary>
        /// Convert integer to char array
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="value"></param>
        public static void Set(this char[] ch, int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            string ID = value.ToString();
            ch.Set(ID);
        }

        /// <summary>
        /// Convert string to short array
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="value"></param>
        public static void SetResolutions(this short[] ch, string value)
        {
            string[] resolutions = value.Split(' ');
            for (int i = 0; i < resolutions.Length; i++)
            {
                ch[i] = Convert.ToInt16(resolutions[i]);
            }
        }

        /// <summary>
        /// Set source array default with '-9'
        /// </summary>
        /// <param name="src"></param>
        public static void SetDefault(this short[] src)
        {
            if (src == null || src.Length == 0)
            {
                throw new ArgumentNullException();
            }
            for (int i = 0; i < src.Length; i++)
            {
                src[i] = -9;

            }
        }

        /// <summary>
        /// Convert string date to short array
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="value"></param>
        public static void SetDate(this short[] ch, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException();
            }

            DateTime date = DateTime.Parse(value);
            ch[0] = (short)date.Day;
            ch[1] = (short)date.Month;
            ch[2] = (short)date.Year;
        }

        /// <summary>
        /// Convert Date to short array
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="value"></param>
        public static void SetDate(this short[] ch, DateTime value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            ch[0] = (short)value.Day;
            ch[1] = (short)value.Month;
            ch[2] = (short)value.Year;
        }

        /// <summary>
        /// Convert DateTime to short array
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="value"></param>
        public static void SetTime(this short[] ch, DateTime value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            ch[0] = (short)value.Hour;
            ch[1] = (short)value.Minute;
            ch[2] = (short)value.Second;
        }

         /// <summary>
         /// Coverte string to short array
         /// </summary>
         /// <param name="ch"></param>
         /// <param name="value"></param>
         public static void SetSex(this short[] ch, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            value = value.ToLower();
            if (!value.Equals("man") && !value.Equals("male") && !value.Equals("woman") && !value.Equals("female"))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (value.Equals("man") || value.Equals("male"))
            {
                ch[0] = 1;
            }
            else if (value.Equals("woman") || value.Equals("female"))
            {
                ch[0] = 2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="value"></param>
        public static void SetPackmaker(this short ch, string value)
        {

        }
    }
}
