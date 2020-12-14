using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uvic_Ecg_Model
{
    public class FileRelatedException : Exception
    {
        public FileRelatedException()
        {
        }

        public FileRelatedException(string message)
            : base(message)
        {
        }

        public FileRelatedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
