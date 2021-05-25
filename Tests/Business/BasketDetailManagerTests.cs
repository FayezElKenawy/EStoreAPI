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
    public class BasketDetailManagerTests
    {
        private Mock<IBasketDetailDal> _mockBasketDetailDal;
        private List<BasketDetail> _mockBasketDetails;
        private BasketDetailValidator _validator;


        [TestInitialize]
        public void Setup()
        {
            _mockBasketDetailDal = new Mock<IBasketDetailDal>();
            _mockBasketDetails = new List<BasketDetail>
            {
                new BasketDetail{Id=1, BasketId=It.IsAny<int>(), ProductId=It.IsAny<int>(), Count=It.IsAny<int>(), CreateDate=DateTime.Now, Active=true},
                  new BasketDetail{Id=2, BasketId=It.IsAny<int>(), ProductId=It.IsAny<int>(), Count=It.IsAny<int>(), CreateDate=DateTime.Now, Active=true}
            };

            _mockBasketDetailDal.Setup(m => m.GetAll(null)).Returns(_mockBasketDetails);
            _validator = new BasketDetailValidator();
        }

        [TestMethod]
        public void GetAll_AllBasketDetailsCanListed()
        {
            IBasketDetailService basketDetailService = new BasketDetailManager(_mockBasketDetailDal.Object);
            List<BasketDetail> basketDetails = basketDetailService.GetAll().Data;
            Assert.AreEqual(2, basketDetails.Count);
        }


        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockBasketDetails.SingleOrDefault(m => m.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddBasketDetail_ReturnTrueResult()
        {
            IBasketDetailService basketDetailService = new BasketDetailManager(_mockBasketDetailDal.Object);
            BasketDetail basketDetail = new BasketDetail
            {
                Id = 1,
                BasketId = It.IsAny<int>(),
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = basketDetailService.Add(basketDetail);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            BasketDetail basketDetail = new BasketDetail
            {
                Id = 1,
                BasketId = 0,
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(basketDetail);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateBasketDetail_ReturnTrueResult()
        {
            IBasketDetailService basketDetailService = new BasketDetailManager(_mockBasketDetailDal.Object);
            BasketDetail basketDetail = new BasketDetail
            {
                Id = 1,
                BasketId = It.IsAny<int>(),
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = basketDetailService.Update(basketDetail);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteBasketDetail_ReturnTrueResult()
        {
            IBasketDetailService basketDetailService = new BasketDetailManager(_mockBasketDetailDal.Object);
            BasketDetail basketDetail = new BasketDetail
            {
                Id = 1,
                BasketId = It.IsAny<int>(),
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = basketDetailService.Delete(basketDetail);
            Assert.IsTrue(result.Success);
        }
    }
}
