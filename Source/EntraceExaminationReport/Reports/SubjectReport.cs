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
        public StudentsGroup StudentsGroup { get; set; }
        public double AverageResult { get; set; }
        public double MedianResult { get; set; }
        public int ModusResult { get; set; }
    }

    public class SubjectReport
    {
        public List<SubjectReportItem> Collection = new List<SubjectReportItem>();

        public void Compute(ExaminationSet set)
        {
            Collection.Clear();
            Subject[] subjects = (Subject[])Enum.GetValues(typeof(Subject));
            StudentsGroup[] groups = (StudentsGroup[])Enum.GetValues(typeof(StudentsGroup));

            Dictionary<SubjectStudenGroup, Average> average = new Dictionary<SubjectStudenGroup, Average>();
            Dictionary<SubjectStudenGroup, Median> median = new Dictionary<SubjectStudenGroup, Median>();
            Dictionary<SubjectStudenGroup, Modus> modus = new Dictionary<SubjectStudenGroup, Modus>();

            // preparation empty collections
            foreach (Subject subject in subjects)
            {
                foreach (StudentsGroup group in groups)
                {
                    SubjectStudenGroup ssg = new SubjectStudenGroup() { StudentsGroup = group, Subject = subject, };
                    average.Add(ssg, new Average());
                    median.Add(ssg, new Median());
                    modus.Add(ssg, new Modus());
                }
            }

            ComputeSubjectReport(set, groups, average, median, modus);
            CollectResults(subjects, groups, average, median, modus);
        }

        private static void ComputeSubjectReport(ExaminationSet set, StudentsGroup[] groups, Dictionary<SubjectStudenGroup, Average> average, Dictionary<SubjectStudenGroup, Median> median, Dictionary<SubjectStudenGroup, Modus> modus)
        {
            foreach (StudentsGroup group in groups)
            {
                foreach (Examination exam in set.GetGroup(group))
                {
                    foreach (var subjectResult in exam.Results)
                    {
                        SubjectStudenGroup ssg = new SubjectStudenGroup() { StudentsGroup = group, Subject = subjectResult.Key, };

                        average[ssg].Add(subjectResult.Value);
                        median[ssg].Add(subjectResult.Value);
                        modus[ssg].Add(subjectResult.Value);
                    }
                }
            }
        }

        private void CollectResults(Subject[] subjects, StudentsGroup[] groups, Dictionary<SubjectStudenGroup, Average> average, Dictionary<SubjectStudenGroup, Median> median, Dictionary<SubjectStudenGroup, Modus> modus)
        {
            foreach (Subject subject in subjects)
            {
                foreach (StudentsGroup group in groups)
                {
                    SubjectStudenGroup ssg = new SubjectStudenGroup() { StudentsGroup = group, Subject = subject, };

                    SubjectReportItem reportItem = new SubjectReportItem()
                    {
                        AverageResult = average[ssg].Value(),
                        MedianResult = median[ssg].Value(),
                        ModusResult = modus[ssg].Value(),
                        Subject = ssg.Subject,
                        StudentsGroup = ssg.StudentsGroup,
                    };
                    Collection.Add(reportItem);
                }
            }
        }
    }
}

