using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasKubes.EntraceExaminationReport.Reports
{
    public class Average
    {
        int _count = 0;
        double _sum = 0.0;

        public void Add(int result)
        {
            _count++;
            _sum += result;
        }

        public double Value()
        {
            if (_count == 0)
                return Double.NaN;

            return _sum / _count;
        }
    }
}
