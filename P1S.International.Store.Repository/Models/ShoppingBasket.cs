using P1S.International.Store.Repository.Events;
using P1S.International.Store.Repository.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P1S.International.Store.Repository.Models
{
    /// <summary>
    /// A shopping basket allows the user to collect an arbitrary quantity
    /// of an arbitrary number of products, both domestic and imported. The
    /// shopping basket 
    /// </summary>
    public class ShoppingBasket
    {
        #region Fields

        private Dictionary<BaseProduct, int> _contents;

        #endregion Fields

        #region Properties

        public Dictionary<BaseProduct, int> Contents
        {
            get
            {
                return _contents;
            }
        }

        #endregion Properties

        #region Events

        public event EventHandler ItemAdded;
        public event EventHandler ItemRemoved;

        protected virtual void OnItemAdded(EventArgs e)
        {
            EventHandler handler = ItemAdded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnItemRemoved(EventArgs e)
        {
            EventHandler handler = ItemRemoved;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public delegate void ItemAddedEventHandler(EventArgs e);
        public delegate void ItemRemovedEventHandler(EventArgs e);

        #endregion Events

        #region Constuctors

        public ShoppingBasket()
        {
            _contents = new Dictionary<BaseProduct, int>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Add a product to the shopping basket. If the product is already
        /// in the basket, just increase the quantity of the existing product.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void Add(BaseProduct product, int quantity)
        {
            if (quantity <= 0)
            {
                throw new InvalidBasketQuantityException();
            }
            if (Contents.ContainsKey(product))
            {
                Contents[product] += quantity;
            }
            else
            {
                Contents.Add(product, quantity);
            }
            OnItemAdded(new ShoppingBasketEventArgs { ProductName = product.Name });
        }

        /// <summary>
        /// Remove all or some of a product from the shopping basket. If the quantity
        /// of the items in the basket exceeds the quantity to be removed, decrease 
        /// the quantity of the item in the basket. 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void Remove(BaseProduct product, int quantity)
        {
            if (quantity <= 0)
            {
                throw new InvalidBasketQuantityException();
            }
            if (Contents.ContainsKey(product))
            {
                if (Contents[product] == quantity)
                {
                    Contents.Remove(product);
                    OnItemRemoved(new ShoppingBasketEventArgs { ProductName = product.Name, Quantity = 0 });
                }
                else if (Contents[product] > quantity)
                {
                    Contents[product] -= quantity;
                    OnItemRemoved(new ShoppingBasketEventArgs { ProductName = product.Name, Quantity = Contents[product] });
                }
                else
                {
                    // For clarity, this should be distinquishable from the
                    // quantity validation above, but since this isn't part
                    // of the exercise, leave for later
                    throw new InvalidBasketQuantityException();
                }
            }
        }

        public int TotalItems()
        {
            return Contents.Sum(p => p.Value);
        }

        public decimal TotalSalesTax()
        {
            return _contents.Sum(p => p.Value * p.Key.GetTaxes());
        }

        public decimal TotalCost()
        {
            return _contents.Sum(p => p.Value * p.Key.GetCost());
        }

        #endregion Methods
    }
}
