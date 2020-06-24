using System.Collections.Generic;
using Uvic_Ecg_Model;

namespace ECG_ISHNE
{
    /// <summary>
    /// Convert the raw data form sensor to ISHNE data format due to mathch the input of data analysis tools.
    /// Also, add information into ISHNE format data.
    /// The sequential structure of the Standard ISHNE Output File :
    /// three blocks of data preceded by a magic number and a checksum calculated over the two blocks of the header.
    /// Magic number + CRC + Header( fixed length block + var length block) + ECG data
    /// </summary>
    public class ISHNEUtility
    {
        /// <summary>
        /// Interface to output standard ISHNE file
        /// </summary>
        /// <param name="rawDataPaths"></param>
        /// <param name="patientInfo"></param>
        /// <param name="test"></param>
        /// <param name="outputPath"></param>
        public static void ConvertToISHNE(List<string> rawDataPaths, PatientInfo patientInfo, EcgTest ecgTest, string outputPath)
        {
            ConvertToISHNE(rawDataPaths, patientInfo, ecgTest, outputPath, "");
        }

        /// <summary>
        /// Interface with addition message need to be stored in ISHNE file
        /// </summary>
        /// <param name="rawDataPaths"></param>
        /// <param name="patientInfo"></param>
        /// <param name="test"></param>
        /// <param name="outputPath"></param>
        /// <param name="varLengthBlockMessage"></param>
        public static void ConvertToISHNE(List<string> rawDataPaths, PatientInfo patientInfo, EcgTest ecgTest, string outputPath, string varLengthBlockMessage)
        {
            ISHNEPackage package = new ISHNEPackage();
            package.ReadDataFromFiles(rawDataPaths);
            package.PatientInfo = patientInfo;
            package.EcgTest = ecgTest;
            package.VarLengthBlockContent = varLengthBlockMessage;
            package.WriteToFile(outputPath);
        }

        public static void Main()
        {

        }
    }
}