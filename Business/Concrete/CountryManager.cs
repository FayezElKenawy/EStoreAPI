using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CountryManager : ICountryService
    {
        private readonly ICountryDal _countryDal;

        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        public IResult Add(Country country)
        {
            _countryDal.Add(country);
            return new SuccessResult(Messages.CountryAdded);
        }

        public IResult Delete(Country country)
        {
            _countryDal.Delete(country);
            return new SuccessResult(Messages.CountryDeleted);
        }

        public IDataResult<Country> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<Country>(_countryDal.Get(c => c.Id == id), Messages.CountrytDetailsListed);
            }

            return new ErrorDataResult<Country>();

        }

        public IDataResult<List<Country>> GetAll()
        {
            return new SuccessDataResult<List<Country>>(_countryDal.GetAll(), Messages.CountriesListed);
        }

        public IResult Update(Country country)
        {
            _countryDal.Update(country);
            return new SuccessResult(Messages.CountryUpdated);
        }

        //Business Rules

        private IResult CheckIfEntityIdValid(int id)
        {
            var result = _countryDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}
