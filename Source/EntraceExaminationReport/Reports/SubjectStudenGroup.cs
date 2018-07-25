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
}
