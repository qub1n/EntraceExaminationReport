using Microsoft.VisualStudio.TestTools.UnitTesting;
using TomasKubes.EntraceExaminationReport.Reports;

namespace TomasKubes.EntranceExaminationReport.Test
{

    [TestClass]
    public class ModusTest
    {
        [TestMethod]
        public void TestModus123()
        {            
            Modus m = new Modus();

            Assert.AreEqual(0, m.Value());

            m.Add(1);
            Assert.AreEqual(1, m.Value());

            m.Add(2);
            Assert.AreEqual(1, m.Value());

            m.Add(2);
            Assert.AreEqual(2, m.Value());

            m.Add(3);
            Assert.AreEqual(2, m.Value());

            m.Add(0);
            m.Add(0);
            Assert.AreEqual(0, m.Value());
        }
    }
}
