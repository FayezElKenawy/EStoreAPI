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
    public class CountryManagerTests
    {
        private Mock<ICountryDal> _mockCountryDal;
        private List<Country> _mockCountries;
        private CountryValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _mockCountryDal = new Mock<ICountryDal>();
            _mockCountries = new List<Country>
            {
                new Country{Id=1, Name="Country1", CreateDate=DateTime.Now, Active=true},
                new Country{Id=2, Name="Country2", CreateDate=DateTime.Now, Active=true},
                new Country{Id=3, Name="Country3", CreateDate=DateTime.Now, Active=true}
            };
            _validator = new CountryValidator();

            _mockCountryDal.Setup(m => m.GetAll(null)).Returns(_mockCountries);
        }

        [TestMethod]
        public void GetAll_AllCountriesCanListed()
        {
            ICountryService countryService = new CountryManager(_mockCountryDal.Object);
            List<Country> countries = countryService.GetAll().Data;
            Assert.AreEqual(3, countries.Count);
        }

        [TestMethod]
        public void GetById_InvalidId_ReturnError()
        {
            var result = _mockCountries.SingleOrDefault(c => c.Id == 4);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_AddCountry_ReturnTrueResult()
        {
            ICountryService countryService = new CountryManager(_mockCountryDal.Object);
            Country country = new Country()
            {
                Id = 4,
                Name = "Country4",
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = countryService.Add(country);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void InvalidParameters_ThrowValidationException()
        {
            Country country = new Country
            {
                Id = 1,
                Name = "C",
                CreateDate = DateTime.Now,
                Active = true
            };

            var result = _validator.TestValidate(country);
            result.ShouldHaveAnyValidationError();
        }

        [TestMethod]
        public void Update_UpdateCountry_ReturnTrueResult()
        {
            ICountryService countryService = new CountryManager(_mockCountryDal.Object);
            Country country = new Country()
            {
                Id = 1,
                Name = "Country1",
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = countryService.Update(country);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void Delete_DeleteCountry_ReturnTrueResult()
        {
            ICountryService countryService = new CountryManager(_mockCountryDal.Object);
            Country country = new Country()
            {
                Id = 1,
                Name = "Country1",
                CreateDate = DateTime.Now,
                Active = true
            };
            var result = countryService.Delete(country);
            Assert.IsTrue(result.Success);
        }
    }
}
