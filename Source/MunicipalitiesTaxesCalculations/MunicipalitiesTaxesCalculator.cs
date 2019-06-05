namespace SundayCSharpCodingTask.MunicipalitiesTaxesCalculations
{
    using System;
    using System.Linq;
    using SundayCSharpCodingTask.MunicipalitiesTaxesCalculations.Storage;

    /// <summary>
    /// Represents municipalities taxes calculator.
    /// </summary>
    public class MunicipalitiesTaxesCalculator : IMunicipalitiesTaxesCalculator
    {
        /// <summary>
        /// Represents municipality taxes schedules storage.
        /// </summary>
        private readonly IMunicipalitiesTaxesSchedulesStorage municipalitiesTaxesSchedulesStorage;

        /// <summary>
        /// Initializes a new instance of <see cref="MunicipalitiesTaxesCalculator" class./>
        /// </summary>
        /// <param name="municipalitiesTaxesSchedulesStorage">Municipality taxes schedules storage.</param>
        public MunicipalitiesTaxesCalculator(IMunicipalitiesTaxesSchedulesStorage municipalitiesTaxesSchedulesStorage)
            =>
                this.municipalitiesTaxesSchedulesStorage =
                    municipalitiesTaxesSchedulesStorage ?? throw new ArgumentNullException(nameof(municipalitiesTaxesSchedulesStorage));

        /// <summary>
        /// Calculates municipality taxes for the specified date.
        /// </summary>
        /// <param name="municipality">Municipality name.</param>
        /// <param name="date">A date for which to calculate taxes.</param>
        /// <returns>Returns calculated taxes or zero if no data for the given municipality exists.</returns>
        public decimal Calculate(string municipality, DateTime date)
            =>
                this.municipalitiesTaxesSchedulesStorage
                    .GetMunicipalityTaxesSchedules(municipality, date)
                    .OrderByDescending(schedule => schedule.Type)
                    .Select(schedule => schedule.Tax)
                    .FirstOrDefault();
    }
}
