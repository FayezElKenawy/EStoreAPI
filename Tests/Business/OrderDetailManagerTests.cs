using Business.Concrete;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using DataAccess.Abstract;
using System.Linq;
using Business.ValidationRules.FluentValidation;
using FluentValidation.TestHelper;

namespace Tests.Business
{
    [TestClass]
    public class OrderDetailManagerTests
    {
        private Mock<IOrderDetailDal> _mockOrderDetailDal;
        private List<OrderDetail> _mockOrderDetails;
        private OrderDetailValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockOrderDetailDal = new Mock<IOrderDetailDal>();
            _mockOrderDetails = new List<OrderDetail>
            {
                new OrderDetail{Id=1,OrderId=It.IsAny<int>(), ProductId=It.IsAny<int>(), Count=It.IsAny<int>(), SalePrice = It.IsAny<decimal>(), CreateDate=DateTime.Now, Active=true},
                 new OrderDetail{Id=2,OrderId=It.IsAny<int>(), ProductId=It.IsAny<int>(), Count=It.IsAny<int>(), SalePrice = It.IsAny<decimal>(), CreateDate=DateTime.Now, Active=true}
            };
            _mockOrderDetailDal.Setup(m => m.GetAll(null)).Returns(_mockOrderDetails);

            _validator = new OrderDetailValidator();
        }

        [TestMethod]
        public void GetAll_AllOrderDetailsCanListed()
        {
            IOrderDetailService service = new OrderDetailManager(_mockOrderDetailDal.Object);
            List<OrderDetail> orderDetails = service.GetAll().Data;
            Assert.AreEqual(2, orderDetails.Count);
        }


        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockOrderDetails.SingleOrDefault(m => m.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddOrderDetail_ReturnTrueResult()
        {
            IOrderDetailService service = new OrderDetailManager(_mockOrderDetailDal.Object);
            OrderDetail orderDetail = new OrderDetail
            {
                Id = 3,
                OrderId = It.IsAny<int>(),
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                SalePrice = It.IsAny<decimal>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = service.Add(orderDetail);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            OrderDetail orderDetail = new OrderDetail
            {
                Id = 1,
                OrderId = 0,
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                SalePrice = It.IsAny<decimal>(),
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(orderDetail);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateOrderDetail_ReturnTrueResult()
        {
            IOrderDetailService service = new OrderDetailManager(_mockOrderDetailDal.Object);
            OrderDetail orderDetail = new OrderDetail
            {
                Id = 3,
                OrderId = It.IsAny<int>(),
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                SalePrice = It.IsAny<decimal>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = service.Update(orderDetail);
            Assert.IsTrue(result.Success);
        }


        [TestMethod]
        public void Delete_DelteOrderDetail_ReturnTrueResult()
        {
            IOrderDetailService service = new OrderDetailManager(_mockOrderDetailDal.Object);
            OrderDetail orderDetail = new OrderDetail
            {
                Id = 3,
                OrderId = It.IsAny<int>(),
                ProductId = It.IsAny<int>(),
                Count = It.IsAny<int>(),
                SalePrice = It.IsAny<decimal>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = service.Delete(orderDetail);
            Assert.IsTrue(result.Success);
        }
    }
}
