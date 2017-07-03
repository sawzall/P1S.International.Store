using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1S.International.Store.Contracts.Contracts
{
    /// <summary>
    /// Define the methods that all products must support.
    /// 
    /// </summary>
    public interface IProduct
    {
        #region Properties

        /// <summary>
        /// The product name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The cost per unit (excluding any taxes, duty or tariffs) 
        /// </summary>
        /// <remarks>Everything is assuming US Dollars for all transactions. 
        /// In reality a more complex representation for the cost that includes 
        /// currency is necessary.</remarks>
        decimal UnitCost { get; set; }

        /// <summary>
        /// Sales tax (as a decimal, not percent). E.g. 5% should be 0.05
        /// </summary>
        decimal SalesTax { get; set; }

        /// <summary>
        /// Indicates if the product is exempt from Sales Tax. Import Duty
        /// would still apply.
        /// </summary>
        bool TaxExempt { get; set; }

        #endregion Properties

        #region Methods

        decimal GetTaxes();

        #endregion Methods
    }
}
