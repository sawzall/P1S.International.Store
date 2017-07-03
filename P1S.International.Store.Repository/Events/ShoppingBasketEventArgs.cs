using System;

namespace P1S.International.Store.Repository.Events
{
    /// <summary>
    /// A trivial implementation of custom event arguments for 
    /// events in the ShoppingBasket. Not sufficient for 
    /// production.
    /// </summary>
    public class ShoppingBasketEventArgs : EventArgs
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
