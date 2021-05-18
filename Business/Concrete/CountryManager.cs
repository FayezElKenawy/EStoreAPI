using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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

        [ValidationAspect(typeof(CountryValidator))]
        public IResult Add(Country country)
        {
            country.CreateDate = System.DateTime.Now;
            country.Active = true;
            _countryDal.Add(country);
            return new SuccessResult(BusinessMessages.CountryAdded);
        }

        [ValidationAspect(typeof(CountryValidator))]
        public IResult Delete(Country country)
        {
            _countryDal.Delete(country);
            return new SuccessResult(BusinessMessages.CountryDeleted);
        }

        public IDataResult<Country> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<Country>(_countryDal.Get(c => c.Id == id), BusinessMessages.CountrytDetailsListed);
            }

            return new ErrorDataResult<Country>();

        }

        public IDataResult<List<Country>> GetAll()
        {
            return new SuccessDataResult<List<Country>>(_countryDal.GetAll(), BusinessMessages.CountriesListed);
        }

        [ValidationAspect(typeof(CountryValidator))]
        public IResult Update(Country country)
        {
            _countryDal.Update(country);
            return new SuccessResult(BusinessMessages.CountryUpdated);
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
