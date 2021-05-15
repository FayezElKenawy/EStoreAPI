using Business.Abstract;
using Business.Concrete;
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
    public class CityManagerTests
    {
        private Mock<ICityDal> _mockCityDal;
        private List<City> _mockCities;
        private CityValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockCityDal = new Mock<ICityDal>();
            _mockCities = new List<City>
            {
                new City{Id=1, CountryId=1, Name="City1", CreateDate=DateTime.Now, Active=true},
                new City{Id=2, CountryId=1, Name="City2", CreateDate=DateTime.Now, Active=true},
                new City{Id=3, CountryId=1, Name="City3", CreateDate=DateTime.Now, Active=true}
            };
            _validator = new CityValidator();

            _mockCityDal.Setup(m => m.GetAll(null)).Returns(_mockCities);
        }

        [TestMethod]
        public void GetAll_AllCitiesCanListed()
        {
            ICityService cityService = new CityManager(_mockCityDal.Object);
            List<City> cities = cityService.GetAll().Data;
            Assert.AreEqual(3, cities.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockCities.SingleOrDefault(c => c.Id == 4);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddCity_ReturnTrueResult()
        {
            ICityService cityService = new CityManager(_mockCityDal.Object);
            City city = new City()
            {
                Id=4,
                CountryId=1, 
                Name="City4", 
                CreateDate=DateTime.Now, 
                Active=true
            };
            var result = cityService.Add(city);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            City city = new City
            {
                Id = 1,
                CountryId = 0,
                Name = "C",
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(city);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateCity_ReturnTrueResult()
        {
            ICityService cityService = new CityManager(_mockCityDal.Object);
            City city = new City()
            {
                Id = 1,
                CountryId = 1,
                Name = "City1",
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = cityService.Update(city);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteCity_ReturnTrueResult()
        {
            ICityService cityService = new CityManager(_mockCityDal.Object);
            City city = new City()
            {
                Id = 1,
                CountryId = 1,
                Name = "City1",
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = cityService.Delete(city);
            Assert.IsTrue(result.Success);
        }
    }
}
