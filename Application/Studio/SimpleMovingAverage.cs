using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJetronicStudio
{
    // adapted from: https://andrewlock.net/creating-a-simple-moving-average-calculator-in-csharp-1-a-simple-moving-average-calculator/
    internal class SimpleMovingAverage
    {
        private readonly int _k;
        private readonly double[] _values;

        private int _index = 0;
        private double _sum = 0;

        public SimpleMovingAverage
            (
            int k
            )
        {
            if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k), "Must be greater than 0");

            _k = k;
            _values = new double[k];
        }

        public double Update
            (
            double nextInput
            )
        {
            // calculate the new sum
            _sum = _sum - _values[_index] + nextInput;

            // overwrite the old value with the new one
            _values[_index] = nextInput;

            // increment the index (wrapping back to 0)
            _index = (_index + 1) % _k;

            // calculate the average
            return ((double)_sum) / _k;
        }

        public void Clear
            (
            )
        {
            _index = 0;
            Array.Clear(_values, 0, _values.Length);
        }
    }
}
