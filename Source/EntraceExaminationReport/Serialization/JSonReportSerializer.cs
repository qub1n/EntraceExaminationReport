using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TomasKubes.EntraceExaminationReport.Serialization
{
    public class JSonReportSerializer : IReportSerializer
    {
        public void Serialize(string path, object obj)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string json = ser.Serialize(obj);
            File.WriteAllText(path, json);
        }
    }
}
