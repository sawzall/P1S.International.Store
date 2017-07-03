using P1S.International.Store.Repository.Events;
using P1S.International.Store.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1S.International.Store.Repository.Repositories
{
    public class ProductRepository
    {
        #region Events

        public event EventHandler ProductAdded;
        public event EventHandler ProductRemoved;
        public event EventHandler ProductUpdated;

        protected virtual void OnProductAdded(ProductRepositoryEventArgs e)
        {
            EventHandler handler = ProductAdded;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnProductRemoved(ProductRepositoryEventArgs e)
        {
            EventHandler handler = ProductRemoved;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnProductUpdated(ProductRepositoryEventArgs e)
        {
            EventHandler handler = ProductUpdated;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public delegate void ProductAddedEventHandler(ProductRepositoryEventArgs e);
        public delegate void ProductRemovedEventHandler(ProductRepositoryEventArgs e);
        public delegate void ProductUpdatedEventHandler(ProductRepositoryEventArgs e);

        #endregion Events

        private List<BaseProduct> _repository;

        public ProductRepository()
        {
            _repository = new List<BaseProduct>();
        }

        #region Data Mock

        /// <summary>
        /// A bunch of well known product identifiers.
        /// </summary>
        public static readonly Guid BOOK_ID = new Guid("C1E513AD-83F5-4DFC-8023-BCC88F9F49F6");
        public static readonly Guid MUSIC_CD_ID = new Guid("2A2C226D-F7BF-41BF-9A3C-066841DCC90D");
        public static readonly Guid CHOCOLATE_BAR_ID = new Guid("D1239FFD-B60E-42E2-A37E-B8CB056B667C");
        public static readonly Guid IMPORTED_BOX_OF_CHOCOLATES_ID = new Guid("72DDF2E7-FD08-4EE4-9CDF-C850BA799B03");
        public static readonly Guid IMPORTED_BOTTLE_OF_PERFUME_ID = new Guid("BE0AE76C-FCBB-4467-8B74-502614084406");
        public static readonly Guid BOTTLE_OF_PERFUME_ID = new Guid("BE604F2B-D488-4516-94CB-1EB6CBCFA100");
        public static readonly Guid PACKET_OF_HEADACHE_PILLS_ID = new Guid("0AD4FB5E-F4CC-4251-8E48-A87B7AABE659");
        public static readonly Guid IMPORTED_BOX_OF_CHOCOLATES2_ID = new Guid("0B87C95A-C4FF-4A88-A748-547A5E08E9A2");
        public static readonly Guid IMPORTED_BOTTLE_OF_PERFUME2_ID = new Guid("2F04C3E2-3E53-43DF-BFCB-C0650CE7B3EA");

        /// <summary>
        /// Mock up products in the repository for use by the assessment.
        /// </summary>
        /// <param name="salesTax"></param>
        /// <param name="importDuty"></param>
        public void Mock(decimal salesTax, decimal importDuty)
        {
            // create the following products to be available in the store
            Add(new DomesticProduct(BOOK_ID, "book", 12.49m, salesTax, true));
            Add(new DomesticProduct(MUSIC_CD_ID, "music CD", 14.99m, salesTax));
            Add(new DomesticProduct(CHOCOLATE_BAR_ID, "chocolate bar", 0.85m, salesTax, true));

            Add(new ImportedProduct(IMPORTED_BOX_OF_CHOCOLATES_ID, "imported box of chocolates", 10.00m, salesTax, importDuty, true));
            Add(new ImportedProduct(IMPORTED_BOTTLE_OF_PERFUME_ID, "imported bottle of perfume", 47.50m, salesTax, importDuty));

            Add(new ImportedProduct(IMPORTED_BOTTLE_OF_PERFUME2_ID, "imported bottle of perfume", 27.99m, salesTax, importDuty));
            Add(new DomesticProduct(BOTTLE_OF_PERFUME_ID, "bottle of perfume", 18.99m, salesTax));
            Add(new DomesticProduct(PACKET_OF_HEADACHE_PILLS_ID, "packet of headache pills", 9.75m, salesTax, true));
            Add(new ImportedProduct(IMPORTED_BOX_OF_CHOCOLATES2_ID, "imported box of chocolates", 11.25m, salesTax, importDuty, true));

        }

        #endregion Data Mock

        #region Methods

        public IEnumerable<BaseProduct> FindByName(string name)
        {
            return _repository.FindAll(p => p.Name.Contains(name));
        }
        public BaseProduct FindById(Guid id)
        {
            return _repository.Single(p => p.Id.Equals(id));
        }
        public IEnumerable<BaseProduct> FindAll()
        {
            return _repository;
        }

        /// <summary>
        /// Check if a product with the same id already is in the repository. There is
        /// no check for duplicate names, which may lead to confusion.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private bool DoesProductExist(BaseProduct product)
        {
            return _repository.FindAll(p => p.Id.Equals(product.Id)).Count() > 0;
        }

        /// <summary>
        /// Add the product to the repository.
        /// </summary>
        /// <param name="product"></param>
        public void Add(BaseProduct product)
        {
            if (!DoesProductExist(product))
            {
                _repository.Add(product);
                OnProductAdded(new ProductRepositoryEventArgs { ProductName = product.Name });
            }
        }

        /// <summary>
        /// Remove a product from the repository
        /// </summary>
        /// <remarks>This should have more safety checks to avoid errors when removing a product
        /// that does not exist. However, since this isn't used by the assessment, that work has
        /// been left for later.</remarks>
        /// <param name="id"></param>
        public void Remove(Guid id)
        {
            var productName = FindById(id).Name;
            _repository.Remove(FindById(id));
            OnProductRemoved(new ProductRepositoryEventArgs { ProductName = productName });
        }

        /// <summary>
        /// Update a product in the repository
        /// </summary>
        /// <remarks>This should have more safety checks to avoid errors when updating a product
        /// that does not exist. However, since this isn't used by the assessment, that work has
        /// been left for later.</remarks>
        /// <param name="product"></param>
        public void Update(BaseProduct product)
        {
            _repository.Remove(FindById(product.Id));
            _repository.Add(product);
        }

        #endregion Methods
    }
}
