namespace SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Models
{
    using System;

    /// <summary>
    /// Represents a municipality tax for the given time.
    /// </summary>
    public class MunicipalityTaxSchedule
    {
        /// <summary>
        /// Gets or sets municipality name.
        /// </summary>
        public string Municipality { get; set; }

        /// <summary>
        /// Gets or sets municipality tax schedule type.
        /// </summary>
        public MunicipalityTaxScheduleType Type { get; set; }

        /// <summary>
        /// Gets or sets municipality tax.
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Gets or sets date since when the tax is active.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets date since when the tax is no longer active.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
