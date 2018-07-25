using Microsoft.VisualStudio.TestTools.UnitTesting;
using TomasKubes.EntraceExaminationReport.Reports;

namespace TomasKubes.EntranceExaminationReport.Test
{
    [TestClass]
    public class MedianTest
    {
        [TestMethod]
        public void TestMedian123()
        {
            double delta = 0.001;
            Median m = new Median();

            Assert.AreEqual(double.NaN, m.Value());

            m.Add(1);
            Assert.AreEqual(1, m.Value(), delta);

            m.Add(2);
            Assert.AreEqual(1.5, m.Value(), delta);

            m.Add(3);
            Assert.AreEqual(2, m.Value(), delta);

            m.Add(0);
            m.Add(0);
            Assert.AreEqual(1, m.Value(), delta);
        }
    }
}
