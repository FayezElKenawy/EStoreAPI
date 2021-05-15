using Business.Abstract;
using Business.Concrete;
using Business.Helpers.CodeGenerator;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Business
{
    [TestClass]
    public class ProductManagerTests
    {
        private Mock<IProductDal> _mockProductDal;
        private Mock<ICodeGeneratorService> _mockCodeGeneratorService;
        private List<Product> _mockProducts;
        private ProductValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockProductDal = new Mock<IProductDal>();
            _mockCodeGeneratorService = new Mock<ICodeGeneratorService>();
            _mockProducts = new List<Product>
            {
                new Product{Id=1, CategoryId=1, BrandId=1, Name="Product1", Code="11", Price=10, CreateDate = DateTime.Now, Active=true },
                new Product{Id=2, CategoryId=1, BrandId=1, Name="Product2", Code="12", Price=10, CreateDate = DateTime.Now, Active=true }
            };
            _validator = new ProductValidator();

            _mockProductDal.Setup(m => m.GetAll(null)).Returns(_mockProducts);
        }

        [TestMethod]
        public void GetAll_AllProductsCanListed()
        {
            IProductService productService = new ProductManager(_mockProductDal.Object, _mockCodeGeneratorService.Object);
            List<Product> products = productService.GetAll().Data;
            Assert.AreEqual(2, products.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockProducts.SingleOrDefault(b => b.Id == 3);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddProduct_ReturnTrueResult()
        {
            IProductService productService = new ProductManager(_mockProductDal.Object, _mockCodeGeneratorService.Object);
            Product product = new Product
            {
                Id = 3,
                CategoryId = 1,
                BrandId = 1,
                Name = "Product3",
                Code = "11",
                Price = 10,
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = productService.Add(product);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            Product product = new Product
            {
                Id = 3,
                CategoryId = 1,
                BrandId = 0,
                Name = "P",
                Code = "1",
                Price = 0,
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(product);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateProduct_ReturnTrueResult()
        {
            IProductService productService = new ProductManager(_mockProductDal.Object, _mockCodeGeneratorService.Object);
            Product product = new Product
            {
                Id = 3,
                CategoryId = 1,
                BrandId = 1,
                Name = "Product3",
                Code = "11",
                Price = 10,
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = productService.Update(product);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteProduct_ReturnTrueResult()
        {
            IProductService productService = new ProductManager(_mockProductDal.Object, _mockCodeGeneratorService.Object);
            Product product = new Product
            {
                Id = 3,
                CategoryId = 1,
                BrandId = 1,
                Name = "Product3",
                Code = "11",
                Price = 10,
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = productService.Delete(product);
            Assert.IsTrue(result.Success);
        }
    }
}
