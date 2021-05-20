using Business.Concrete;
using Business.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using DataAccess.Abstract;
using System.Linq;
using Business.ValidationRules.FluentValidation;
using FluentValidation.TestHelper;

namespace Tests.Business
{
    [TestClass]
    public class AddressManagerTests
    {
        private Mock<IAddressDal> _mockAddressDal;
        private List<Address> _mockAddresses;
        private AddressValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockAddressDal = new Mock<IAddressDal>();
            _mockAddresses = new List<Address>
            {
                new Address{Id=1, CityId=It.IsAny<int>(), UserId=It.IsAny<int>(), AddressDetail=It.IsAny<string>(), PostalCode=It.IsAny<string>(),CreateDate=DateTime.Now, Active=true },
                 new Address{Id=2, CityId=It.IsAny<int>(), UserId=It.IsAny<int>(), AddressDetail=It.IsAny<string>(), PostalCode=It.IsAny<string>(),CreateDate=DateTime.Now, Active=true }
            };
            _mockAddressDal.Setup(m => m.GetAll(null)).Returns(_mockAddresses);

            _validator = new AddressValidator();
        }

        [TestMethod]
        public void GetAll_AllAddressesCanListed()
        {
            IAddressService service = new AddressManager(_mockAddressDal.Object);
            List<Address> addresses = service.GetAll().Data;
            Assert.AreEqual(2, addresses.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockAddresses.SingleOrDefault(m => m.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddAddress_ReturnTrueResult()
        {
            IAddressService service = new AddressManager(_mockAddressDal.Object);
            Address address = new Address
            {
                Id = 2,
                CityId = It.IsAny<int>(),
                UserId = It.IsAny<int>(),
                AddressDetail = It.IsAny<string>(),
                PostalCode = It.IsAny<string>(),
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = service.Add(address);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            Address address = new Address
            {
                Id = 2,
                CityId = 0,
                UserId = It.IsAny<int>(),
                AddressDetail = It.IsAny<string>(),
                PostalCode = It.IsAny<string>(),
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(address);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateAddress_ReturnTrueResult()
        {
            IAddressService service = new AddressManager(_mockAddressDal.Object);
            Address address = new Address
            {
                Id = 2,
                CityId = It.IsAny<int>(),
                UserId = It.IsAny<int>(),
                AddressDetail = It.IsAny<string>(),
                PostalCode = It.IsAny<string>(),
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = service.Update(address);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteAddress_ReturnTrueResult()
        {
            IAddressService service = new AddressManager(_mockAddressDal.Object);
            Address address = new Address
            {
                Id = 2,
                CityId = It.IsAny<int>(),
                UserId = It.IsAny<int>(),
                AddressDetail = It.IsAny<string>(),
                PostalCode = It.IsAny<string>(),
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = service.Delete(address);
            Assert.IsTrue(result.Success);
        }
    }
}
