using Microsoft.VisualStudio.TestTools.UnitTesting;
using P1S.International.Store.Repository.Models;
using System;

namespace P1S.International.Store.Tests
{
    [TestClass]
    public class ShoppingBasketTests
    {
        [TestMethod]
        public void CreateAndFillBasketTest()
        {
            var basket = new ShoppingBasket();
            var productId = Guid.NewGuid();
            var testProduct1 = new DomesticProduct(productId, "Test Product", 10.40m, 0.05m, false);

            basket.Add(testProduct1, 1);
            Assert.AreEqual(1, basket.TotalItems(), "One item was not successfully added to the basket.");

            basket.Add(testProduct1, 2);
            Assert.AreEqual(3, basket.TotalItems(), "Expected there to be three items in the basket.");

            var testProduct2 = new ImportedProduct(Guid.NewGuid(), "Imported Test Product", 19.99m, 0.05m, 0.10m);
            basket.Add(testProduct2, 1);
            Assert.AreEqual(4, basket.TotalItems(), "After adding an imported test product, there should be four items in the basket.");
        }
    }
}
