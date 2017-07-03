using P1S.International.Store.Repository.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1S.International.Store.Repository.Models
{
    /// <summary>
    /// Imported products have sales tax, plus import duty tax. Tax exemption only 
    /// applies to sales tax, not import duty tax. 
    /// </summary>
    public class ImportedProduct : BaseProduct
    {
        #region Fields

        private decimal _importDuty;

        #endregion Fields

        #region Constructors

        public ImportedProduct(Guid id, string name, decimal unitCost, decimal salesTax, decimal importDuty, bool taxExempt = false)
        {
            this.Id = id;
            this.Name = name;
            this.UnitCost = unitCost;
            this.SalesTax = salesTax;
            this.ImportDuty = importDuty;
            this.TaxExempt = taxExempt;
        }

        #endregion Constructors

        #region Properties

        public decimal ImportDuty
        {
            get
            {
                return _importDuty;
            }

            set
            {
                if (value < 0)
                {
                    throw new InvalidImportDutyValueException();
                }
                _importDuty = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Tax calculation for imported products. 
        /// </summary>
        /// <returns></returns>
        public override decimal GetTaxes()
        {
            if (TaxExempt)
            {
                return base.RoundTaxes(UnitCost * ImportDuty);
            }
            else
            {
                return base.RoundTaxes(UnitCost * (ImportDuty + SalesTax));
            }
        }

        #endregion

    }
}
