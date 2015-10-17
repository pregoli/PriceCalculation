using BroadbandChoices.Entities;
using BroadbandChoices.Entities.Base;
using BroadbandChoices.Infrastructure;
using BroadbandChoices.Services.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadbandChoices.Services
{
    public class BasketService : IBasketService<Product, Basket>
    {
        private readonly ILogger _logger;
        public BasketService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Add a product to the basket and Get back the updated one
        /// </summary>
        /// <param name="product">product added to the basket</param>
        /// <param name="basket">Current basket</param>
        public void Add(Product product, Basket basket, out Basket basketUpdated)
        {
            try
            {
                basket.Products.Add(product);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            basketUpdated = basket;
        }

        /// <summary>
        /// Remove a product from the basket and Get back the updated one
        /// </summary>
        /// <param name="product">product removed to the basket</param>
        /// <param name="basket">Current basket</param>
        public void Remove(Product product, Basket basket, out Basket basketUpdated)
        {
            try
            {
                var sameTypeItem = basket.Products.Where(x => x.GetType() == product.GetType()).FirstOrDefault();
                basket.Products.Remove(sameTypeItem);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            basketUpdated = basket;
        }

        /// <summary>
        /// Retrieve the final basket result
        /// Including all products with infos/ compared prices infos/ offers.discounts if available
        /// </summary>
        /// <param name="basket">Current basket</param>
        /// <returns></returns>
        public BasketResult CalculateTotalPrice(Basket basket)
        {
            try
            {
                if (basket.Products.Count == 0)
                    return new BasketResult();
                else
                    return PriceCalculationManager.GetBasketResult(basket);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            return null;
        }
    }
}
