using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasKubes.EntraceExaminationReport.Reports
{
    public class SubjectReportItem
    {
        public Subject Subject { get; set; }
        public double AverageResult { get; set; }
        public double MedianResult { get; set; }
        public int ModusResult { get; set; }
    }

    public class SubjectReport
    {
        public List<SubjectReportItem> Collection = new List<SubjectReportItem>();

        internal void Compute(ExaminationSet set)
        {
            Collection.Clear();
            Subject[] subjects = (Subject[])Enum.GetValues(typeof(Subject));

            Dictionary<Subject, Average> average = new Dictionary<Subject, Average>();
            Dictionary<Subject, Median> median = new Dictionary<Subject, Median>();
            Dictionary<Subject, Modus> modus = new Dictionary<Subject, Modus>();

            foreach (Subject subject in subjects)
            {
                average.Add(subject, new Average());
                median.Add(subject, new Median());
                modus.Add(subject, new Modus());
            }

            foreach (StudentsGroup studentsGroup in Enum.GetValues(typeof(StudentsGroup)))
            {
                foreach (Examination exam in set.GetGroup(studentsGroup))
                {
                    foreach (var subjectResult in exam.Results)
                    {
                        average[subjectResult.Key].Add(subjectResult.Value);
                        median[subjectResult.Key].Add(subjectResult.Value);
                        modus[subjectResult.Key].Add(subjectResult.Value);
                    }
                }
            }

            foreach (Subject subject in subjects)
            {                
                SubjectReportItem reportItem = new SubjectReportItem()
                {
                    AverageResult = average[subject].Value(),
                    MedianResult = median[subject].Value(),
                    ModusResult = modus[subject].Value(),
                };
                Collection.Add(reportItem);
            }
        }
    }
}

