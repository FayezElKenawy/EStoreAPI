using Business.Abstract;
using Business.Concrete;
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
    public class CountryManagerTests
    {
        private Mock<ICountryDal> _mockCountryDal;
        private List<Country> _mockCountries;

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
