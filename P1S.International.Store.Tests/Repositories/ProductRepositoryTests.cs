using Microsoft.VisualStudio.TestTools.UnitTesting;
using P1S.International.Store.Repository.Models;
using P1S.International.Store.Repository.Repositories;
using System;
using System.Linq;

namespace P1S.International.Store.Tests.Repositories
{
    [TestClass]
    public class ProductRepositoryTests
    {
        [TestMethod]
        public void TestBasicProductRepository()
        {
            var repo = new ProductRepository();
            Assert.IsInstanceOfType(repo, typeof(ProductRepository));

            var salesTax = 0.10m;
            var dutyTax = 0.05m;
            repo.Mock(salesTax, dutyTax);

            Assert.AreEqual(9, repo.FindAll().Count(), "The product repository has the incorrect number of products after being mocked.");

            Assert.AreEqual(salesTax, repo.FindAll().First().SalesTax, "The first product in the repository does not have the correct sales tax.");

            Assert.AreEqual(dutyTax, ((ImportedProduct)repo.FindAll().Where(p => p.GetType().Equals(typeof(ImportedProduct))).First()).ImportDuty, "The first imported product does not have the correct Import Duty Tax.");

            repo.Remove(repo.FindAll().First().Id);
            Assert.AreEqual(8, repo.FindAll().Count(), "The product repository failed to remove a product.");

            try
            {
                repo.Remove(Guid.NewGuid());
                Assert.AreEqual(8, repo.FindAll().Count(), "The product repository did not remove a product it does not have.");

            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InvalidOperationException));
            }
        }
    }
}
