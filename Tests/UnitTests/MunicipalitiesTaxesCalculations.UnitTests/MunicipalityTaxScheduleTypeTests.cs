namespace MunicipalitiesTaxesCalculations.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Models;

    [TestClass]
    public class MunicipalityTaxScheduleTypeTests
    {
        [TestMethod]
        public void UnknownIsDefault()
            =>
                Assert.IsTrue(MunicipalityTaxScheduleType.Unknown == default);

        [TestMethod]
        public void YearlyIsLessThanMonthly()
            =>
                Assert.IsTrue(MunicipalityTaxScheduleType.Yearly < MunicipalityTaxScheduleType.Monthly);

        [TestMethod]
        public void MonthlyIsLessThanWeekly()
            =>
                Assert.IsTrue(MunicipalityTaxScheduleType.Monthly < MunicipalityTaxScheduleType.Weekly);

        [TestMethod]
        public void WeeklyIsLessThanDaily()
            =>
                Assert.IsTrue(MunicipalityTaxScheduleType.Weekly < MunicipalityTaxScheduleType.Daily);
    }
}
