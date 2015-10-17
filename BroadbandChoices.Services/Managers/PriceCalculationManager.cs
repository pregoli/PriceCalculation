using BroadbandChoices.Entities;
using BroadbandChoices.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Services.Managers
{
    public static class PriceCalculationManager
    {
        /// <summary>
        /// Retrieve the final basket result
        /// Including all products with infos/ compared prices infos/ offers.discounts if available
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        public static BasketResult GetBasketResult(Basket basket)
        {
            var basketResult = new BasketResult();

            basket = ResetCurrentBasketFinalPrices(basket);

            basket.Products.ForEach(x => x.FinalPrice = 0.0);

            var products = basket.Products;

            var numberOfBreadsToCutPrice = 0;

            //Manage prices and offers on butter
            basketResult = ManageButter(products, basketResult, out numberOfBreadsToCutPrice);
            //Manage prices and offers on bread
            basketResult = ManageBread(products, basketResult, numberOfBreadsToCutPrice);
            //Manage prices and offers on milk
            basketResult = ManageMilk(products, basketResult);

            //Prices Result
            basketResult.FinalPrice = Math.Round(basketResult.Products.Sum(x => x.FinalPrice),2);
            basketResult.OriginalPrice = Math.Round(basketResult.Products.Sum(x => x.UnitPrice), 2);
            basketResult.SavedAmount = Math.Round((basketResult.OriginalPrice - basketResult.FinalPrice), 2);

            return basketResult;
        }

        #region products prices and offers methods

        /// <summary>
        /// Retrieve the basket result filled with Butter product items updated infos
        /// about discounts and/or offers
        /// </summary>
        /// <param name="products">Current products(typeOf(butter)) to check prices/discounts to update the basketResult obj</param>
        /// <param name="basketResult">The current updated basketResult obj</param>
        /// <param name="numberOfBreadsToCutPrice">Number of products typeOf(Bread) bread to discount</param>
        /// <returns></returns>
        private static BasketResult ManageButter(List<Product> products, BasketResult basketResult, out int numberOfBreadsToCutPrice)
        {
            var butters = products.Where(x => x.GetType() == typeof(Butter)).ToList();
            var totalButters = butters.Count;
            var buttersFinalPrice = butters.Sum(x => x.UnitPrice);
            if (totalButters >= OfferManager.MinimumForButtesOffer)
            {
                var offerOnButter = new OfferOnButter
                {
                    TotalItems = totalButters
                };

                numberOfBreadsToCutPrice = totalButters / OfferManager.MinimumForButtesOffer;
                basketResult.Offers.Add(offerOnButter);
            }
            else
                numberOfBreadsToCutPrice = 0;

            butters.ForEach(x => x.FinalPrice = x.UnitPrice);
            basketResult.Products.AddRange(butters);

            return basketResult;
        }

        /// <summary>
        /// Retrieve the basket result filled with Bread product items updated infos
        /// about discounts and/or offers
        /// </summary>
        /// <param name="products">Current products(typeOf(bread)) to check prices/discounts to update the basketResult obj</param>
        /// <param name="basketResult">The current updated basketResult obj</param>
        /// <param name="numberOfBreadsToCutPrice"></param>
        /// <returns></returns>
        private static BasketResult ManageBread(List<Product> products, BasketResult basketResult, int numberOfBreadsToCutPrice)
        {
            var breads = products.Where(x => x.GetType() == typeof(Bread)).ToList();
            var totalBreads = breads.Count;

            var breadsInitialPrice = breads.Sum(x => x.UnitPrice);
            if (numberOfBreadsToCutPrice > 0)
            {
                for (int i = 0; i < numberOfBreadsToCutPrice; i++)
                {
                    if (totalBreads >= numberOfBreadsToCutPrice)
                    {
                        breads[i].FinalPrice = breads[i].UnitPrice * (100 - 50) / 100;
                    }
                }
            }
            breads.Where(x => x.FinalPrice == 0).ToList().ForEach(x => x.FinalPrice = x.UnitPrice);
            basketResult.Products.AddRange(breads);

            return basketResult;
        }

        /// <summary>
        /// Retrieve the basket result filled with Milk product items updated infos
        /// about discounts and/or offers
        /// </summary>
        /// <param name="products">Current products(typeOf(milk)) to check prices/discounts to update the basketResult obj</param>
        /// <param name="basketResult">The current updated basketResult obj</param>
        /// <returns></returns>
        private static BasketResult ManageMilk(List<Product> products, BasketResult basketResult)
        {
            var milks = products.Where(x => x.GetType() == typeof(Milk)).ToList();
            var totalMilks = milks.Count;

            milks.Where(x => x.FinalPrice == 0).ToList().ForEach(x => x.FinalPrice = x.UnitPrice);
            if (totalMilks > OfferManager.MinimumForMilksOffer)
            {
                var numberOfMilkForFree = totalMilks / OfferManager.MinimumForMilksOffer;

                milks.Take(numberOfMilkForFree).ToList().ForEach(x => x.FinalPrice = 0.0);
            }

            if (totalMilks >= OfferManager.MinimumForMilksOffer)
                basketResult.Offers.Add(new OfferOnMilk());

            basketResult.Products.AddRange(milks);

            return basketResult;
        }

        #endregion

        /// <summary>
        /// Reset the products final prices on every request
        /// </summary>
        /// <param name="basket">The current basket containing the customer order infos.</param>
        /// <returns></returns>
        private static Basket ResetCurrentBasketFinalPrices(Basket basket)
        {
            basket.Products.ForEach(x => x.FinalPrice = 0.0);
            return basket;
        }
    }
}
