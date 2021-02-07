using System;
using System.Collections.Generic;
using System.IO;
using Uvic_Ecg_Model;

namespace ECG_ISHNE
{
    public class ISHNEPackage
    {
        #region constant
        /// <summary>
        /// MagicNumber 8 bytes + CheckSum 2 bytes
        /// </summary>
        private static int MAGICNUM_CRC_LEN = 10;

        private static string MAGIC_NUMBER = "ISHNE1.0";
        #endregion

        #region properties
        /// <summary>
        /// 8 bytes
        /// </summary>
        public string MagicNumber { get; private set; }

        /// <summary>
        /// 2 bytes
        /// </summary>
        public byte[] CheckSum { get { return CalculateCheckSum(); } }

        /// <summary>
        /// 512 + var bytes
        /// </summary>
        public ISHNEHeader Header { get; private set; }

        /// <summary>
        /// ECG test data
        /// </summary>
        public byte[][] Data { get; private set; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ISHNEPackage()
        {
            MagicNumber = MAGIC_NUMBER;
            Header = new ISHNEHeader();
        }

        /// <summary>
        /// Read Files into continues byte array
        /// </summary>
        /// <param name="FilePaths"></param>
        /// <returns></returns>
        public void ReadDataFromFiles(List<string> FilePaths)
        {
            Data = new byte[FilePaths.Count][];
            uint length = 0;

            for (int i = 0; i < FilePaths.Count; i++)
            {
                byte[] rawData;
                try
                {
                    rawData = File.ReadAllBytes(FilePaths[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "\n Cannot open file.");
                    throw;
                }

                uint samples = (uint)(rawData.Length / 5);
                length += samples;
                if (samples != 0)
                {
                    Data[i] = new byte[samples * 4];
                }

                ulong outputIdx = 0;
                for (int j = 0; j < 5 * samples; j++)
                {
                    if (j % 5 == 0)
                        continue;
                    else if (j % 5 == 1 || j % 5 == 3)
                    {
                        Data[i][outputIdx + 1] = rawData[j];
                    }
                    else
                    {
                        Data[i][outputIdx] = rawData[j];
                        outputIdx += 2;
                    }
                }
            }

            Header.SampleSizeECG = length * 2;
        }

        public PatientInfo PatientInfo { set => Header.PatientInfo = value; }

        public EcgTest EcgTest { set => Header.EcgTest = value; }

        public string VarLengthBlockContent { set => Header.VarLengthBlockContent = value; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="outputPath"></param>
        public void WriteToFile(string outputPath)
        {
            FileStream fs;
            try
            {
                fs = File.Create(outputPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n Cannot create file.");
                return;
            }
            try
            {
                byte[] pa = Serialize();
                fs.Write(pa, 0, pa.Length);
                fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n Cannot write file.");
                return;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize()
        {
            int len = (int)(Header.FixLengthBlock.SampleSizeECG * 2);
            byte[] PackageBlcok = new byte[MAGICNUM_CRC_LEN + (int) Header.Length + len];
            int desIdx = 0;

            desIdx = Utility.Copy(MagicNumber, PackageBlcok, desIdx);
            desIdx = Utility.Copy(CheckSum,PackageBlcok, desIdx);
            desIdx = Utility.Copy(Header.Serialize(), PackageBlcok, desIdx);
            foreach (byte[] e in Data)
            {
                if (e == null)
                    continue;
                desIdx = Utility.Copy(e, PackageBlcok, desIdx);
            }

            return PackageBlcok;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private byte[] CalculateCheckSum()
        {
            byte CRCHI = 0xff; // High Byte(most significant) of the 16 - bit CRC
            byte CRCLO = 0xff; // Low Byte (least significant) of the 16-bit CRC

            byte[] headerBlock = Header.Serialize();
            for (int i = 0; i < headerBlock.Length; i++)
            {
                byte A = headerBlock[i];
                A = (byte)(A ^ CRCHI);
                CRCHI = A;
                A = (byte)(A >> 4);                 // SHIFT A RIGHT FOUR TIMES {ZERO FILL}
                A = (byte)(A ^ CRCHI);              // {I J K L M N O P}
                CRCHI = CRCLO;                      // SWAP CRCHI, CRCLO
                CRCLO = A;
                A = (byte)((A >> 4) | (A << 4));    // ROTATE A LEFT 4 TIMES { M N O P I J K L}
                byte B = A;                         // TEMP SAVE
                A = (byte)((A >> 7) | (A << 1));    // ROTATE A LEFT ONCE {N O P I J K L M}
                A = (byte)(A & 0x1f);               // {0 0 0 I J K L M}
                CRCHI = (byte)(A ^ CRCHI);
                A = (byte)(B & 0xf0);               // {M N O P 0 0 0 0}
                CRCHI = (byte)(A ^ CRCHI);          // CRCHI complete
                B = (byte)((B >> 7) | (B << 1));    // ROTATE B LEFT ONCE {N O P 0 0 0 0 M}
                B = (byte)(B & 0xe0);               // {N O P 0 0 0 0 0}
                CRCLO = (byte)(B ^ CRCLO);          // CRCLO complete
            }

            byte[] checkSum = new byte[2];
            checkSum[0] = CRCLO;
            checkSum[1] = CRCHI;
            return checkSum;
        }
    }
}
