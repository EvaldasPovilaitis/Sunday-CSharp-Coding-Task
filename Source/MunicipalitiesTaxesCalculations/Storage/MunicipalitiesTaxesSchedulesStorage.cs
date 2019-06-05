namespace SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Models;

    /// <summary>
    /// Represents municipalities taxes schedules storage.
    /// </summary>
    public class MunicipalitiesTaxesSchedulesStorage : IMunicipalitiesTaxesSchedulesStorage
    {
        /// <summary>
        /// Storage context.
        /// </summary>
        private readonly MunicipalitiesTaxesSchedulesContext context;

        /// <summary>
        /// Initializes a new instance of <see cref="MunicipalitiesTaxesSchedulesStorage"/> class./>
        /// </summary>
        /// <param name="context">Storage context.</param>
        public MunicipalitiesTaxesSchedulesStorage(MunicipalitiesTaxesSchedulesContext context)
            =>
                this.context = 
                    context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// Gets all municipality taxes schedules where <paramref name="date"/> is between 
        /// <see cref="MunicipalityTaxSchedule.StartDate"/> and <see cref="MunicipalityTaxSchedule.EndDate"/> dates.
        /// </summary>
        /// <param name="municipality">Municipality name.</param>
        /// <param name="date">Just schedules where this date is between 
        /// <see cref="MunicipalityTaxSchedule.StartDate"/> and <see cref="MunicipalityTaxSchedule.EndDate"/> dates
        /// will be returned.</param>
        /// <returns>Given municipality taxes schedules or nothing.</returns>
        public IEnumerable<MunicipalityTaxSchedule> GetMunicipalityTaxesSchedules(string municipality, DateTime date)
        {
            if(string.IsNullOrEmpty(municipality))
            {
                return Enumerable.Empty<MunicipalityTaxSchedule>();
            }

            DateTime dateUtc = date.ToUniversalTime().Date;

            return this.context.MunicipalitiesTaxesSchedules
                .Where(s =>
                        s.Municipality.Equals(municipality, StringComparison.InvariantCultureIgnoreCase)
                        && s.StartDate.ToUniversalTime().Date <= dateUtc
                        && dateUtc <= s.EndDate.ToUniversalTime().Date);
        }
                
    }
}
