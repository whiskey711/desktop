using System;
using System.IO;
using System.Text;

namespace ECG_ISHNE
{
    public class Utility
    {
        /// <summary>
        /// Fill header Block with source
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static int Copy(byte[] src, byte[] dest, int offset)
        {
            if (src == null || dest == null)
            {
                throw new ArgumentNullException();
            }
            else if (offset < 0 || offset >= dest.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int pos = offset;
            for (int i = 0; i < src.Length && pos < dest.Length; i++, pos++)
            {
                dest[pos] = src[i];
            }
            return pos;
        }

        /// <summary>
        /// Fill header Block with source
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int Copy(char[] src, byte[] dest, int offset)
        {
            return Copy(Encoding.ASCII.GetBytes(src), dest, offset);
        }

        /// <summary>
        /// Fill header Block with source
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int Copy(short src, byte[] dest, int offset)
        {
            return Copy(new short[] {src}, dest, offset);
        }

        /// <summary>
        /// Fill header Block with source
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int Copy(short[] src, byte[] dest, int offset)
        {
            return Copy(ConvertToByteArray(src), dest, offset);
        }

        /// <summary>
        /// Fill header Block with source
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int Copy(uint src, byte[] dest, int offset)
        {
            return Copy(BitConverter.GetBytes(src), dest, offset);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int Copy(string src, byte[] dest, int offset)
        {
            return Copy(Encoding.ASCII.GetBytes(src), dest, offset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int Copy(byte[][] src, byte[] dest, int offset)
        {
            if (src == null)
            {
                throw new ArgumentNullException();
            }

            int idx = offset;
            for (int i = 0; i < src.Length && idx < dest.Length; i++)
            {
                idx = Copy(src[i], dest, idx);
            }
            return Math.Min(idx, dest.Length);
        }

        /// <summary>
        /// Convert short array to byte array, store in an little_endian form (LSB first)
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(short[] src)
        {
            if (src == null)
            {
                throw new ArgumentNullException();
            }

            byte[] dest = new byte[src.Length * 2];
            int idx = 0;
            for (int i = 0; i < src.Length; i++)
            {
                // LBS  15: 0F-00     -15: F1-FF
                byte[] bs = BitConverter.GetBytes(src[i]);
                dest[idx++] = bs[0];
                dest[idx++] = bs[1];
            }
            return dest;
        }
    }
}
