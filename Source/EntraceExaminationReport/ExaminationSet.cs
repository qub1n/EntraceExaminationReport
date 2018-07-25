using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntraceExaminationReport
{
    public class ExaminationSet
    {
        Dictionary<StudentsGroup, List<Examination>> _set = new Dictionary<StudentsGroup, List<Examination>>();

        public void Deserialize(string path)
        {
            throw new NotImplementedException();
        }       

        internal void MakeReports(string directoryOutput, ReportFormat format)
        {
            throw new NotImplementedException();
        }
    }
}
