using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasKubes.EntraceExaminationReport
{
    public class Examination
    {
        public string Name { get; set; }

        public Dictionary<Subject, int> Results = new Dictionary<Subject, int>();

        public void Deserialize(string line)
        {
            int sujectsCount = Enum.GetValues(typeof(Subject)).Length;

            var parts = line.Split(';');
            if (parts.Length != sujectsCount + 1)
                throw new ArgumentException($"Incorrect number of parts in line, expected {sujectsCount} parts, but found only {parts.Length}");

            foreach (var part in parts)
            {
                string[] keyValue = part.Split('=');
                if (keyValue.Length == 1)
                {
                    Name = keyValue[0];
                }
                else
                {
                    Subject subject = (Subject)Enum.Parse(typeof(Subject), keyValue[0]);
                    int result = int.Parse(keyValue[1]);
                    if (result < 0 || result > 100)
                        throw new ArgumentOutOfRangeException($"Expected result {result} for subject {subject} is out of range 0-100");
                    if( Results.ContainsKey(subject))
                        throw new ArgumentOutOfRangeException($"Result from subject {subject} is duplicated.");

                    Results.Add(subject, result);
                }
            }            
        }
    }
}
