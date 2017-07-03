using System;

namespace P1S.International.Store.Repository.Exceptions
{
    /// <summary>
    /// Thrown when a product name is not valid. An exmaple would be
    /// null, empty string, or all whitespace.
    /// </summary>
    public class InvalidNameValueException : Exception
    {
    }
}
