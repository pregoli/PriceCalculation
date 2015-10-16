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

            OfferOnButter offerOnButter = null;
            OfferOnMilk offerOnMilk = null;

            var products = basket.Products;
            var originalPrice = products.Sum(x => x.UnitPrice);

            var butters = products.Where(x => x.GetType() == typeof(Butter)).ToList();
            var totalButters = butters.Count;

            var milks = products.Where(x => x.GetType() == typeof(Milk)).ToList();
            var totalMilks = milks.Count;

            var breads = products.Where(x => x.GetType() == typeof(Bread)).ToList();
            var totalBreads = breads.Count;

            var minNumberOfButtersToReceiveOffer = OfferManager.MinimumForButtesOffer;
            var minNumberOfMilksToReceiveOffer = OfferManager.MinimumForMilksOffer;

            var numberOfBreadsToCutPrice = 0;

            //butters
            var buttersFinalPrice = butters.Sum(x => x.UnitPrice);
            if (totalButters >= minNumberOfButtersToReceiveOffer)
            {
                offerOnButter = new OfferOnButter
                {
                    TotalItems = totalButters
                };

                numberOfBreadsToCutPrice = totalButters / minNumberOfButtersToReceiveOffer;
                basketResult.Offers.Add(offerOnButter);
            }

            butters.ForEach(x => x.FinalPrice = x.UnitPrice);
            basketResult.Products.AddRange(butters);

            //bread
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
            var numberOfMilkForFree = totalMilks / minNumberOfMilksToReceiveOffer;
            if (totalMilks > minNumberOfMilksToReceiveOffer)
            {
                for (int i = 0; i < numberOfMilkForFree; i++)
                {
                    basketResult.Products.Add(
                        new Milk
                        {
                            FinalPrice = 0  
                        }
                    );
                }

                offerOnMilk = new OfferOnMilk
                {
                    TotalItems = totalMilks
                };

                basketResult.Offers.Add(offerOnMilk);
            }
            else
                milks.Where(x => x.FinalPrice == 0).ToList().ForEach(x => x.FinalPrice = x.UnitPrice);

            basketResult.Products.AddRange(milks);

            //foreach (var product in basketResult.Products)
            //{
               
            //}

            basketResult.FinalPrice = basketResult.Products.Sum(x => x.FinalPrice);
            basketResult.OriginalPrice = basketResult.Products.Sum(x => x.UnitPrice);
            basketResult.SavedAmount = basketResult.OriginalPrice - basketResult.FinalPrice;

            return basketResult;
        }
    }
}
