using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasKubes.EntraceExaminationReport.Reports
{
    public class SubjectStudenGroup
    {
        public Subject Subject { get; set; }
        public StudentsGroup StudentsGroup { get; set; }

        public override int GetHashCode()
        {
            return (int)Subject + ((int)StudentsGroup << 16);
        }

        public override bool Equals(object obj)
        {
            SubjectStudenGroup ssg = obj as SubjectStudenGroup;
            if (ssg == null)
                return false;
            return Subject == ssg.Subject && Subject == ssg.Subject;
        }
    }

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

        internal void Compute(ExaminationSet set)
        {
            Collection.Clear();
            Subject[] subjects = (Subject[])Enum.GetValues(typeof(Subject));
            StudentsGroup[] groups = (StudentsGroup[])Enum.GetValues(typeof(StudentsGroup));

            Dictionary<SubjectStudenGroup, Average> average = new Dictionary<SubjectStudenGroup, Average>();
            Dictionary<SubjectStudenGroup, Median> median = new Dictionary<SubjectStudenGroup, Median>();
            Dictionary<SubjectStudenGroup, Modus> modus = new Dictionary<SubjectStudenGroup, Modus>();

            foreach (Subject subject in subjects)
            {
                foreach (StudentsGroup group in groups)
                {
                    SubjectStudenGroup ssg = new SubjectStudenGroup(){StudentsGroup = group, Subject = subject,};

                    average.Add(ssg, new Average());
                    median.Add(ssg, new Median());
                    modus.Add(ssg, new Modus());
                }
            }

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
                        StudentsGroup =ssg.StudentsGroup,
                    };
                    Collection.Add(reportItem);
                }            
            }
        }
    }
}

