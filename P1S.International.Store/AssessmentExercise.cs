using NLog;
using P1S.International.Store.ExampleBaskets;
using P1S.International.Store.Repository.Events;
using P1S.International.Store.Repository.Models;
using P1S.International.Store.Repository.Repositories;
using System;

namespace P1S.International.Store
{
    internal class AssessmentExercise
    {
        /// <summary>
        /// Logging isn't specifically required by the assessment, but some kind
        /// of logging is required in most enterprise applications. This is using 
        /// the NLog library which can be installed using the NuGet Package
        /// Manager using the following command "Install-Package NLog.Config"
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// PerformAssessment executes the three required inputs and displays the
        /// output. 
        /// </summary>
        public void PerformAssessment()
        {
            var productRepository = new ProductRepository();
            productRepository.ProductAdded += ProductRepository_ProductAdded;

            productRepository.Mock(0.10m, 0.05m);

            var myCashier = new Cashier("David K", "123456");

            Console.WriteLine(myCashier.Checkout(ShoppingBasket1.Run(productRepository), "Output 1:"));

            Console.WriteLine(myCashier.Checkout(ShoppingBasket2.Run(productRepository), "Output 2:"));

            Console.WriteLine(myCashier.Checkout(ShoppingBasket3.Run(productRepository), "Output 3:"));
        }

        /// <summary>
        /// Event handling, or any messaging, was not specified in the assessment.
        /// The following is demonstrating event handling and logging in an albeit
        /// trivial implementation. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductRepository_ProductAdded(object sender, EventArgs e)
        {
            logger.Debug(string.Format("A cool new product, {0}, was added to the repository.", ((ProductRepositoryEventArgs)e).ProductName));
        }
    }
}
