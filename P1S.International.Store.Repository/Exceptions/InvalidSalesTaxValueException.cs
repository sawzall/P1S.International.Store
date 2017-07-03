using System;

namespace P1S.International.Store.Repository.Exceptions
{
    /// <summary>
    /// Thrown when an invalid value is specified for Sales Tax. For
    /// example, a negative value for sales tax doesn't make sense.
    /// </summary>
    public class InvalidSalesTaxValueException : Exception
    {
    }
}
