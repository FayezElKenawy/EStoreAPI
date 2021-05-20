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
    public class OrderManagerTests
    {
        private Mock<IOrderDal> _mockOrderDal;
        private List<Order> _mockOrders;
        private OrderValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockOrderDal = new Mock<IOrderDal>();
            _mockOrders = new List<Order>
            {
                new Order{Id=1, UserId=It.IsAny<int>(), AddressId=It.IsAny<int>(), OrderStatusId = It.IsAny<int>(), CreateDate=DateTime.Now, Active=true},
                 new Order{Id=2, UserId=It.IsAny<int>(), AddressId=It.IsAny<int>(), OrderStatusId = It.IsAny<int>(), CreateDate=DateTime.Now, Active=true},
            };
            _mockOrderDal.Setup(m => m.GetAll(null)).Returns(_mockOrders);

            _validator = new OrderValidator();
        }

        [TestMethod]
        public void GetAll_AllOrdersCanListed()
        {
            IOrderService orderService = new OrderManager(_mockOrderDal.Object);
            List<Order> orders = orderService.GetAll().Data;
            Assert.AreEqual(2, orders.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockOrders.SingleOrDefault(m => m.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddOrder_ReturnTrueResult()
        {
            IOrderService orderService = new OrderManager(_mockOrderDal.Object);
            Order order = new Order
            {
                Id = 3,
                UserId = It.IsAny<int>(),
                AddressId = It.IsAny<int>(),
                OrderStatusId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = orderService.Add(order);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            Order order = new Order
            {
                Id = 3,
                UserId = 0,
                AddressId = It.IsAny<int>(),
                OrderStatusId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(order);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateOrder_ReturnTrueResult()
        {
            IOrderService orderService = new OrderManager(_mockOrderDal.Object);
            Order order = new Order
            {
                Id = 3,
                UserId = It.IsAny<int>(),
                AddressId = It.IsAny<int>(),
                OrderStatusId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = orderService.Update(order);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteOrder_ReturnTrueResult()
        {
            IOrderService orderService = new OrderManager(_mockOrderDal.Object);
            Order order = new Order
            {
                Id = 3,
                UserId = It.IsAny<int>(),
                AddressId = It.IsAny<int>(),
                OrderStatusId = It.IsAny<int>(),
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = orderService.Delete(order);
            Assert.IsTrue(result.Success);
        }
    }
}
