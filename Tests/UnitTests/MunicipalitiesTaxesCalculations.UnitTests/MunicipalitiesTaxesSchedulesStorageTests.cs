namespace MunicipalitiesTaxesCalculations.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Models;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Storage;

    [TestClass]
    public class MunicipalitiesTaxesSchedulesStorageTests
    {
        [TestMethod]
        public void GivenNullMunicipalitiesTaxesSchedulesContextConstructorThrowsArgumentNullException()
            =>
                Assert.ThrowsException<ArgumentNullException>(() => new MunicipalitiesTaxesSchedulesStorage(null));

        [TestMethod]
        public void GivenNonExistingMunicipalityReturnsEmptyEnumerable()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            try
            {
                connection.Open();

                DbContextOptions<MunicipalitiesTaxesSchedulesContext> options = 
                    new DbContextOptionsBuilder<MunicipalitiesTaxesSchedulesContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new MunicipalitiesTaxesSchedulesContext(options))
                {
                    context.Database.EnsureCreated();

                    Assert.IsFalse(
                        new MunicipalitiesTaxesSchedulesStorage(context)
                            .GetMunicipalityTaxesSchedules(
                                "Unknown",
                                DateTime.Now)
                            .Any());
                }
            }
            finally 
            {
                connection.Close();
            }
        }

        [TestMethod]
        public void GivenDateReturnsJustSchedulesWhereDateIsBetweenStartAndEndDatesOfTheSchedule()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            try
            {
                connection.Open();

                DbContextOptions<MunicipalitiesTaxesSchedulesContext> options =
                    new DbContextOptionsBuilder<MunicipalitiesTaxesSchedulesContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new MunicipalitiesTaxesSchedulesContext(options))
                {
                    context.Database.EnsureCreated();

                    context.MunicipalitiesTaxesSchedules.Add(
                        new MunicipalityTaxSchedule
                        {
                            Municipality = "Kaunas",
                            Type = MunicipalityTaxScheduleType.Yearly,
                            Tax = 0.1M,
                            StartDate = new DateTime(2016, 01, 01),
                            EndDate = new DateTime(2016, 12, 31)
                         });

                    context.MunicipalitiesTaxesSchedules.Add(
                        new MunicipalityTaxSchedule
                        {
                            Municipality = "Vilnius",
                            Type = MunicipalityTaxScheduleType.Yearly,
                            Tax = 0.2M,
                            StartDate = new DateTime(2016, 01, 01),
                            EndDate = new DateTime(2016, 12, 31)
                        });

                    context.MunicipalitiesTaxesSchedules.Add(
                        new MunicipalityTaxSchedule
                        {
                            Municipality = "Vilnius",
                            Type = MunicipalityTaxScheduleType.Monthly,
                            Tax = 0.4M,
                            StartDate = new DateTime(2016, 05, 01),
                            EndDate = new DateTime(2016, 05, 31)
                        });

                    context.MunicipalitiesTaxesSchedules.Add(
                        new MunicipalityTaxSchedule
                        {
                            Municipality = "Vilnius",
                            Type = MunicipalityTaxScheduleType.Daily,
                            Tax = 0.1M,
                            StartDate = new DateTime(2016, 01, 01),
                            EndDate = new DateTime(2016, 01, 01)
                        });

                    context.MunicipalitiesTaxesSchedules.Add(
                        new MunicipalityTaxSchedule
                        {
                            Municipality = "Vilnius",
                            Type = MunicipalityTaxScheduleType.Daily,
                            Tax = 0.1M,
                            StartDate = new DateTime(2016, 12, 25),
                            EndDate = new DateTime(2016, 12, 25)
                        });

                    context.SaveChanges();

                    var result = new MunicipalitiesTaxesSchedulesStorage(context)
                            .GetMunicipalityTaxesSchedules(
                                "Vilnius",
                                new DateTime(2016, 01, 01))
                            .ToList();

                    Assert.AreEqual(2, result.Count);

                    Assert.AreEqual(new DateTime(2016, 01, 01), result[0].StartDate);
                    Assert.AreEqual(new DateTime(2016, 12, 31), result[0].EndDate);

                    Assert.AreEqual(new DateTime(2016, 01, 01), result[1].StartDate);
                    Assert.AreEqual(new DateTime(2016, 01, 01), result[1].EndDate);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
