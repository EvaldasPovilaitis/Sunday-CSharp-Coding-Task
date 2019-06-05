namespace SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Storage
{
    using System;
    using System.Collections.Generic;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Models;

    /// <summary>
    /// Represents municipality taxes schedules storage.
    /// </summary>
    public interface IMunicipalitiesTaxesSchedulesStorage
    {
        /// <summary>
        /// Gets all municipality taxes schedules where <paramref name="date"/> is between 
        /// <see cref="MunicipalityTaxSchedule.StartDate"/> and <see cref="MunicipalityTaxSchedule.EndDate"/> dates.
        /// </summary>
        /// <param name="municipality">Municipality name.</param>
        /// <param name="date">Just schedules where this date is between 
        /// <see cref="MunicipalityTaxSchedule.StartDate"/> and <see cref="MunicipalityTaxSchedule.EndDate"/> dates
        /// will be returned.</param>
        /// <returns>Given municipality taxes schedules or nothing.</returns>
        IEnumerable<MunicipalityTaxSchedule> GetMunicipalityTaxesSchedules(string municipality, DateTime date);
    }
}
