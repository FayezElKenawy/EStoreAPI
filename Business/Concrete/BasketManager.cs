using Business.Abstract;
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

        [ValidationAspect(typeof(BasketValidator))]
        public IResult Update(Basket basket)
        {
            _basketDal.Add(basket);
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