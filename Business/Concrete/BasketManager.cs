﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        readonly IBasketDal _basketDal;
        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

        [ValidationAspect(typeof(BasketValidator))]
        public IResult Add(Basket basket)
        {
            basket.CreateDate = DateTime.Now;
            basket.Active = true;

            _basketDal.Add(basket);
            return new SuccessResult(BusinessMessages.BasketAdded);
        }

        [ValidationAspect(typeof(BasketValidator))]
        public IResult Delete(Basket basket)
        {
            _basketDal.Delete(basket);
            return new SuccessResult(BusinessMessages.BasketDeleted);
        }

        public IDataResult<List<Basket>> GetAll()
        {
            return new SuccessDataResult<List<Basket>>(_basketDal.GetAll(), BusinessMessages.BasketsListed);
        }

        public IDataResult<Basket> GetById(int id)
        {
            var result = BusinessRules.Run(CheckIfEntityIdValid(id));
            if (result == null)
            {
                return new SuccessDataResult<Basket>(_basketDal.Get(c => c.Id == id), BusinessMessages.BasketDetailsListed);
            }

            return new ErrorDataResult<Basket>();
        }

        public IDataResult<Basket> GetByUserIdActive(int userId)
        {
            return new SuccessDataResult<Basket>(_basketDal.Get(b => b.UserId == userId && b.Active == true));
        }

        public IResult SetPassive(int id)
        {
            _basketDal.SetPassive(id);
            return new SuccessResult(BusinessMessages.BasketPassived);
        }

        [ValidationAspect(typeof(BasketValidator))]
        public IResult Update(Basket basket)
        {
            _basketDal.Update(basket);
            return new SuccessResult(BusinessMessages.BasketUpdated);
        }

        //Business Rules

        private IResult CheckIfEntityIdValid(int id)
        {
            var result = _basketDal.Get(c => c.Id == id);
            if (result != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}