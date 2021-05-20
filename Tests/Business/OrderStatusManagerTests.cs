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
    public class OrderStatusManagerTests
    {
        private Mock<IOrderStatusDal> _mockOrderStatusDal;
        private List<OrderStatus> _mockOrderStatuses;
        private OrderStatusValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockOrderStatusDal = new Mock<IOrderStatusDal>();
            _mockOrderStatuses = new List<OrderStatus>
            {
                new OrderStatus{Id=1, Name=It.IsAny<string>(), CreateDate=DateTime.Now, Active=true},
                 new OrderStatus{Id=2, Name=It.IsAny<string>(), CreateDate=DateTime.Now, Active=true}
            };
            _mockOrderStatusDal.Setup(m => m.GetAll(null)).Returns(_mockOrderStatuses);

            _validator = new OrderStatusValidator();
        }

        [TestMethod]
        public void GetAll_AllOrdersCanListed()
        {
            IOrderStatusService service = new OrderStatusManager(_mockOrderStatusDal.Object);
            List<OrderStatus> orderStatuses = service.GetAll().Data;
            Assert.AreEqual(2, orderStatuses.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockOrderStatuses.SingleOrDefault(c => c.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddOrderStatus_ReturnTrueResult()
        {
            IOrderStatusService service = new OrderStatusManager(_mockOrderStatusDal.Object);
            OrderStatus orderStatus = new OrderStatus
            {
                Id = 1,
                Name = It.IsAny<string>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = service.Add(orderStatus);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            OrderStatus orderStatus = new OrderStatus
            {
                Id = 1,
                Name = "O",
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(orderStatus);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateOrderStatus_ReturnTrueResult()
        {
            IOrderStatusService service = new OrderStatusManager(_mockOrderStatusDal.Object);
            OrderStatus orderStatus = new OrderStatus
            {
                Id = 1,
                Name = It.IsAny<string>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = service.Update(orderStatus);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteOrderStatus_ReturnTrueResult()
        {
            IOrderStatusService service = new OrderStatusManager(_mockOrderStatusDal.Object);
            OrderStatus orderStatus = new OrderStatus
            {
                Id = 1,
                Name = It.IsAny<string>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = service.Delete(orderStatus);
            Assert.IsTrue(result.Success);
        }
    }
}
