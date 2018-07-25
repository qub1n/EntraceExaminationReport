using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntraceExaminationReport
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
            set.Deserialize(opts.InputFileName);

            set.MakeReports(opts.OutputDirectory, opts.ReportFormat);
        }
    }
}
