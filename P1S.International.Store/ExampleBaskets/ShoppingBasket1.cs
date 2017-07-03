using P1S.International.Store.Repository.Models;
using P1S.International.Store.Repository.Repositories;

namespace P1S.International.Store.ExampleBaskets
{
    internal static class ShoppingBasket1
    {
        /// <summary>
        /// Shopping Basket 1:
        ///     1 book at 12.49
        ///     1 music CD at 14.99
        ///     1 chocolate bar at 0.85
        /// Output 1:
        ///     1 book: 12.49
        ///     1 music CD: 16.49
        ///     1 chocolate bar: 0.85 
        ///     Sales Taxes: 1.50 
        ///     Total: 29.83
        /// </summary>
        /// <param name="products"></param>
        public static ShoppingBasket Run(ProductRepository products)
        {
            var basket = new ShoppingBasket();
            basket.Add(products.FindById(ProductRepository.BOOK_ID), 1);
            basket.Add(products.FindById(ProductRepository.MUSIC_CD_ID), 1);
            basket.Add(products.FindById(ProductRepository.CHOCOLATE_BAR_ID), 1);

            return basket;
        }
    }
}
