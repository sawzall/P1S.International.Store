using Microsoft.VisualStudio.TestTools.UnitTesting;
using P1S.International.Store.Repository.Exceptions;
using P1S.International.Store.Repository.Models;
using System;

namespace P1S.International.Store.Tests.Models
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void TestProductCreation()
        {
            var name = "Product Name";
            var unitCost = 18.99m;
            var salesTax = 0.10m;
            var id = Guid.NewGuid();
            var product = new DomesticProduct(id, name, unitCost, salesTax);
            Assert.AreEqual(name, product.Name);
            Assert.AreEqual(unitCost, product.UnitCost);
            Assert.AreEqual(salesTax, product.SalesTax);
            Assert.AreEqual(id, product.Id);
        }

        [TestMethod]
        public void TestProductValidation()
        {
            try
            {
                new DomesticProduct(Guid.Empty, "name", 10m, 0.05m);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InvalidIdValueException));
            }

            try
            {
                new DomesticProduct(Guid.NewGuid(), null, 10m, 0.05m);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InvalidNameValueException));
            }

            try
            {
                new DomesticProduct(Guid.NewGuid(), "   ", 10m, 0.05m);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InvalidNameValueException));
            }

            try
            {
                new DomesticProduct(Guid.NewGuid(), "name", -1m, 0.05m);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InvalidUnitCostValueException));
            }

            try
            {
                new DomesticProduct(Guid.NewGuid(), "name", 10m, -1m);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InvalidSalesTaxValueException));
            }

            try
            {
                new ImportedProduct(Guid.NewGuid(), "name", 10m, 0.05m, -1m);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InvalidImportDutyValueException));
            }
        }

        [TestMethod]
        public void TestSalesTaxRounding()
        {
            var product1 = new DomesticProduct(Guid.NewGuid(), "Product One", 18.99m, 0.10m, false);
            var expectedTaxes = 1.9m;
            Assert.AreEqual(expectedTaxes, product1.GetTaxes(), string.Format("{0} expected taxes {1}, actual taxes {2}.", product1.Name, expectedTaxes, product1.GetTaxes()));

            var product2 = new ImportedProduct(Guid.NewGuid(), "Product Two", 27.99m, 0.10m, 0.05m);
            var expectedTaxesAndDuty = 4.2m;
            Assert.AreEqual(expectedTaxesAndDuty, product2.GetTaxes(), string.Format("{0} expected taxes {1}, actual taxes {2}.", product2.Name, expectedTaxesAndDuty, product2.GetTaxes()));

            var product3 = new DomesticProduct(Guid.NewGuid(), "Product Three", 18.99m, 0.10m, true);
            var expectedDomesticTaxExempt = 0m;
            Assert.AreEqual(expectedDomesticTaxExempt, product3.GetTaxes(), string.Format("{0} expected taxes {1}, actual taxes {2}.", product3.Name, expectedDomesticTaxExempt, product3.GetTaxes()));

            var product4 = new ImportedProduct(Guid.NewGuid(), "Product Four", 27.99m, 0.10m, 0.05m, true);
            var expectedImportedDutyTaxExempt = 1.40m;
            Assert.AreEqual(expectedImportedDutyTaxExempt, product4.GetTaxes(), string.Format("{0} expected taxes {1}, actual taxes {2}.", product4.Name, expectedImportedDutyTaxExempt, product4.GetTaxes()));

        }
    }
}
