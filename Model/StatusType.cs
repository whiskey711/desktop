using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uvic_Ecg_Model
{
    public class StatusType
    {
        public enum Status {
            NOTSTARTED,
            HOOKUP,
            RECORDING,
            TERMINATED
        };
    }
}
