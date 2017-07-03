using System;

namespace P1S.International.Store.Repository.Exceptions
{
    /// <summary>
    /// Thrown when an invalid value is specified for a product
    /// unit cost. An example would be a negative value.
    /// </summary>
    public class InvalidUnitCostValueException : Exception
    {
    }
}
