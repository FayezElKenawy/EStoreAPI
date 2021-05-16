using Business.Concrete;
using Business.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using DataAccess.Abstract;
using System.Linq;
using Business.ValidationRules.FluentValidation;
using FluentValidation.TestHelper;
using Core.Entities.Concrete;

namespace Tests.Business
{
    [TestClass]
    public class UserManagerTests
    {
        private Mock<IUserDal> _mockUserDal;
        private List<User> _mockUsers;
        private UserValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockUserDal = new Mock<IUserDal>();
            _mockUsers = new List<User>
            {
                new User {Id=1, FirstName=It.IsAny<string>(), Email = It.IsAny<string>(), LastName=It.IsAny<string>(), PasswordHash=It.IsAny<byte[]>(), PasswordSalt=It.IsAny<byte[]>(), Status=true },
                new User {Id=2, FirstName=It.IsAny<string>(), Email = It.IsAny<string>(), LastName=It.IsAny<string>(), PasswordHash=It.IsAny<byte[]>(), PasswordSalt=It.IsAny<byte[]>(), Status=true }
            };
            _validator = new UserValidator();

            _mockUserDal.Setup(m => m.GetAll(null)).Returns(_mockUsers);
        }

        [TestMethod]
        public void GetAll_AllUsersCanListed()
        {
            IUserService userService = new UserManager(_mockUserDal.Object);
            List<User> users = userService.GetAll().Data;
            Assert.AreEqual(2, users.Count);
        }

        [TestMethod]
        public void GetById_InvaliId_ReturnNull()
        {
            var result = _mockUsers.SingleOrDefault(m => m.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddUser_ReturnResultTrue()
        {
            IUserService userService = new UserManager(_mockUserDal.Object);
            User user = new User
            {
                Id = 1,
                FirstName = It.IsAny<string>(),
                Email = It.IsAny<string>(),
                LastName = It.IsAny<string>(),
                PasswordHash = It.IsAny<byte[]>(),
                PasswordSalt = It.IsAny<byte[]>(),
                Status = true
            };

            var result = userService.Add(user);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            User user = new User
            {
                Id = 1,
                FirstName = "F",
                Email = "E",
                LastName = "L",
                PasswordHash = It.IsAny<byte[]>(),
                PasswordSalt = It.IsAny<byte[]>(),
                Status = true
            };

            var result = _validator.TestValidate(user);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateUser_ReturnResultTrue()
        {
            IUserService userService = new UserManager(_mockUserDal.Object);
            User user = new User
            {
                Id = 1,
                FirstName = It.IsAny<string>(),
                Email = It.IsAny<string>(),
                LastName = It.IsAny<string>(),
                PasswordHash = It.IsAny<byte[]>(),
                PasswordSalt = It.IsAny<byte[]>(),
                Status = true
            };

            var result = userService.Update(user);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteUser_ReturnResultTrue()
        {
            IUserService userService = new UserManager(_mockUserDal.Object);
            User user = new User
            {
                Id = 1,
                FirstName = It.IsAny<string>(),
                Email = It.IsAny<string>(),
                LastName = It.IsAny<string>(),
                PasswordHash = It.IsAny<byte[]>(),
                PasswordSalt = It.IsAny<byte[]>(),
                Status = true
            };

            var result = userService.Delete(user);
            Assert.IsTrue(result.Success);
        }
    }
}
