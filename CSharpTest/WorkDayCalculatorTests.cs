using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2021, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count - 1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestOneDayWeedend()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 23))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 4, 26), result);
        }

        [TestMethod]
        public void TestNegativeDaysCalculation()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = -5;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(new DateTime(2021, 4, 21), result);
        }

        [TestMethod]
        public void TestSameDayWeedend()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 1;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 21), new DateTime(2021, 4, 21))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 4, 22), result);
        }

        [TestMethod]
        public void TestSameDayAndWeekendAfter()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 1;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 22), new DateTime(2021, 4, 24))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 4, 21), result);
        }

        [TestMethod]
        public void TestWeedendStartOverlap()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 19), new DateTime(2021, 4, 23))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 4, 28), result);
        }

        [TestMethod]
        public void TestWeedendsOutOfPeriod()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 17), new DateTime(2021, 4, 20)),
                new WeekEnd(new DateTime(2021, 4, 26), new DateTime(2021, 4, 28))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 4, 25), result);
        }

        [TestMethod]
        public void TestMultipleWeekendsInRow()
        {
            DateTime startDate = new DateTime(2021, 4, 12);
            int count = 8;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 13), new DateTime(2021, 4, 15)),
                new WeekEnd(new DateTime(2021, 4, 16), new DateTime(2021, 4, 18))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 4, 25), result);
        }

        [TestMethod]
        public void TestMultipleWeekendsGaps()
        {
            DateTime startDate = new DateTime(2021, 4, 12);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 13), new DateTime(2021, 4, 15)),
                new WeekEnd(new DateTime(2021, 4, 18), new DateTime(2021, 4, 21))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(new DateTime(2021, 4, 28), result);
        }
    }
}
