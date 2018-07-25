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

        [TestMethod]
        [Description("Test that user is warned if subject is duplicated.")]
        public void TestDuplicateSubject()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
           {
                "Entrance examination",
                "",
                "Group1",
                "Alba Hopper;Math=60;Math=38;English=65"
           };
            var warnigns = set.Deserialize(lines).ToArray();
            AssertSingleMessage(warnigns, 3, "math is duplicated");
        }

        [TestMethod]
        [Description("Test that user is warned if subject is missing.")]
        public void TestSubjectMissing()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
           {
                "Entrance examination",
                "",
                "Group1",
                "Alba Hopper;Math=60;Math=38"
           };
            var warnigns = set.Deserialize(lines).ToArray();
            AssertSingleMessage(warnigns, 3, "incorrect number of parts in line");
        }

        [TestMethod]
        [Description("Test that user is warned if subject is unknown.")]
        public void TestUnknownSubject()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
           {
                "Entrance examination",
                "",
                "Group1",
                "Alba Hopper;Math=60;Physics=38;German=65"
           };
            var warnigns = set.Deserialize(lines).ToArray();
            AssertSingleMessage(warnigns, 3, "german");
        }

        [TestMethod]
        [Description("Test that one exam result is correctly parsed.")]
        public void TestParseOneExam()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
           {
                "Entrance examination",
                "",
                "Group1",
                "Alba Hopper;Math=60;Physics=38;English=65"
           };
            var warnigns = set.Deserialize(lines).ToArray();
            Assert.AreEqual(0, warnigns.Length);

            Assert.AreEqual(1, set.GetGroup(StudentsGroup.Group1).Count);
            Assert.AreEqual(0, set.GetGroup(StudentsGroup.Group2).Count);
            Assert.AreEqual(0, set.GetGroup(StudentsGroup.Group3).Count);

            var list = set.GetGroup(StudentsGroup.Group1);
            var exam = list.Single();

            Assert.AreEqual("Alba Hopper", exam.Name);
            Assert.AreEqual(60, exam.Results[Subject.Math]);
            Assert.AreEqual(38, exam.Results[Subject.Physics]);
            Assert.AreEqual(65, exam.Results[Subject.English]);
        }

        [TestMethod]
        [Description("Test that parsing failed with appropriate message if result is out of range.")]
        public void TestResultOver100()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
           {
                "Entrance examination",
                "",
                "Group1",
                "Alba Hopper;Math=101;Physics=38;English=65"
           };
            var warnigns = set.Deserialize(lines).ToArray();
            AssertSingleMessage(warnigns, 3, "math");
        }

        [TestMethod]
        [Description("Test that one exam result is correctly parsed.")]
        public void TestParseMultipleGroups()
        {
            ExaminationSet set = new ExaminationSet();
            var lines = new string[]
           {
                "Entrance examination",
                "",
                "Group1",
                "Alba Hopper;Math=60;Physics=38;English=65",
                "Harrison Perez;Math=25;Physics=13;English=40",
                "",
                "Group2",
                "Celeste Blake; Math = 98; Physics = 45; English = 92",
                "Kristy Mendez; Math = 65; Physics = 50; English = 78",
                "Cheyenne Almond;Math=87;Physics=34;English=45",
                "",
                "Group3",
                "Hettie Durham;Math=74;Physics=55;English=46",
           };
            var warnigns = set.Deserialize(lines).ToArray();
            Assert.AreEqual(0, warnigns.Length);

            Assert.AreEqual(2, set.GetGroup(StudentsGroup.Group1).Count);
            Assert.AreEqual(3, set.GetGroup(StudentsGroup.Group2).Count);
            Assert.AreEqual(1, set.GetGroup(StudentsGroup.Group3).Count);

            var harrison = set.GetGroup(StudentsGroup.Group1)[1];            
            Assert.AreEqual("Harrison Perez", harrison.Name);
            Assert.AreEqual(25, harrison.Results[Subject.Math]);
            Assert.AreEqual(13, harrison.Results[Subject.Physics]);
            Assert.AreEqual(40, harrison.Results[Subject.English]);

            var cheyenne = set.GetGroup(StudentsGroup.Group2)[2];
            Assert.AreEqual("Cheyenne Almond", cheyenne.Name);
            Assert.AreEqual(87, cheyenne.Results[Subject.Math]);
            Assert.AreEqual(34, cheyenne.Results[Subject.Physics]);
            Assert.AreEqual(45, cheyenne.Results[Subject.English]);

            var durham = set.GetGroup(StudentsGroup.Group3)[0];
            Assert.AreEqual("Hettie Durham", durham.Name);
            Assert.AreEqual(74, durham.Results[Subject.Math]);
            Assert.AreEqual(55, durham.Results[Subject.Physics]);
            Assert.AreEqual(46, durham.Results[Subject.English]);
        }
    }
}
