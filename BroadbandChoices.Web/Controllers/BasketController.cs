using BroadbandChoices.Entities;
using BroadbandChoices.Entities.Base;
using BroadbandChoices.Infrastructure;
using BroadbandChoices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BroadbandChoices.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService<Product, Basket> _basketService;
        private readonly ILogger _logger;
        public BasketController(IBasketService<Product, Basket> basketService, ILogger logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        // GET: Basket
        public ActionResult Index()
        {
            Session.Clear();
            Session.Add("basketUpdated", new Basket());
            return View();
        }

        #region BUTTER

        public string AddButter(Butter butter)
        {
            try
            {
                Basket updatedBasket = null;
                _basketService.Add(butter, CurrentBasket, out updatedBasket);
                CurrentBasket = updatedBasket;
                return new JavaScriptSerializer().Serialize(_basketService);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            return "error";
        }

        public string RemoveButter(Butter butter)
        {
            try
            {
                Basket updatedBasket = null;
                _basketService.Remove(butter, CurrentBasket, out updatedBasket);
                CurrentBasket = updatedBasket;
                return new JavaScriptSerializer().Serialize(_basketService);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            return "error";
        }

        #endregion

        #region BREAD

        public string AddBread(Bread bread)
        {
            try
            {
                Basket updatedBasket = null;
                _basketService.Add(bread, CurrentBasket, out updatedBasket);
                CurrentBasket = updatedBasket;
                return new JavaScriptSerializer().Serialize(_basketService);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            return "error";
        }

        public string RemoveBread(Bread bread)
        {
            try
            {
                Basket updatedBasket = null;
                _basketService.Remove(bread, CurrentBasket, out updatedBasket);
                CurrentBasket = updatedBasket;
                return new JavaScriptSerializer().Serialize(_basketService);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            return "error";
        }

        #endregion

        #region MILK

        public string AddMilk(Milk milk)
        {
            try
            {
                Basket updatedBasket = null;
                _basketService.Add(milk, CurrentBasket, out updatedBasket);
                CurrentBasket = updatedBasket;
                return new JavaScriptSerializer().Serialize(_basketService);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            return "error";
        }

        public string RemoveMilk(Milk milk)
        {
            try
            {
                Basket updatedBasket = null;
                _basketService.Remove(milk, CurrentBasket, out updatedBasket);
                CurrentBasket = updatedBasket;
                return new JavaScriptSerializer().Serialize(_basketService);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            return "error";
        }

        #endregion

        public string CalculateTotalAndGetOffers()
        {
            try
            {
                var basketResult = _basketService.CalculateTotalPrice(CurrentBasket);
                return new JavaScriptSerializer().Serialize(basketResult);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            return "error";
        }

        public string ClearOrder()
        {
            try
            {
                CurrentBasket = new Basket();
                return new JavaScriptSerializer().Serialize(CurrentBasket);
            }
            catch (Exception ex)
            {
                _logger.Handle(ex);
            }

            return string.Empty;
        }

        public Basket CurrentBasket { get { return (Basket)Session["basketUpdated"]; } set { Session["basketUpdated"] = value; } }
    }
}
