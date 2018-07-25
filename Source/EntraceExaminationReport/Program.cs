using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TomasKubes.EntraceExaminationReport.Reports;
using TomasKubes.EntraceExaminationReport.Serialization;

namespace TomasKubes.EntraceExaminationReport
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<CommandOptions>(args)
                .WithParsed<CommandOptions>(opts => CheckParsed(opts))
                .WithParsed<CommandOptions>(opts => RunOptionsAndReturnExitCode(opts));
        }

        private static void CheckParsed(CommandOptions opts)
        {
            if (!File.Exists(opts.InputFileName))
                throw new FileNotFoundException($"Specified file {opts.InputFileName} does not exists.");

            if(!Directory.Exists(opts.OutputDirectory))
                throw new DirectoryNotFoundException($"Specified Output Directory {opts.OutputDirectory} does not exists.");
        }

        private static void RunOptionsAndReturnExitCode(CommandOptions opts)
        {
            ExaminationSet set = new ExaminationSet();
            CorruptedInputWarning[] parseWarnigns = set.Deserialize(opts.InputFileName)
                .ToArray();

            IReportSerializer serializer = GetSerializer(opts.ReportFormat);

            ReportCorruptedRecords(serializer, parseWarnigns, opts);

            MakeReports(serializer, opts.OutputDirectory, set, opts);
        }

        private static void MakeReports(IReportSerializer serializer, string outputDirectory, ExaminationSet set, CommandOptions opts)
        {
            string pathStudentReport = Path.Combine(opts.OutputDirectory, opts.StudentReportFileName + "." + opts.ReportFormat);

            StudentReport studentReport = new StudentReport();
            studentReport.Compute(set);
            serializer.Serialize(pathStudentReport, studentReport);

            string pathSubjectReport = Path.Combine(opts.OutputDirectory, opts.SubjectReportFileName + "." + opts.ReportFormat);

            SubjectReport subjectReport = new SubjectReport();
            subjectReport.Compute(set);
            serializer.Serialize(pathSubjectReport, subjectReport);
        }

        private static void ReportCorruptedRecords(IReportSerializer serializer, CorruptedInputWarning[] parseWarnigns, CommandOptions opts)
        {
            if (parseWarnigns.Length == 0)
                return;

            string path = Path.Combine(opts.OutputDirectory, opts.CorruptedReportFileName + "." +  opts.ReportFormat);

           
            serializer.Serialize(path, parseWarnigns);
        }

        private static IReportSerializer GetSerializer(ReportFormat reportFormat)
        {
            switch (reportFormat)
            {
                case ReportFormat.XML:
                    return new XmlReportSerializer();
                case ReportFormat.JSON:
                    return new JSonReportSerializer();                    ;
                default:
                    throw new NotSupportedException(reportFormat.ToString());
            }
        }
    }
}
