using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasKubes.EntraceExaminationReport.Reports
{
    public class StudentReportItem
    {
        public string Name { get; set; }
        public double AverageResult { get; set; }
    }

    public class StudentReport
    {
        public List<StudentReportItem> Collection = new List<StudentReportItem>();

        public void Compute(ExaminationSet set)
        {
            foreach (StudentsGroup studentsGroup in Enum.GetValues(typeof(StudentsGroup)))
            {
                foreach (Examination exam in set.GetGroup(studentsGroup))
                {
                    StudentReportItem reportItem = new StudentReportItem()
                    {
                        Name = exam.Name,
                        AverageResult = ComputeAverage(exam.Results),
                    };
                    Collection.Add(reportItem);
                }
            }
        }

        private double ComputeAverage(Dictionary<Subject, int> results)
        {
            int math = results[Subject.Math];
            int physics = results[Subject.Physics];
            int english = results[Subject.English];


            return math * 0.4 + physics * 0.35 + english * 0.25;
        }
    }
}
