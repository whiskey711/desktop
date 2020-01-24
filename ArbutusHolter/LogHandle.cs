using System;
using System.IO;
namespace Uvic_Ecg_ArbutusHolter
{
    public class LogHandle
    {
        public static void Log(string message, string trace, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{message}");
            w.WriteLine($"  :{trace}");
            w.WriteLine("-------------------------------");
        }
        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}
