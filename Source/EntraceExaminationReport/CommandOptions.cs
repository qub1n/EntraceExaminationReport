using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomasKubes.EntraceExaminationReport
{

    public class CommandOptions
    {
        [Option('i', "input", Required = true, HelpText = "Input file to be processed.")]
        public string InputFileName { get; set; }

        [Option('f', "format", Required = false, Default = ReportFormat.XML, HelpText = "Output files format (XML/JSON).")]
        public ReportFormat ReportFormat { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output directory for saving reports.")]
        public string OutputDirectory { get; set; }

        [Option('c', "CorruptedReportFileName", Required = false, Default = "corrupted", HelpText = "Name of file for recording corrupted input without extension.")]
        public string CorruptedReportFileName { get; set; }

        [Option('s', "StudentReportFileName", Required = false, Default = "students", HelpText = "Name of report file with content: For each student count average using the following weights: Math 40%, Physics 35%, English 25%.")]
        public string StudentReportFileName { get; set; }
    }
}
