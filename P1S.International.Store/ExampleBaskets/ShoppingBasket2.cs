using P1S.International.Store.Repository.Models;
using P1S.International.Store.Repository.Repositories;

namespace P1S.International.Store.ExampleBaskets
{
    internal static class ShoppingBasket2
    {
        /// <summary>
        /// Shopping Basket 2:
        ///     1 imported box of chocolates at 10.00
        ///     1 imported bottle of perfume at 47.50
        ///  Output 2:
        ///     1 imported box of chocolates: 10.50 
        ///     1 imported bottle of perfume: 54.65 
        ///     Sales Taxes: 7.65
        ///     Total: 65.15
        /// </summary>
        /// <param name="products"></param>
        public static ShoppingBasket Run(ProductRepository products)
        {
            var basket = new ShoppingBasket();
            basket.Add(products.FindById(ProductRepository.IMPORTED_BOX_OF_CHOCOLATES_ID), 1);
            basket.Add(products.FindById(ProductRepository.IMPORTED_BOTTLE_OF_PERFUME_ID), 1);

            return basket;
        }
    }
}
