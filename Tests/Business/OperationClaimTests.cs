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
    public class OperationClaimTests
    {
        private Mock<IOperationClaimDal> _mockOperationClaimDal;
        private List<OperationClaim> _mockOperationClaims;
        private OperationClaimValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockOperationClaimDal = new Mock<IOperationClaimDal>();
            _mockOperationClaims = new List<OperationClaim>
            {
                new OperationClaim{Id=1, Name=It.IsAny<string>()},
                new OperationClaim{Id=2, Name=It.IsAny<string>()}
            };
            _mockOperationClaimDal.Setup(m => m.GetAll(null)).Returns(_mockOperationClaims);

            _validator = new OperationClaimValidator();
        }

        [TestMethod]
        public void GetAll_AllOperationClaimsCanListed()
        {
            IOperationClaimService service = new OperationClaimManager(_mockOperationClaimDal.Object);
            List<OperationClaim> operationClaims = service.GetAll().Data;
            Assert.AreEqual(2, operationClaims.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockOperationClaims.SingleOrDefault(c => c.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddOperationClaim_ReturnTrueResult()
        {
            IOperationClaimService service = new OperationClaimManager(_mockOperationClaimDal.Object);
            OperationClaim operationClaim = new OperationClaim
            {
                Id = 1,
                Name = It.IsAny<string>()
            };

            var result = service.Add(operationClaim);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            OperationClaim operationClaim = new OperationClaim
            {
                Id = 1,
                Name = "O"
            };

            var result = _validator.TestValidate(operationClaim);
            result.ShouldHaveValidationErrorFor("Name");
        }

        [TestMethod]
        public void Update_UpdateOperationClaim_ReturnTrueResult()
        {
            IOperationClaimService service = new OperationClaimManager(_mockOperationClaimDal.Object);
            OperationClaim operationClaim = new OperationClaim
            {
                Id = 1,
                Name = It.IsAny<string>()
            };

            var result = service.Update(operationClaim);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteOperationClaim_ReturnTrueResult()
        {
            IOperationClaimService service = new OperationClaimManager(_mockOperationClaimDal.Object);
            OperationClaim operationClaim = new OperationClaim
            {
                Id = 1,
                Name = It.IsAny<string>()
            };

            var result = service.Delete(operationClaim);
            Assert.IsTrue(result.Success);
        }
    }
}
