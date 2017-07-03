using System;

namespace P1S.International.Store.Repository.Exceptions
{
    /// <summary>
    /// Thrown when an invalid quantity is specified in a shopping basket.
    /// An example would be having a negative quantity of an item in a 
    /// basket does not make sense.
    /// </summary>
    public class InvalidBasketQuantityException : Exception
    {
    }
}
