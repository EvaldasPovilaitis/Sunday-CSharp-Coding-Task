namespace SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Models
{
    /// <summary>
    /// Specifies how municipality tax is scheduled in time.
    /// <para>
    /// Schedule types are defined in particular order which defines precedence.
    /// This precedence is important when for the given municipality there are
    /// multiple schedules which active periods are overlapping, i.e. at the same
    /// time there is Yearly and Monthly schedules active then Monthly takes precedence.
    /// </para>
    /// </summary>
    public enum MunicipalityTaxScheduleType
    {
        /// <summary>
        /// Default values.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Yearly municipality tax.
        /// </summary>
        Yearly = 1,

        /// <summary>
        /// Monthly municipality tax.
        /// </summary>
        Monthly = 2,

        /// <summary>
        /// Weekly municipality tax.
        /// </summary>
        Weekly = 3,

        /// <summary>
        /// Daily municipality tax.
        /// </summary>
        Daily = 4
    }
}
