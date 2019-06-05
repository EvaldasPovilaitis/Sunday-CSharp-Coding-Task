namespace SundayCSharpCodingTask.MunicipalitiesTaxesCalculations
{
    using System;

    /// <summary>
    /// Represents municipalities taxes calculator.
    /// </summary>
    public interface IMunicipalitiesTaxesCalculator
    {
        /// <summary>
        /// Calculates municipality taxes for the specified date.
        /// </summary>
        /// <param name="municipality">Municipality name.</param>
        /// <param name="date">A date for which to calculate taxes.</param>
        /// <returns>Calculated taxes or zero if no data for the given municipality exists.</returns>
        decimal Calculate(string municipality, DateTime date);
    }
}
