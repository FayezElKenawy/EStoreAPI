﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CityManager : ICityService
    {
        public ICityDal _cityDal;

        public CityManager(ICityDal cityDal)
        {
            _cityDal = cityDal;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CityValidator))]
        public IResult Add(City city)
        {
            city.CreateDate = System.DateTime.Now;
            city.Active = true;
            _cityDal.Add(city);
            return new SuccessResult(BusinessMessages.CityAdded);
        }

        [ValidationAspect(typeof(CityValidator))]
        public IResult Delete(City city)
        {
            _cityDal.Delete(city);
            return new SuccessResult(BusinessMessages.CityDeleted);
        }

        public IDataResult<List<City>> GetAll()
        {
            return new SuccessDataResult<List<City>>(_cityDal.GetAll(), BusinessMessages.CitiesListed);
        }

        public IDataResult<City> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<City>(_cityDal.Get(c => c.Id == id), BusinessMessages.CityDetailsListed);
            }

            return new ErrorDataResult<City>();
        }

        [ValidationAspect(typeof(CityValidator))]
        public IResult Update(City city)
        {
            _cityDal.Update(city);
            return new SuccessResult(BusinessMessages.CityUpdated);
        }

        //Business Rules

        private IResult CheckIfEntityIdValid(int id)
        {
            var result = _cityDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}