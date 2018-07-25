﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntraceExaminationReport
{
    public class ExaminationSet
    {
        Dictionary<StudentsGroup, List<Examination>> _set = new Dictionary<StudentsGroup, List<Examination>>();

        public IEnumerable<CorruptedInputWarning> Deserialize(string path)
        {
            var lines = File.ReadAllLines(path, Encoding.ASCII);

            return ProcessLines(lines);
        }

        private IEnumerable<CorruptedInputWarning> ProcessLines(string[] lines)
        {
            //stav stavove automatu na parsovani souboru
            StudentsGroup? currentGroup = null;

            for (int i = 1; i < lines.Length; i++) //first line is skipped
            {
                string trimmedLine = lines[i].Trim();

                if (string.IsNullOrEmpty(trimmedLine))
                {
                    // space separator of groups found
                    currentGroup = null;
                }
                else if (currentGroup == null)
                {
                    StudentsGroup group;
                    if (!Enum.TryParse<StudentsGroup>(trimmedLine, out group))
                    {
                        yield return new CorruptedInputWarning()
                        {
                            Message = $"Uknown group {trimmedLine} found.",
                            LineNumber = i,
                        };

                        //skiping corrupted record to first empty line
                        while (i + 1 < lines.Length && string.IsNullOrWhiteSpace(lines[i + 1]))
                        {
                            i++;
                        }
                    }
                    currentGroup = group;
                }
                else
                {
                    CorruptedInputWarning warning = ParseLine(currentGroup.Value, trimmedLine);
                    if (warning != null)
                        yield return warning;
                }
            }          
        }

        private CorruptedInputWarning ParseLine(StudentsGroup group, string line)
        {
            Examination exam = new Examination();
            CorruptedInputWarning warning = exam.Deserialize(line);
            if (warning != null)
                return warning;

            Add(group, exam);
            return null;
        }

        private void Add(StudentsGroup group, Examination exam)
        {
            throw new NotImplementedException();
        }

        internal void MakeReports(string directoryOutput, ReportFormat format)
        {
            throw new NotImplementedException();
        }
    }
}
