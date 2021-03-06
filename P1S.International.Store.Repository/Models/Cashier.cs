﻿using System.Globalization;
using System.Text;

namespace P1S.International.Store.Repository.Models
{
    /// <summary>
    /// The cashier will checkout a customer's shopping basket. 
    /// </summary>
    public class Cashier
    {
        #region Properties

        public string Name { get; }

        public string EmployeeId { get; }

        #endregion Properties

        #region Constructors

        public Cashier(string name, string employeeId)
        {
            Name = name;
            EmployeeId = employeeId;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Eventually this could do more, including notifying inventory 
        /// management of a change in quantity available for sale. For now,
        /// just create the receipt for the shopping basket. 
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public string Checkout(ShoppingBasket basket, string transactionId)
        {
            return BuildReceipt(basket, transactionId);
        }

        /// <summary>
        /// Create the receipt for the transaction based on the contents of the
        /// shoppig basket. 
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        private string BuildReceipt(ShoppingBasket basket, string transactionId)
        {
            const string LINE = "\t{0} {1}: {2}";
            StringBuilder receipt = new StringBuilder();

            receipt.AppendLine(transactionId);

            foreach (var item in basket.Contents)
            {
                var product = item.Key;
                var quantity = item.Value;
                var lineCost = quantity * product.GetCost();

                receipt.AppendFormat(LINE, quantity, product.Name, lineCost);
                receipt.AppendLine();
            }

            receipt.AppendFormat("\tSales Taxes: {0}", basket.TotalSalesTax().ToString("F2", CultureInfo.CurrentCulture));
            receipt.AppendLine();

            receipt.AppendFormat("\tTotal: {0}", basket.TotalCost().ToString("F2", CultureInfo.CurrentCulture));
            receipt.AppendLine();

            return receipt.ToString();
        }

        #endregion Methods
    }
}
