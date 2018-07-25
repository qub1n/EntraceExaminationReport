using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntraceExaminationReport
{
    public class Examination
    {
        public string Name { get; set; }

        public Dictionary<Subject, int> Results = new Dictionary<Subject, int>();
    }
}
