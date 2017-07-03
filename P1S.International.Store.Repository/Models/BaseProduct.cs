using P1S.International.Store.Repository.Exceptions;
using System;

namespace P1S.International.Store.Repository.Models
{
    /// <summary>
    /// An abstract class to define the common functionality of products in the
    /// P1S International Store. Validation is pretty brute force, throwing 
    /// exceptions is certainly one way to avoid creating an object with 
    /// bad data. A validation framework such as Fluent Validation could 
    /// be used.
    /// </summary>
    public abstract class BaseProduct
    {

        #region Fields

        private Guid _id;
        private string _name;
        private decimal _unitCost;
        private decimal _salesTax;
        private bool _taxExempt;

        #endregion Fields

        #region Properties

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (Guid.Empty.Equals(value))
                {
                    throw new InvalidIdValueException();
                }
                _id = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim()))
                {
                    throw new InvalidNameValueException();
                }
                _name = value;
            }
        }

        public decimal UnitCost
        {
            get
            {
                return _unitCost;
            }

            set
            {
                if (value < 0)
                {
                    throw new InvalidUnitCostValueException();
                }
                _unitCost = value;
            }
        }

        public decimal SalesTax
        {
            get
            {
                return _salesTax;
            }

            set
            {
                if (value < 0)
                {
                    throw new InvalidSalesTaxValueException();
                }
                _salesTax = value;
            }
        }

        public bool TaxExempt
        {
            get
            {
                return _taxExempt;
            }

            set
            {
                _taxExempt = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Each type of product could calculate its taxes differently, leave the implementation
        /// to the child class.
        /// </summary>
        /// <returns>Taxes on the item, rounded up to the next nickel.</returns>
        public abstract decimal GetTaxes();

        /// <summary>
        /// Get the total cost of the item as defined as the unit cost plus any applicable taxes.
        /// </summary>
        /// <returns>Total cost of the item.</returns>
        public decimal GetCost()
        {
            return _unitCost + GetTaxes();
        }

        /// <summary>
        /// Round the value up to the next nickel.
        /// </summary>
        /// <remarks>This should be extracted to a more general location and a more general rounding target.</remarks>
        /// <param name="taxes"></param>
        /// <returns>Taxes rounded up to the nearest nickel.</returns>
        protected decimal RoundTaxes(decimal taxes)
        {
            return Math.Ceiling(taxes * 20.0m) / 20.0m;
        }

        #endregion Methods
    }
}
