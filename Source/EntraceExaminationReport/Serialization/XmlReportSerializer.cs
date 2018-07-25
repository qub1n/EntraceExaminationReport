using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TomasKubes.EntraceExaminationReport.Serialization
{
    public class XmlReportSerializer : IReportSerializer
    {
        public void Serialize(string path, object obj)
        {
            XmlSerializer ser = new XmlSerializer(obj.GetType());            
            using (TextWriter writer = new StreamWriter(path))
            {
                ser.Serialize(writer, obj);
            }
        }
    }
}
