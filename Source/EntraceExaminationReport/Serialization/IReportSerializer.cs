using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasKubes.EntraceExaminationReport.Serialization
{
    public interface IReportSerializer
    {
        void Serialize(string path, object obj);
    }
}

    
