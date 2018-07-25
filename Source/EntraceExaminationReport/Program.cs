using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntraceExaminationReport
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathInput = "";
            string directoryOutput = "";
            ReportFormat format = ReportFormat.XML;

            ExaminationSet set = new ExaminationSet();
            set.Deserialize(pathInput);

            set.MakeReports(directoryOutput, format);
        }
    }
}
