using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uvic_Ecg_Model
{
    public class FileName
    {
        private string name;
        public FileName(string name)
        {
            this.name = name;
        }
        public static readonly FileName Log = new FileName("log.txt");
        public static readonly FileName MailPdf = new FileName("");

        public string Name { get => name;}
    }
}
