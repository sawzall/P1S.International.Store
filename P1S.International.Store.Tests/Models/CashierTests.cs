using Microsoft.VisualStudio.TestTools.UnitTesting;
using P1S.International.Store.Repository.Repositories;
using System;

namespace P1S.International.Store.Repository.Models.Tests
{
    [TestClass()]
    public class CashierTests
    {
        [TestMethod()]
        public void CashierTest()
        {
            var cashierName = Guid.NewGuid().ToString();
            var employeeId = Guid.NewGuid().ToString();

            var cashier = new Cashier(cashierName, employeeId);

            Assert.AreEqual(cashierName, cashier.Name);
            Assert.AreEqual(employeeId, cashier.EmployeeId);
        }

        [TestMethod()]
        public void CheckoutTest()
        {
            // need a basket with items in it
            var basket = new ShoppingBasket();
            // need items to put in the basket
            var repo = new ProductRepository();
            // need some known values to test with
            var salesTax = 0.055m;
            var importDuty = 0.17m;
            // generate some products
            repo.Mock(salesTax, importDuty);

            // get a couple of items to add to the basket
            var item1 = repo.FindById(ProductRepository.BOTTLE_OF_PERFUME_ID);
            var item2 = repo.FindById(ProductRepository.IMPORTED_BOX_OF_CHOCOLATES2_ID);

            // now add some items to the basket
            basket.Add(item1, 1);
            basket.Add(item2, 3);

            Assert.AreEqual(4, basket.TotalItems());

            var cashier = new Cashier("Testy McTester", "000000");

            var expectedReceipt = @"Test 001
	1 bottle of perfume: 20.04
	3 imported box of chocolates: 39.60
	Sales Taxes: 6.90
	Total: 59.64";
            Assert.AreEqual(expectedReceipt.Trim(), cashier.Checkout(basket, "Test 001").Trim());
        }
    }
}