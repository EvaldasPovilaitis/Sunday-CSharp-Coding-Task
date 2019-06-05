namespace MunicipalitiesTaxesCalculations.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Models;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Storage;

    [TestClass]
    public class MunicipalitiesTaxesCalculatorTests
    {
        private Mock<IMunicipalitiesTaxesSchedulesStorage> municipalitiesTaxesSchedulesStorageMock;

        [TestInitialize]
        public void TestInitialize()
            =>
                this.municipalitiesTaxesSchedulesStorageMock = new Mock<IMunicipalitiesTaxesSchedulesStorage>();

        [TestMethod]
        public void GivenNullMunicipalitiesTaxesSchedulesStorageConstructorThrowsArgumentNullException()
            =>
                Assert.ThrowsException<ArgumentNullException>(() => new MunicipalitiesTaxesCalculator(null));

        [TestMethod]
        public void GivenNonExistingMunicipalityReturnsZeroTaxes()
        {
            this.municipalitiesTaxesSchedulesStorageMock
                .Setup(s => s.GetMunicipalityTaxesSchedules(It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(() => Enumerable.Empty<MunicipalityTaxSchedule>());

            var result = new MunicipalitiesTaxesCalculator(this.municipalitiesTaxesSchedulesStorageMock.Object)
                .Calculate("Unknown", DateTime.Now);

            Assert.AreEqual(0M, result);
        }

        [TestMethod]
        public void GivenExistingMunicipalityReturnsItsTaxes()
        {
            var municipality = "Vilnius";
            var date = new DateTime(2016, 03, 04);
            var tax = 0.2M;

            this.municipalitiesTaxesSchedulesStorageMock
                .Setup(s => s.GetMunicipalityTaxesSchedules(municipality, date))
                .Returns(() => new[] 
                {
                    new MunicipalityTaxSchedule
                    {
                        Municipality = municipality,
                        Type = MunicipalityTaxScheduleType.Yearly,
                        Tax = tax,
                        StartDate = new DateTime(2016, 01, 01),
                        EndDate = new DateTime(2016, 12, 31)
                    }
                });

            var result = new MunicipalitiesTaxesCalculator(this.municipalitiesTaxesSchedulesStorageMock.Object)
                .Calculate(municipality, date);

            Assert.AreEqual(tax, result);
        }

        [TestMethod]
        public void GivenOverlappingYearlyAndMonthlySchedulesMonthlyTakesPrecedence()
        {
            var municipality = "Vilnius";
            var date = new DateTime(2016, 03, 04);
            var monthlyTax = 0.4M;

            this.municipalitiesTaxesSchedulesStorageMock
                .Setup(s => s.GetMunicipalityTaxesSchedules(municipality, date))
                .Returns(() => new[]
                {
                    new MunicipalityTaxSchedule
                    {
                        Municipality = municipality,
                        Type = MunicipalityTaxScheduleType.Yearly,
                        Tax = 0.2M,
                        StartDate = new DateTime(2016, 01, 01),
                        EndDate = new DateTime(2016, 12, 31)
                    },
                    new MunicipalityTaxSchedule
                    {
                        Municipality = municipality,
                        Type = MunicipalityTaxScheduleType.Monthly,
                        Tax = monthlyTax,
                        StartDate = new DateTime(2016, 03, 01),
                        EndDate = new DateTime(2016, 03, 31)
                    }
                });

            var result = new MunicipalitiesTaxesCalculator(this.municipalitiesTaxesSchedulesStorageMock.Object)
                .Calculate(municipality, date);

            Assert.AreEqual(monthlyTax, result);
        }

        [TestMethod]
        public void GivenOverlappingMonthlyAndWeeklySchedulesWeeklyTakesPrecedence()
        {
            var municipality = "Vilnius";
            var date = new DateTime(2016, 03, 04);
            var weaklyTax = 0.3M;

            this.municipalitiesTaxesSchedulesStorageMock
                .Setup(s => s.GetMunicipalityTaxesSchedules(municipality, date))
                .Returns(() => new[]
                {
                    new MunicipalityTaxSchedule
                    {
                        Municipality = municipality,
                        Type = MunicipalityTaxScheduleType.Monthly,
                        Tax = 0.4M,
                        StartDate = new DateTime(2016, 03, 01),
                        EndDate = new DateTime(2016, 03, 31)
                    },
                    new MunicipalityTaxSchedule
                    {
                        Municipality = municipality,
                        Type = MunicipalityTaxScheduleType.Weekly,
                        Tax = weaklyTax,
                        StartDate = new DateTime(2016, 02, 29),
                        EndDate = new DateTime(2016, 03, 06)
                    }
                });

            var result = new MunicipalitiesTaxesCalculator(this.municipalitiesTaxesSchedulesStorageMock.Object)
                .Calculate(municipality, date);

            Assert.AreEqual(weaklyTax, result);
        }

        [TestMethod]
        public void GivenOverlappingWeeklyAndDailySchedulesDailyTakesPrecedence()
        {
            var municipality = "Vilnius";
            var date = new DateTime(2016, 03, 04);
            var dailyTax = 0.1M;

            this.municipalitiesTaxesSchedulesStorageMock
                .Setup(s => s.GetMunicipalityTaxesSchedules(municipality, date))
                .Returns(() => new[]
                {
                    new MunicipalityTaxSchedule
                    {
                        Municipality = municipality,
                        Type = MunicipalityTaxScheduleType.Weekly,
                        Tax = 0.3M,
                        StartDate = new DateTime(2016, 02, 29),
                        EndDate = new DateTime(2016, 03, 06)
                    },
                    new MunicipalityTaxSchedule
                    {
                        Municipality = municipality,
                        Type = MunicipalityTaxScheduleType.Daily,
                        Tax = dailyTax,
                        StartDate = new DateTime(2016, 03, 04),
                        EndDate = new DateTime(2016, 03, 04)
                    }
                });

            var result = new MunicipalitiesTaxesCalculator(this.municipalitiesTaxesSchedulesStorageMock.Object)
                .Calculate(municipality, date);

            Assert.AreEqual(dailyTax, result);
        }
    }
}
