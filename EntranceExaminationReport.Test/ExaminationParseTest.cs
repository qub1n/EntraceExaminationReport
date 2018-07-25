using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TomasKubes.EntraceExaminationReport;

namespace TomasKubes.EntranceExaminationReport.Test
{
    [TestClass]    
    public class ExaminationParseTest
    {
        void AssertSingleMessage(CorruptedInputWarning[] warnings, int line, string contains)
        {
            Assert.AreEqual(1, warnings.Length);
            Assert.AreEqual(line, warnings.Single().LineNumber);
            Assert.IsTrue(warnings.Single().Message.ToLower().Contains(contains));
        }

        [TestMethod]
        [Description("Test that user is warned if empty line after title is missing.")]
        public void TestWarningNoSpaceAfterTitle()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
            {
                "Entrance examination",
                "Group1"
            };
            var warnigns = set.Deserialize(lines).ToArray();

            AssertSingleMessage(warnigns, 1, "no space after title");          
        }

        [TestMethod]
        [Description("Test that user is warned if uknown group is found.")]
        public void TestWarningUknownGroup()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
            {
                "Entrance examination",
                "",
                "GroupX"
            };
            var warnigns = set.Deserialize(lines).ToArray();
            AssertSingleMessage(warnigns, 2, "uknown group");
        }

        [TestMethod]
        [Description("Test that user is warned if file is empty.")]
        public void TestWarningFileEmpty()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
            {                
            };
            var warnigns = set.Deserialize(lines).ToArray();
            AssertSingleMessage(warnigns, 0, "file is empty");
        }
    }
}
