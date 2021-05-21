using Business.Concrete;
using Business.Abstract;
using Core.Entities.Concrete;
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
    public class UserOperationClaimManagerTests
    {
        private Mock<IUserOperationClaimDal> _mockUserOperationClaimDal;
        private List<UserOperationClaim> _mockuserOperationClaims;
        private UserOperationClaimValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockUserOperationClaimDal = new Mock<IUserOperationClaimDal>();
            _mockuserOperationClaims = new List<UserOperationClaim>
            {
                new UserOperationClaim{Id=1, UserId=It.IsAny<int>(), OperationClaimId=It.IsAny<int>()},
                new UserOperationClaim{Id=2, UserId=It.IsAny<int>(), OperationClaimId=It.IsAny<int>()}
            };
            _mockUserOperationClaimDal.Setup(m => m.GetAll(null)).Returns(_mockuserOperationClaims);

            _validator = new UserOperationClaimValidator();
        }

        [TestMethod]
        public void GetAll_AllUserOperationClaimsListed()
        {
            IUserOperationClaimService service = new UserOperationClaimManager(_mockUserOperationClaimDal.Object);
            List<UserOperationClaim> userOperationClaims = service.GetAll().Data;
            Assert.AreEqual(2, userOperationClaims.Count);
        }


        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockuserOperationClaims.SingleOrDefault(b => b.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddUserOperationClaim_ReturnTrueResult()
        {
            IUserOperationClaimService service = new UserOperationClaimManager(_mockUserOperationClaimDal.Object);
            UserOperationClaim userOperationClaim = new UserOperationClaim
            {
                Id = 1,
                UserId = It.IsAny<int>(),
                OperationClaimId = It.IsAny<int>()
            };

            var result = service.Add(userOperationClaim);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            UserOperationClaim userOperationClaim = new UserOperationClaim
            {
                Id = 1,
                UserId = 0,
                OperationClaimId = It.IsAny<int>()
            };

            var result = _validator.TestValidate(userOperationClaim);
            result.ShouldHaveValidationErrorFor("UserId");
        }

        [TestMethod]
        public void Update_UpdateUserOperationClaim_ReturnTrueResult()
        {
            IUserOperationClaimService service = new UserOperationClaimManager(_mockUserOperationClaimDal.Object);
            UserOperationClaim userOperationClaim = new UserOperationClaim
            {
                Id = 1,
                UserId = It.IsAny<int>(),
                OperationClaimId = It.IsAny<int>()
            };

            var result = service.Update(userOperationClaim);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteUserOperationClaim_ReturnTrueResult()
        {
            IUserOperationClaimService service = new UserOperationClaimManager(_mockUserOperationClaimDal.Object);
            UserOperationClaim userOperationClaim = new UserOperationClaim
            {
                Id = 1,
                UserId = It.IsAny<int>(),
                OperationClaimId = It.IsAny<int>()
            };

            var result = service.Delete(userOperationClaim);
            Assert.IsTrue(result.Success);
        }
    }
}
