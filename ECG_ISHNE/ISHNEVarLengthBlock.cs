namespace ECG_ISHNE
{
    /// <summary>
    /// The variable-length block will consist simply of a stream of ASCII(extended
    /// set of 256 characters) characters that any user or manufacturer will use according to his needs.
    /// </summary>
    public class ISHNEVarLengthBlock
    {
        /// <summary>
        /// a stream of ASCII(extendedset of 256 characters) characters that any user or manufacturer will use according to his needs.
        /// </summary>
        public char[] Reserved { get; private set; }

        public uint Length { get => (uint)(Reserved == null ? 0 : Reserved.Length); }

        public string Content
        {
            set
            {
                Reserved = !string.IsNullOrEmpty(value) ? value.ToCharArray() : null;
            }
        }

        public byte[] Serialize()
        {
            byte[] VarLengthBlock = new byte[Length];
            Utility.Copy(Reserved, VarLengthBlock, 0);
            return VarLengthBlock;
        }
    }
}
