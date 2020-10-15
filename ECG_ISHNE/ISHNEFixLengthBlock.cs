using System;
using Uvic_Ecg_Model;

namespace ECG_ISHNE
{
    /// <summary>
    /// The fixed-length (512 bytes) header block.
    /// </summary>
    public class ISHNEFixLengthBlock
    {
        #region constant
        /// <summary>
        /// Fixed-length (512 bytes) of header block.
        /// </summary>
        public static readonly uint FIX_BLOCK_LEN = 512;
        private static readonly short RESOLUTION_DEFALUT = 4103;

        /// <summary>
        /// Default sample rate : 250
        /// </summary>
        private static readonly ushort SAMPLE_RATE = 250;

        private static readonly short SEX_UNKNOWN = 0;
        private static readonly short SEX_MALE = 1;
        private static readonly short SEX_FEMALE = 2;

        private static readonly short RACE_UNKNOWN = 0;
        private static readonly short RACE_CAUCASIAN = 1;
        private static readonly short RACE_BLACK = 2;
        private static readonly short RACE_ORIENTAL = 3;

        private static readonly short LEAD_I = 5;
        private static readonly short LEAD_II = 6;
        #endregion

        /// <summary>
        /// Size(in bytes) of variable length block: 4 bytes
        /// </summary>
        public uint VarLengthBlockSize { get; set; }

        /// <summary>
        /// Size (in samples) of ECG: 4 bytes
        /// The actual size in bytes will be twice of the size in sample multiplied by number of leads
        /// (one ECG sample will be stored in two bytes
        /// </summary>
        public uint SampleSizeECG { get; set; }

        /// <summary>
        /// Offset of variable length block(in bytes from beginning of file): 4 byte
        /// </summary>
        public uint OffsetVarLengthBlock { get; private set; }

        /// <summary>
        /// Offset of ECG block(in bytes from beginning of life): 4 bytes
        /// </summary>
        public uint OffsetECGBlock { get => OffsetVarLengthBlock + VarLengthBlockSize; }

        /// <summary>
        /// Version of the file: 2 bytes
        /// </summary>
        public short FileVersion { get; private set; }

        /// <summary>
        /// Subject first name: 40 bytes
        /// </summary>
        public char[] FirstName { get; private set; }
        public string FirstNameStr { set { FirstName.Set(value); } }

        /// <summary>
        /// Subject last name: 40 bytes
        /// </summary>
        public char[] LastName { get; private set; }
        public string LastNameStr { set { LastName.Set(value); } }

        /// <summary>
        /// Subject ID: 20 bytes
        /// </summary>
        public char[] Id { get; private set; }
        public int IdInt { set { Id.Set(value); } }

        /// <summary>
        /// Subject sex: (0: unknown, 1: male, 2:female) 2 bytes
        /// </summary>
        public short Sex { get => Ssex[0]; }
        public short[] Ssex { get; private set; }
        public string SexStr { set { Ssex.SetSex(value); } }

        /// <summary>
        /// Subject race: (0: unknown, 1: Caucasian, 2:Black, 3: Oriental, 4-9: Reserved) 2 bytes
        /// </summary>
        public short Race { get; private set; }

        /// <summary>
        /// Date of birth(European: day, month, year): 6 bytes
        /// </summary>
        public short[] BirthDate { get; private set; }
        public string BirthDateStr { set { BirthDate.SetDate(value); } }

        /// <summary>
        /// Date of recording (European: day, month, year): 6 bytes
        /// </summary>
        public short[] RecordDate { get; private set; }
        public DateTime RecordDateStr { set { RecordDate.SetDate(value); } }

        /// <summary>
        /// Date of creation of Output file (European): 6 bytes
        /// </summary>
        public short[] FileDate { get; private set; }
        public DateTime FileDateStr { set { FileDate.SetDate(value); } }

        /// <summary>
        /// Start time (European: hour[0-23], min, sec): 6 bytes
        /// </summary>
        public short[] StartTime { get; private set; }
        public DateTime StartTimeStr { set { StartTime.SetTime(value); } }

        /// <summary>
        /// Number of stored leads: 2 bytes
        /// </summary>
        public short NLeads { get; private set; }

        /// <summary>
        /// Lead specification:  2 * 12 bytes
        /// </summary>
        public short[] LeadSpec { get; private set; }

        /// <summary>
        /// Lead quality: 2* 12 bytes
        /// </summary>
        public short[] LeadQual { get; private set; }

        /// <summary>
        /// Amplitude resolution in integer no.of nV: 2* 12 bytes
        /// </summary>
        public short[] Resolution { get; private set; }
        public string ResolutionStr { set { Resolution.SetResolutions(value); } }

        /// <summary>
        /// Pacemaker code: 2 bytes
        /// This will be set to 0 (nopacemaker), 1 (pacemaker type not known),
        /// 2 (single chamber unipolar ], 3 (dual chamber unipolar], 4 (single chamber bipolar), or 5 (dual chamber bipolar).
        /// </summary>
        public short Pacemaker { get; private set; }
        public string PacemakerStr { set { Pacemaker.SetPackmaker(value); } }

        /// <summary>
        /// Type of recorder (either analog or digital): 40 bytes
        /// </summary>
        public char[] Recorder { get; private set; }
        public string RecorderStr { set { Recorder.Set(value); } }

        /// <summary>
        /// Sampling rate (in hertz): 2 bytes
        /// </summary>
        public ushort SamplingRate { get; private set; }

        /// <summary>
        /// Proprietary of ECG (if any): 80 bytes
        /// </summary>
        public char[] Proprietary { get; private set; }
        public string ProprietaryStr { set { Proprietary.Set(value); } }

        /// <summary>
        /// Copyright and restriction of diffusion(if any): 80 bytes
        /// </summary>
        public char[] Copyright { get; private set; }
        public string CopyrightStr { set { Copyright.Set(value); } }

        /// <summary>
        /// 88 bytes
        /// </summary>
        public char[] Reserved { get; private set; }
        public string ReservedStr { set { Reserved.Set(value); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public ISHNEFixLengthBlock()
        {
            VarLengthBlockSize = 0;
            SampleSizeECG = 0;
            OffsetVarLengthBlock = 522;
            FileVersion = 1;

            FirstName = new char[40];
            FirstName[0] = '\0';

            LastName = new char[40];
            LastName[0] = '\0';

            Id = new char[20];
            Id[0] = '\0';

            Ssex = new short[1];
            Ssex[0] = SEX_UNKNOWN;
            Race = RACE_UNKNOWN;

            BirthDate = new short[3];

            RecordDate = new short[3];
  
            FileDate = new short[3];
        
            StartTime = new short[3];

            NLeads = 2;

            LeadSpec = new short[12];
            LeadSpec.SetDefault();
            LeadSpec[0] = LEAD_I;
            LeadSpec[1] = LEAD_II;

            LeadQual = new short[12];
            LeadQual.SetDefault();
            LeadQual[0] = 0;
            LeadQual[1] = 0;

            Resolution = new short[12];
            Resolution.SetDefault();
            Resolution[0] = RESOLUTION_DEFALUT;
            Resolution[1] = RESOLUTION_DEFALUT;

            Pacemaker = 0;

            Recorder = new char[40];
            Recorder[0] = '\0';

            SamplingRate = SAMPLE_RATE;

            Proprietary = new char[80];
            Proprietary[0] = '\0';

            Copyright = new char[80];
            Copyright[0] = '\0';

            Reserved = new char[88];
            Reserved[0] = '\0';
        }

        public PatientInfo PatientInfo
        {
            set
            {
                FirstNameStr = value.PatientFirstName;
                LastNameStr = value.PatientLastName;
                IdInt = value.PatientId;
                SexStr = value.Gender;
                BirthDateStr = value.Birthdate;
            }
        }

        public EcgTest EcgTest
        {
            set
            {
                RecordDateStr = value.StartTime;
                FileDateStr = value.StartTime;
                StartTimeStr = value.StartTime;
            }
        }

        public byte[] Serialize()
        {
            byte[] FixLengthBlcok = new byte[ISHNEFixLengthBlock.FIX_BLOCK_LEN];

            int desIdx = 0;
            desIdx = Utility.Copy(VarLengthBlockSize, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(SampleSizeECG, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(OffsetVarLengthBlock, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(OffsetECGBlock, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(FileVersion, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(FirstName, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(LastName, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(Id, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(Sex, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(Race, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(BirthDate, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(RecordDate, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(FileDate, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(StartTime, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(NLeads, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(LeadSpec, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(LeadQual, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(Resolution, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(Pacemaker, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(Recorder, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(SamplingRate, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(Proprietary, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(Copyright, FixLengthBlcok, desIdx);
            desIdx = Utility.Copy(Reserved, FixLengthBlcok, desIdx);

            return FixLengthBlcok;
        }
    }
}