using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;

namespace Tests.Business
{
    [TestClass]
    public class BrandManagerTests
    {
        private Mock<IBrandDal> _mockBrandDal;
        private List<Brand> _mockBrands;
        private BrandValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockBrandDal = new Mock<IBrandDal>();
            _mockBrands = new List<Brand>
            {
                new Brand {Id = 1, Name="Brand1", CreateDate=DateTime.Now, Active=true},
                new Brand {Id = 2, Name="Brand2", CreateDate=DateTime.Now, Active=true},
                new Brand {Id = 3, Name="Brand3", CreateDate=DateTime.Now, Active=true}
            };
            _validator = new BrandValidator();

            _mockBrandDal.Setup(m => m.GetAll(null)).Returns(_mockBrands);
        }

        [TestMethod]
        public void GetAll_AllBrandsCanListed()
        {
            IBrandService brandService = new BrandManager(_mockBrandDal.Object);
            List<Brand> brands = brandService.GetAll().Data;
            Assert.AreEqual(3, brands.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockBrands.SingleOrDefault(b => b.Id == 4);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddBrand_ReturnTrueResult()
        {
            IBrandService brandService = new BrandManager(_mockBrandDal.Object);
            Brand brand = new Brand
            {
                Id = 4,
                Name = "Brand",
                CreateDate = DateTime.Now,
                Active = true
            };
        
            var result = brandService.Add(brand);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            Brand brand = new Brand
            {
                Id = 1,
                Name = "B",
                CreateDate = DateTime.Now,
                Active = true
            };

           var result = _validator.TestValidate(brand); 
           result.ShouldHaveAnyValidationError();
        }


        [TestMethod]
        public void Update_UpdateBrand_ReturnTrueResult()
        {
            IBrandService brandService = new BrandManager(_mockBrandDal.Object);
            Brand brand = new Brand
            {
                Id = 1,
                Name = "Brand1",
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = brandService.Update(brand);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteBrand_ReturnTrueResult()
        {
            IBrandService brandService = new BrandManager(_mockBrandDal.Object);
            Brand brand = new Brand
            {
                Id = 1,
                Name = "Brand1",
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = brandService.Delete(brand);
            Assert.IsTrue(result.Success);
        }
    }
}
