using System;

namespace P1S.International.Store.Repository.Events
{
    /// <summary>
    /// A trivial implementation of custom event arguments, not
    /// sufficient for production.
    /// </summary>
    public class ProductRepositoryEventArgs : EventArgs
    {
        public string ProductName { get; set; }
    }
}
