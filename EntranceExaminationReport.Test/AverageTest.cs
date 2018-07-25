using Microsoft.VisualStudio.TestTools.UnitTesting;
using TomasKubes.EntraceExaminationReport.Reports;

namespace TomasKubes.EntranceExaminationReport.Test
{
    [TestClass]
    public class AverageTest
    {
        [TestMethod]
        public void TestAverage123()
        {
            double delta = 0.001;
            Average avg = new Average();

            Assert.AreEqual(double.NaN, avg.Value());

            avg.Add(1);
            Assert.AreEqual(1, avg.Value(), delta);

            avg.Add(2);
            Assert.AreEqual(1.5, avg.Value(), delta);

            avg.Add(3);
            Assert.AreEqual(2, avg.Value(), delta);

            avg.Add(0);
            avg.Add(0);
            avg.Add(0);

            Assert.AreEqual(1, avg.Value(), delta);
        }
    }
}
