using Business;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.Business
{
    [TestClass]
    public class CategoryManagerTests
    {
        private Mock<ICategoryDal> _mockCategoryDal;
        private List<Category> _mockCategories;

        [TestInitialize]
        public void Setup()
        {
            _mockCategoryDal = new Mock<ICategoryDal>();
            _mockCategories = new List<Category>
            {
                new Category{Id=1, Name="Category1", CreateDate=DateTime.Now, Active=true},
                new Category{Id=2, Name="Category2", CreateDate=DateTime.Now, Active=true},
                new Category{Id=3, Name="Category3", CreateDate=DateTime.Now, Active=true}
            };

            _mockCategoryDal.Setup(m => m.GetAll(null)).Returns(_mockCategories);

        }

        [TestMethod]
        public void GetAll_AllCategoriesCanListed()
        {
            ICategoryService categoryService = new CategoryManager(_mockCategoryDal.Object);
            List<Category> categories = categoryService.GetAll().Data;
            Assert.AreEqual(3, categories.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockCategories.SingleOrDefault(c => c.Id == 4);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_CategoryAdd_ReturnTrueResult()
        {
            ICategoryService categoryService = new CategoryManager(_mockCategoryDal.Object);
            Category category = new Category
            {
                Id = 4,
                Name = "Category4",
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = categoryService.Add(category);
            Assert.IsTrue(result.Success);
        }


        [TestMethod]
        public void Update_CategoryUpdate_ReturnTrueResult()
        {
            ICategoryService categoryService = new CategoryManager(_mockCategoryDal.Object);
            Category category = new Category
            {
                Id = 1,
                Name = "Category1",
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = categoryService.Update(category);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void  Delete_CategoryDelete_ReturnTrueResult()
        {
            ICategoryService categoryService = new CategoryManager(_mockCategoryDal.Object);
            Category category = new Category
            {
                Id = 1,
                Name = "Category1",
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = categoryService.Delete(category);
            Assert.IsTrue(result.Success);
        }

    }
}
