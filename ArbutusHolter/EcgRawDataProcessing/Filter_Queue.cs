using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Uvic_Ecg_ArbutusHolter.EcgRawDataProcessing
{
    class Filter_Queue
    {
        private static readonly int LIST_SIZE = 21;
        private readonly Queue<Double> Q = new Queue<double>();
        private readonly List<Double> h = new List<double>();
        public Filter_Queue()
        {
            // Convert 1~45 (out of 250) Hz to radian
            double omec1 = 2.0 / 250 * Math.PI;
            double omec2 = 90.0 / 250 * Math.PI;
            for (int i = 0; i < LIST_SIZE / 2; ++i)
            {
                h.Add((Math.Sin((i - LIST_SIZE / 2) * omec2) - Math.Sin((i - LIST_SIZE / 2) * omec1)) / ((i - LIST_SIZE / 2) * Math.PI)
                        * (0.54 - 0.46 * Math.Cos(Math.PI * i / (LIST_SIZE / 2))));
            }
            h.Add((omec2 - omec1) / Math.PI);
            for (int i = LIST_SIZE / 2 - 1; i >= 0; --i)
            {
                h.Add(h[i]);
            }
        }
        public int get_size()
        {
            return Q.Count();
        }
        public bool isFull()
        {
            return get_size() >= LIST_SIZE;
        }
        public void push(double value)
        {
            if (isFull())
                return;
            Q.Enqueue(value);
        }
        public double pop()
        {
            if (!isFull())
                return -1;
            int i = 20;
            double sum = 0;
            foreach (double value in Q)
            {
                sum += value * h[i--];
            }
            Q.Dequeue();
            return sum;
        }
    }
}
