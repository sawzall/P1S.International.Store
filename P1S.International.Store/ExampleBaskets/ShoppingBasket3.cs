using P1S.International.Store.Repository.Models;
using P1S.International.Store.Repository.Repositories;

namespace P1S.International.Store.ExampleBaskets
{
    internal static class ShoppingBasket3
    {
        /// <summary>
        /// Shopping Basket 3:
        ///     1 imported bottle of perfume at 27.99 
        ///     1 bottle of perfume at 18.99
        ///     1 packet of headache pills at 9.75
        ///     1 imported box of chocolates at 11.25
        /// Output 3:
        ///     1 imported bottle of perfume: 32.19 
        ///     1 bottle of perfume: 20.89
        ///     1 packet of headache pills: 9.75
        ///     1 imported box of chocolates: 11.85 
        ///     Sales Taxes: 6.70
        ///     Total: 74.68
        /// </summary>
        /// <param name="products"></param>
        public static ShoppingBasket Run(ProductRepository products)
        {
            var basket = new ShoppingBasket();
            basket.Add(products.FindById(ProductRepository.IMPORTED_BOTTLE_OF_PERFUME2_ID), 1);
            basket.Add(products.FindById(ProductRepository.BOTTLE_OF_PERFUME_ID), 1);
            basket.Add(products.FindById(ProductRepository.PACKET_OF_HEADACHE_PILLS_ID), 1);
            basket.Add(products.FindById(ProductRepository.IMPORTED_BOX_OF_CHOCOLATES2_ID), 1);

            return basket;
        }
    }
}
