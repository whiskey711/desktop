using System;
using System.Text;
using Uvic_Ecg_Model;

namespace ECG_ISHNE
{
    /// <summary>
    /// The main goal of the header is to provide all necessary information on the associated ECG file.
    /// ISHNE Header( fixed length of 512 bytes block + variable length block )
    /// </summary>
    public class ISHNEHeader
    {
        /// <summary>
        /// 512 bytes
        /// </summary>
        public ISHNEFixLengthBlock FixLengthBlock { get; private set; }

        /// <summary>
        /// var bytes
        /// </summary>
        public ISHNEVarLengthBlock VarLengthBlock { get; private set; }

        /// <summary>
        ///
        /// </summary>
        public uint Length { get => ISHNEFixLengthBlock.FIX_BLOCK_LEN + VarLengthBlock.Length; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ISHNEHeader()
        {
            FixLengthBlock = new ISHNEFixLengthBlock();
            VarLengthBlock = new ISHNEVarLengthBlock();
        }

        public PatientInfo PatientInfo { set => FixLengthBlock.PatientInfo = value; }

        public EcgTest EcgTest { set => FixLengthBlock.EcgTest = value; }

        public string VarLengthBlockContent
        {
            set
            {
                VarLengthBlock.Content = value;
                FixLengthBlock.VarLengthBlockSize = VarLengthBlock.Length;
            }
        }

        public uint SampleSizeECG { set => FixLengthBlock.SampleSizeECG = value; }

        /// <summary>
        /// Serialize all elements in ISHNE header into a single byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize()
        {
            byte[] headerBlcok = new byte[ISHNEFixLengthBlock.FIX_BLOCK_LEN + VarLengthBlock.Length];

            int desIdx = 0;

            desIdx = Utility.Copy(FixLengthBlock.Serialize(), headerBlcok, desIdx);
            if (VarLengthBlock.Length > 0)
            {
                desIdx = Utility.Copy(VarLengthBlock.Serialize(), headerBlcok, desIdx);
            }

            return headerBlcok;
        }
    }
}
