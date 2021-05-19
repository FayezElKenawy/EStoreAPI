using Business.Concrete;
using Business.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using Moq;
using DataAccess.Abstract;
using System.Linq;
using Business.ValidationRules.FluentValidation;
using FluentValidation.TestHelper;

namespace Tests.Business
{
    [TestClass]
    public class BasketManagerTests
    {
        private Mock<IBasketDal> _mockBasketDal;
        private List<Basket> _mockBaskets;
        private BasketValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockBasketDal = new Mock<IBasketDal>();
            _mockBaskets = new List<Basket>
            {
                new Basket {Id=1, ProductId=It.IsAny<int>(), Count=It.IsAny<int>(), UserId=It.IsAny<int>(), CreateDate=DateTime.Now, Active=true},
                new Basket {Id=2, ProductId=It.IsAny<int>(), Count=It.IsAny<int>(), UserId=It.IsAny<int>(), CreateDate=DateTime.Now, Active=true}
            };
            _mockBasketDal.Setup(m => m.GetAll(null)).Returns(_mockBaskets);

            _validator = new BasketValidator();
        }

        [TestMethod]
        public void GetAll_AllBasketsCanListed()
        {
            IBasketService basketService = new BasketManager(_mockBasketDal.Object);
            List<Basket> baskets = basketService.GetAll().Data;
            Assert.AreEqual(2, baskets.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockBaskets.SingleOrDefault(m => m.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddBasket_ReturnTrueResult()
        {
            IBasketService basketService = new BasketManager(_mockBasketDal.Object);
            Basket basket = new Basket
            {
                Id = 1,
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                UserId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = basketService.Add(basket);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            IBasketService basketService = new BasketManager(_mockBasketDal.Object);
            Basket basket = new Basket
            {
                Id = 1,
                ProductId = 0,
                Count = It.IsAny<int>(),
                UserId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(basket);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateBasket_ReturnTrueResult()
        {
            IBasketService basketService = new BasketManager(_mockBasketDal.Object);
            Basket basket = new Basket
            {
                Id = 1,
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                UserId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = basketService.Update(basket);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteBasket_ReturnTrueResult()
        {
            IBasketService basketService = new BasketManager(_mockBasketDal.Object);
            Basket basket = new Basket
            {
                Id = 1,
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                UserId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = basketService.Delete(basket);
            Assert.IsTrue(result.Success);
        }
    }
}
