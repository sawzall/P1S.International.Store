using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1S.International.Store.Repository.Models
{
    /// <summary>
    /// Domestic products do not have an import duty associated with them. 
    /// </summary>
    public class DomesticProduct : BaseProduct
    {
        #region Constructors

        public DomesticProduct(Guid id, string name, decimal unitCost, decimal salesTax, bool taxExempt = false)
        {
            this.Id = id;
            this.Name = name;
            this.UnitCost = unitCost;
            this.SalesTax = salesTax;
            this.TaxExempt = taxExempt;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Tax calculation for domestic products. 
        /// </summary>
        /// <returns></returns>
        public override decimal GetTaxes()
        {
            if (TaxExempt)
            {
                return 0;
            }
            else
            {
                return base.RoundTaxes(UnitCost * SalesTax);
            }
        }

        #endregion Methods
    }
}
