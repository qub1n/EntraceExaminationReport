using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            ReportCorruptedRecords(parseWarnigns, opts);

            set.MakeReports(opts.OutputDirectory, opts.ReportFormat);
        }

        private static void ReportCorruptedRecords(CorruptedInputWarning[] parseWarnigns, CommandOptions opts)
        {
            if (parseWarnigns.Length == 0)
                return;

            string path = Path.Combine(opts.OutputDirectory, opts.CorruptedReportFileName + "." +  opts.ReportFormat);

            IReportSerializer serializer = GetSerializer(opts.ReportFormat);
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
