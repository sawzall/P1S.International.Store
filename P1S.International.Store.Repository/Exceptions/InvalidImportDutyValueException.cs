using System;

namespace P1S.International.Store.Repository.Exceptions
{
    /// <summary>
    /// Thrown when an invalid value is specified for the Import Duty Tax.
    /// For example, a negative value does not make sense for a tax.
    /// </summary>
    public class InvalidImportDutyValueException : Exception
    {
    }
}
