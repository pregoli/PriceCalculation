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
        public static BasketResult GetBasketResult(Basket basket)
        {
            var basketResult = new BasketResult();
            basket.Products.ForEach(x => x.FinalPrice = 0.0);

            OfferOnButter offerOnButter = null;
            OfferOnMilk offerOnMilk = null;

            var products = basket.Products;
            var originalPrice = products.Sum(x => x.UnitPrice);

            var numberOfBreadsToCutPrice = 0;

            //butters
            var butters = products.Where(x => x.GetType() == typeof(Butter)).ToList();
            var totalButters = butters.Count;
            var buttersFinalPrice = butters.Sum(x => x.UnitPrice);
            if (totalButters >= OfferManager.MinimumForButtesOffer)
            {
                offerOnButter = new OfferOnButter
                {
                    TotalItems = totalButters
                };

                numberOfBreadsToCutPrice = totalButters / OfferManager.MinimumForButtesOffer;
                basketResult.Offers.Add(offerOnButter);
            }

            butters.ForEach(x => x.FinalPrice = x.UnitPrice);
            basketResult.Products.AddRange(butters);

            //bread
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

            //milks
            var milks = products.Where(x => x.GetType() == typeof(Milk)).ToList();
            var totalMilks = milks.Count;

            milks.Where(x => x.FinalPrice == 0).ToList().ForEach(x => x.FinalPrice = x.UnitPrice);
            if (totalMilks > OfferManager.MinimumForMilksOffer)
            {
                var numberOfMilkForFree = totalMilks / OfferManager.MinimumForMilksOffer;

                milks.Take(numberOfMilkForFree).ToList().ForEach(x => x.FinalPrice = 0.0);
                
                basketResult.Offers.Add(new OfferOnMilk());
            }

            basketResult.Products.AddRange(milks);


            basketResult.FinalPrice = Math.Round(basketResult.Products.Sum(x => x.FinalPrice),2);
            basketResult.OriginalPrice = Math.Round(basketResult.Products.Sum(x => x.UnitPrice), 2);
            basketResult.SavedAmount = Math.Round((basketResult.OriginalPrice - basketResult.FinalPrice), 2);

            return basketResult;
        }
    }
}
