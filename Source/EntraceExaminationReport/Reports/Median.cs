using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasKubes.EntraceExaminationReport.Reports
{
    public class Median
    {
        List<double> _list = new List<double>();

        public void Add(int result)
        {
            _list.Add(result);
        }

        public double Value()
        {
            if (_list.Count == 0)
                return Double.NaN;

            _list.Sort();

            if (_list.Count % 2 == 1)
                return _list[_list.Count/ 2];
            else
            {
                int half = _list.Count / 2;
                return (_list[half] + _list[half - 1]) / 2;
            }
        }
    }
}
