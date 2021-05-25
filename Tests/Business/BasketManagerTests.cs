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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.IoC;
using System.Security.Claims;

namespace Tests.Business
{
    [TestClass]
    public class BasketManagerTests
    {
        private Mock<IBasketDal> _mockBasketDal;
        private List<Basket> _mockBaskets;
        private BasketValidator _validator;
        private Mock<IHttpContextAccessor> _mockAccessor;

        [TestInitialize]
        public void Setup()
        {
            _mockBasketDal = new Mock<IBasketDal>();
            _mockAccessor = new Mock<IHttpContextAccessor>();
            _mockBaskets = new List<Basket>
            {
                new Basket {Id=1, UserId=It.IsAny<int>(), CreateDate=DateTime.Now, Active=true},
                new Basket {Id=2,UserId=It.IsAny<int>(), CreateDate=DateTime.Now, Active=true}
            };

            _mockBasketDal.Setup(m => m.GetAll(null)).Returns(_mockBaskets);
            _validator = new BasketValidator();

            _mockAccessor.SetupGet(m => m.HttpContext.User.Claims).Returns(new List<Claim> { new Claim("nameidentifier", "1") });
        }

        [TestMethod]
        public void GetAll_AllBasketsCanListed()
        {
            IBasketService basketService = new BasketManager(_mockBasketDal.Object, _mockAccessor.Object);
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
            IBasketService basketService = new BasketManager(_mockBasketDal.Object, _mockAccessor.Object);
            Basket basket = new Basket
            {
                Id = 1,
                UserId = 2,
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = basketService.Add(basket);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            Basket basket = new Basket
            {
                Id = 1,
                UserId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(basket);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [TestMethod]
        public void Update_UpdateBasket_ReturnTrueResult()
        {
            IBasketService basketService = new BasketManager(_mockBasketDal.Object, _mockAccessor.Object);

            Basket basket = new Basket
            {
                Id = 1,
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
            IBasketService basketService = new BasketManager(_mockBasketDal.Object, _mockAccessor.Object);
            Basket basket = new Basket
            {
                Id = 1,
                UserId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = basketService.Delete(basket);
            Assert.IsTrue(result.Success);
        }
    }
}
