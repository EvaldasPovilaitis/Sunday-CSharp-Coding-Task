namespace SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Storage
{
    using Microsoft.EntityFrameworkCore;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Models;

    /// <summary>
    /// Represents Entity Framework municipalities taxes schedules context.
    /// </summary>
    public class MunicipalitiesTaxesSchedulesContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MunicipalitiesTaxesSchedulesContext"/> class./>
        /// </summary>
        /// <param name="options">Context options.</param>
        public MunicipalitiesTaxesSchedulesContext(DbContextOptions<MunicipalitiesTaxesSchedulesContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Get or sets Entity Framework <see cref="DbSet{TEntity}"/> for municipalities taxes schedules.
        /// </summary>
        public DbSet<MunicipalityTaxSchedule> MunicipalitiesTaxesSchedules { get; set; }
    }
}
