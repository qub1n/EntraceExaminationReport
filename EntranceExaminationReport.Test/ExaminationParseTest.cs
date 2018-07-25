using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TomasKubes.EntraceExaminationReport;

namespace TomasKubes.EntranceExaminationReport.Test
{
    [TestClass]
    public class ExaminationParseTest
    {
        [TestMethod]
        public void TestWarningNoSpaceAfterTitle()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
            {
                "Entrance examination",
                "Group1"
            };
            var parseWarnigns = set.Deserialize(lines)
                .ToArray();

            Assert.AreEqual(1, parseWarnigns.Length);

            Assert.AreEqual(1, parseWarnigns.Single().LineNumber);
            Assert.IsTrue(parseWarnigns.Single().Message.Contains("no space after title"));
        }
    }
}
