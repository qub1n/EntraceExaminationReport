using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasKubes.EntraceExaminationReport.Reports
{
    public class Modus
    {
        int[] _countPervalue = new int[100];

        internal void Add(int value)
        {
            _countPervalue[value]++;
        }

        internal int Value()
        {
            int max = -1;
            int maxIndex = 0;
            for (int i = 0; i < _countPervalue.Length; i++)
            {
                if (_countPervalue[i] > max)
                {
                    max = _countPervalue[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
    }
}
