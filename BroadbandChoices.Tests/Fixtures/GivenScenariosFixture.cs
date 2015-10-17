using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BroadbandChoices.Services.Managers;
using BroadbandChoices.Entities;
using System.Collections.Generic;

namespace BroadbandChoices.Tests.Fixtures
{
    [TestClass]
    public class GivenScenariosFixture
    {
        #region Tests methods

        [TestMethod]
        public void First_Scenario_One_Item_Per_Type()
        {
            //Arrange
            var basket = FirstScenarioGiveBasket;

            //Act
            var expectedFinalPrice = 2.95;
            var result = PriceCalculationManager.GetBasketResult(FirstScenarioGiveBasket);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Products);
            Assert.AreEqual(result.Products.Count, 3);
            Assert.AreEqual(result.OriginalPrice, result.FinalPrice);
            Assert.AreEqual(result.FinalPrice, expectedFinalPrice);
        }

        [TestMethod]
        public void Second_Scenario_One_Bread_For_Half_Price()
        {
            //Arrange
            var basket = FirstScenarioGiveBasket;

            //Act
            var expectedFinalPrice = 3.10;
            var expectedBreadFinalPrice = 0.5;
            var result = PriceCalculationManager.GetBasketResult(SecondScenarioGiveBasket);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Products);
            Assert.AreEqual(result.Products.Count, 4);
            Assert.AreNotEqual(result.OriginalPrice, result.FinalPrice);
            Assert.AreEqual(result.FinalPrice, expectedFinalPrice);
            Assert.IsTrue(result.FinalPrice < result.OriginalPrice);
            Assert.IsNotNull(result.Products[2]);
            Assert.AreEqual(result.Products[2].FinalPrice, expectedBreadFinalPrice);
        }

        [TestMethod]
        public void Third_Scenario_Four_Milk_For_Three()
        {
            //Arrange
            var basket = ThirdScenarioGiveBasket;

            //Act
            var expectedFinalPrice = 3.45;
            var result = PriceCalculationManager.GetBasketResult(ThirdScenarioGiveBasket);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Products);
            Assert.AreEqual(result.Products.Count, 4);
            Assert.AreNotEqual(result.OriginalPrice, result.FinalPrice);
            Assert.AreEqual(result.FinalPrice, expectedFinalPrice);
            Assert.IsTrue(result.FinalPrice < result.OriginalPrice);
            Assert.AreEqual(result.Products[0].FinalPrice, 0);
        }

        [TestMethod]
        public void Fourth_Scenario_Eight_Milk_For_Six_One_Bread_Half_Price()
        {
            //Arrange
            var basket = FourthScenarioGiveBasket;

            //Act
            var expectedFinalPrice = 9;
            var result = PriceCalculationManager.GetBasketResult(FourthScenarioGiveBasket);
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Products);
            Assert.AreEqual(result.Products.Count, 11);
            Assert.AreNotEqual(result.OriginalPrice, result.FinalPrice);
            Assert.AreEqual(result.FinalPrice, expectedFinalPrice);
            Assert.IsTrue(result.FinalPrice < result.OriginalPrice);
            Assert.AreEqual(result.Products[3].FinalPrice, 0);
        }

        #endregion

        #region Mocked objects
        public Entities.Base.Basket FirstScenarioGiveBasket
        {
            get
            {
                return new Entities.Base.Basket
                {
                    Products = new List<Entities.Base.Product>
                    {
                        new Butter(),
                        new Bread(),
                        new Milk()
                }
                };
            }
        }

        public Entities.Base.Basket SecondScenarioGiveBasket
        {
            get
            {
                return new Entities.Base.Basket
                {
                    Products = new List<Entities.Base.Product>
                    {
                        new Butter(),
                        new Butter(),
                        new Bread(),
                        new Bread()
                    }
                };
            }
        }

        public Entities.Base.Basket ThirdScenarioGiveBasket
        {
            get
            {
                return new Entities.Base.Basket
                {
                    Products = new List<Entities.Base.Product>
                    {
                        new Milk(),
                        new Milk(),
                        new Milk(),
                        new Milk()
                }
                };
            }
        }

        public Entities.Base.Basket FourthScenarioGiveBasket
        {
            get
            {
                return new Entities.Base.Basket
                {
                    Products = new List<Entities.Base.Product>
                    {
                        new Butter(),
                        new Butter(),
                        new Bread(),
                        new Milk(),
                        new Milk(),
                        new Milk(),
                        new Milk(),
                        new Milk(),
                        new Milk(),
                        new Milk(),
                        new Milk()
                }
                };
            }
        }
        #endregion
    }
}
