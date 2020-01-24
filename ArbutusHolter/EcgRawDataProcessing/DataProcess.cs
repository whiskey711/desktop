using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Uvic_Ecg_ArbutusHolter.EcgRawDataProcessing
{
    class DataProcess
    {
        private byte[] data;
        private static readonly int MAX_INPUT = (1 << 16) - 1;
        private static readonly int DIV = (1 << 15) - 1;
        private static readonly double MAX_VOLTAGE = 4.84d;
        private Filter_Queue filter = new Filter_Queue();
        public DataProcess(byte[] Data)
        {
            this.data = Data;
        }
        public DataProcess()
        {
        }
        public double[] process()
        {
            double[] dataIndouble = new double[data.Length];
            int count = 0;
            foreach(int value in data)
            {
                dataIndouble[count] = dataConvert(value);
                count++;
            }
            return dataIndouble;
        }
        public double dataConvert(int data)
        {
            double convertedData = convertInput(data);
            if (!filter.isFull())
            {
                filter.push(convertedData);
                return 0;
            }
            double tmp = filter.pop();
            filter.push(convertedData);
            return tmp / 12 * 1000;
        }
        private double convertInput(int input)
        {
            if (input > DIV)
            {
                input = -(((~input) & MAX_INPUT) + 1);
            }
            return input * 1.0d / DIV * MAX_VOLTAGE;
        }
        public byte[] getData()
        {
            return data;
        }
        public void setData(byte[] Data)
        {
            this.data = Data;
        }
    }
}
